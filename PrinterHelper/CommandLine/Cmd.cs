using System;
using System.ComponentModel;
using System.Diagnostics;
using PrinterHelper.Helpers;

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

                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.Arguments = arguments;
            }

            try
            {
                using var process = Process.Start(processStartInfo);
            }
            catch (Exception ex) when (ex is Win32Exception or ObjectDisposedException or InvalidOperationException)
            {
                UIHelper.ShowError(ex.Message);
            }
        }
    }
}