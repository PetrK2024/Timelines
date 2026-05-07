using System;
using System.Drawing;
using System.Windows.Forms;

namespace Timelines
{
    internal partial class DetailBubble : Form
    {
        private Bubble bubble;
        private Line owningLine;

        private const string NamePlaceholder = "Zadejte název události";
        private const string LineNamePlaceholder = "Zadejte název osy";

        private Label lbTitle;

        private TextBox txtLineName;
        private Label lbLineName;
        private Button btnEditLineName;

        private TextBox txtName;
        private Label lbName;
        private Button btnEditName;

        private Panel pnDate;
        private Label lbYear;
        private Label lbMonth;
        private Label lbDay;
        private Label lbPrnlNl;
        private Label lbSep1;
        private Label lbSep2;

        private NumericUpDown numMonth;
        private NumericUpDown numDay;
        private Button btnEditDate;

        private TextBox txtDescription;

        private ListBox listBoxBubbles;

        private NumericUpDown numNewBubbleYear;
        private RadioButton rbNewBubblePrnl;
        private RadioButton rbNewBubbleNl;

        private Button btnAddBubble;
        private Button btnDeleteBubble;
        private Button btnChangeBubbleColor;

        private Button btnSave;
        private Button btnClose;

        private Font fontTitle;
        private Font fontName;
        private Font fontDate;
        private Font fontNormal;

        private bool editStateName = false;
        private bool editStateDate = false;
        private bool editStateLineName = false;
        private bool changingBubble = false;

        private ColorDialog bubbleColorDialog;

        private Image editPencil;

        internal DetailBubble(Bubble bubble)
        {
            this.bubble = bubble;
            owningLine = bubble.OwningLine;

            InitializeWindow();

            editPencil = Properties.Resources.edit_pencil;

            CreateControls();

            bubbleColorDialog = new ColorDialog();

            LoadBubbleToForm(bubble);

            UpdateBubbleList();
            SelectCurrentBubbleInList();
        }

        private void InitializeWindow()
        {
            Text = "Detail bubliny";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            fontTitle = new Font("Arial", 18, FontStyle.Bold);
            fontName = new Font("Arial", 24, FontStyle.Bold);
            fontDate = new Font("Arial", 20, FontStyle.Bold);
            fontNormal = new Font("Arial", 11, FontStyle.Regular);
        }

        private void CreateControls()
        {
            lbTitle = new Label();
            lbTitle.Text = "Detail bubliny";
            lbTitle.Font = fontTitle;
            lbTitle.AutoSize = true;
            lbTitle.Location = new Point(20, 20);
            Controls.Add(lbTitle);

            Label lbLineTitle = new Label();
            lbLineTitle.Text = "Název osy:";
            lbLineTitle.Font = fontNormal;
            lbLineTitle.AutoSize = true;
            lbLineTitle.Location = new Point(20, 65);
            Controls.Add(lbLineTitle);

            txtLineName = new TextBox();
            txtLineName.Location = new Point(20, 90);
            txtLineName.Size = new Size(600, 40);
            txtLineName.Font = fontName;
            txtLineName.KeyDown += TxtLineName_KeyDown;
            Controls.Add(txtLineName);

            lbLineName = new Label();
            lbLineName.Location = txtLineName.Location;
            lbLineName.Size = txtLineName.Size;
            lbLineName.Font = fontName;
            lbLineName.TextAlign = ContentAlignment.MiddleLeft;
            lbLineName.ForeColor = Color.DarkRed;
            Controls.Add(lbLineName);

            btnEditLineName = new Button();
            btnEditLineName.Location = new Point(txtLineName.Right + 10, txtLineName.Top);
            btnEditLineName.Size = new Size(45, 40);
            SetEditButtonImage(btnEditLineName);
            btnEditLineName.Click += BtnEditLineName_Click;
            Controls.Add(btnEditLineName);

            Label lbBubbleTitle = new Label();
            lbBubbleTitle.Text = "Název události:";
            lbBubbleTitle.Font = fontNormal;
            lbBubbleTitle.AutoSize = true;
            lbBubbleTitle.Location = new Point(20, 150);
            Controls.Add(lbBubbleTitle);

            txtName = new TextBox();
            txtName.Location = new Point(20, 175);
            txtName.Size = new Size(600, 40);
            txtName.Font = fontName;
            txtName.KeyDown += TxtName_KeyDown;
            Controls.Add(txtName);

            lbName = new Label();
            lbName.Location = txtName.Location;
            lbName.Size = txtName.Size;
            lbName.Font = fontName;
            lbName.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(lbName);

            btnEditName = new Button();
            btnEditName.Location = new Point(txtName.Right + 10, txtName.Top);
            btnEditName.Size = new Size(45, 40);
            SetEditButtonImage(btnEditName);
            btnEditName.Click += BtnEditName_Click;
            Controls.Add(btnEditName);

            Label lbDateTitle = new Label();
            lbDateTitle.Text = "Datum:";
            lbDateTitle.Font = fontNormal;
            lbDateTitle.AutoSize = true;
            lbDateTitle.Location = new Point(20, 235);
            Controls.Add(lbDateTitle);

            pnDate = new Panel();
            pnDate.Location = new Point(20, 260);
            pnDate.Size = new Size(500, 70);
            pnDate.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pnDate);

