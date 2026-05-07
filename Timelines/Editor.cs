using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Timelines
{
    public partial class Editor : Form
    {
        bool dragging = false;

        bool isLineFocused = false;
        Line focusedLine;
        Line clickedLine;

        bool isBubbleFocused = false;
        Bubble focusedBuble;
        Bubble clickedBubble;

        int lastX = 0;

        Timer timer;
        ColorDialog bubbleColorDialog;
        ColorDialog lineColorDialog;

        List<Line> lines = new List<Line>();
        //List<Bubble> bubbles = new List<Bubble>();

        readonly Color defaultBubbleColor = Color.Blue;
        readonly Color defaultLineColor = Color.Red;

        private string currentFilePath = null;

        private bool changingLineList = false;
        public Editor()
        {
            InitializeComponent();
            pnCanvas.Dock = DockStyle.Fill;
            Padding = new Padding(10, 100, 10, 50);

            pnCanvas.Paint += PnCanvas_Paint;

            pnCanvas.MouseWheel += PnCanvas_MouseWheel;
            pnCanvas.MouseDown += PnCanvas_MouseDown;
            pnCanvas.MouseUp += PnCanvas_MouseUp;
            pnCanvas.MouseMove += PnCanvas_MouseMove;

            SizeChanged += Editor_SizeChanged;

            pnFindYear.Location = new Point(10, 10);
            pnLine.Location = new Point(pnFindYear.Location.X + pnFindYear.ClientSize.Width + 10, 10);
            pnBubble.Location = new Point(pnLine.Location.X + pnLine.ClientSize.Width + 10, 10);
            pnBubble.Enabled = false;

            rbNl.Checked = true;

            rbLineNlFrom.Checked = true;
            rbLineNlTo.Checked = true;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();

            lineColorDialog = new ColorDialog();
            bubbleColorDialog = new ColorDialog();

            btnDetailLine.Enabled = false;
            btnDeleteLine.Enabled = false;

            SetDoubleBuffered(pnCanvas, true);

            listBoxLines.SelectedIndexChanged += ListboxLines_SelectedIndexChanged;
            UpdateLineList();
        }

        internal Editor(TimelineProject project, string filePath) : this()
        {
            currentFilePath = filePath;
            LoadProjectData(project);
        }

        private void ListboxLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changingLineList)
            {
                return;
            }

            if (listBoxLines.SelectedItem == null)
            {
                return;
            }

            LineListItem selectedItem = listBoxLines.SelectedItem as LineListItem;

            if (selectedItem == null)
            {
                return;
            }

            SelectLine(selectedItem.Line);
            TeleportToLine(selectedItem.Line);

            pnCanvas.Invalidate();
        }

        private void UpdateLineList()
        {
            changingLineList = true;

            listBoxLines.Items.Clear();

            foreach (Line line in lines)
            {
                listBoxLines.Items.Add(new LineListItem(line, GetLineListText(line)));
            }

            changingLineList = false;
        }

        private void SelectLine(Line lineToSelect)
        {
            foreach (Line line in lines)
            {
                line.IsClicked = false;
                line.ReduceThickness();
            }

            clickedLine = lineToSelect;
            focusedLine = lineToSelect;

            clickedLine.IsClicked = true;
            clickedLine.IncreaseThickness();

            clickedBubble = null;
            focusedBuble = null;

            isLineFocused = true;
            isBubbleFocused = false;

            pnBubble.Enabled = true;

            btnDetailLine.Enabled = true;
            btnDeleteLine.Enabled = true;
        }

        private void TeleportToLine(Line line)
        {
            int centerYear = GetSignedCenterYear(line);

            if (centerYear < 0)
            {
                Meter.Offset = Meter.GetXFromYear(Math.Abs(centerYear));
            }
            else
            {
                Meter.Offset = -Meter.GetXFromYear(centerYear);
            }
        }

        private int GetSignedCenterYear(Line line)
        {
            int from = GetSignedYear(line.FromYear, line.LineDirectionFrom);
            int to = GetSignedYear(line.ToYear, line.LineDirectionTo);

            return (from + to) / 2;
        }

        private int GetSignedYear(int year, Direction direction)
        {
            if (direction == Direction.Left)
            {
                return -Math.Abs(year);
            }

            return Math.Abs(year);
        }

        private string GetLineListText(Line line)
        {
            string name = string.IsNullOrWhiteSpace(line.Name)
                ? "Bez názvu osy"
                : line.Name;

            return $"{name} | {GetLineIntervalText(line)} | bubliny: {line.OwningBubbles.Count}";
        }

        private string GetLineIntervalText(Line line)
        {
            return FormatYear(line.FromYear, line.LineDirectionFrom)
                + " - "
                + FormatYear(line.ToYear, line.LineDirectionTo);
        }

        private string FormatYear(int year, Direction direction)
        {
            string era = direction == Direction.Left ? "Př.n.l." : "N.l.";

            return $"{Math.Abs(year)} {era}";
        }

        private void SelectLineInList(Line lineToSelect)
        {
            changingLineList = true;

            for (int i = 0; i < listBoxLines.Items.Count; i++)
            {
                LineListItem item = listBoxLines.Items[i] as LineListItem;

                if (item != null && item.Line == lineToSelect)
                {
                    listBoxLines.SelectedIndex = i;
                    break;
                }
            }

            changingLineList = false;
        }

        private TimelineProject CreateProjectData()
        {
            TimelineProject project = new TimelineProject();
            project.ProjectName = "Můj timeline projekt";

            foreach (Line line in lines)
            {
                LineData lineData = new LineData();

                lineData.Name = line.Name;

                lineData.FromYear = line.FromYear;
                lineData.ToYear = line.ToYear;

                lineData.LineDirectionFrom = line.LineDirectionFrom;
                lineData.LineDirectionTo = line.LineDirectionTo;

                lineData.LineColorArgb = line.LineColor.ToArgb();

                foreach (Bubble bubble in line.OwningBubbles)
                {
                    BubbleData bubbleData = new BubbleData();

                    bubbleData.Name = bubble.Name;
                    bubbleData.Description = bubble.Description;

                    bubbleData.Year = bubble.Year;
                    bubbleData.Month = bubble.Month;
                    bubbleData.Day = bubble.Day;

                    bubbleData.BubbleDirection = bubble.BubbleDirection;
                    bubbleData.BubbleColorArgb = bubble.BubbleColor.ToArgb();

                    lineData.Bubbles.Add(bubbleData);
                }

                project.Lines.Add(lineData);
            }

            return project;
        }

        private void LoadProjectData(TimelineProject project)
        {
            lines.Clear();

            clickedLine = null;
            focusedLine = null;

            clickedBubble = null;
            focusedBuble = null;

            isLineFocused = false;
            isBubbleFocused = false;

            pnBubble.Enabled = false;

            Line.IncresingLineY = 0;

            if (project == null || project.Lines == null)
            {
                UpdateLineList();
                pnCanvas.Invalidate();
                return;
            }

            foreach (LineData lineData in project.Lines)
            {
                Color lineColor = Color.FromArgb(lineData.LineColorArgb);

                Line line = new Line(
                    lineData.FromYear,
                    lineData.ToYear,
                    lineData.LineDirectionFrom,
                    lineData.LineDirectionTo,
                    lineColor
                );

                line.Name = lineData.Name;

                if (lineData.Bubbles != null)
                {
                    foreach (BubbleData bubbleData in lineData.Bubbles)
                    {
                        Color bubbleColor = Color.FromArgb(bubbleData.BubbleColorArgb);

                        Bubble bubble = new Bubble(
                            line,
                            bubbleData.Year,
                            bubbleData.BubbleDirection,
                            bubbleColor
                        );

                        bubble.Name = bubbleData.Name;
                        bubble.Description = bubbleData.Description;

                        bubble.Day = bubbleData.Day;
                        bubble.Month = bubbleData.Month;

                        line.OwningBubbles.Add(bubble);
                    }
                }

                lines.Add(line);
            }

            RecalculateLinePositions();

            UpdateLineList();

            pnCanvas.Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lbEra.Text = Meter.Offset > 0 ? "Př.n.l." : "N.l.";
        }

        private void Editor_SizeChanged(object sender, EventArgs e)
        {
            lbEra.Location = new Point((Width / 2) - 10, ClientSize.Height - 40);
        }

        private void PnCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                int dx = e.X - lastX;
                lastX = e.X;

                Meter.Offset += dx;
                pnCanvas.Invalidate();
                return;
            }

            focusedLine = null;
            focusedBuble = null;
            isLineFocused = false;
            isBubbleFocused = false;
            Cursor = Cursors.Default;

            foreach (Line line in lines)
            {
                if (!line.IsClicked)
                {
                    line.ReduceThickness();
                }
            }

            foreach (Line line in lines)
            {
                foreach (Bubble bubble in line.OwningBubbles)
                {
                    if (bubble.IsBubbleFocused(e.Location))
                    {
                        focusedLine = line;
                        focusedBuble = bubble;
                        isBubbleFocused = true;

                        Cursor = Cursors.Cross;

                        pnCanvas.Invalidate();
                        return;
                    }
                }
            }

            foreach (Line line in lines)
            {
                if (line.IsLineFocused(e.Location, false))
                {
                    focusedLine = line;
                    isLineFocused = true;

                    pnCanvas.Invalidate();
                    return;
                }
            }


            pnCanvas.Invalidate();
        }

        private void PnCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void PnCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            lastX = e.X;

            if (isBubbleFocused && focusedBuble != null)
            {
                clickedBubble = focusedBuble;
                clickedLine = focusedBuble.OwningLine;

                foreach (Line line in lines)
                {
                    line.IsClicked = false;
                    line.ReduceThickness();

                    foreach (Bubble bubble in line.OwningBubbles)
                    {
                        bubble.IsClicked = false;
                    }
                }

                clickedLine.IsClicked = true;
                clickedLine.IncreaseThickness();

                clickedBubble.IsClicked = true;

                dragging = false;

                using (DetailBubble detailBubble = new DetailBubble(clickedBubble))
                {
                    detailBubble.ShowDialog(this);
                }

                UpdateLineList();

                if (clickedLine != null)
                {
                    SelectLineInList(clickedLine);
                }

                clickedBubble.IsClicked = false;
                isBubbleFocused = false;

                pnCanvas.Invalidate();
                return;
            }

            if (isLineFocused && focusedLine != null)
            {
                foreach (Line line in lines)
                {
                    line.IsClicked = false;
                    line.ReduceThickness();

                    foreach (Bubble bubble in line.OwningBubbles)
                    {
                        bubble.IsClicked = false;
                    }
                }

                clickedLine = focusedLine;
                clickedLine.IsClicked = true;
                clickedLine.IncreaseThickness();

                pnBubble.Enabled = true;

                dragging = false;

                pnCanvas.Invalidate();

                btnDetailLine.Enabled = true;
                btnDeleteLine.Enabled = true;
                return;
            }

            dragging = true;

            foreach (Line line in lines)
            {
                line.IsClicked = false;
                line.ReduceThickness();
            }

            clickedLine = null;
            clickedBubble = null;
            pnBubble.Enabled = false;

            pnCanvas.Invalidate();
        }

        private void PnCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            int canvasWidth = pnCanvas.ClientSize.Width;
            int canvasCenterX = pnCanvas.ClientSize.Width / 2;

            Meter.ZoomAtMouse(e.X, canvasWidth, e.Delta);
            pnCanvas.Invalidate();
        }

        private void PnCanvas_Paint(object sender, PaintEventArgs e)
        {
            Meter.DrawScale(e.Graphics, pnCanvas);

            foreach (Line line in lines)
            {
                line.DrawLine(e.Graphics, pnCanvas);
            }

            foreach (Line line in lines)
            {
                foreach (Bubble bubble in line.OwningBubbles)
                {
                    bubble.DrawBubble(e.Graphics);
                }
            }
        }

        private void SetDoubleBuffered(Control control, bool enabled)
        {
            PropertyInfo prop = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(control, enabled, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (rbPrnl.Checked)
            {
                Meter.Offset = Meter.GetXFromYear((int)numFindYear.Value);
            }
            else if (rbNl.Checked)
            {
                Meter.Offset = -Meter.GetXFromYear((int)numFindYear.Value);
            }
            else
            {
                MessageBox.Show("Vyplňte Př.n.l nebo N.l.!");
            }

            pnCanvas.Invalidate();
        }

        private void btnCreateLine_Click(object sender, EventArgs e)
        {
            focusedLine = null;
            clickedLine = null;

            Line line = null;

            Color lineColor = defaultLineColor;

            if (lineColorDialog.ShowDialog() == DialogResult.OK)
            {
                lineColor = lineColorDialog.Color;
            }

            int centerYear;

            if (rbLineNlFrom.Checked && rbLineNlTo.Checked && (int)numLineFrom.Value < (int)numLineTo.Value)
            {
                line = new Line((int)numLineFrom.Value, (int)numLineTo.Value, Direction.Right, Direction.Right, lineColor);
                centerYear = (int)Direction.Right * (int)numLineFrom.Value + ((int)numLineTo.Value - (int)numLineFrom.Value) / 2;
            }
            else if (rbLinePrnlFrom.Checked && rbLinePrnlTo.Checked && (int)numLineFrom.Value < (int)numLineTo.Value)
            {
                line = new Line((int)numLineFrom.Value, (int)numLineTo.Value, Direction.Left, Direction.Left, lineColor);
                centerYear = (int)Direction.Left * (int)numLineFrom.Value + ((int)numLineTo.Value - (int)numLineFrom.Value) / 2;
            }
            else if (rbLinePrnlFrom.Checked && rbLineNlTo.Checked)
            {
                line = new Line((int)numLineFrom.Value, (int)numLineTo.Value - (2 * (int)numLineTo.Value), Direction.Left, Direction.Right, lineColor);
                centerYear = (int)Direction.Left * (int)numLineFrom.Value + ((int)numLineTo.Value - (int)numLineFrom.Value) / 2;
            }
            else
            {
                MessageBox.Show("Takto to nelze zakliknout!");
                return;
            }

            lines.Add(line);
            UpdateLineList();
            SelectLineInList(line);
            Meter.Offset = -Meter.GetXFromYear(centerYear);

            pnCanvas.Invalidate();
        }

        private void btnCreateBubble_Click(object sender, EventArgs e)
        {
            if (clickedLine == null)
            {
                MessageBox.Show("Nejdřív vyber osu, ke které chceš přidat bublinu.");
                return;
            }

            Direction bubbleDirection;

            if (rbBubblePrnl.Checked)
            {
                bubbleDirection = Direction.Left;
            }
            else if (rbBubbleNl.Checked)
            {
                bubbleDirection = Direction.Right;
            }
            else
            {
                MessageBox.Show("Zaškrtněte Př.n.l. nebo N.l.!");
                return;
            }

            int bubbleYear = (int)numBubbleYear.Value;

            if (!IsBubbleYearValidForLine(clickedLine, bubbleYear, bubbleDirection))
            {
                MessageBox.Show("Tento rok nepatří na vybranou osu.");
                return;
            }

            if (BubbleAlreadyExists(clickedLine, bubbleYear, bubbleDirection))
            {
                MessageBox.Show("V tomto roce už na této ose bublina existuje.");
                return;
            }

            Color bubbleColor = defaultBubbleColor;

            if (bubbleColorDialog.ShowDialog() == DialogResult.OK)
            {
                bubbleColor = bubbleColorDialog.Color;
            }

            Bubble bubble = new Bubble(clickedLine, bubbleYear, bubbleDirection, bubbleColor);

            clickedLine.OwningBubbles.Add(bubble);

            UpdateLineList();
            SelectLineInList(clickedLine);

            pnCanvas.Invalidate();
        }

        private bool BubbleAlreadyExists(Line line, int year, Direction direction)
        {
            foreach (Bubble bubble in line.OwningBubbles)
            {
                if (bubble.Year == year && bubble.BubbleDirection == direction)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsBubbleYearValidForLine(Line line, int year, Direction direction)
        {
            // Osa pouze Př.n.l.
            if (line.LineDirectionFrom == Direction.Left && line.LineDirectionTo == Direction.Left)
            {
                if (direction != Direction.Left)
                {
                    return false;
                }

                return year >= line.FromYear && year <= line.ToYear;
            }

            // Osa pouze N.l.
            if (line.LineDirectionFrom == Direction.Right && line.LineDirectionTo == Direction.Right)
            {
                if (direction != Direction.Right)
                {
                    return false;
                }

                return year >= line.FromYear && year <= line.ToYear;
            }

            // Osa jde z Př.n.l. do N.l.
            if (line.LineDirectionFrom == Direction.Left && line.LineDirectionTo == Direction.Right)
            {
                if (direction == Direction.Left)
                {
                    return year >= 1 && year <= line.FromYear;
                }

                if (direction == Direction.Right)
                {
                    return year >= 1 && year <= Math.Abs(line.ToYear);
                }
            }

            return false;
        }

        private void RecalculateLinePositions()
        {
            int offsetY = 0;

            foreach (Line line in lines)
            {
                line.LineY = Meter.TickBaseY + 50 + offsetY;
                offsetY += 25;
            }

            Line.IncresingLineY = offsetY;
        }

        private void btnDetailLine_Click_1(object sender, EventArgs e)
        {
            if (clickedLine == null)
            {
                return;
            }

            using (DetailLine detailLine = new DetailLine(clickedLine, lines))
            {
                detailLine.ShowDialog(this);
            }

            UpdateLineList();

            if (clickedLine != null && !lines.Contains(clickedLine))
            {
                clickedLine = null;
                focusedLine = null;

                clickedBubble = null;
                focusedBuble = null;

                isLineFocused = false;
                isBubbleFocused = false;

                pnBubble.Enabled = false;
                btnDetailLine.Enabled = false;
                btnDeleteLine.Enabled = false;
            }

            RecalculateLinePositions();

            pnCanvas.Invalidate();
        }

        private void btnDeleteLine_Click_1(object sender, EventArgs e)
        {
            if (clickedLine == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Opravdu chcete smazat tuto lajnu?\n\nSmazáním lajny přijdete také o všechny její bubliny ({clickedLine.OwningBubbles.Count}).",
                "Smazání lajny",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            lines.Remove(clickedLine);
            UpdateLineList();

            clickedLine = null;
            focusedLine = null;

            clickedBubble = null;
            focusedBuble = null;

            isLineFocused = false;
            isBubbleFocused = false;

            pnBubble.Enabled = false;
            btnDetailLine.Enabled = false;
            btnDeleteLine.Enabled = false;

            RecalculateLinePositions();

            pnCanvas.Invalidate();
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(currentFilePath))
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Timeline project (*.json)|*.json";
                    dialog.Title = "Uložit projekt";
                    dialog.DefaultExt = "json";

                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    currentFilePath = dialog.FileName;
                }
            }

            TimelineProject project = CreateProjectData();

            TimelineStorage.Save(currentFilePath, project);

            MessageBox.Show("Projekt byl uložen.");
        }

        private void btnSaveProjectAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Timeline project (*.json)|*.json";
                dialog.Title = "Uložit projekt jako";
                dialog.DefaultExt = "json";

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                currentFilePath = dialog.FileName;
            }

            TimelineProject project = CreateProjectData();

            TimelineStorage.Save(currentFilePath, project);

            MessageBox.Show("Projekt byl uložen.");
        }

        private class LineListItem
        {
            public Line Line { get; }
            private string Text { get; }

            public LineListItem(Line line, string text)
            {
                Line = line;
                Text = text;
            }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
