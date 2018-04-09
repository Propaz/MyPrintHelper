// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterParser
{
    public partial class Form2 : Form
    {
        private readonly Form1 _fm1;

        public Form2(Form1 fm1)
        {
            _fm1 = fm1;
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Button1_Click(null, null); //Calling a GetPrinterProperty when a form2 is loaded
        }

        private async Task
            GetPrinterProperty(SynchronizationContext sync, IDisposable box) //Get printer Additional property
        {
            using (var searcher =
                new ManagementObjectSearcher("SELECT * from Win32_Printer WHERE Name LIKE \'" +
                                             _fm1.listBox1.SelectedItem +
                                             "\'"))
            using (var coll = searcher.Get())
            {
                await Task.Factory.StartNew(b =>
                {
                    if (coll == null) return;
                    foreach (var o in coll)
                        using (var printer = (ManagementObject) o)
                        {
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

        private async void Button1_Click(object sender, EventArgs e) //get Additional property
        {
            Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            PrinterAdditionalProperty.Items.Clear();
            try
            {
                await GetPrinterProperty(SynchronizationContext.Current,
                    PrinterAdditionalProperty); //Get Printer Additional property
            }
            catch (ManagementException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                button1.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void PrinterAdvaicedProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List of Printer Additional properties
        }
    }
}