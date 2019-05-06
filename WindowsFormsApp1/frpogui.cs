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
            {"DrawArc", "!R! RES; UNIT C;MAP 5, 8;PAT 52;ARC 1, 2, -45, 45;MAP 5, 10;PIE 2, 0, 10, 20, 30;ARC 0, 2, 0, 60;PAT 20;ARC 0, 2, 60, 180;PAT 10;ARC 0, 2, 180, 360;PAT 1;NEWP;PMZP 4, 2;PARC 4, 3, 1, 90, 270;PARC 5, 4, 1, 180, 360;PARC 6, 3, 1, 270, 90;PARC 5, 2, 1, 0, 180;STRK;MRP 0.6, 1.1;SFNT \"Univers-Md\";TEXT \"ARC\";PAGE; EXIT;"},
            {"FillPattern", "!R! RES; DAM; UNIT C;FSET 1p08V0s0b5T;MCRO PATTERN;FPAT %1, %2, %3, %4, %5, %6, %7, %8;BOX 5, 1; BLK 5, 1; MRP 0, 1.55;TEXT ’FPAT %1, %2, %3, %4, %5, %6, %7, %8;’, L;ENDM;MAP 2, 2; SCP;CALL PATTERN, 3, 3, 0, 0, 0, 0, 0, 0;CALL PATTERN, 255, 0, 0, 0, 0, 0, 0, 0;CALL PATTERN, 1, 1, 1, 1, 1, 1, 1, 255;RPP; MRP 5.5, 0;CALL PATTERN, 0, 0, 24, 60, 60, 24, 0, 0;CALL PATTERN, 1, 2, 4, 8, 16, 32, 64, 128;CALL PATTERN, 8, 8, 8, 8, 8, 8, 8, 8;PAGE;EXIT;"},
            {"MarginTest", "!R! MZP 0,0; BOX 30,30; PAGE; EXIT;"}
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

                case ("Draw Arc"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, _frpocommand["DrawArc"]);
                    break;

                case ("Fill Pattern"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, _frpocommand["FillPattern"]);
                    break;

                case ("Margin Test"):
                    _ = SendRawDataToPrinter.SendStringToPrinter(SelectedPrinterName, _frpocommand["MarginTest"]);
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
            OpenFileDialog openFileDialog;
            using (openFileDialog = new OpenFileDialog() { Filter = "TXT Files(*.txt)|*.txt|All Files(*.*)|*.*" })
            {
                if (DialogResult.OK != openFileDialog.ShowDialog(this)) return;
                _ = SendRawDataToPrinter.SendFileToPrinter(SelectedPrinterName, openFileDialog.FileName);
            }
        }
    }
}