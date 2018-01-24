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

        private async Task GetPrinterList(SynchronizationContext _sync, ListBox box)
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");//Entry point
            scope.Connect();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_Printer");//Query from WIN32_Printer namespace
            string printerName = "";
            await Task.Factory.StartNew((b) =>
            {
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToLower();//get PrinterName
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

        private async void button1_Click(object sender, EventArgs e)
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
            finally
            {
                //Enable all buttons
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){}

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first"); }
            else
            {
            //Call printer settings via CMD
            string SelectedPrinter = listBox1.SelectedItem.ToString();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;//CMD is hidden
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C rundll32 printui.dll,PrintUIEntry /p /n \"" + SelectedPrinter + "\"";//CMD command
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first"); }
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
                    doc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    doc.Print();
                }
                else { MessageBox.Show("Print Aborted"); }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Draw a grid
            int w = 1654;//A4 size
            int h = 2339;//A4 size
            int widthLines = 20;//cell size
            int heightLines = 20;//cell size
            for (int i = 0; i < w; i += widthLines)//fill all list A4
            {
                //Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0), new Point(i + widthLines, h));
                //Lines
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines), new Point(w, i + heightLines));
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first"); }
            else
            {
                button3.Enabled = false;//disable button, clear listbox2
                listBox2.Items.Clear();
                try
                {
                    await GetPrinterProperty(SynchronizationContext.Current, listBox2);//call GetPrinterProperty async method
                }
                finally
                {
                    button3.Enabled = true;
                }

            }

        }

        private async Task GetPrinterProperty(SynchronizationContext _sync, ListBox box)
        {
            string SelectedPrinter = listBox1.SelectedItem.ToString();
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", SelectedPrinter);//Entry point
            await Task.Factory.StartNew((b) =>
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection coll = searcher.Get())
                {
                    try
                    {
                        foreach (ManagementObject printer in coll)
                        {
                            foreach (PropertyData property in printer.Properties)
                            {
                                string PrinterPropertyData = property.Name + ":" + property.Value; //give all prop. in one string
                                if(property.Value != null)//add only not null Value
                                {
                                    _sync.Send((pn) =>
                                    {
                                        (b as ListBox).Items.Add(pn);//send all prop. in listbox2 with async method
                                    }, PrinterPropertyData);
                                }
                            }
                        }
                    }
                    catch (ManagementException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }, box);

        }

    }
}
