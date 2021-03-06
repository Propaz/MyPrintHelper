﻿// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PrinterHelper.Properties;
using System;
using System.ComponentModel;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterHelper
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.mainicon;
            Text = $"Printer Helper {Assembly.GetExecutingAssembly().GetName().Version} build at 07/07/2019";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
            ListOfColorsForPrint.SelectedIndex = 0;
            ListOfColorsForPrint.Enabled = false;
            PrintBWGrid.Enabled = false;
            PrintTheRainbowBtn.Enabled = false;
            PrintTheColor.Enabled = false;
            GridTestCopies.Enabled = false;
            RainbowTestPageCopies.Enabled = false;
            SingleColorTestPageCopies.Enabled = false;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private string ColorToPrint => ListOfColorsForPrint.SelectedItem.ToString();
        private string SelectedPrinterName => ListOfPrintersListBox.SelectedItem.ToString();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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

            ManagementObjectSearcher searcher;
            using (searcher = new ManagementObjectSearcher(queryString: queryString))
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

        private void AddNewPrinter_Click(object sender, EventArgs e) => new Cmd(Resources.AddNewPrinterKey).PrinterTasks();

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
                        new Cmd(Resources.DeleteSelectedPrinterKey, SelectedPrinterName).PrinterTasks();
                    }
                    finally
                    {
                        _ = MessageBox.Show(
                            text: $"The [{SelectedPrinterName}] has been Deleted",
                            caption: "Information", buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information);
                        FindThePrinterBtnClick(null, null);
                    }
                    break;

                case DialogResult.Cancel:
                    break;
            }
        }

        private async void FindThePrinterBtnClick(object sender, EventArgs e)
        {
            FindPrinters.Enabled = false;
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
                FindPrinters.Enabled = true;
                GridTestCopies.Value = 1;
                SingleColorTestPageCopies.Value = 1;
                RainbowTestPageCopies.Value = 1;
                ListOfColorsForPrint.SelectedIndex = 0;
                ListOfPrintersListBox.SelectedIndex = 0;
            }
        }

        private void FRPOToolStripMenuItem_Click(object sender, EventArgs e) => new Frpogui(SelectedPrinterName).Show();

        private void GetPrintServerProperties(object sender, EventArgs e) => new Cmd(Resources.GetPrintServerProperties).PrinterTasks();

        private void ListOfPrintersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintBWGrid.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            PrintTheRainbowBtn.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            PrintTheColor.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            ListOfColorsForPrint.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            GridTestCopies.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            RainbowTestPageCopies.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
            SingleColorTestPageCopies.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
        }

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e) => ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);

        private void PrintTheColor_Click(object sender, EventArgs e)
        {
            int copiesOfSingleColor = Convert.ToInt16(SingleColorTestPageCopies.Value);
            try
            {
                SetSelectedPrinterAsDefault();
            }
            finally
            {
                PrintSingleColorTestPage(copiesOfSingleColor);
            }
        }

        private void PrintSingleColorTestPage(int copiesOfSingleColor) => new PrintTestPage(SelectedPrinterName, "SingleColorTestPage", ColorToPrint, copiesOfSingleColor).SendDocumentToPrinter();

        private void PrintTheGridBtnClick(object sender, EventArgs e)
        {
            int copiesOfBwGrid = Convert.ToInt16(GridTestCopies.Value);
            try
            {
                SetSelectedPrinterAsDefault();
            }
            finally
            {
                PrintBWGridTestPage(copiesOfBwGrid);
            }
        }

        private void PrintBWGridTestPage(int copiesOfBwGrid) => new PrintTestPage(SelectedPrinterName, "BWGridTestPage", copiesOfBwGrid).SendDocumentToPrinter();

        private void PrintTheRainbowClick(object sender, EventArgs e)
        {
            int copiesOfRainbow = Convert.ToInt16(RainbowTestPageCopies.Value);
            try
            {
                SetSelectedPrinterAsDefault();
            }
            finally
            {
                PrintRainbowTestPage(copiesOfRainbow);
            }
        }

        private void PrintRainbowTestPage(int copiesOfRainbow) => new PrintTestPage(SelectedPrinterName, "RainbowTestPage", copiesOfRainbow).SendDocumentToPrinter();

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e) => new Cmd(Resources.GetPropertiesOfSelectedPrinter, SelectedPrinterName).PrinterTasks();

        private void QueueOfPrinter_Click(object sender, EventArgs e) => new Cmd(Resources.QueueOfSelectedPrinter, SelectedPrinterName).PrinterTasks();

        private void RestartPrintSpool_Click(object sender, EventArgs e) => new Cmd(Resources.RestartSpooler).PrinterTasks();

        private void SendFileToPrinter(object sender, EventArgs e)
        {
            try
            {
                SetSelectedPrinterAsDefault();
            }
            finally
            {
                new SendFileToPrint(SelectedPrinterName).SendFileToSelectedPrinter();
            }
        }

        private void SendTestPage_Click(object sender, EventArgs e) => new Cmd(Resources.SendDefaultTestPage, SelectedPrinterName).PrinterTasks();

        private void SetSelectedPrinterAsDefault() => new Cmd(Resources.SetPrinterAsDefaultKey, SelectedPrinterName).PrinterTasks();

        private void StartPrintSpool_Click(object sender, EventArgs e) => new Cmd(Resources.StartSpooler).PrinterTasks();

        private void StopPrintSpool_Click(object sender, EventArgs e) => new Cmd(Resources.StopSpooler).PrinterTasks();
    }
}