            lbDay = CreateLabel(pnDate, "01", new Point(10, 18), fontDate);

            lbSep1 = CreateLabel(pnDate, ".", new Point(lbDay.Right + 2, 18), fontDate);
            lbSep1.Width = 10;

            lbMonth = CreateLabel(pnDate, "01", new Point(lbSep1.Right + 2, 18), fontDate);

            lbSep2 = CreateLabel(pnDate, ".", new Point(lbMonth.Right + 2, 18), fontDate);
            lbSep2.Width = 10;

            lbYear = CreateLabel(pnDate, "1", new Point(lbSep2.Right + 5, 18), fontDate);

            lbPrnlNl = CreateLabel(pnDate, "N.l.", new Point(lbYear.Right + 10, 18), fontDate);
            lbPrnlNl.ForeColor = Color.Gray;

            numDay = CreateNumericUpDown(pnDate, 1, 31, 1, lbDay.Location);
            numDay.Font = fontDate;

            numMonth = CreateNumericUpDown(pnDate, 1, 12, 1, new Point(numDay.Right + 10, lbDay.Location.Y));
            numMonth.Font = fontDate;

            btnEditDate = new Button();
            btnEditDate.Location = new Point(pnDate.Right + 10, pnDate.Top + 10);
            btnEditDate.Size = new Size(45, 40);
            SetEditButtonImage(btnEditDate);
            btnEditDate.Click += BtnEditDate_Click;
            Controls.Add(btnEditDate);

            Label lbDescriptionTitle = new Label();
            lbDescriptionTitle.Text = "Popis události:";
            lbDescriptionTitle.Font = fontNormal;
            lbDescriptionTitle.AutoSize = true;
            lbDescriptionTitle.Location = new Point(20, 350);
            Controls.Add(lbDescriptionTitle);

            txtDescription = new TextBox();
            txtDescription.Location = new Point(20, 375);
            txtDescription.Size = new Size(650, 130);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Font = fontNormal;
            Controls.Add(txtDescription);

            Label lbListTitle = new Label();
            lbListTitle.Text = "Bubliny na této ose:";
            lbListTitle.Font = fontNormal;
            lbListTitle.AutoSize = true;
            lbListTitle.Location = new Point(700, 65);
            Controls.Add(lbListTitle);

            listBoxBubbles = new ListBox();
            listBoxBubbles.Location = new Point(700, 90);
            listBoxBubbles.Size = new Size(250, 220);
            listBoxBubbles.Font = fontNormal;
            listBoxBubbles.SelectedIndexChanged += ListBoxBubbles_SelectedIndexChanged;
            Controls.Add(listBoxBubbles);

            Label lbAddTitle = new Label();
            lbAddTitle.Text = "Přidat novou bublinu:";
            lbAddTitle.Font = new Font(fontNormal, FontStyle.Bold);
            lbAddTitle.AutoSize = true;
            lbAddTitle.Location = new Point(700, 330);
            Controls.Add(lbAddTitle);

            Label lbYearTitle = new Label();
            lbYearTitle.Text = "Rok:";
            lbYearTitle.Font = fontNormal;
            lbYearTitle.AutoSize = true;
            lbYearTitle.Location = new Point(700, 365);
            Controls.Add(lbYearTitle);

            numNewBubbleYear = new NumericUpDown();
            numNewBubbleYear.Location = new Point(750, 360);
            numNewBubbleYear.Size = new Size(90, 30);
            numNewBubbleYear.Minimum = 1;
            numNewBubbleYear.Maximum = 100000;
            numNewBubbleYear.Font = fontNormal;
            Controls.Add(numNewBubbleYear);

