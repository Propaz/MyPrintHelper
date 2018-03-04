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
        }

        private static async Task
            GetPrinterList(SynchronizationContext sync, IDisposable box) //Find all online Printers
        {
            const string query = "SELECT * FROM Win32_Printer";
            using (var searcher = new ManagementObjectSearcher(query)) //Query from WIN32_Printer namespace
            using (var printerList = searcher.Get())
            {
                await Task.Factory.StartNew(b =>
                {
                    if (printerList == null) return;
                    foreach (var o in printerList)
                    {
                        var printer = (ManagementObject) o;
                        var printerName = printer["Name"].ToString().ToLower(); //get PrinterName
                        if (printer["WorkOffline"].ToString().ToLower().Equals("false") &&
                            printerName.Contains("fax") == false &&
                            printerName.Contains("xps") == false) //Only online Printer, not fax and XPS devices
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
            get_properties_btn.Enabled = false;
            print_grid_btn.Enabled = false;
            Cursor = Cursors.WaitCursor; //Show Waiting Cursor while working
            listBox1.Items.Clear();
            listBox2.Items.Clear(); //clear all listboxes
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
                get_properties_btn.Enabled = true;
                print_grid_btn.Enabled = true;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //list of all online printers
        }

        private void PrinterTasks(string key)
        {
            var selectedPrinter = listBox1.SelectedItem.ToString();
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden, //process is hidden
                FileName = "cmd.exe",
                Arguments =
                    "/C rundll32 printui.dll,PrintUIEntry " + key + " /n \"" + selectedPrinter +
                    "\"" //Send task to printer 
            };
            process.StartInfo = startInfo;
            process.Start();
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
                var selectedPrinter = listBox1.SelectedItem.ToString();
                var pd = new PrintDialog
                {
                    PrinterSettings = {PrinterName = selectedPrinter},
                    AllowSomePages = true
                };
                if (pd.ShowDialog() != DialogResult.OK) return;
                var doc = new PrintDocument {PrinterSettings = {PrinterName = selectedPrinter}};
                doc.PrintPage += PrintDocument1_PrintPage;
                doc.Print();
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

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List of Printer Properties
        }

        private async void GetPropertiesBtnClick(object sender, EventArgs e) //Get Properties of Selected Printer
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select Printer first", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                findprinter_btn.Enabled = false;
                get_properties_btn.Enabled = false;
                print_grid_btn.Enabled = false;
                Cursor = Cursors.WaitCursor; //disable buttons, clear listbox2, show waiting cursor
                listBox2.Items.Clear();
                try
                {
                    await GetPrinterProperty(SynchronizationContext.Current, listBox2); //Get printer property
                }
                catch (ManagementException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Default; //Turn buttons, back default cursor
                    findprinter_btn.Enabled = true;
                    get_properties_btn.Enabled = true;
                    print_grid_btn.Enabled = true;
                }
            }
        }

        private async Task GetPrinterProperty(SynchronizationContext sync, IDisposable box) //Get printer properties
        {
            var selectedPrinter = listBox1.SelectedItem.ToString();
            var query = "SELECT * from Win32_Printer WHERE Name LIKE \'" + selectedPrinter + "\'";
            using (var searcher = new ManagementObjectSearcher(query))
            using (var coll = searcher.Get())
            {
                await Task.Factory.StartNew(b =>
                {
                    if (coll == null) return;
                    foreach (var o in coll)
                    {
                        var printer = (ManagementObject) o;
                        foreach (var property in printer.Properties)
                        {
                            var printerPropertyData =
                                property.Name + ":" + property.Value; //give all prop. in one string
                            if (property.Value != null) //add only not null Value
                                sync.Send(pn =>
                                {
                                    (b as ListBox)?.Items.Add(pn); //send all prop. in listbox2 with async method
                                }, printerPropertyData);
                        }
                    }
                }, box);
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
    }
}

