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
            Text = $"Printer Helper v{Assembly.GetExecutingAssembly().GetName().Version} build at 08/04/2019";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
            ListOfColorsForPrint.SelectedIndex = 0;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                FindThePrinterBtnClick(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            if (sync == null)
            {
                throw new ArgumentNullException(nameof(sync));
            }

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString: "SELECT * FROM Win32_Printer"))
            using (ManagementObjectCollection printerList = searcher.Get())
            {
                CancellationToken token = new CancellationToken(false);
                TaskScheduler scheduler = TaskScheduler.Default;
                await Task.Factory.StartNew(b =>
                {
                    foreach (ManagementBaseObject o in printerList)
                    {
                        using (ManagementObject printer = (ManagementObject)o)
                        {
                            string printerName = printer["Name"].ToString();
                            if (printerName != null)
                            {
                                if (printer[propertyName: "WorkOffline"].ToString().Equals(value: "false", comparisonType: StringComparison.OrdinalIgnoreCase)) //Only Printer with flag "online"
                                {
                                    sync.Send(a => (b as ListBox)?.Items.Add(a), printerName);
                                }
                            }
                        }
                    }
                }, box, token, TaskCreationOptions.LongRunning, scheduler).ConfigureAwait(false);
            }
        }

        private static Color
            MapRainbowColor(float value, float redValue, float blueValue)
        {
            int intValue =
                (int)(1023 * (value - redValue) / (blueValue - redValue)); // Convert into a value between 0 and 1023.

            if (intValue < 256)
            {
                return Color.FromArgb(255, intValue, 0); // Map different color bands.
            }

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

        private void AddNewPrinter_Click(object sender, EventArgs e)
        {
            PrinterTasks("/il");//Call "Add New Printer" Dialog
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuOfCommands.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
        }

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                text: $"Are you sure you want to Delete [{ListOfPrintersListBox.SelectedItem}] ?",
                caption: "Confirmation",
                buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Information);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        PrinterTasks(key: "/dl"); //Delete local printer
                    }
                    finally
                    {
                        _ = MessageBox.Show(
                            text: $"The [{ListOfPrintersListBox.SelectedItem}] was Deleted",
                            caption: "Information", buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information);
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
            }
        }

        private async void FindThePrinterBtnClick(object sender, EventArgs e)
        {
            FindPriners.Enabled = false;
            PrintBWGrid.Enabled = false;
            PrintTheRainbowBtn.Enabled = false;
            PrintTheColor.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ListOfPrintersListBox.Items.Clear();
            try
            {
                await GetPrinterList(sync: SynchronizationContext.Current,
                    box: ListOfPrintersListBox).ConfigureAwait(true);
            }
            catch (ManagementException ex)
            {
                _ = MessageBox.Show(text: ex.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                FindPriners.Enabled = true;
                PrintBWGrid.Enabled = true;
                PrintTheRainbowBtn.Enabled = true;
                PrintTheColor.Enabled = true;
                BWGirdUpDownNumeric.Value = 1;
                numericUpDownTheSingleColor.Value = 1;
                RinbowUpDownNumeric.Value = 1;
                ListOfColorsForPrint.SelectedIndex = 0;
            }
        }

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e)
        {
            ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);
        }

        private void PrinterTasks(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            using (Process process = new Process())
            {
                if (key == "/il")
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments =
                        $"/C rundll32 printui.dll,PrintUIEntry {key}"
                    };
                }
                else
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments =
                            $"/C rundll32 printui.dll,PrintUIEntry {key} /n \"{ListOfPrintersListBox.SelectedItem}\""
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

        private void PrintSpoolCmd(string spoolCmd)
        {
            if (spoolCmd == null)
            {
                throw new ArgumentNullException(nameof(spoolCmd));
            }

            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = "cmd.exe",
                    Arguments = "/c " + spoolCmd,
                    Verb = "runas",
                };

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

        private void PrintTheColor_Click(object sender, EventArgs e)
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                PrinterTasks(key: "/y");

                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() } })
                {
                    document.PrintPage += PrintTheSingleColor;
                    if (numericUpDownTheSingleColor != null)
                    {
                        document.PrinterSettings.Copies = Convert.ToInt16(numericUpDownTheSingleColor.Value);
                    }

                    try
                    {
                        document.Print();
                    }
                    catch (InvalidPrinterException exc)
                    {
                        _ = MessageBox.Show(text: exc.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PrintTheGridBtnClick(object sender, EventArgs e) //Print the Grid
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                PrinterTasks("/y"); //Set Selected Printer as Default
                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() } })
                {
                    document.PrintPage += PrintTheGridDocument;
                    if (BWGirdUpDownNumeric != null)
                    {
                        document.PrinterSettings.Copies = Convert.ToInt16(BWGirdUpDownNumeric.Value);
                    }

                    try
                    {
                        document.Print();
                    }
                    catch (InvalidPrinterException exc)
                    {
                        _ = MessageBox.Show(text: exc.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PrintTheGridDocument(object sender, PrintPageEventArgs e)
        {
            //Draw a grid
            const int w = 1654; //A4 size
            const int h = 2339;
            const int widthLines = 20; //cell size
            const int heightLines = 20;
            for (int i = 0; i < w; i += widthLines)
            {
                //Width Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0), new Point(i + widthLines, h));
                //Height Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines),
                    new Point(w, i + heightLines));
            }
        }

        private void PrintTheRainbowClick(object sender, EventArgs e)
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                PrinterTasks(key: "/y");

                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() } })
                {
                    document.PrintPage += PrintTheRainbowPage;
                    if (BWGirdUpDownNumeric != null)
                    {
                        document.PrinterSettings.Copies = Convert.ToInt16(RinbowUpDownNumeric.Value);
                    }

                    try
                    {
                        document.Print();
                    }
                    catch (InvalidPrinterException exc)
                    {
                        _ = MessageBox.Show(text: exc.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }
                }
            }
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

        private void PrintTheSingleColor(object sender, PrintPageEventArgs e)
        {
            switch (ListOfColorsForPrint.SelectedItem.ToString())

            {
                case "Black":
                    e.Graphics.FillRectangle(Brushes.Black, 50, 50, 720, 1000);
                    break;

                case "Cyan":
                    e.Graphics.FillRectangle(Brushes.Cyan, 50, 50, 720, 1000);
                    break;

                case "Magenta":
                    e.Graphics.FillRectangle(Brushes.Magenta, 50, 50, 720, 1000);
                    break;

                case "Yellow":
                    e.Graphics.FillRectangle(Brushes.Yellow, 50, 50, 720, 1000);
                    break;

                case "Red":
                    e.Graphics.FillRectangle(Brushes.Red, 50, 50, 720, 1000);
                    break;

                case "Green":
                    e.Graphics.FillRectangle(Brushes.Green, 50, 50, 720, 1000);
                    break;

                case "Blue":
                    e.Graphics.FillRectangle(Brushes.Blue, 50, 50, 720, 1000);
                    break;
            }
        }

        private void PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrinterTasks(key: "/p"); // Properties of printer
        }

        private void QueueOfPrinter_Click(object sender, EventArgs e)
        {
            PrinterTasks(key: "/o"); //Displays the queue
        }

        private void RestartPrintSpool_Click(object sender, EventArgs e)
        {
            PrintSpoolCmd(spoolCmd: "net stop spooler&&DEL /F /S /Q %systemroot%\\System32\\spool\\PRINTERS\\*&&net start spooler&&pause");
        }

        private void SendFileToPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrinterTasks(key: "/y");
            SendFileToSelectedPrinter();
        }

        private void SendFileToSelectedPrinter()
        {
            using (PrintDialog printDialog = new PrintDialog
            {
                PrinterSettings = { PrinterName = ListOfPrintersListBox.SelectedItem.ToString() },
                AllowSomePages = true
            })
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter =
                       "TXT Files(*.txt)|*.txt|Office Files|*.doc;*.docx;*.xlsx;*.xls;*.ppt;*.pptx|PDF Files(*.pdf)|*.pdf|Image Files|*.png;*.jpg;*.tiff;*.gif|All Files(*.*)|*.*"
                })
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    if (printDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    try
                    {
                        _ = Process.Start(startInfo: new ProcessStartInfo(fileName: openFileDialog.FileName)
                        {
                            Verb = "Print",
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                    }
                    catch (System.IO.FileNotFoundException exfilenotfound)
                    {
                        _ = MessageBox.Show(text: exfilenotfound.Message, caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
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

        private void SendTestPage_Click(object sender, EventArgs e)
        {
            PrinterTasks(key: "/k"); // Send Default Windows Test page
        }

        private void StartPrintSpool_Click(object sender, EventArgs e)
        {
            PrintSpoolCmd(spoolCmd: "net start spooler&&pause");
        }

        private void StopPrintSpool_Click(object sender, EventArgs e)
        {
            PrintSpoolCmd(spoolCmd: "net stop spooler&&pause");
        }
    }
}