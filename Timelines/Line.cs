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

        public int LineY = Meter.TICKBASEY + 50;
        public static int IncresingLineY = 0;
        public Line(int fromYear, int toYear, Direction lineDirectionFrom, Direction lineDirectionTo, Color lineColor) 
        { 
            FromYear = fromYear;
            ToYear = toYear;

            if(FromYear == ToYear)
            {
                MessageBox.Show("Nastavte délku osy!");
                return;
            }

            LineDirectionFrom = lineDirectionFrom;
            LineDirectionTo = lineDirectionTo;

            LineColor = lineColor;
            LinePen = new Pen(LineColor, LINETHICKNESS);
            LineY += IncresingLineY;
            IncresingLineY += 25;
        }

        public void DrawLine(Graphics g, Panel pnCanvas)
        {
            int centerX = pnCanvas.ClientSize.Width / 2;

            int x1 = 0;
            int x2 = 0;

            if (LineDirectionFrom == Direction.Left && LineDirectionTo == Direction.Left ||
                LineDirectionFrom == Direction.Right && LineDirectionTo == Direction.Right)
            {
                x1 = ((int)LineDirectionFrom * Meter.GetXFromYear(FromYear)) + Meter.Offset + centerX - Meter.CenterYear;
                x2 = ((int)LineDirectionTo * Meter.GetXFromYear(ToYear)) + Meter.Offset + centerX - Meter.CenterYear;
            }
            else if (LineDirectionFrom == Direction.Left && LineDirectionTo == Direction.Right)
            {
                x1 = ((int)LineDirectionFrom * Meter.GetXFromYear(FromYear)) + Meter.Offset + centerX - Meter.CenterYear;
                x2 = ((int)LineDirectionTo * (-Meter.GetXFromYear(ToYear))) + Meter.Offset + centerX - Meter.CenterYear;
            }
            else
            {
                return;
            }

            P1 = new Point(x1, LineY);
            P2 = new Point(x2, LineY);

            g.DrawLine(LinePen,P1,P2);

            //hrany
            g.DrawLine(LinePen,new Point(P1.X,LineY + 10), new Point(P1.X, LineY - 10)); 
            g.DrawLine(LinePen,new Point(P2.X,LineY + 10), new Point(P2.X, LineY - 10));
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
            {
                return false;
            }

            if (LineDirectionFrom == Direction.Left && LineDirectionTo == Direction.Right)
            {
                for (int i = P1.X; i < (P2.X); i++)
                {
                    for (int j = P1.Y - 7; j < P1.Y + 7; j++)
                    {
                        Point currentPointOnLine = new Point(i, j);
                        if (currentPointOnLine.X * (int)LineDirectionFrom == (mousePosition.X * (int)LineDirectionFrom) && currentPointOnLine.Y == mousePosition.Y)
                        {
                            IncreaseThickness();
                            return true;
                        }
                    }
                }
            }
            else
            {
                for (int i = P1.X * (int)LineDirectionFrom; i < (P2.X * (int)LineDirectionTo); i++)
                {
                    for (int j = P1.Y - 7; j < P1.Y + 7; j++)
                    {
                        Point currentPointOnLine = new Point(i, j);
                        if (currentPointOnLine.X == (mousePosition.X * (int)LineDirectionFrom) && currentPointOnLine.Y == mousePosition.Y)
                        {
                            IncreaseThickness();
                            return true;
                        }
                    }
                }
            }
            
            if (!IsClicked)
                ReduceThickness();

            return false;
        }

        
    }
}
