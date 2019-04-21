// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterHelper
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, string> CommandList = new Dictionary<string, string>()
        {
            {"SetPrinterAsDefaultKey", "/y"},
            {"AddNewPrinterKey", "/il"},
            {"DeleteSelectedPrinterKey", "/dl"},
            {"GetPropertiesOfSelectedPrinter", "/p"},
            {"QueueOfSelectedPrinter", "/o"},
            {"SendDefaultTestPage", "/k"},
            {"GetPrintServerProperties", "/s"},
            {"RestartSpooler", "/c net stop spooler&&DEL /F /S /Q %systemroot%\\System32\\spool\\PRINTERS\\*&&net start spooler&&pause"},
            {"StartSpooler", "/c net start spooler&&pause"},
            {"StopSpooler", "/c net stop spooler&&pause"}
        };

        public Form1()
        {
            InitializeComponent();
            Text = $"Printer Helper{Assembly.GetExecutingAssembly().GetName().Version} build at 21/04/2019";
            ListOfPrintersListBox.MouseDown += ListOfPrintersListBoxMouseDown;
            ListOfColorsForPrint.SelectedIndex = 0;
        }

        public string GetSelectedPrinterName()
        {
            return ListOfPrintersListBox.SelectedIndex != -1 ? ListOfPrintersListBox.SelectedItem.ToString() : string.Empty;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData = Keys.None)
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
            if (box == null) throw new ArgumentNullException(nameof(box));

            if (sync == null) throw new ArgumentNullException(nameof(sync));

            const string QueryString = "SELECT * FROM Win32_Printer";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString: QueryString))
            {
                using (ManagementObjectCollection managementObjects = searcher.Get())
                {
                    CancellationToken token = new CancellationToken(false);
                    TaskScheduler scheduler = TaskScheduler.Default;
                    await Task.Factory.StartNew(b =>
                    {
                        if (managementObjects == null) return;
                        foreach (ManagementBaseObject managementBaseObject in managementObjects)
                        {
                            using (ManagementObject managementObject = (ManagementObject)managementBaseObject)
                            {
                                string PrinterNameFromWMI = managementObject["Name"].ToString();
                                if (string.IsNullOrEmpty(PrinterNameFromWMI)) continue;
                                if (managementObject["WorkOffline"].ToString().Equals(value: "false",
                                    comparisonType: StringComparison.OrdinalIgnoreCase)
                                ) //Only Printer with flag "online"
                                {
                                    sync.Send(a => (b as ListBox)?.Items.Add(a), PrinterNameFromWMI);
                                }
                            }
                        }
                    }, box, token, TaskCreationOptions.LongRunning, scheduler).ConfigureAwait(false);
                }
            }
        }

        private static Color
            MapRainbowColor(float value, float redValue, float blueValue)
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

        private static void PrintTheRainbowPage(object sender, PrintPageEventArgs e)
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

        private void AddNewPrinter_Click(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["AddNewPrinterKey"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuOfCommands.Enabled = ListOfPrintersListBox.SelectedIndex != -1;
        }

        private void DeleteThePrinterClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                text: $"Are you sure you want to Delete [{GetSelectedPrinterName()}] ?",
                caption: "Confirmation",
                buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Information);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    try
                    {
                        Cmd SendTask = new Cmd(CommandList["DeleteSelectedPrinterKey"], GetSelectedPrinterName());
                        SendTask.PrinterTasks();
                    }
                    finally
                    {
                        _ = MessageBox.Show(
                            text: $"The [{GetSelectedPrinterName()}] was Deleted",
                            caption: "Information", buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information);
                        FindThePrinterBtnClick(null, null);
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
                await GetPrinterList(SynchronizationContext.Current,
                    ListOfPrintersListBox).ConfigureAwait(true);
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

        private void GetPrintServerProperties(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["GetPrintServerProperties"], GetSelectedPrinterName()); //in Win7 not working, WTF why?!
            SendTask.PrinterTasks();
        }

        private void ListOfPrintersListBoxMouseDown(object sender, MouseEventArgs e)
        {
            ListOfPrintersListBox.SelectedIndex = ListOfPrintersListBox.IndexFromPoint(e.X, e.Y);
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
                Cmd SendTask = new Cmd(CommandList["SetPrinterAsDefaultKey"], GetSelectedPrinterName());
                SendTask.PrinterTasks();

                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = GetSelectedPrinterName() } })
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

        private void PrintTheGridBtnClick(object sender, EventArgs e)
        {
            if (ListOfPrintersListBox.SelectedIndex == -1)
            {
                _ = MessageBox.Show(text: "Please select Printer first", caption: "Error", buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
            }
            else
            {
                Cmd SendTask = new Cmd(CommandList["SetPrinterAsDefaultKey"], GetSelectedPrinterName());
                SendTask.PrinterTasks();

                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = GetSelectedPrinterName() } })
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
                Cmd SendTask = new Cmd(CommandList["SetPrinterAsDefaultKey"], GetSelectedPrinterName());
                SendTask.PrinterTasks();

                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = GetSelectedPrinterName() } })
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
            Cmd SendTask = new Cmd(CommandList["GetPropertiesOfSelectedPrinter"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }

        private void QueueOfPrinter_Click(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["QueueOfSelectedPrinter"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }

        private void RestartPrintSpool_Click(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["RestartSpooler"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }

        private void SendFileToPrinter(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["SetPrinterAsDefaultKey"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
            SendFileToSelectedPrinter();
        }

        private void SendFileToSelectedPrinter()
        {
            const string FilterOfFileTypes =
            "TXT Files(*.txt)|*.txt|Office Files|*.doc;*.docx;*.xlsx;*.xls;*.ppt;*.pptx|PDF Files(*.pdf)|*.pdf|Image Files|*.png;*.jpg;*.tiff;*.gif|All Files(*.*)|*.*";

            using (PrintDialog printDialog = new PrintDialog
            {
                PrinterSettings = { PrinterName = GetSelectedPrinterName() },
                AllowSomePages = true
            })
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter =
                        FilterOfFileTypes
                })
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                    if (printDialog.ShowDialog() != DialogResult.OK) return;

                    try
                    {
                        _ = Process.Start(new ProcessStartInfo(fileName: openFileDialog.FileName)
                        {
                            Verb = "Print",
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                    }
                    catch (FileNotFoundException exfilenotfound)
                    {
                        _ = MessageBox.Show(text: exfilenotfound.Message, caption: "Error", buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Error);
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
            Cmd SendTask = new Cmd(CommandList["SendDefaultTestPage"], GetSelectedPrinterName());
            SendTask.PrinterTasks(); // Send Default Windows Test page
        }

        private void StartPrintSpool_Click(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["StartSpooler"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }

        private void StopPrintSpool_Click(object sender, EventArgs e)
        {
            Cmd SendTask = new Cmd(CommandList["StopSpooler"], GetSelectedPrinterName());
            SendTask.PrinterTasks();
        }
    }
}