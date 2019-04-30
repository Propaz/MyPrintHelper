// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, string> CommandList = new Dictionary<string, string>()
        {
            {"SetPrinterAsDefaultKey", "/y"},
            {"AddNewPrinterKey", "/il"},
            {"DeleteSelectedPrinterKey", "/dl"},
            {"GetPropertiesOfSelectedPrinter", "/p"},
            {"QueueOfSelectedPrinter", "/o"},
            {"SendDefaultTestPage", "/k"},
            {"GetPrintServerProperties", "/s"},
            {"RestartSpooler", "/c net stop spooler&&DEL /F /S /Q %systemroot%\\System32\\spool\\PRINTERS\\*&&net start spooler&&pause"},
            {"StartSpooler", "/c net start spooler&&pause"},
            {"StopSpooler", "/c net stop spooler&&pause"}
        };

        public Form1()
        {
            InitializeComponent();
            Text = $"Printer Helper{Assembly.GetExecutingAssembly().GetName().Version} build at 29/04/2019";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
            ListOfColorsForPrint.SelectedIndex = 0;
        }

        public string ColorToPrint => ListOfColorsForPrint.SelectedItem.ToString();
        public string SelectedPrinterName => ListOfPrintersListBox.SelectedItem.ToString();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData = Keys.None)
        {
            if (keyData == Keys.F5)
            {
                FindThePrinterBtnClick(null, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box)
        {
            if (box == null) throw new ArgumentNullException(nameof(box));

            if (sync == null) throw new ArgumentNullException(nameof(sync));

            const string QueryString = "SELECT * FROM Win32_Printer";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString: QueryString))
            {
                using (ManagementObjectCollection managementObjects = searcher.Get())
                {
                    CancellationToken cancellationToken = new CancellationToken(false);
                    TaskScheduler taskScheduler = TaskScheduler.Default;
                    await Task.Factory.StartNew(b =>
                    {
                        if (managementObjects == null) return;
                        foreach (ManagementBaseObject managementBaseObject in managementObjects)
                        {
                            using (ManagementObject managementObject = (ManagementObject)managementBaseObject)
                            {
                                string PrinterNameFromWMI = managementObject["Name"].ToString();
                                if (string.IsNullOrEmpty(PrinterNameFromWMI)) continue;
                                if (managementObject["WorkOffline"].ToString().Equals(value: "false",
                                    comparisonType: StringComparison.OrdinalIgnoreCase)
                                ) //Only Printer with flag "online"
                                {
                                    sync.Send(a => (b as ListBox)?.Items.Add(a), PrinterNameFromWMI);
                                }
                            }
                        }
                    }, box, cancellationToken, TaskCreationOptions.LongRunning, taskScheduler).ConfigureAwait(false);
                }
            }
        }

        private void AddNewPrinter_Click(object sender, EventArgs e) => new Cmd(CommandList["AddNewPrinterKey"]).PrinterTasks();

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e) => contextMenuOfCommands.Enabled = ListOfPrintersListBox.SelectedIndex != -1;

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                text: $"Are you sure you want to Delete [{SelectedPrinterName}] ?",
                caption: "Confirmation",
                buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Information);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        new Cmd(CommandList["DeleteSelectedPrinterKey"], SelectedPrinterName).PrinterTasks();
                    }
                    finally
                    {
                        _ = MessageBox.Show(
                            text: $"The [{SelectedPrinterName}] was Deleted",
                            caption: "Information", buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information);
                        FindThePrinterBtnClick(null, null);
                    }

                    break;

                case DialogResult.Cancel:
                case DialogResult.None:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.Yes:
                case DialogResult.No:
                    break;
            }
        }

        private async void FindThePrinterBtnClick(object sender, EventArgs e)
        {
            FindPriners.Enabled = false;
            PrintBWGrid.Enabled = false;
            PrintTheRainbowBtn.Enabled = false;
            PrintTheColor.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ListOfPrintersListBox.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current,
                    ListOfPrintersListBox).ConfigureAwait(true);
            }
            catch (ManagementException ex)
            {
                _ = MessageBox.Show(text: ex.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                FindPriners.Enabled = true;
                PrintBWGrid.Enabled = true;
                PrintTheRainbowBtn.Enabled = true;
                PrintTheColor.Enabled = true;
                BWGirdUpDownNumeric.Value = 1;
                numericUpDownTheSingleColor.Value = 1;
                RinbowUpDownNumeric.Value = 1;
                ListOfColorsForPrint.SelectedIndex = 0;
            }
        }

        private void GetPrintServerProperties(object sender, EventArgs e) => new Cmd(CommandList["GetPrintServerProperties"]).PrinterTasks();

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e) => ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);

        private void PrintTheColor_Click(object sender, EventArgs e)
        {
            int CopiesOfSingleColor = Convert.ToInt16(numericUpDownTheSingleColor.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(CommandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "SingleColorTestPage", ColorToPrint, CopiesOfSingleColor).SendDocumentToPrinter();
            }
        }

        private void PrintTheGridBtnClick(object sender, EventArgs e)
        {
            int CopiesOfBWGrid = Convert.ToInt16(BWGirdUpDownNumeric.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(CommandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "BWGridTestPage", CopiesOfBWGrid).SendDocumentToPrinter();
            }
        }

        private void PrintTheRainbowClick(object sender, EventArgs e)
        {
            int CopiesOfRainbow = Convert.ToInt16(RinbowUpDownNumeric.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(CommandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "RainbowTestPage", CopiesOfRainbow).SendDocumentToPrinter();
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e) => new Cmd(CommandList["GetPropertiesOfSelectedPrinter"], SelectedPrinterName).PrinterTasks();

        private void QueueOfPrinter_Click(object sender, EventArgs e) => new Cmd(CommandList["QueueOfSelectedPrinter"], SelectedPrinterName).PrinterTasks();

        private void RestartPrintSpool_Click(object sender, EventArgs e) => new Cmd(CommandList["RestartSpooler"]).PrinterTasks();

        private void SendFileToPrinter(object sender, EventArgs e)
        {
            new Cmd(CommandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
            new SendFileToPrint(SelectedPrinterName).SendFileToSelectedPrinter();
        }

        private void SendTestPage_Click(object sender, EventArgs e) => new Cmd(CommandList["SendDefaultTestPage"], SelectedPrinterName).PrinterTasks();

        private void StartPrintSpool_Click(object sender, EventArgs e) => new Cmd(CommandList["StartSpooler"]).PrinterTasks();

        private void StopPrintSpool_Click(object sender, EventArgs e) => new Cmd(CommandList["StopSpooler"]).PrinterTasks();
    }
}