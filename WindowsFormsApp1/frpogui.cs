// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Frpogui : Form
    {
        private readonly Dictionary<string, string> _frpocommand = new Dictionary<string, string>()
        {
            {"PrintEventLog", "!R! ELOG; EXIT;"},
            {"PrintStatusPage","!R! STAT; STAT1; EXIT;"},
        };

        private readonly ListBox _listOfPrintersListBox;

        public Frpogui(ListBox listOfPrintersListBox)
        {
            InitializeComponent();
            Icon = Properties.Resources.mainicon;
            _listOfPrintersListBox = listOfPrintersListBox;
            Text = "FRPO For Kyocera build at 03/05/2019";
            NameOfSelectedprinter.Text = $"Send To: [{SelectedPrinterName}]";
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
                case ("Event Log"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, _frpocommand["PrintEventLog"]);
                    break;

                case ("Status Page"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, _frpocommand["PrintStatusPage"]);
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
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "TXT Files(*.txt)|*.txt|All Files(*.*)|*.*" })
            {
                if (DialogResult.OK != openFileDialog.ShowDialog(this)) return;
                _ = SendRawDataToPrinter.SendFileToPrinter(SelectedPrinterName, openFileDialog.FileName);
            }
        }
    }
}