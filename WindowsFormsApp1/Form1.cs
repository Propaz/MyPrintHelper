// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
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
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
        }

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e)
        {
            ListOfPrintersListBox.SelectedIndex =
                ListOfPrintersListBox.IndexFromPoint(e.X, e.Y); //Right Click to select items in a ListBox
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box) //Find all online Printers
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            using (var printerList = searcher.Get())
            {
                await Task.Factory.StartNew(b =>
                {
                    if (printerList == null) return;
                    foreach (var o in printerList)
                        using (var printer = (ManagementObject) o)
                        {
                            var printerName = printer["Name"].ToString().ToLower(); //get PrinterName
                            if (printer["WorkOffline"].ToString().ToLower().Equals("false") &&
                                printerName.Contains("xps") == false) //Only online Printer, XPS devices
                                sync.Send(a => { (b as ListBox)?.Items.Add(a); }, printerName);
                        }
                }, box);
            }
        }

        private async void FindThePrinterBtnClick(object sender, EventArgs e)
        {
            findprinter_btn.Enabled = false;
            print_grid_btn.Enabled = false;
            PrintTheRainbowBtn.Enabled = false;
            Cursor = Cursors.WaitCursor;
            ListOfPrintersListBox.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current,
                    ListOfPrintersListBox);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
                findprinter_btn.Enabled = true;
                print_grid_btn.Enabled = true;
                PrintTheRainbowBtn.Enabled = true;
            }
        }

        private void ListOfPrintersChanged(object sender, EventArgs e)
        {
        }

        private void PrinterTasks(string key)
        {
            using (var process = new Process())
            {
                var startInfo = new ProcessStartInfo
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
            const int h = 2339; //A4 size
            const int widthLines = 20; //cell size
            const int heightLines = 20; //cell size
            for (var i = 0; i < w; i += widthLines) //fill all list A4
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
                using (var document = new PrintDocument
                    {PrinterSettings = {PrinterName = ListOfPrintersListBox.SelectedItem.ToString()}})
                {
                    document.PrintPage += PrintTheGridDocument;
                    if (numericUpDown1 != null) document.PrinterSettings.Copies = Convert.ToInt16(numericUpDown1.Value);
                    document.Print();
                }
            }
        }

        private static Color
            MapRainbowColor(float value, float redValue, float blueValue) // Map a value to a rainbow color.
        {
            var intValue =
                (int) (1023 * (value - redValue) / (blueValue - redValue)); // Convert into a value between 0 and 1023.

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
            var wid = ClientSize.Width;
            var hgt = ClientSize.Height;
            var hgt2 = hgt / 2;
            for (var x = 0; x < wid; x++)
            {
                using (var thePen = new Pen(MapRainbowColor(x, 0, wid)))
                {
                    e.Graphics.DrawLine(thePen, x, 0, x, hgt2);
                }

                using (var thePen = new Pen(MapRainbowColor(x, wid, 0)))
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
                PrinterTasks("/y"); //Set Selected Printer as Default
                using (var document = new PrintDocument
                    {PrinterSettings = {PrinterName = ListOfPrintersListBox.SelectedItem.ToString()}})
                {
                    document.PrintPage += PrintTheRainbowPage;
                    if (numericUpDown1 != null) document.PrinterSettings.Copies = Convert.ToInt16(numericUpDown2.Value);
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
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(
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
                    catch (ManagementException ex)
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
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
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
            catch (ManagementException ex)
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
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendFileToPrinterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterTasks("/y"); //Set Selected Printer as Default
                SendFileToSelectedPrinter();
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendFileToSelectedPrinter()
        {
            using (var printDialog = new PrintDialog
            {
                PrinterSettings = {PrinterName = ListOfPrintersListBox.SelectedItem.ToString()},
                AllowSomePages = true
            })
            {
                var openFileDialog = new OpenFileDialog
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
    }
}