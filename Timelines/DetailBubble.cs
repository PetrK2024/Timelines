using System;
using System.Drawing;
using System.Windows.Forms;

namespace Timelines
{
    public partial class DetailBubble : Form
    {
        Bubble bubble;

        Label lbYear;
        Label lbMonth;
        Label lbDay;

        Label lbPrnlNl;

        Label lbSep1;
        Label lbSep2;

        Label lbName;

        NumericUpDown numMonth;
        NumericUpDown numDay;

        bool editStateName = false;
        bool editStateDate = false;
        internal DetailBubble(Bubble bubble)
        {
            InitializeComponent();
            this.bubble = bubble;

            string bubbleDirection = bubble.BubbleDirection == Direction.Left ? "Př.n.l." : "N.l.";

            txtName.Text = bubble.Name;
            txtDescription.Text = bubble.Description;

            int gap = 60;

            lbDay = CreateLabel(pnDate, bubble.Day.ToString("00"), new Point(10, 20));
            lbSep1 = CreateLabel(pnDate, ".", new Point(lbDay.Right - 5, 20));
            lbSep1.Width = 10;

            lbMonth = CreateLabel(pnDate, bubble.Month.ToString("00"), new Point(lbSep1.Right + 3, 20));
            lbSep2 = CreateLabel(pnDate, ".", new Point(lbMonth.Right - 5, 20));
            lbSep2.Width = 10;

            lbYear = CreateLabel(pnDate, bubble.Year.ToString(), new Point(lbSep2.Right + 3, 20));
            lbPrnlNl = CreateLabel(pnDate, bubbleDirection, new Point(lbYear.Right + 10, 20));

            numDay = CreateNumericUpDown(pnDate, bubble.Day, lbDay.Location);
            numMonth = CreateNumericUpDown(pnDate, bubble.Month, new Point(numDay.Location.X + gap, lbDay.Location.Y));

            numDay.Minimum = 1;
            numDay.Maximum = 31;

            numMonth.Minimum = 1;
            numMonth.Maximum = 12;

            if(bubble.Name == null)
            {
                txtName.Text = "Zadejte název události";
                txtName.SelectAll();
            }
            else
            {
                SetEditModeName(false);
                lbName = CreateLabel(this, txtName.Text, txtName.Location);
            }

            SetEditModeDate(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bubble.Name = txtName.Text;
            bubble.Description = txtDescription.Text;

            bubble.Day = (int)numDay.Value;
            bubble.Month = (int)numMonth.Value;

            lbDay.Text = bubble.Day.ToString();
            lbMonth.Text = bubble.Month.ToString();

            SetEditModeDate(false);
            Close();
        }

        public Label CreateLabel(Control panel, string text, Point p)
        {
            Label lb = new Label();
            lb.Font = new Font(lb.Font,FontStyle.Bold);
            lb.Width = 23;
            lb.Location = p;
            lb.Text = text;
            panel.Controls.Add(lb);
            return lb;
        }

        public NumericUpDown CreateNumericUpDown(Control panel, int value, Point p)
        {
            NumericUpDown num = new NumericUpDown();
            num.Value = value;
            num.Location = p;
            num.Width = 50;
            num.Visible = false;
            panel.Controls.Add(num);
            return num;
        }

        private void pbEditPencil_Click(object sender, EventArgs e)
        {
            editStateDate = !editStateDate;
            SetEditModeDate(editStateDate);
        }

        private void SetEditModeDate(bool editing)
        {
            lbDay.Visible = !editing;
            lbMonth.Visible = !editing;

            numDay.Visible = editing;
            numMonth.Visible = editing;

            if (editing)
            {
                lbYear.Location = new Point(numMonth.Location.X + 60, numMonth.Location.Y);
                lbPrnlNl.Location = new Point(lbYear.Location.X + 45, lbYear.Location.Y);
                lbSep1.Visible = false;
                lbSep2.Visible = false;
            }
            else
            {
                lbYear.Location = new Point(lbMonth.Right + 10, 20);
                lbPrnlNl.Location = new Point(lbYear.Right + 10, 20);

                bubble.Day = (int)numDay.Value;
                bubble.Month = (int)numMonth.Value;

                lbDay.Text = numDay.Value.ToString("00");
                lbMonth.Text = numMonth.Value.ToString("00");

                lbSep1.Visible = true;
                lbSep2.Visible = true;
            }
        }

        private void SetEditModeName(bool editing)
        {
            if (editing)
            {
                txtName.Visible = false;
            }
            else
            {
                if(txtName.Text != null)
                {
                    bubble.Name = txtName.Text;
                }
                txtName.Visible = true;
            }
        }

        private void pbEditPencil2_Click(object sender, EventArgs e)
        {
            editStateName = !editStateName;
            SetEditModeName(true);
        }
    }
}
