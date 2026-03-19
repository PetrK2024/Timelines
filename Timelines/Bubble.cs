using System.Drawing;
using System.Windows.Forms;

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
        public int Year { get; set; }
        public Line OwningLine { get; set; }
        public bool SizeIsChanging { get; set; }

        public const int Width = 10;
        public const int IncreaseWidth = 6;

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

            if(OwningLine.FromYear * (int)OwningLine.LineDirectionFrom <= Year * (int)BubbleDirection && 
                OwningLine.ToYear * (int)OwningLine.LineDirectionTo >= Year * (int)BubbleDirection)
            {
                if (Year * (int)BubbleDirection <= 0 && !SizeIsChanging)
                {
                    X = OwningLine.P1.X - (Width / 2) + (Year * Meter.PixelsPerTick * (int)BubbleDirection) - (OwningLine.FromYear * Meter.PixelsPerTick * (int)BubbleDirection);
                }
                else if (Year * (int)BubbleDirection >= 0 && !SizeIsChanging)
                {
                    X = OwningLine.P1.X - (Width / 2) + (Year * Meter.PixelsPerTick * (int)BubbleDirection) - (OwningLine.FromYear * Meter.PixelsPerTick);
                }


                Rectangle rec = new Rectangle(new Point(X,Y), BubbleSize);

                g.FillEllipse(BubbleBrush, rec);
            }
           

          
        }

        public void IncreaseSize()
        {
            SizeIsChanging = true;
            BubbleSize = new Size(Width + IncreaseWidth, Width + IncreaseWidth);
            X -= ((Width + IncreaseWidth) / 4);

        }

        public void ReduceSize()
        {
            BubbleSize = new Size(Width, Width);
           // X = (BubbleSize.Width / 4);
            SizeIsChanging = false;
        }

        public bool IsBubbleFocused(Point mousePosition)
        {
            for (int i = (X * (int)BubbleDirection) - Width; i < (Width + X * (int)BubbleDirection); i++)
            {
                for (int j = Y - Width; j < Y + Width; j++)
                {
                    Point currentPointOnBubble = new Point(i, j);
                    if (mousePosition.Equals(currentPointOnBubble))
                    {
                        IncreaseSize();
                        return true;
                    }
                }
            }

           ReduceSize();
           return false;
        }
    }
}
