using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace PrinterHelper.CommandLine
{
    internal static class Cmd
    {
        private const string CmdArgumentForPrinterTasks = "/C rundll32 printui.dll,PrintUIEntry";
        private const string FileNameToExec = "cmd.exe";

        public static void PrinterTasks(string key, string selectedPrinter = "")
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var processStartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = FileNameToExec
            };

            if (key.IndexOf("spooler", StringComparison.OrdinalIgnoreCase) != -1)
            {
                processStartInfo.UseShellExecute = true;
                processStartInfo.Arguments = key;
                processStartInfo.Verb = "runas";
            }
            else
            {
                var arguments = $"{CmdArgumentForPrinterTasks} {key}";
                if (!string.IsNullOrEmpty(selectedPrinter))
                {
                    arguments += $" /n \"{selectedPrinter}\" ";
                }

                processStartInfo.Arguments = arguments;
            }

            try
            {
                using var process = Process.Start(processStartInfo);
                // Process started, no need to wait for it to exit.
            }
            catch (Exception ex) when (ex is Win32Exception or ObjectDisposedException or InvalidOperationException)
            {
                _ = MessageBox.Show(text: ex.Message, caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
            }
        }
    }
}