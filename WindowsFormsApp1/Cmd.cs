// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class MainForm : Form
    {
        private class Cmd
        {
            private const string CmdArgumentForPrinterTasks = "/C rundll32 printui.dll,PrintUIEntry";
            private const string FileNameToExec = "cmd.exe";
            private readonly string _key;
            private readonly string _selectedPrinter;

            public Cmd(string keyforcmd, string nameofprinter)
            {
                _key = keyforcmd ?? throw new ArgumentNullException(nameof(keyforcmd));
                _selectedPrinter = nameofprinter ?? throw new ArgumentNullException(nameof(nameofprinter));
            }

            public Cmd(string keyforcmd)
            {
                _key = keyforcmd ?? throw new ArgumentNullException(nameof(keyforcmd));
                _selectedPrinter = string.Empty;
            }

            public void PrinterTasks()
            {
                Process process;
                using (process = new Process())
                {
                    process.StartInfo = string.IsNullOrEmpty(_selectedPrinter)
                        ? new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = FileNameToExec,
                            Arguments =
                                $"{CmdArgumentForPrinterTasks} {_key}"
                        }
                        : new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = FileNameToExec,
                            Arguments =
                               $"{CmdArgumentForPrinterTasks} {_key} /n \"{_selectedPrinter}\""
                        };

                    if (_key.IndexOf("spooler", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            UseShellExecute = true,
                            FileName = FileNameToExec,
                            Arguments = _key,
                            Verb = "runas"
                        };
                    }
                    try
                    {
                        _ = process.Start();
                    }
                    catch (ObjectDisposedException exd)
                    {
                        _ = MessageBox.Show(text: exd.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                    catch (InvalidOperationException exc)
                    {
                        _ = MessageBox.Show(text: exc.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
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