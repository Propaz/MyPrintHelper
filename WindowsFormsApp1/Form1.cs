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
            listBox1.MouseDown += ListBox1_MouseDown;
        }

        private void ListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y); //Right Click to select items in a ListBox
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box) //Find all online Printers
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer")
            ) //Query from WIN32_Printer namespace
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
                                sync.Send(a =>
                                {
                                    (b as ListBox)?.Items.Add(a); //add all printers in listbox1 via async method
                                }, printerName);
                        }
                }, box);
            }
        }

        private async void FindprinterBtnClick(object sender, EventArgs e) //Find all printers
        {
            findprinter_btn.Enabled = false; //disable buttons while GetPrinterList is working
            print_grid_btn.Enabled = false;
            Cursor = Cursors.WaitCursor; //Show Waiting Cursor while working
            listBox1.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current, listBox1); //Call GetPrinterList via async method
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default; //Turn on Default Cursor, and enable buttons
                findprinter_btn.Enabled = true;
                print_grid_btn.Enabled = true;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //list of all online printers
        }

        private void PrinterTasks(string key)
        {
            using (var process = new Process())
            {
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden, //process is hidden
                    FileName = "cmd.exe",
                    Arguments =
                        "/C rundll32 printui.dll,PrintUIEntry " + key + " /n \"" + listBox1.SelectedItem +
                        "\"" //Send task to printer 
                };
                if (key != null) process.StartInfo = startInfo;

                process.Start();
            }
        }

        private void PrintTheGridBtnClick(object sender, EventArgs e) //Print the Grid
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select Printer first", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                using (var pd = new PrintDialog
                {
                    PrinterSettings = {PrinterName = listBox1.SelectedItem.ToString()},
                    AllowSomePages = true
                })
                {
                    if (pd.ShowDialog() != DialogResult.OK) return;
                }

                using (var doc = new PrintDocument {PrinterSettings = {PrinterName = listBox1.SelectedItem.ToString()}})
                {
                    doc.PrintPage += PrintDocument1_PrintPage;
                    doc.Print();
                }
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
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

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Enabled = listBox1.SelectedIndex != -1;
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

        private void Deleteprinter_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(@"Are you sure you want to Delete selected Printer?", @"Confirmation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            switch (res)
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
                        MessageBox.Show(@"Printer was Deleted", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        FindprinterBtnClick(null, null); //Renew results
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
                PrinterTasks("/k"); // Send Standart Windows Test page
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdditionalPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fm2 = new Form2(this) {Text = @"Properties of [" + listBox1.SelectedItem + "]"})
            {
                fm2.ShowDialog(); //Show Printer Additional properties in new Form
            }
        }
    }
}