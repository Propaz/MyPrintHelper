using System;
using System.Windows.Forms;
using System.Management;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            
            ManagementScope scope = new ManagementScope(@"\root\cimv2"); //Определяем точку входа
            scope.Connect();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            string printerName = "";
            await Task.Factory.StartNew((b) =>
            {
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToLower();
                    if (printer["WorkOffline"].ToString().ToLower().Equals("false"))//Если МФУ Offline не добавляем
                    {
                        _sync.Send((a) =>
                        {
                            (b as ListBox).Items.Add(a);
                        }, printerName);
                    }
                }
            }, box);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            listBox1.Items.Clear();
            try
            {
                await GetPrinterList(SynchronizationContext.Current, listBox1);
            }
            finally
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e){}

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select Printer first"); }
            else
            {
            //Запускаем скрытый cmd с параметрами для вызова свойств указанного МФУ.
            string SelectedPrinter = listBox1.SelectedItem.ToString();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C rundll32 printui.dll,PrintUIEntry /p /n \"" + SelectedPrinter + "\"";
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
            Font myFont = new Font("m_svoboda", 14, FontStyle.Bold, GraphicsUnit.Point);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // Draw a rectangle around the page margin.
            Pen blackPen = new Pen(Color.Black, 5);
            e.Graphics.DrawRectangle(blackPen, e.MarginBounds);
            // Draw an ellipse inside the page margin.
            e.Graphics.DrawEllipse(blackPen, e.MarginBounds);
            SolidBrush blueBrush = new SolidBrush(Color.Gray);
            e.Graphics.FillEllipse(blueBrush, e.MarginBounds);
            // Draw text around printed figures.
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 50));
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 250));
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 450));
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 650));
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 850));
            e.Graphics.DrawString("L_TEST", myFont, Brushes.Black, new PointF(5, 1050));
            e.Graphics.DrawString("UP_TEST", myFont, Brushes.Black, new PointF(100, 50));
            e.Graphics.DrawString("UP_TEST", myFont, Brushes.Black, new PointF(250, 50));
            e.Graphics.DrawString("UP_TEST", myFont, Brushes.Black, new PointF(400, 50));
            e.Graphics.DrawString("UP_TEST", myFont, Brushes.Black, new PointF(550, 50));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 50));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 250));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 450));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 650));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 850));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 1050));
            e.Graphics.DrawString("R_TEST", myFont, Brushes.Black, new PointF(735, 1250));
            e.HasMorePages = false;
        }

    }
}
