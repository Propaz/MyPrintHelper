// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PrinterHelper.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class MainForm
    {
        private class SendFileToPrint
        {
            private readonly string _printerName;

            public SendFileToPrint(string nameofprinter) => _printerName = nameofprinter ?? throw new ArgumentNullException(nameof(nameofprinter));

            public void SendFileToSelectedPrinter()
            {
                PrintDialog printDialog;
                using (printDialog = new PrintDialog
                {
                    PrinterSettings = { PrinterName = _printerName },
                    AllowSomePages = true
                })
                {
                    OpenFileDialog openFileDialog;
                    using (openFileDialog = new OpenFileDialog { Filter = Resources.FileFilterForOpenFileDialog })
                    {
                        if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                        if (printDialog.ShowDialog() != DialogResult.OK) return;

                        try
                        {
                            _ = Process.Start(new ProcessStartInfo(fileName: openFileDialog.FileName)
                            {
                                Verb = "Print",
                                CreateNoWindow = true,
                                WindowStyle = ProcessWindowStyle.Hidden
                            });
                        }
                        catch (ArgumentNullException nullex)
                        {
                            _ = MessageBox.Show(text: nullex.Message, caption: "Error", buttons: MessageBoxButtons.OK,
                                icon: MessageBoxIcon.Error);
                        }
                        catch (FileNotFoundException exfilenotfound)
                        {
                            _ = MessageBox.Show(text: exfilenotfound.Message, caption: "Error", buttons: MessageBoxButtons.OK,
                                icon: MessageBoxIcon.Error);
                        }
                        catch (ObjectDisposedException exdisposed)
                        {
                            _ = MessageBox.Show(text: exdisposed.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                        catch (InvalidOperationException exo)
                        {
                            _ = MessageBox.Show(text: exo.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                        catch (Win32Exception ex)
                        {
                            _ = MessageBox.Show(text: ex.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}