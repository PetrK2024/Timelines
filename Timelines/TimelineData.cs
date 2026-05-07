using System.Collections.Generic;

namespace Timelines
{
    internal class TimelineProject
    {
        public string ProjectName { get; set; }
        public List<LineData> Lines { get; set; } = new List<LineData>();
    }

    internal class LineData
    {
        public string Name { get; set; }

        public int FromYear { get; set; }
        public int ToYear { get; set; }

        public Direction LineDirectionFrom { get; set; }
        public Direction LineDirectionTo { get; set; }

        public int LineColorArgb { get; set; }

        public List<BubbleData> Bubbles { get; set; } = new List<BubbleData>();
    }

    internal class BubbleData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public Direction BubbleDirection { get; set; }

        public int BubbleColorArgb { get; set; }
    }
}