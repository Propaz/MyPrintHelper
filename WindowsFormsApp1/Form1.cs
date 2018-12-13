// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Printer Helper v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " build at 10/12/2018";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
        }

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e)
        {
            ListOfPrintersListBox.SelectedIndex =
                ListOfPrintersListBox.IndexFromPoint(e.X, e.Y); //Right Click to select items in a ListBox
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            using (ManagementObjectCollection printerList = searcher.Get())
            {
                await Task.Factory.StartNew(b =>
                {
                    if (printerList == null) return;
                    foreach (ManagementBaseObject o in printerList)
                    {
                        using (ManagementObject printer = (ManagementObject)o)
                        {
                            string printerName = printer["Name"].ToString().ToLower();
                            if (printer["WorkOffline"].ToString().Equals("false", StringComparison.OrdinalIgnoreCase)
                                && !printerName.Contains("xps")) //Only on-line Printer, XPS devices
                            {
                                sync.Send(a => (b as ListBox)?.Items.Add(a), printerName);
                            }
                        }
                    }
                }, box).ConfigureAwait(false);
            }
        }

        private async void FindThePrinterBtnClick(object sender, EventArgs e)
        {
            FindPriners.Enabled = false;
            PrintBWGrid.Enabled = false;
            PrintTheRainbowBtn.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ListOfPrintersListBox.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current,
                    ListOfPrintersListBox).ConfigureAwait(true);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                FindPriners.Enabled = true;
                PrintBWGrid.Enabled = true;
                PrintTheRainbowBtn.Enabled = true;
            }
        }

        private void ListOfPrintersChanged(object sender, EventArgs e)
        {
        }

        private void PrinterTasks(string key)
        {
            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments =
                        "/C rundll32 printui.dll,PrintUIEntry " + key + " /n \"" + ListOfPrintersListBox.SelectedItem +
                        "\""
                };
                if (key != null) process.StartInfo = startInfo;

                process.Start();
            }
        }

        private void PrintTheGridDocument(object sender, PrintPageEventArgs e)
        {
            //Draw a grid
            const int w = 1654; //A4 size
            const int h = 2339;
            const int widthLines = 20; //cell size
            const int heightLines = 20;
            for (int i = 0; i < w; i += widthLines) //fill all list A4
            {
                //Width Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0), new Point(i + widthLines, h));
                //Height Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines),
                    new Point(w, i + heightLines));
            }
        }

        private void PrintTheGridBtnClick(object sender, EventArgs e) //Print the Grid
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select Printer first", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                PrinterTasks("/y"); //Set Selected Printer as Default
                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() } })
                {
                    document.PrintPage += PrintTheGridDocument;
                    if (BWGirdUpDownNumeric != null) document.PrinterSettings.Copies = Convert.ToInt16(BWGirdUpDownNumeric.Value);
                    document.Print();
                }
            }
        }

        private static Color
            MapRainbowColor(float value, float redValue, float blueValue) // Map a value to a rainbow color.
        {
            int intValue =
                (int)(1023 * (value - redValue) / (blueValue - redValue)); // Convert into a value between 0 and 1023.

            if (intValue < 256) return Color.FromArgb(255, intValue, 0); // Map different color bands.

            if (intValue < 512)
            {
                // Yellow to green. (255, 255, 0) to (0, 255, 0).
                intValue -= 256;
                return Color.FromArgb(255 - intValue, 255, 0);
            }

            if (intValue < 768)
            {
                // Green to aqua. (0, 255, 0) to (0, 255, 255).
                intValue -= 512;
                return Color.FromArgb(0, 255, intValue);
            }

            // Aqua to blue. (0, 255, 255) to (0, 0, 255).
            intValue -= 768;
            return Color.FromArgb(0, 255 - intValue, 255);
        }

        private void PrintTheRainbowPage(object sender, PrintPageEventArgs e)
        {
            const int wid = 600;
            const int hgt = 600;
            const int hgt2 = hgt / 2;
            for (int x = 0; x < wid; x++)
            {
                using (Pen thePen = new Pen(MapRainbowColor(x, 0, wid)))
                {
                    e.Graphics.DrawLine(thePen, x, 0, x, hgt2);
                }

                using (Pen thePen = new Pen(MapRainbowColor(x, wid, 0)))
                {
                    e.Graphics.DrawLine(thePen, x, hgt2, x, hgt);
                }
            }
        }

        private void PrintTheRainbowClick(object sender, EventArgs e)
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select Printer first", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                PrinterTasks("/y");
                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() } })
                {
                    document.PrintPage += PrintTheRainbowPage;
                    if (BWGirdUpDownNumeric != null) document.PrinterSettings.Copies = Convert.ToInt16(RinbowUpDownNumeric.Value);
                    document.Print();
                }
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
        }

        private void QueueOfPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/o"); //Displays the queue for a printer.
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                @"Are you sure you want to Delete [" + ListOfPrintersListBox.SelectedItem.ToString().ToUpper() + @"] ?",
                @"Confirmation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        PrinterTasks("/dl"); //Delete local printer
                    }
                    catch (Win32Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        MessageBox.Show(
                            @"The [" + ListOfPrintersListBox.SelectedItem.ToString().ToUpper() + @"] was Deleted",
                            @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        FindThePrinterBtnClick(null, null); //Renew results
                    }

                    break;

                case DialogResult.Cancel:
                case DialogResult.None:
                case DialogResult.Abort:
                case DialogResult.Retry:
                case DialogResult.Ignore:
                case DialogResult.Yes:
                case DialogResult.No:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/p"); // Properties of printer
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendTestPage_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/k"); // Send Default Windows Test page
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendFileToPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/y");
                SendFileToSelectedPrinter();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendFileToSelectedPrinter()
        {
            using (PrintDialog printDialog = new PrintDialog
            {
                PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() },
                AllowSomePages = true
            })
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter =
                        @"TXT Files(*.txt)|*.txt|Office Files|*.doc;*.docx;*.xlsx;*.xls;*.ppt;*.pptx|PDF Files(*.pdf)|*.pdf|Image Files|*.png;*.jpg;*.tiff;*.gif|All Files(*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                if (openFileDialog.FileName == null) return;
                var processStartInfo = new ProcessStartInfo(openFileDialog.FileName)
                {
                    Verb = "Print",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                if (printDialog.ShowDialog() != DialogResult.OK) return;
                Process.Start(processStartInfo);
            }
        }

        private void NumericUpDownCopiesOfGridChanged(object sender, EventArgs e)
        {
        }

        private void NumericUpDown2TheRainbowCopiesChanged(object sender, EventArgs e)
        {
        }

        private void RestartPrintSpool_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RestartPrintSpool.Enabled = false;
            StartPrintSpool.Enabled = false;
            StopPrintSpool.Enabled = false;
            AddNewPrinter.Enabled = false;
            try
            {
                PrintSpoolCmd("net stop spooler&&DEL /F /S /Q %systemroot%\\System32\\spool\\PRINTERS\\*&&net start spooler&&pause");
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                RestartPrintSpool.Enabled = true;
                StartPrintSpool.Enabled = true;
                StopPrintSpool.Enabled = true;
                AddNewPrinter.Enabled = true;
            }
        }

        private void StartPrintSpool_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RestartPrintSpool.Enabled = false;
            StartPrintSpool.Enabled = false;
            StopPrintSpool.Enabled = false;
            AddNewPrinter.Enabled = false;
            try
            {
                PrintSpoolCmd("net start spooler&&pause");
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                RestartPrintSpool.Enabled = true;
                StartPrintSpool.Enabled = true;
                StopPrintSpool.Enabled = true;
                AddNewPrinter.Enabled = true;
            }
        }

        private void StopPrintSpool_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            RestartPrintSpool.Enabled = false;
            StartPrintSpool.Enabled = false;
            StopPrintSpool.Enabled = false;
            AddNewPrinter.Enabled = false;
            try
            {
                PrintSpoolCmd("net stop spooler&&pause");
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                RestartPrintSpool.Enabled = true;
                StartPrintSpool.Enabled = true;
                StopPrintSpool.Enabled = true;
                AddNewPrinter.Enabled = true;
            }
        }

        private void PrintSpoolCmd(string SpoolCmd)
        {
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\cmd.exe")
            {
                UseShellExecute = true,
                Arguments = "/c " + SpoolCmd,
                Verb = "runas",
            };
            try
            {
                Process.Start(info);
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                    MessageBox.Show("Cancelled!");
                else
                    throw;
            }
        }

        private void AddNewPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/il");
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}