// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Management;
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

            ListOfAdditionalPrinterProperties.View = View.Details;
            ListOfAdditionalPrinterProperties.GridLines = true;
            ListOfAdditionalPrinterProperties.FullRowSelect = true;

            ListOfAdditionalPrinterProperties.Columns.Add("Property", 250);
            ListOfAdditionalPrinterProperties.Columns.Add("Value", 240);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            GetAdditionalPrinterPropertiesClick(null, null); //Calling a GetPrinterProperty when a form2 is loaded
        }

        private void GetPrinterProperty() //Get printer Additional property
        {
            using (var searcher =
                new ManagementObjectSearcher("SELECT * from Win32_Printer WHERE Name LIKE \'" +
                                             _fm1.ListOfPrintersListBox.SelectedItem +
                                             "\'"))
            using (var coll = searcher.Get())
            {
                foreach (var o in coll)
                    using (var printer = (ManagementObject) o)
                    {
                        foreach (var property in printer.Properties)
                        {
                            var arr = new string[2];
                            if (property != null)
                            {
                                arr[0] = property.Name;
                                if (property.Value != null) arr[1] = property.Value.ToString();
                            }

                            var itm = new ListViewItem(arr);
                            ListOfAdditionalPrinterProperties?.Items.Add(itm);
                        }
                    }
            }
        }

        private void GetAdditionalPrinterPropertiesClick(object sender, EventArgs e) //get Additional property
        {
            Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            ListOfAdditionalPrinterProperties.Items.Clear();
            try
            {
                GetPrinterProperty();
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

        private void ListOfAdditionalPropertiesOfPrinterChanged(object sender, EventArgs e)
        {
        }
    }
}