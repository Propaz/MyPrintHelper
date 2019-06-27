// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PrinterHelper
{
    internal partial class MainForm
    {
        private class PrintTestPage
        {
            private readonly int _copiesOfTestPage;
            private readonly string _selectedPrinter;
            private readonly string _singleColorToPrint;
            private readonly string _testPageName;

            public PrintTestPage(string nameOfPrinter, string nameoftestpage, string colortoprint, int copies)
            {
                _selectedPrinter = nameOfPrinter ?? throw new ArgumentNullException(nameof(nameOfPrinter));
                _testPageName = nameoftestpage ?? throw new ArgumentNullException(nameof(nameoftestpage));
                _copiesOfTestPage = copies;
                _singleColorToPrint = colortoprint ?? throw new ArgumentNullException(nameof(colortoprint));
            }

            public PrintTestPage(string nameOfPrinter, string nameoftestpage, int copies)
            {
                _selectedPrinter = nameOfPrinter ?? throw new ArgumentNullException(nameof(nameOfPrinter));
                _testPageName = nameoftestpage ?? throw new ArgumentNullException(nameof(nameoftestpage));
                _copiesOfTestPage = copies;
                _singleColorToPrint = string.Empty;
            }

            public void SendDocumentToPrinter()
            {
                using (PrintDocument document = new PrintDocument
                { PrinterSettings = { PrinterName = _selectedPrinter } })
                {
                    switch (_testPageName)
                    {
                        case ("BWGridTestPage"):
                            document.PrintPage += PrintTheGridDocument;
                            break;

                        case ("RainbowTestPage"):
                            document.PrintPage += PrintTheRainbowPage;
                            break;

                        case ("SingleColorTestPage"):
                            document.PrintPage += PrintTheSingleColor;
                            break;
                    }

                    document.PrinterSettings.Copies = Convert.ToInt16(_copiesOfTestPage);

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

            private static void PrintTheGridDocument(object sender, PrintPageEventArgs e)
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

            private static void PrintTheRainbowPage(object sender, PrintPageEventArgs e)
            {
                const int wid = 600;
                const int hgt = 600;
                const int hgt2 = hgt / 2;
                for (int x = 20; x < wid; x++)
                {
                    Pen thePen;
                    using (thePen = new Pen(MapRainbowColor(x, 0, wid)))
                    {
                        e.Graphics.DrawLine(thePen, x, 20, x, hgt2);
                    }

                    using (thePen = new Pen(MapRainbowColor(x, wid, 0)))
                    {
                        e.Graphics.DrawLine(thePen, x, hgt2, x, hgt);
                    }
                }
            }

            private void PrintTheSingleColor(object sender, PrintPageEventArgs e)
            {
                switch (_singleColorToPrint)
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
        }
    }
}