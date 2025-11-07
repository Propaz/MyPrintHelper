using System;
using System.Drawing;
using System.Drawing.Printing;
using PrinterHelper.Helpers;
using PrinterHelper.Models;

namespace PrinterHelper
{
    internal partial class MainForm
    {
        private class PrintTestPage
        {
            private readonly int _copiesOfTestPage;
            private readonly string _selectedPrinter;
            private readonly string _singleColorToPrint;
            private readonly PageType _testPageName;

            // A4 size
            private const int HeightA4 = 2339;
            private const int WidthA4 = 1654;
            // A4 size

            public PrintTestPage(string nameOfPrinter, PageType nameOfTestPage, string colorToPrint, int copies)
            {
                _selectedPrinter = nameOfPrinter ?? throw new ArgumentNullException(nameof(nameOfPrinter));
                _testPageName = nameOfTestPage;
                _copiesOfTestPage = copies;
                _singleColorToPrint = colorToPrint ?? throw new ArgumentNullException(nameof(colorToPrint));
            }

            public PrintTestPage(string nameOfPrinter, PageType nameOfTestPage, int copies)
            {
                _selectedPrinter = nameOfPrinter ?? throw new ArgumentNullException(nameof(nameOfPrinter));
                _testPageName = nameOfTestPage;
                _copiesOfTestPage = copies;
                _singleColorToPrint = string.Empty;
            }

            public void SendDocumentToPrinter()
            {
                using var document = new PrintDocument();
                document.PrinterSettings.PrinterName = _selectedPrinter;
                document.PrintPage += _testPageName switch
                {
                    PageType.Grid => PrintTheGridDocument,
                    PageType.Rainbow => PrintTheRainbowPage,
                    PageType.SingleColor => PrintTheSingleColor,
                    PageType.HorizonLines => PrintHorizonLines,
                    PageType.VerticalLines => PrintVerticalLines,
                    _ => PrintTheGridDocument
                };

                document.PrinterSettings.Copies = Convert.ToInt16(_copiesOfTestPage);

                try
                {
                    document.Print();
                }
                catch (InvalidPrinterException exc)
                {
                    UIHelper.ShowError(exc.Message);
                }
            }

            private static Color
                MapRainbowColor(float value, float redValue, float blueValue)
            {
                var intValue =
                    (int)(1023 * (value - redValue) /
                          (blueValue - redValue)); // Convert into a value between 0 and 1023.

                switch (intValue)
                {
                    case < 256:
                        return Color.FromArgb(255, intValue, 0); // Map different color bands.
                    case < 512:
                        // Yellow to green. (255, 255, 0) to (0, 255, 0).
                        intValue -= 256;
                        return Color.FromArgb(255 - intValue, 255, 0);
                    case < 768:
                        // Green to aqua. (0, 255, 0) to (0, 255, 255).
                        intValue -= 512;
                        return Color.FromArgb(0, 255, intValue);
                    default:
                        // Aqua to blue. (0, 255, 255) to (0, 0, 255).
                        intValue -= 768;
                        return Color.FromArgb(0, 255 - intValue, 255);
                }
            }

            private static void PrintTheGridDocument(object sender, PrintPageEventArgs e)
            {
                const int widthLines = 20; //cell size
                const int heightLines = 20;
                for (var i = 0; i < WidthA4; i += widthLines)
                {
                    //Width Lines
                    e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0),
                        new Point(i + widthLines, HeightA4));
                    //Height Lines
                    e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines),
                        new Point(WidthA4, i + heightLines));
                }
            }

            private static void PrintHorizonLines(object sender, PrintPageEventArgs e)
            {
                const int heightLines = 320;
                for (var i = 0; i < WidthA4; i += heightLines)
                {
                    e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(0, i + heightLines),
                        new Point(WidthA4, i + heightLines));
                }
            }

            private static void PrintVerticalLines(object sender, PrintPageEventArgs e)
            {
                const int widthLines = 220;
                for (var i = 0; i < WidthA4; i += widthLines)
                {
                    e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(i + widthLines, 0),
                        new Point(i + widthLines, HeightA4));
                }
            }

            private static void PrintTheRainbowPage(object sender, PrintPageEventArgs e)
            {
                const int wid = 600;
                const int hgt = 600;
                const int hgt2 = hgt / 2;
                for (var x = 20; x < wid; x++)
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
                if (string.IsNullOrWhiteSpace(_singleColorToPrint))
                {
                    return;
                }

                if (_singleColorToPrint.Equals("White", StringComparison.OrdinalIgnoreCase))
                {
                    e.Graphics.FillRectangle(Brushes.White, 1, 1, 1, 1);
                    return;
                }

                var color = (Color)new ColorConverter().ConvertFromString(_singleColorToPrint)!;
                using var brush = new SolidBrush(color);
                e.Graphics.FillRectangle(brush, 50, 50, 720, 1000);
            }
        }
    }
}