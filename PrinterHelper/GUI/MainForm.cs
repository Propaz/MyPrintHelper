using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrinterHelper.CommandLine;
using PrinterHelper.Core;
using PrinterHelper.Helpers;
using PrinterHelper.Models;
using PrinterHelper.Properties;

namespace PrinterHelper
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.mainicon;
            Text =
                $"Printer Helper {Assembly.GetExecutingAssembly().GetName().Version} build at {BuildVersion.GetBuildDate(Assembly.GetExecutingAssembly()):dd/MM/yyyy}";
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

        private string ColorToPrint => ListOfColorsForPrint.SelectedItem as string;
        private string SelectedPrinterName => ListOfPrintersListBox.SelectedItem as string;

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

            // Сначала получаем все необходимые данные в основном потоке.
            var printerNames = new List<string>();
            using (var searcher = new ManagementObjectSearcher(queryString))
            {
                using (var managementObjects = searcher.Get())
                {
                    foreach (var managementBaseObject in managementObjects)
                    {
                        using var managementObject = (ManagementObject)managementBaseObject;
                        var printerNameFromWmi = managementObject["Name"]?.ToString();
                        if (string.IsNullOrEmpty(printerNameFromWmi)) continue;

                        //Only Printer with flag "online"
                        if (managementObject["WorkOffline"]?.ToString()
                                .Equals("false", StringComparison.OrdinalIgnoreCase) == true)
                        {
                            printerNames.Add(printerNameFromWmi);
                        }
                    }
                }
            }

            await Task.Run(() =>
            {
                foreach (var printerName in printerNames)
                {
                    sync.Send(state => (box as ListBox)?.Items.Add(state), printerName);
                }
            }).ConfigureAwait(false);
        }

        private void AddNewPrinter_Click(object sender, EventArgs e) => Cmd.PrinterTasks(Resources.AddNewPrinterKey);

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e) =>
            contextMenuOfCommands.Enabled = ListOfPrintersListBox.SelectedIndex != -1;

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            var printerName = SelectedPrinterName;

            if (string.IsNullOrEmpty(printerName)) return;

            var dialogResult = MessageBox.Show(
                text: $"Are you sure you want to Delete [{printerName}] ?",
                caption: "Confirmation",
                buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Information);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        Cmd.PrinterTasks(Resources.DeleteSelectedPrinterKey, printerName);
                    }
                    finally
                    {
                        _ = MessageBox.Show(
                            text: $"The [{printerName}] has been Deleted",
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
                    throw new ArgumentOutOfRangeException();
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
                UIHelper.ShowError(ex.Message);
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

        private void FRPOToolStripMenuItem_Click(object sender, EventArgs e) => new FRPOGui(SelectedPrinterName).Show();

        private void GetPrintServerProperties(object sender, EventArgs e) =>
            Cmd.PrinterTasks(Resources.GetPrintServerProperties);

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

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e) =>
            ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);

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

        private void PrintSingleColorTestPage(int copiesOfSingleColor) =>
            new PrintTestPage(SelectedPrinterName, PageType.SingleColor, ColorToPrint, copiesOfSingleColor)
                .SendDocumentToPrinter();

        private void PrintTheGridBtnClick(object sender, EventArgs e)
        {
            int copiesOfBwGrid = Convert.ToInt16(GridTestCopies.Value);
            try
            {
                SetSelectedPrinterAsDefault();
            }
            finally
            {
                PrintGridTestPage(copiesOfBwGrid);
            }
        }

        private void PrintGridTestPage(int copiesOfBwGrid) => new PrintTestPage(
            SelectedPrinterName,
            PageType.Grid,
            copiesOfBwGrid
        ).SendDocumentToPrinter();

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

        private void PrintRainbowTestPage(int copiesOfRainbow) =>
            new PrintTestPage(SelectedPrinterName, PageType.Rainbow, copiesOfRainbow).SendDocumentToPrinter();

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e) =>
            Cmd.PrinterTasks(Resources.GetPropertiesOfSelectedPrinter, SelectedPrinterName);

        private void QueueOfPrinter_Click(object sender, EventArgs e) =>
            Cmd.PrinterTasks(Resources.QueueOfSelectedPrinter, SelectedPrinterName);

        private void RestartPrintSpool_Click(object sender, EventArgs e) => Cmd.PrinterTasks(Resources.RestartSpooler);

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

        private void SendTestPage_Click(object sender, EventArgs e) =>
            Cmd.PrinterTasks(Resources.SendDefaultTestPage, SelectedPrinterName);

        private void SetSelectedPrinterAsDefault() =>
            Cmd.PrinterTasks(Resources.SetPrinterAsDefaultKey, SelectedPrinterName);

        private void StartPrintSpool_Click(object sender, EventArgs e) => Cmd.PrinterTasks(Resources.StartSpooler);

        private void StopPrintSpool_Click(object sender, EventArgs e) => Cmd.PrinterTasks(Resources.StopSpooler);
    }
}