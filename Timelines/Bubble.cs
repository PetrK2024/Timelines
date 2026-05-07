using System.Drawing;

namespace Timelines
{
    internal class Bubble
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color BubbleColor { get; set; }
        public Brush BubbleBrush { get; set; }
        public Direction BubbleDirection { get; set; }
        public Size BubbleSize { get; set; }
        public bool IsClicked { get; set; } = false;
        public Line OwningLine { get; set; }
        public bool SizeIsChanging { get; set; }

        public const int Width = 10;
        public const int IncreaseWidth = 6;

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Bubble(Line owningLine, int year, Direction direction, Color bubbleColor)
        {
            OwningLine = owningLine;
            Year = year;
            BubbleDirection = direction;
            BubbleColor = bubbleColor;
            BubbleBrush = new SolidBrush(BubbleColor);
            BubbleSize = new Size(Width, Width);
        }

        public void DrawBubble(Graphics g)
        {
            Y = OwningLine.LineY - (Width / 2);

            if (OwningLine.FromYear <= Year && OwningLine.ToYear >= Year)
            {
                if (Year * (int)BubbleDirection <= 0 && !SizeIsChanging)
                {
                    X = OwningLine.P1.X - (Width / 2) + (Year * Meter.PixelsPerTick * (int)BubbleDirection) - (OwningLine.FromYear * Meter.PixelsPerTick * (int)BubbleDirection);
                }
                else if (Year * (int)BubbleDirection >= 0 && !SizeIsChanging)
                {
                    X = OwningLine.P1.X - (Width / 2) + (Year * Meter.PixelsPerTick * (int)BubbleDirection) - (OwningLine.FromYear * Meter.PixelsPerTick);
                }

                Rectangle rec = new Rectangle(new Point(X, Y), BubbleSize);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(BubbleBrush, rec);
            }



        }

        public bool IsBubbleFocused(Point mousePosition)
        {
            Rectangle bubble = new Rectangle(new Point(X, Y), BubbleSize);
            Rectangle mouse = new Rectangle(new Point(mousePosition.X - (BubbleSize.Width / 2), mousePosition.Y - 5), BubbleSize);

            if (bubble.IntersectsWith(mouse))
            {
                return true;
            }

            //Cursor.Current = Cursors.Default;
            return false;
        }

        public void SetColor(Color color)
        {
            BubbleColor = color;

            if (BubbleBrush != null)
            {
                BubbleBrush.Dispose();
            }

            BubbleBrush = new SolidBrush(BubbleColor);
        }
    }

}