            rbNewBubblePrnl = new RadioButton();
            rbNewBubblePrnl.Text = "Př.n.l.";
            rbNewBubblePrnl.Location = new Point(700, 400);
            rbNewBubblePrnl.AutoSize = true;
            rbNewBubblePrnl.Font = fontNormal;
            Controls.Add(rbNewBubblePrnl);

            rbNewBubbleNl = new RadioButton();
            rbNewBubbleNl.Text = "N.l.";
            rbNewBubbleNl.Location = new Point(780, 400);
            rbNewBubbleNl.AutoSize = true;
            rbNewBubbleNl.Checked = true;
            rbNewBubbleNl.Font = fontNormal;
            Controls.Add(rbNewBubbleNl);

            btnAddBubble = new Button();
            btnAddBubble.Text = "Přidat bublinu";
            btnAddBubble.Location = new Point(700, 435);
            btnAddBubble.Size = new Size(120, 35);
            btnAddBubble.Click += BtnAddBubble_Click;
            Controls.Add(btnAddBubble);

            btnDeleteBubble = new Button();
            btnDeleteBubble.Text = "Smazat bublinu";
            btnDeleteBubble.Location = new Point(830, 435);
            btnDeleteBubble.Size = new Size(120, 35);
            btnDeleteBubble.Click += BtnDeleteBubble_Click;
            Controls.Add(btnDeleteBubble);

            btnChangeBubbleColor = new Button();
            btnChangeBubbleColor.Text = "Změnit barvu";
            btnChangeBubbleColor.Location = new Point(700, 480);
            btnChangeBubbleColor.Size = new Size(250, 35);
            btnChangeBubbleColor.Click += BtnChangeBubbleColor_Click;
            Controls.Add(btnChangeBubbleColor);

            btnSave = new Button();
            btnSave.Text = "Uložit";
            btnSave.Location = new Point(760, 560);
            btnSave.Size = new Size(90, 35);
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            btnClose = new Button();
            btnClose.Text = "Zavřít";
            btnClose.Location = new Point(860, 560);
            btnClose.Size = new Size(90, 35);
            btnClose.Click += BtnClose_Click;
            Controls.Add(btnClose);
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

        private void LoadBubbleToForm(Bubble selectedBubble)
        {
            bubble = selectedBubble;
            owningLine = bubble.OwningLine;

            int day = bubble.Day <= 0 ? 1 : bubble.Day;
            int month = bubble.Month <= 0 ? 1 : bubble.Month;

            string bubbleDirection = bubble.BubbleDirection == Direction.Left ? "Př.n.l." : "N.l.";

            txtName.Text = string.IsNullOrWhiteSpace(bubble.Name)
                ? NamePlaceholder
                : bubble.Name;

            txtLineName.Text = string.IsNullOrWhiteSpace(owningLine.Name)
                ? LineNamePlaceholder
                : owningLine.Name;

            txtDescription.Text = bubble.Description ?? string.Empty;

            numDay.Value = day;
            numMonth.Value = month;

            lbDay.Text = day.ToString("00");
            lbMonth.Text = month.ToString("00");
            lbYear.Text = bubble.Year.ToString();
            lbPrnlNl.Text = bubbleDirection;

            lbName.Text = GetNameForDisplay();
            lbLineName.Text = GetLineNameForDisplay();

            SetEditModeDate(false);
            SetEditModeName(string.IsNullOrWhiteSpace(bubble.Name));
            SetEditModeLineName(string.IsNullOrWhiteSpace(owningLine.Name));
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || name == NamePlaceholder)
            {
                MessageBox.Show("Zadejte název události.");
                SetEditModeName(true);
                return;
            }

            string lineName = txtLineName.Text.Trim();

            if (string.IsNullOrWhiteSpace(lineName) || lineName == LineNamePlaceholder)
            {
                MessageBox.Show("Zadejte název osy.");
                SetEditModeLineName(true);
                return;
            }

            SaveCurrentBubbleWithoutClosing();

            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            SaveCurrentBubbleWithoutClosing();
            Close();
        }

        private void BtnEditName_Click(object sender, EventArgs e)
        {
            SetEditModeName(!editStateName);
        }

        private void BtnEditLineName_Click(object sender, EventArgs e)
        {
            SetEditModeLineName(!editStateLineName);
        }

        private void BtnEditDate_Click(object sender, EventArgs e)
        {
            SetEditModeDate(!editStateDate);
        }

