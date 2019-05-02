using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static PrinterHelper.Form1;

namespace PrinterHelper
{
    public partial class Frpogui : Form
    {
        private readonly ListBox listOfPrintersListBox;
        private string SelectedPrinterName => listOfPrintersListBox.SelectedItem.ToString();

        private readonly Dictionary<string, string> frpocommand = new Dictionary<string, string>()
        {
            {"PrintEventLog", "!R! ELOG; EXIT;"},
            {"PrintStatusPage","!R! STAT; STAT1; EXIT;"},
        };

        public Frpogui(ListBox listOfPrintersListBox)
        {
            InitializeComponent();
            this.listOfPrintersListBox = listOfPrintersListBox;
            Text = "FRPO For Kyocera";
            NameOfSelectedprinter.Text = SelectedPrinterName;
            ComboBoxOfCommands.SelectedIndex = 0;
        }

        private void SendCommand()
        {
            switch (ComboBoxOfCommands.SelectedItem.ToString())
            {
                case ("Event Log"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, frpocommand["PrintEventLog"]);
                    break;

                case ("Status Page"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, frpocommand["PrintStatusPage"]);
                    break;
            }
        }

        private void SendFRPOcommand_Click(object sender, EventArgs e)
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (DialogResult.OK != openFileDialog.ShowDialog(this)) return;
            _ = SendRawDataToPrinter.SendFileToPrinter(SelectedPrinterName, openFileDialog.FileName);
        }
    }
}