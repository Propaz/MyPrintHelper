// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PrinterHelper.Properties;
using System;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Frpogui : Form
    {
        private readonly string _selectedPrinterName;

        public Frpogui(string selectedPrinterFromMainForm)
        {
            InitializeComponent();
            Icon = Resources.mainicon;
            _selectedPrinterName = selectedPrinterFromMainForm ?? throw new ArgumentNullException(nameof(selectedPrinterFromMainForm));
            Text = "FRPO Command For Kyocera build at 11/05/2019";
            Label_SelectedPrinterName.Text = $"Send To: [{_selectedPrinterName}]";
            ComboBoxOfCommands.SelectedIndex = 0;
            TextBoxCustomFRPOCommand.Clear();
            SendCustomFRPOcommand.Enabled = false;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private string CustomFrpoCommand => TextBoxCustomFRPOCommand.Text;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SendCommand()
        {
            switch (ComboBoxOfCommands.SelectedItem.ToString())
            {
                case "Event Log":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOPrintEventLog);
                    break;

                case "Status Page":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOPrintStatusPage);
                    break;

                case "Draw Arc":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPODrawArc);
                    break;

                case "Fill Pattern":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOFillPattern);
                    break;

                case "Margin Test":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOMarginTest);
                    break;

                case "Draw Filled-in Block":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPODrawFilledInBlock);
                    break;

                case "Draw Circle":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPODrawCircle);
                    break;

                case "Dashed Pattern":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPODashedPattern);
                    break;

                case "Draw Cube":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPODrawCube);
                    break;

                case "Fill Closed Path":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOFillClosedPath);
                    break;

                case "Font List":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOFontList);
                    break;

                case "FRPO INIT":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOInit);
                    break;

                case "Check Disk":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOCheckDisk);
                    break;

                case "Simple Color Palette":
                    _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, Resources.FRPOSimpleColorPalette);
                    break;
            }
        }

        private void SendCustomFRPOcommand_Click(object sender, EventArgs e) => _ = SendRawDataToPrinter.SendStringToPrinter(_selectedPrinterName, CustomFrpoCommand);

        private void SendFRPOCommandFromList_Click(object sender, EventArgs e) => SendCommand();

        private void SendScriptToPrinter_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog;
            using (openFileDialog = new OpenFileDialog() { Filter = Resources.FileFilterForOpenFileDialog })
            {
                if (DialogResult.OK != openFileDialog.ShowDialog(this)) return;
                _ = SendRawDataToPrinter.SendFileToPrinter(_selectedPrinterName, openFileDialog.FileName);
            }
        }

        private void TextBoxClearButton_Click(object sender, EventArgs e) => TextBoxCustomFRPOCommand.Clear();

        private void TextBoxCustomFRPOCommand_TextChanged(object sender, EventArgs e)
        {
            SendCustomFRPOcommand.Enabled = !string.IsNullOrEmpty(CustomFrpoCommand);
            SendFRPOCommandFromList.Enabled = string.IsNullOrEmpty(CustomFrpoCommand);
            SendScriptToPrinter.Enabled = string.IsNullOrEmpty(CustomFrpoCommand);
            ComboBoxOfCommands.Enabled = string.IsNullOrEmpty(CustomFrpoCommand);
        }
    }
}