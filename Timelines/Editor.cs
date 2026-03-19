using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

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

        List <Line> lines = new List<Line>();
        List<Bubble> bubbles = new List<Bubble>();

        readonly Color defaultBubbleColor = Color.Blue;
        readonly Color defaultLineColor = Color.Red;

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

            pnFindYear.Location = new Point(10,10);
            pnLine.Location = new Point(pnFindYear.Location.X + pnFindYear.ClientSize.Width + 10,10);
            pnBubble.Location = new Point(pnLine.Location.X + pnLine.ClientSize.Width + 10,10);
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

            SetDoubleBuffered(pnCanvas, true);
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
                Cursor = Cursors.SizeWE;

                int dx = e.X - lastX;
                lastX = e.X;

                Meter.Offset += dx;
            }
            else
            {
                //label2.Text = e.Location.ToString();
                foreach (Line line in lines)
                {
                    isLineFocused = line.IsLineFocused(e.Location);
                    if (isLineFocused)
                    {
                        label2.Text = line.IsClicked.ToString();

                        focusedLine = line;
                        label1.Text = "Shoda";
                        break;
                    }
                    else
                    {
                        label2.Text = line.IsClicked.ToString();
                        label1.Text = "Neshoda";
                    }
                }

                foreach (Bubble bubble in bubbles)
                {
                    isBubbleFocused = bubble.IsBubbleFocused(e.Location);
                    if (isBubbleFocused)
                    {
                        label2.Text = bubble.IsClicked.ToString();

                        focusedBuble = bubble;
                        label1.Text = "Shoda Bubble";
                        break;
                    }
                    else
                    {
                        label2.Text = bubble.IsClicked.ToString();
                        label1.Text = "Neshoda Bubble";
                    }
                }

            }
          
            pnCanvas.Invalidate();
        }

        private void PnCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            Cursor = Cursors.Default;
        }

        private void PnCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            lastX = e.X;

            if (lines.Count == 0)
                return;

            foreach (Line line in lines)
            {
                line.IsClicked = false;
                line.ReduceThickness();
            }
            if (isLineFocused)
            {
                pnBubble.Enabled = true;
                clickedLine = focusedLine;
                focusedLine.IsClicked = true;
               
                label2.Text += focusedLine.FromYear + " " + focusedLine.ToYear;
            }
            else
            {
                focusedLine.IsClicked = false;
                pnBubble.Enabled = false;
            }

            //if (bubbles.Count == 0 || !isBubbleFocused)
            //    return;

            //foreach(Bubble bubble in bubbles)
            //{
            //    bubble.IsClicked = false;
            //    bubble.ReduceSize();
            //}

            //if (isBubbleFocused)
            //{
            //    //pnBubble.Enabled = true;
            //    clickedBubble = focusedBuble;
            //    focusedBuble.IsClicked = true;
            //}
            //else
            //{
            //    focusedBuble.IsClicked = false;
            //    //pnBubble.Enabled = false;
            //}
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

            foreach(Bubble bubble in bubbles)
            {
                bubble.DrawBubble(e.Graphics);
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
     
            if(rbLineNlFrom.Checked && rbLineNlTo.Checked)
            {
                line = new Line((int)numLineFrom.Value, (int)numLineTo.Value, Direction.Right, Direction.Right, lineColor);
                centerYear = (int)Direction.Right * (int)numLineFrom.Value + ((int)numLineTo.Value - (int)numLineFrom.Value) / 2;
            }
            else if(rbLinePrnlFrom.Checked && rbLinePrnlTo.Checked)
            {
                line = new Line((int)numLineFrom.Value, (int)numLineTo.Value, Direction.Left, Direction.Left, lineColor);
                centerYear = (int)Direction.Left * (int)numLineFrom.Value + ((int)numLineTo.Value - (int)numLineFrom.Value) / 2;
            }
            else if(rbLinePrnlFrom.Checked && rbLineNlTo.Checked)
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
            Meter.Offset = -Meter.GetXFromYear(centerYear);

            pnCanvas.Invalidate();
        }
       
        private void btnCreateBubble_Click(object sender, EventArgs e)
        {
            Bubble bubble = null;

            Color bubbleColor = defaultBubbleColor;

            if (bubbleColorDialog.ShowDialog() == DialogResult.OK)
            {
                bubbleColor = bubbleColorDialog.Color;
            }

            if (rbBubblePrnl.Checked)
            {
                bubble = new Bubble(clickedLine, (int)numBubbleYear.Value, Direction.Left, bubbleColor);
            } 
            else if (rbBubbleNl.Checked)
            {
                bubble = new Bubble(clickedLine, (int)numBubbleYear.Value, Direction.Right, bubbleColor);
            }
            else
            {
                MessageBox.Show("Zašktněte Př.n.l nebo N.l.!");
                return;
            }

            bubbles.Add(bubble);

            pnCanvas.Invalidate();
            
        }

    }
}
