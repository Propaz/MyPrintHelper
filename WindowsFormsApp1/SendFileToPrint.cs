// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Form1
    {
        internal class SendFileToPrint
        {
            private readonly string PrinterName;

            public SendFileToPrint(string nameofprinter)
            {
                PrinterName = nameofprinter ?? throw new ArgumentNullException(nameof(nameofprinter));
            }

            public void SendFileToSelectedPrinter()
            {
                const string FilterOfFileTypes =
                "TXT Files(*.txt)|*.txt|Office Files|*.doc;*.docx;*.xlsx;*.xls;*.ppt;*.pptx|PDF Files(*.pdf)|*.pdf|Image Files|*.png;*.jpg;*.tiff;*.gif|All Files(*.*)|*.*";

                using (PrintDialog printDialog = new PrintDialog
                {
                    PrinterSettings = { PrinterName = PrinterName },
                    AllowSomePages = true
                })
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter =
                            FilterOfFileTypes
                    })
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