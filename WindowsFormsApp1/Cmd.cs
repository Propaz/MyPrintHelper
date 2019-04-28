// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Form1 : Form
    {
        internal class Cmd
        {
            private readonly string CmdArgumentForPrinterTasks = "/C rundll32 printui.dll,PrintUIEntry";
            private readonly string FileNameToExec = "cmd.exe";
            private readonly string key;
            private readonly string SelectedPrinter;

            public Cmd(string keyforcmd, string nameofprinter)
            {
                key = keyforcmd ?? throw new ArgumentNullException(nameof(keyforcmd));
                SelectedPrinter = nameofprinter ?? throw new ArgumentNullException(nameof(nameofprinter));
            }

            public void PrinterTasks()
            {
                using (Process process = new Process())
                {
                    if (key == "/il" && key == "/s")
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = FileNameToExec,
                            Arguments =
                                $"{CmdArgumentForPrinterTasks} {key}"
                        };
                    }
                    else
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = FileNameToExec,
                            Arguments =
                               $"{CmdArgumentForPrinterTasks} {key} /n \"{SelectedPrinter}\""
                        };
                    }
                    if (key.IndexOf("spooler", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        process.StartInfo = new ProcessStartInfo
                        {
                            UseShellExecute = true,
                            FileName = FileNameToExec,
                            Arguments = key,
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