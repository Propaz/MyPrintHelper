using System;
using System.Windows.Forms;
using System.Management;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async Task GetPrinterList(SynchronizationContext _sync, ListBox box)//Find all online Printers
        {
            string query = string.Format("SELECT * FROM Win32_Printer");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))//Query from WIN32_Printer namespace
            using (ManagementObjectCollection PrinterList = searcher.Get())
            {
                await Task.Factory.StartNew((b) =>
                {
                    foreach (ManagementObject printer in PrinterList)
                    {
                        string printerName = printer["Name"].ToString().ToLower();//get PrinterName
                        if (printer["WorkOffline"].ToString().ToLower().Equals("false") && printerName.Contains("fax") == false && printerName.Contains("xps") == false)//Only online Printer, not fax and XPS devices
                        {
                            _sync.Send((a) =>
                            {
                                (b as ListBox).Items.Add(a);//add all printers in listbox1 via async method
                            }, printerName);
                        }
                    }
                }, box);

            }
   
        }

        private async void Button1_Click(object sender, EventArgs e)//Find all printers
        {
            //disable all buttons while GetPrinterList is working
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current, listBox1);//Call GetPrinterList via async method
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Enable all buttons
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e){}//list of all online printers

        private void Button2_Click(object sender, EventArgs e)//Show System "Printer Settings"
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first", "Error"); }
            else
            {
            button1.Enabled = false;//Diasble all buttons untill Settings Window is active
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            string SelectedPrinter = listBox1.SelectedItem.ToString();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//process is hidden
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C rundll32 printui.dll,PrintUIEntry /p /n \"" + SelectedPrinter + "\"";//Call Printer settings via cmd.exe
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            button1.Enabled = true;//Enable all buttons When Settings Window is closed
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)//Print the Grid
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first", "Error"); }
            else
            {
                string SelectedPrinter = listBox1.SelectedItem.ToString();
                var pd = new PrintDialog();
                var settings = pd.PrinterSettings;
                var name = settings.PrinterName;
                pd.PrinterSettings.PrinterName = SelectedPrinter;
                pd.AllowSomePages = true;
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    PrintDocument doc = new PrintDocument();
                    doc.PrinterSettings.PrinterName = SelectedPrinter;
                    doc.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage);
                    doc.Print();
                }
                else { MessageBox.Show("Print Aborted", "Warning!"); }
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Draw a grid
            int w = 1654;//A4 size
            int h = 2339;//A4 size
            int widthLines = 20;//cell size
            int heightLines = 20;//cell size
            for (int i = 0; i < w; i += widthLines)//fill all list A4
            {
                //Width Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0), new Point(i + widthLines, h));
                //Height Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines), new Point(w, i + heightLines));
            }
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e){}//List of Printer Properties

        private async void Button3_Click(object sender, EventArgs e)//Get Properties of Selected Printer
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first", "Error"); }
            else
            {
                button3.Enabled = false;//disable button, clear listbox2
                listBox2.Items.Clear();
                try
                {
                    await GetPrinterProperty(SynchronizationContext.Current, listBox2);//call GetPrinterProperty async method
                }
                catch (ManagementException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    button3.Enabled = true;
                }

            }

        }

        private async Task GetPrinterProperty(SynchronizationContext _sync, ListBox box)//Get printer properties
        {
            string SelectedPrinter = listBox1.SelectedItem.ToString();
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", SelectedPrinter);//Entry point
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection coll = searcher.Get())
            {
                await Task.Factory.StartNew((b) =>
                {
                    foreach (ManagementObject printer in coll)
                    {
                        foreach (PropertyData property in printer.Properties)
                        {
                            string PrinterPropertyData = property.Name + ":" + property.Value; //give all prop. in one string
                            if (property.Value != null)//add only not null Value
                            {
                                _sync.Send((pn) =>
                                    {
                                        (b as ListBox).Items.Add(pn);//send all prop. in listbox2 with async method
                                    }, PrinterPropertyData);
                            }
                        }
                    }
          
                }, box);
            }

        }
                

    }

}

