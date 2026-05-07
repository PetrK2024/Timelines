using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Timelines
{
    internal partial class DetailLine : Form
    {
        private const string LineNamePlaceholder = "Zadejte název osy";

        private Line currentLine;
        private readonly List<Line> allLines;

        private Label lbTitle;

        private TextBox txtLineName;
        private Label lbLineName;
        private Button btnEditLineName;

        private Label lbInterval;
        private Label lbBubbleCount;

        private Label lbRelatedTitle;
        private ListBox listBoxRelatedLines;

        private Button btnSave;
        private Button btnClose;
        private Button btnDeleteLine;

        private bool editStateLineName = false;
        private bool changingList = false;

        private Font fontTitle;
        private Font fontName;
        private Font fontNormal;

        private Image editPencil;

        public DetailLine(Line selectedLine, List<Line> allLines)
        {
            this.currentLine = selectedLine;
            this.allLines = allLines;

            InitializeWindow();

            editPencil = Properties.Resources.edit_pencil;

            CreateControls();

            LoadLineToForm(currentLine);
        }

        private void InitializeWindow()
        {
            Text = "Detail osy";
            Size = new Size(750, 500);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            fontTitle = new Font("Arial", 18, FontStyle.Bold);
            fontName = new Font("Arial", 22, FontStyle.Bold);
            fontNormal = new Font("Arial", 11, FontStyle.Regular);
        }

        private void CreateControls()
        {
            lbTitle = new Label();
            lbTitle.Text = "Detail osy";
            lbTitle.Font = fontTitle;
            lbTitle.AutoSize = true;
            lbTitle.Location = new Point(20, 20);
            Controls.Add(lbTitle);

            txtLineName = new TextBox();
            txtLineName.Location = new Point(20, 70);
            txtLineName.Size = new Size(500, 40);
            txtLineName.Font = fontName;
            Controls.Add(txtLineName);

            lbLineName = new Label();
            lbLineName.Location = txtLineName.Location;
            lbLineName.Size = txtLineName.Size;
            lbLineName.Font = fontName;
            lbLineName.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(lbLineName);

            btnEditLineName = new Button();
            btnEditLineName.Location = new Point(txtLineName.Right + 10, txtLineName.Top);
            btnEditLineName.Size = new Size(45, 40);
            SetEditButtonImage(btnEditLineName);
            btnEditLineName.Click += BtnEditLineName_Click;
            Controls.Add(btnEditLineName);

            lbInterval = new Label();
            lbInterval.Location = new Point(20, 130);
            lbInterval.Size = new Size(600, 30);
            lbInterval.Font = fontNormal;
            Controls.Add(lbInterval);

            lbBubbleCount = new Label();
            lbBubbleCount.Location = new Point(20, 165);
            lbBubbleCount.Size = new Size(600, 30);
            lbBubbleCount.Font = fontNormal;
            Controls.Add(lbBubbleCount);

            lbRelatedTitle = new Label();
            lbRelatedTitle.Text = "Příbuzné / prolínající se osy:";
            lbRelatedTitle.Location = new Point(20, 215);
            lbRelatedTitle.Size = new Size(400, 25);
            lbRelatedTitle.Font = new Font(fontNormal, FontStyle.Bold);
            Controls.Add(lbRelatedTitle);

            listBoxRelatedLines = new ListBox();
            listBoxRelatedLines.Location = new Point(20, 245);
            listBoxRelatedLines.Size = new Size(690, 140);
            listBoxRelatedLines.Font = fontNormal;
            listBoxRelatedLines.SelectedIndexChanged += ListBoxRelatedLines_SelectedIndexChanged;
            Controls.Add(listBoxRelatedLines);

            btnDeleteLine = new Button();
            btnDeleteLine.Text = "Smazat osu";
            btnDeleteLine.Location = new Point(390, 410);
            btnDeleteLine.Size = new Size(120, 35);
            btnDeleteLine.Click += BtnDeleteLine_Click;
            Controls.Add(btnDeleteLine);

            btnSave = new Button();
            btnSave.Text = "Uložit";
            btnSave.Location = new Point(520, 410);
            btnSave.Size = new Size(90, 35);
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnClose = new Button();
            btnClose.Text = "Zavřít";
            btnClose.Location = new Point(620, 410);
            btnClose.Size = new Size(90, 35);
            btnClose.Click += BtnClose_Click;
            Controls.Add(btnClose);

            txtLineName.KeyDown += TxtLineName_KeyDown;
        }

        private void SetEditButtonImage(Button button)
        {
            button.Text = "";

            button.BackgroundImage = editPencil;
            button.BackgroundImageLayout = ImageLayout.Zoom;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;

            button.Cursor = Cursors.Hand;
        }

        private void LoadLineToForm(Line line)
        {
            currentLine = line;

            if (string.IsNullOrWhiteSpace(currentLine.Name))
            {
                txtLineName.Text = LineNamePlaceholder;
            }
            else
            {
                txtLineName.Text = currentLine.Name;
            }

            lbLineName.Text = GetLineNameForDisplay(currentLine);

            lbInterval.Text = "Rozsah osy: " + GetLineIntervalText(currentLine);
            lbBubbleCount.Text = "Počet bublin na ose: " + currentLine.OwningBubbles.Count;

            SetEditModeLineName(string.IsNullOrWhiteSpace(currentLine.Name));

            UpdateRelatedLinesList();
            SelectCurrentLineInList();
        }

        private void BtnEditLineName_Click(object sender, EventArgs e)
        {
            SetEditModeLineName(!editStateLineName);
        }

        private void TxtLineName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetEditModeLineName(false);
                SaveCurrentLineWithoutClosing();

                e.SuppressKeyPress = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtLineName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || name == LineNamePlaceholder)
            {
                MessageBox.Show("Zadejte název osy.");
                SetEditModeLineName(true);
                return;
            }

            SaveCurrentLineWithoutClosing();

            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            SaveCurrentLineWithoutClosing();

            Close();
        }

        private void ListBoxRelatedLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changingList)
            {
                return;
            }

            if (listBoxRelatedLines.SelectedItem == null)
            {
                return;
            }

            SaveCurrentLineWithoutClosing();

            LineListItem selectedItem = listBoxRelatedLines.SelectedItem as LineListItem;

            if (selectedItem == null)
            {
                return;
            }

            LoadLineToForm(selectedItem.Line);
        }

        private void SetEditModeLineName(bool editing)
        {
            if (!editing)
            {
                string lineName = txtLineName.Text.Trim();

                if (string.IsNullOrWhiteSpace(lineName) || lineName == LineNamePlaceholder)
                {
                    MessageBox.Show("Zadejte název osy.");

                    editStateLineName = true;

                    txtLineName.Visible = true;
                    lbLineName.Visible = false;

                    txtLineName.Text = LineNamePlaceholder;

                    txtLineName.Focus();
                    txtLineName.SelectAll();

                    return;
                }

                currentLine.Name = lineName;
                lbLineName.Text = currentLine.Name;
            }

            editStateLineName = editing;

            txtLineName.Visible = editing;
            lbLineName.Visible = !editing;

            if (editing)
            {
                txtLineName.Focus();
                txtLineName.SelectAll();
            }
        }

        private void SaveCurrentLineWithoutClosing()
        {
            string lineName = txtLineName.Text.Trim();

            if (string.IsNullOrWhiteSpace(lineName) || lineName == LineNamePlaceholder)
            {
                currentLine.Name = null;
            }
            else
            {
                currentLine.Name = lineName;
            }

            lbLineName.Text = GetLineNameForDisplay(currentLine);
        }

        private string GetLineNameForDisplay(Line line)
        {
            if (string.IsNullOrWhiteSpace(line.Name))
            {
                return "Bez názvu osy";
            }

            return line.Name;
        }

        private void UpdateRelatedLinesList()
        {
            changingList = true;

            listBoxRelatedLines.Items.Clear();

            foreach (Line line in allLines)
            {
                if (line == currentLine || LinesOverlap(currentLine, line))
                {
                    string prefix = line == currentLine ? "[Aktuální] " : "";

                    listBoxRelatedLines.Items.Add(
                        new LineListItem(line, prefix + GetLineListText(line))
                    );
                }
            }

            changingList = false;
        }

        private void SelectCurrentLineInList()
        {
            changingList = true;

            for (int i = 0; i < listBoxRelatedLines.Items.Count; i++)
            {
                LineListItem item = listBoxRelatedLines.Items[i] as LineListItem;

                if (item != null && item.Line == currentLine)
                {
                    listBoxRelatedLines.SelectedIndex = i;
                    break;
                }
            }

            changingList = false;
        }

        private bool LinesOverlap(Line a, Line b)
        {
            var intervalA = GetSignedInterval(a);
            var intervalB = GetSignedInterval(b);

            return intervalA.Start <= intervalB.End &&
                   intervalB.Start <= intervalA.End;
        }

        private (int Start, int End) GetSignedInterval(Line line)
        {
            int from = GetSignedYear(line.FromYear, line.LineDirectionFrom);
            int to = GetSignedYear(line.ToYear, line.LineDirectionTo);

            int start = Math.Min(from, to);
            int end = Math.Max(from, to);

            return (start, end);
        }

        private int GetSignedYear(int year, Direction direction)
        {
            if (direction == Direction.Left)
            {
                return -Math.Abs(year);
            }

            return Math.Abs(year);
        }

        private string GetLineIntervalText(Line line)
        {
            return FormatYear(line.FromYear, line.LineDirectionFrom) +
                   " - " +
                   FormatYear(line.ToYear, line.LineDirectionTo);
        }

        private string FormatYear(int year, Direction direction)
        {
            string era = direction == Direction.Left ? "Př.n.l." : "N.l.";

            return Math.Abs(year) + " " + era;
        }

        private string GetLineListText(Line line)
        {
            string name = GetLineNameForDisplay(line);

            return name + " | " + GetLineIntervalText(line);
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

        private void BtnDeleteLine_Click(object sender, EventArgs e)
        {
            if (currentLine == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Opravdu chcete smazat tuto lajnu?\n\nSmazáním lajny přijdete také o všechny její bubliny ({currentLine.OwningBubbles.Count}).",
                "Smazání lajny",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            int currentIndex = allLines.IndexOf(currentLine);

            if (currentIndex < 0)
            {
                Close();
                return;
            }

            allLines.RemoveAt(currentIndex);

            if (allLines.Count == 0)
            {
                Close();
                return;
            }

            if (currentIndex >= allLines.Count)
            {
                currentIndex = allLines.Count - 1;
            }

            Line nextLine = allLines[currentIndex];

            LoadLineToForm(nextLine);
        }
    }
}