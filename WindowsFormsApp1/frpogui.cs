// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PrinterHelper.Properties;
using System;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Frpogui : Form
    {
        private readonly string SelectedPrinterName;

        public Frpogui(string SelectedPrinterFromMainForm)
        {
            InitializeComponent();
            Icon = Resources.mainicon;
            SelectedPrinterName = SelectedPrinterFromMainForm ?? throw new ArgumentNullException(nameof(SelectedPrinterFromMainForm));
            Text = "FRPO For Kyocera build at 10/05/2019";
            Label_SelectedPrinterName.Text = $"Send To: [{SelectedPrinterName}]";
            ComboBoxOfCommands.SelectedIndex = 0;
            TextBoxCustomFRPOCommand.Clear();
            SendCustomFRPOcommand.Enabled = false;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void SendCommand()
        {
            switch (ComboBoxOfCommands.SelectedItem.ToString())
            {
                case "Event Log":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOPrintEventLog);
                    break;

                case "Status Page":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOPrintStatusPage);
                    break;

                case "Draw Arc":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPODrawArc);
                    break;

                case "Fill Pattern":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOFillPattern);
                    break;

                case "Margin Test":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOMarginTest);
                    break;

                case "Draw Filled-in Block":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPODrawFilledInBlock);
                    break;

                case "Draw Circle":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPODrawCircle);
                    break;

                case "Dashed Pattern":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPODashedPattern);
                    break;

                case "Draw Cube":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPODrawCube);
                    break;

                case "Fill Closed Path":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOFillClosedPath);
                    break;

                case "Font List":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOFontList);
                    break;

                case "FRPO INIT":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOInit);
                    break;

                case "Check Disk":
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, Resources.FRPOCheckDisk);
                    break;
            }
        }

        private void SendScriptToPrinter_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog;
            using (openFileDialog = new OpenFileDialog() { Filter = Resources.FileFilterForOpenFileDialog })
            {
                if (DialogResult.OK != openFileDialog.ShowDialog(this)) return;
                _ = SendRawDataToPrinter.SendFileToPrinter(SelectedPrinterName, openFileDialog.FileName);
            }
        }

        private void SendFRPOCommandFromList_Click(object sender, EventArgs e)
        {
            SendCommand();
        }

        private void SendCustomFRPOcommand_Click(object sender, EventArgs e)
        {
            _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, CustomFRPOCommand);
        }

        private void TextBoxCustomFRPOCommand_TextChanged(object sender, EventArgs e)
        {
            SendCustomFRPOcommand.Enabled = !string.IsNullOrEmpty(CustomFRPOCommand);
            SendFRPOCommandFromList.Enabled = string.IsNullOrEmpty(CustomFRPOCommand);
            SendScriptToPrinter.Enabled = string.IsNullOrEmpty(CustomFRPOCommand);
            ComboBoxOfCommands.Enabled = string.IsNullOrEmpty(CustomFRPOCommand);
        }

        private string CustomFRPOCommand => TextBoxCustomFRPOCommand.Text;

        private void TextBoxClearButton_Click(object sender, EventArgs e)
        {
            TextBoxCustomFRPOCommand.Clear();
        }
    }
}