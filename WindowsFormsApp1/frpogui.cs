// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PrinterHelper.Properties;
using System;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Frpogui : Form
    {
        private readonly ListBox _listOfPrintersListBox;

        public Frpogui(ListBox listOfPrintersListBox)
        {
            InitializeComponent();
            Icon = Resources.mainicon;
            _listOfPrintersListBox = listOfPrintersListBox;
            Text = "FRPO For Kyocera build at 10/05/2019";
            Label_SelectedPrinterName.Text = $"Send To: [{SelectedPrinterName}]";
            ComboBoxOfCommands.SelectedIndex = 0;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private string SelectedPrinterName => _listOfPrintersListBox.SelectedItem.ToString();

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
            }
        }

        private void SendFRPO_Command_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCustomFRPOCommand.Text))
            {
                SendCommand();
            }
            else
            {
                _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, TextBoxCustomFRPOCommand.Text);
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
    }
}