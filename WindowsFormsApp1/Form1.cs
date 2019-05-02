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
        private readonly Dictionary<string, string> _commandList = new Dictionary<string, string>()
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
            Text = $"Printer Helper{Assembly.GetExecutingAssembly().GetName().Version} build at 01/05/2019";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
            ListOfColorsForPrint.SelectedIndex = 0;
        }

        private string ColorToPrint => ListOfColorsForPrint.SelectedItem.ToString();
        private string SelectedPrinterName => ListOfPrintersListBox.SelectedItem.ToString();

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

            const string queryString = "SELECT * FROM Win32_Printer";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString: queryString))
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
                                string printerNameFromWmi = managementObject["Name"].ToString();
                                if (string.IsNullOrEmpty(printerNameFromWmi)) continue;
                                if (managementObject["WorkOffline"].ToString().Equals(value: "false",
                                    comparisonType: StringComparison.OrdinalIgnoreCase)
                                ) //Only Printer with flag "online"
                                {
                                    sync.Send(a => (b as ListBox)?.Items.Add(a), printerNameFromWmi);
                                }
                            }
                        }
                    }, box, cancellationToken, TaskCreationOptions.LongRunning, taskScheduler).ConfigureAwait(false);
                }
            }
        }

        private void AddNewPrinter_Click(object sender, EventArgs e) => new Cmd(_commandList["AddNewPrinterKey"]).PrinterTasks();

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
                        new Cmd(_commandList["DeleteSelectedPrinterKey"], SelectedPrinterName).PrinterTasks();
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

                default:
                    throw new ArgumentOutOfRangeException(nameof(e));
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

        private void GetPrintServerProperties(object sender, EventArgs e) => new Cmd(_commandList["GetPrintServerProperties"]).PrinterTasks();

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e) => ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);

        private void PrintTheColor_Click(object sender, EventArgs e)
        {
            int copiesOfSingleColor = Convert.ToInt16(numericUpDownTheSingleColor.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(_commandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "SingleColorTestPage", ColorToPrint, copiesOfSingleColor).SendDocumentToPrinter();
            }
        }

        private void PrintTheGridBtnClick(object sender, EventArgs e)
        {
            int copiesOfBwGrid = Convert.ToInt16(BWGirdUpDownNumeric.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(_commandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "BWGridTestPage", copiesOfBwGrid).SendDocumentToPrinter();
            }
        }

        private void PrintTheRainbowClick(object sender, EventArgs e)
        {
            int copiesOfRainbow = Convert.ToInt16(RinbowUpDownNumeric.Value);

            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                new Cmd(_commandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
                new PrintTestPage(SelectedPrinterName, "RainbowTestPage", copiesOfRainbow).SendDocumentToPrinter();
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e) => new Cmd(_commandList["GetPropertiesOfSelectedPrinter"], SelectedPrinterName).PrinterTasks();

        private void QueueOfPrinter_Click(object sender, EventArgs e) => new Cmd(_commandList["QueueOfSelectedPrinter"], SelectedPrinterName).PrinterTasks();

        private void RestartPrintSpool_Click(object sender, EventArgs e) => new Cmd(_commandList["RestartSpooler"]).PrinterTasks();

        private void SendFileToPrinter(object sender, EventArgs e)
        {
            new Cmd(_commandList["SetPrinterAsDefaultKey"], SelectedPrinterName).PrinterTasks();
            new SendFileToPrint(SelectedPrinterName).SendFileToSelectedPrinter();
        }

        private void SendTestPage_Click(object sender, EventArgs e) => new Cmd(_commandList["SendDefaultTestPage"], SelectedPrinterName).PrinterTasks();

        private void StartPrintSpool_Click(object sender, EventArgs e) => new Cmd(_commandList["StartSpooler"]).PrinterTasks();

        private void StopPrintSpool_Click(object sender, EventArgs e) => new Cmd(_commandList["StopSpooler"]).PrinterTasks();
    }
}