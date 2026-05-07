using System;
using System.Drawing;
using System.Windows.Forms;

internal static class Meter
{
    public static int Offset { get; set; } = 0;
    public static int PixelsPerTick { get; set; } = 10;
    public static int YearsPerTick { get; set; } = 1;
    public static int CenterYear { get; set; } = 0;
    public static int DrawTextGapPixels { get; set; } = 15;

    //Tick
    public static Pen TickPen { get; set; } = new Pen(Color.Black, 1);

    public const int TickBaseY = 20;
    public const int FiveTickY = 30;
    public const int TenTickY = 40;

    //Text
    public static Font TextFont { get; set; } = new Font("Arial", 10);
    public static Brush TextBrush { get; set; } = Brushes.Red;

    //Grid
    public static Pen GridPen { get; set; } = Pens.LightGray;

    public static void DrawScale(Graphics g, Panel pnCanvas)
    {
        int canvasWidth = pnCanvas.ClientSize.Width;
        int canvasHeight = pnCanvas.ClientSize.Height;

        int centerX = canvasWidth / 2;

        int firstVisibleTick = GetFirstVisibleTick(centerX, canvasWidth);
        int lastVisibleTick = GetLastVisibleTick(centerX, canvasWidth);

        for (int tickIndex = firstVisibleTick; tickIndex <= lastVisibleTick; tickIndex++)
        {
            int tickX = centerX + tickIndex * PixelsPerTick + Offset;

            int year = CenterYear + tickIndex * YearsPerTick;


            SizeF textSize = g.MeasureString(year.ToString(), TextFont);
            float textX = tickX - textSize.Width / 2f;
            float textY = (DrawTextGapPixels - 10);

            if (tickIndex % 5 == 0)
            {
                g.DrawLine(TickPen, tickX, TickBaseY, tickX, TenTickY);

                if (tickIndex % 10 == 0)
                {
                    g.DrawString(Math.Abs(year).ToString(), TextFont, TextBrush, textX, textY);
                    g.DrawLine(GridPen, tickX, TenTickY, tickX, canvasHeight);
                }
            }
            else
            {
                g.DrawLine(TickPen, tickX, TickBaseY, tickX, FiveTickY);
            }
        }
    }

    private static int GetFirstVisibleTick(int centerX, int canvasWidth)
    {
        int leftEdgeX = 0;
        int numerator = leftEdgeX - centerX - Offset;

        int firstTick = DivideCeil(numerator, PixelsPerTick);
        return firstTick - 1;
    }

    private static int DivideCeil(int numerator, int denominator)
    {
        int result = numerator / denominator;
        int remainder = numerator % denominator;

        if (remainder != 0 && numerator > 0)
            result++;

        return result;
    }
    private static int GetLastVisibleTick(int centerX, int canvasWidth)
    {
        int rightEdgeX = canvasWidth;
        int numerator = rightEdgeX - centerX - Offset;

        int lastTick = DivideFloor(numerator, PixelsPerTick);
        return lastTick + 1;
    }

    private static int DivideFloor(int numerator, int denominator)
    {
        int result = numerator / denominator;
        int remainder = numerator % denominator;

        if (remainder != 0 && numerator < 0)
            result--;

        return result;
    }

    public static int GetXFromYear(int year)
    {
        return (year * PixelsPerTick * YearsPerTick) - (CenterYear * PixelsPerTick * YearsPerTick);
    }

    public static int GetYearFromX(int mouseX, int canvasWidth)
    {
        int centerX = canvasWidth / 2;
        int tickIndex = (mouseX - centerX - Offset) / PixelsPerTick;

        return CenterYear + tickIndex * YearsPerTick;
    }

    public static void ZoomAtMouse(int mouseX, int canvasWidth, int wheelDelta)
    {
        if (PixelsPerTick <= 0)
        {
            return;
        }

        int nearestYearToCursor = GetYearFromX(mouseX, canvasWidth);

        if (wheelDelta > 0)
        {
            PixelsPerTick++;
        }
        else if (wheelDelta < 0)
        {
            PixelsPerTick--;
        }

        if (PixelsPerTick < 5)
            PixelsPerTick = 5;

        if (PixelsPerTick > 60)
            PixelsPerTick = 60;

        int centerX = canvasWidth / 2;
        int tickIndexAfterZoom = (nearestYearToCursor - CenterYear) / YearsPerTick;

        Offset = mouseX - centerX - tickIndexAfterZoom * PixelsPerTick;
    }
}
