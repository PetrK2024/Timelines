using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timelines
{
    internal class Line
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public Color LineColor { get; set; }
        public Pen LinePen { get; set; }
        public const float LINETHICKNESS = 2;
        public const float INCREASELINETHICKNESS = 3;
        public bool IsClicked { get; set; } = false;

        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public Direction LineDirectionFrom { get; set; }
        public Direction LineDirectionTo { get; set; }

        public int LineY = Meter.TickBaseY + 50;
        public static int IncresingLineY = 0;
        public List<Bubble> OwningBubbles { get; set; }
        public string Name { get; set; }
        public Line(int fromYear, int toYear, Direction lineDirectionFrom, Direction lineDirectionTo, Color lineColor)
        {
            FromYear = fromYear;
            ToYear = toYear;

            if (FromYear == ToYear)
            {
                MessageBox.Show("Nastavte délku osy!");
                return;
            }

            LineDirectionFrom = lineDirectionFrom;
            LineDirectionTo = lineDirectionTo;

            LineColor = lineColor;
            LinePen = new Pen(LineColor, LINETHICKNESS);
            OwningBubbles = new List<Bubble>();
            LineY += IncresingLineY;
            IncresingLineY += 25;
        }

        public void DrawLine(Graphics g, Panel pnCanvas)
        {
            int x1 = GetScreenX(FromYear, LineDirectionFrom, pnCanvas);
            int x2 = GetScreenX(ToYear, LineDirectionTo, pnCanvas);

            P1 = new Point(x1, LineY);
            P2 = new Point(x2, LineY);

            g.DrawLine(LinePen, P1, P2);

            g.DrawLine(LinePen, new Point(P1.X, LineY + 10), new Point(P1.X, LineY - 10));
            g.DrawLine(LinePen, new Point(P2.X, LineY + 10), new Point(P2.X, LineY - 10));
        }

        private int GetScreenX(int year, Direction direction, Panel pnCanvas)
        {
            int centerX = pnCanvas.ClientSize.Width / 2;

            int signedYear = direction == Direction.Left ? -Math.Abs(year) : Math.Abs(year);

            return centerX + Meter.Offset + Meter.GetXFromYear(signedYear);
        }

        public void IncreaseThickness()
        {
            LinePen = new Pen(LineColor, LINETHICKNESS + INCREASELINETHICKNESS);
        }

        public void ReduceThickness()
        {
            LinePen = new Pen(LineColor, LINETHICKNESS);
        }

        public bool IsLineFocused(Point mousePosition, bool isBubbleFocused)
        {
            if (isBubbleFocused)
                return false;

            int minX = Math.Min(P1.X, P2.X);
            int maxX = Math.Max(P1.X, P2.X);

            bool insideX = mousePosition.X >= minX && mousePosition.X <= maxX;
            bool insideY = mousePosition.Y >= LineY - 7 && mousePosition.Y <= LineY + 7;

            if (insideX && insideY)
            {
                IncreaseThickness();
                return true;
            }

            if (!IsClicked)
                ReduceThickness();

            return false;
        }


    }
}
