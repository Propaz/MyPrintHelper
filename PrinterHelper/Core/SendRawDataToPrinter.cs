using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using System.Text;

namespace PrinterHelper
{
    internal partial class Frpogui
    {
        private static class SendRawDataToPrinter
        {
            public static bool SendFileToPrinter(string szPrinterName, string szFileName)
            {
                FileStream fs = new(szFileName, FileMode.Open);
                BinaryReader br = new(fs);

                int nLength = Convert.ToInt32(fs.Length);
                byte[] bytes = br.ReadBytes(nLength);
                IntPtr pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                bool bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                return bSuccess;
            }

            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                int dwCount = System.Text.Encoding.Default.GetByteCount(szString);
                IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }

            private static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
            {
                bool bSuccess = false;
                NativeMethods.DOCINFOA di = new NativeMethods.DOCINFOA
                {
                    pDocName = "FRPO RAW Document",
                    pDataType = "RAW"
                };

                if (NativeMethods.OpenPrinter(szPrinterName.Normalize(), out IntPtr hPrinter, IntPtr.Zero))
                {
                    if (NativeMethods.StartDocPrinter(hPrinter, 1, di))
                    {
                        if (NativeMethods.StartPagePrinter(hPrinter))
                        {
                            bSuccess = NativeMethods.WritePrinter(hPrinter, pBytes, dwCount, out _);
                            NativeMethods.EndPagePrinter(hPrinter);
                        }
                        NativeMethods.EndDocPrinter(hPrinter);
                    }
                    NativeMethods.ClosePrinter(hPrinter);
                }
                if (!bSuccess)
                {
                    _ = MessageBox.Show(text: new Win32Exception(Marshal.GetLastWin32Error()).Message, caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                }
                return bSuccess;
            }
        }
    }
}