        private void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetEditModeName(false);
                SaveCurrentBubbleWithoutClosing();
                UpdateBubbleList();

                e.SuppressKeyPress = true;
            }
        }

        private void TxtLineName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetEditModeLineName(false);
                SaveCurrentBubbleWithoutClosing();

                e.SuppressKeyPress = true;
            }
        }

        private void ListBoxBubbles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changingBubble || listBoxBubbles.SelectedIndex < 0)
                return;

            SaveCurrentBubbleWithoutClosing();

            Bubble selectedBubble = owningLine.OwningBubbles[listBoxBubbles.SelectedIndex];

            LoadBubbleToForm(selectedBubble);

            UpdateBubbleList();
            SelectCurrentBubbleInList();
        }

        private void SetEditModeName(bool editing)
        {
            if (!editing)
            {
                string name = txtName.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) || name == NamePlaceholder)
                {
                    MessageBox.Show("Zadejte název události.");

                    editStateName = true;
                    txtName.Visible = true;
                    lbName.Visible = false;

                    txtName.Text = NamePlaceholder;
                    txtName.Focus();
                    txtName.SelectAll();

                    return;
                }

                bubble.Name = name;
                lbName.Text = bubble.Name;
            }

            editStateName = editing;

            txtName.Visible = editing;
            lbName.Visible = !editing;

            if (editing)
            {
                txtName.Focus();
                txtName.SelectAll();
            }
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

                owningLine.Name = lineName;
                lbLineName.Text = owningLine.Name;
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

        private void SetEditModeDate(bool editing)
        {
            if (!editing)
            {
                bubble.Day = (int)numDay.Value;
                bubble.Month = (int)numMonth.Value;

                lbDay.Text = bubble.Day.ToString("00");
                lbMonth.Text = bubble.Month.ToString("00");
            }

            editStateDate = editing;

            lbDay.Visible = !editing;
            lbMonth.Visible = !editing;
            lbSep1.Visible = !editing;
            lbSep2.Visible = !editing;

            numDay.Visible = editing;
            numMonth.Visible = editing;

            if (editing)
            {
                lbYear.Location = new Point(numMonth.Right + 10, numMonth.Location.Y);
                lbPrnlNl.Location = new Point(lbYear.Right + 10, lbYear.Location.Y);
            }
            else
            {
                lbSep1.Location = new Point(lbDay.Right + 2, lbDay.Location.Y);
                lbMonth.Location = new Point(lbSep1.Right + 2, lbDay.Location.Y);
                lbSep2.Location = new Point(lbMonth.Right + 2, lbDay.Location.Y);
                lbYear.Location = new Point(lbSep2.Right + 5, lbDay.Location.Y);
                lbPrnlNl.Location = new Point(lbYear.Right + 10, lbDay.Location.Y);
            }
        }

        private void SaveCurrentBubbleWithoutClosing()
        {
            string name = txtName.Text.Trim();

            bubble.Name = string.IsNullOrWhiteSpace(name) || name == NamePlaceholder
                ? null
                : name;

            string lineName = txtLineName.Text.Trim();

            owningLine.Name = string.IsNullOrWhiteSpace(lineName) || lineName == LineNamePlaceholder
                ? null
                : lineName;

            bubble.Description = txtDescription.Text;

            bubble.Day = (int)numDay.Value;
            bubble.Month = (int)numMonth.Value;
        }

        private void UpdateBubbleList()
        {
            changingBubble = true;

            listBoxBubbles.Items.Clear();

            int indexBubble = 1;

            foreach (Bubble b in owningLine.OwningBubbles)
            {
                string era = b.BubbleDirection == Direction.Left ? "Př.n.l." : "N.l.";

                if (string.IsNullOrWhiteSpace(b.Name))
                    listBoxBubbles.Items.Add($"Bubble {indexBubble} ({b.Year} {era})");
                else
                    listBoxBubbles.Items.Add($"{b.Name} ({b.Year} {era})");

                indexBubble++;
            }

            changingBubble = false;
        }

        private void SelectCurrentBubbleInList()
        {
            changingBubble = true;

            int index = owningLine.OwningBubbles.IndexOf(bubble);

            if (index >= 0 && index < listBoxBubbles.Items.Count)
                listBoxBubbles.SelectedIndex = index;
            else
                listBoxBubbles.ClearSelected();

            changingBubble = false;
        }

        private void BtnAddBubble_Click(object sender, EventArgs e)
        {
            SaveCurrentBubbleWithoutClosing();

            int year = (int)numNewBubbleYear.Value;

            Direction direction = rbNewBubblePrnl.Checked ? Direction.Left : Direction.Right;

            if (!IsBubbleYearValidForLine(year, direction))
            {
                MessageBox.Show("Tento rok nepatří na vybranou osu.");
                return;
            }

            if (BubbleAlreadyExists(year, direction))
            {
                MessageBox.Show("V tomto roce už bublina existuje.");
                return;
            }

            bubbleColorDialog.Color = bubble.BubbleColor;

            if (bubbleColorDialog.ShowDialog() != DialogResult.OK)
                return;

            Bubble newBubble = new Bubble(owningLine, year, direction, bubbleColorDialog.Color);

            owningLine.OwningBubbles.Add(newBubble);

            bubble = newBubble;

            UpdateBubbleList();
            LoadBubbleToForm(newBubble);
            SelectCurrentBubbleInList();
        }

        private void BtnDeleteBubble_Click(object sender, EventArgs e)
        {
            if (bubble == null)
                return;

            DialogResult result = MessageBox.Show(
                "Opravdu chcete smazat tuto bublinu?",
                "Smazání bubliny",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            int currentIndex = owningLine.OwningBubbles.IndexOf(bubble);

            if (currentIndex < 0)
                return;

            owningLine.OwningBubbles.RemoveAt(currentIndex);

            if (owningLine.OwningBubbles.Count == 0)
            {
                Close();
                return;
            }

            if (currentIndex >= owningLine.OwningBubbles.Count)
                currentIndex = owningLine.OwningBubbles.Count - 1;

            Bubble nextBubble = owningLine.OwningBubbles[currentIndex];

            UpdateBubbleList();
            LoadBubbleToForm(nextBubble);
            SelectCurrentBubbleInList();
        }

        private void BtnChangeBubbleColor_Click(object sender, EventArgs e)
        {
            bubbleColorDialog.Color = bubble.BubbleColor;

            if (bubbleColorDialog.ShowDialog() == DialogResult.OK)
                bubble.SetColor(bubbleColorDialog.Color);
        }

        private bool BubbleAlreadyExists(int year, Direction direction)
        {
            foreach (Bubble b in owningLine.OwningBubbles)
            {
                if (b.Year == year && b.BubbleDirection == direction)
                    return true;
            }

            return false;
        }

        private bool IsBubbleYearValidForLine(int year, Direction direction)
        {
            if (owningLine.LineDirectionFrom == Direction.Left &&
                owningLine.LineDirectionTo == Direction.Left)
            {
                if (direction != Direction.Left)
                    return false;

                return year >= owningLine.FromYear && year <= owningLine.ToYear;
            }

            if (owningLine.LineDirectionFrom == Direction.Right &&
                owningLine.LineDirectionTo == Direction.Right)
            {
                if (direction != Direction.Right)
                    return false;

                return year >= owningLine.FromYear && year <= owningLine.ToYear;
            }

            if (owningLine.LineDirectionFrom == Direction.Left &&
                owningLine.LineDirectionTo == Direction.Right)
            {
                if (direction == Direction.Left)
                    return year >= 1 && year <= owningLine.FromYear;

                if (direction == Direction.Right)
                    return year >= 1 && year <= Math.Abs(owningLine.ToYear);
            }

            return false;
        }

        private string GetNameForDisplay()
        {
            if (string.IsNullOrWhiteSpace(bubble.Name))
                return "Bez názvu události";

            return bubble.Name;
        }

        private string GetLineNameForDisplay()
        {
            if (string.IsNullOrWhiteSpace(owningLine.Name))
                return "Bez názvu osy";

            return owningLine.Name;
        }

        private Label CreateLabel(Control parent, string text, Point location, Font font)
        {
            Label lb = new Label();

            lb.Font = font;
            lb.AutoSize = true;
            lb.Location = location;
            lb.Text = text;

            parent.Controls.Add(lb);

            return lb;
        }

        private NumericUpDown CreateNumericUpDown(Control parent, int minimum, int maximum, int value, Point location)
        {
            NumericUpDown num = new NumericUpDown();

            num.Minimum = minimum;
            num.Maximum = maximum;

            if (value < minimum)
                value = minimum;

            if (value > maximum)
                value = maximum;

            num.Value = value;
            num.Location = location;
            num.Width = 70;
            num.Visible = false;

            parent.Controls.Add(num);

            return num;
        }
    }
}