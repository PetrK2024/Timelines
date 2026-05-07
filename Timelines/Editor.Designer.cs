namespace Timelines
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnCanvas = new System.Windows.Forms.Panel();
            this.numFindYear = new System.Windows.Forms.NumericUpDown();
            this.lbFindYear = new System.Windows.Forms.Label();
            this.lbEra = new System.Windows.Forms.Label();
            this.pnFindYear = new System.Windows.Forms.Panel();
            this.btnFind = new System.Windows.Forms.Button();
            this.rbNl = new System.Windows.Forms.RadioButton();
            this.rbPrnl = new System.Windows.Forms.RadioButton();
            this.pnLine = new System.Windows.Forms.Panel();
            this.btnDeleteLine = new System.Windows.Forms.Button();
            this.btnDetailLine = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbLinePrnlTo = new System.Windows.Forms.RadioButton();
            this.rbLineNlTo = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbLinePrnlFrom = new System.Windows.Forms.RadioButton();
            this.rbLineNlFrom = new System.Windows.Forms.RadioButton();
            this.numLineTo = new System.Windows.Forms.NumericUpDown();
            this.numLineFrom = new System.Windows.Forms.NumericUpDown();
            this.btnCreateLine = new System.Windows.Forms.Button();
            this.pnBubble = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rbBubblePrnl = new System.Windows.Forms.RadioButton();
            this.rbBubbleNl = new System.Windows.Forms.RadioButton();
            this.numBubbleYear = new System.Windows.Forms.NumericUpDown();
            this.btnCreateBubble = new System.Windows.Forms.Button();
            this.btnSaveProject = new System.Windows.Forms.Button();
            this.btnSaveProjectAs = new System.Windows.Forms.Button();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numFindYear)).BeginInit();
            this.pnFindYear.SuspendLayout();
            this.pnLine.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineFrom)).BeginInit();
            this.pnBubble.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBubbleYear)).BeginInit();
            this.SuspendLayout();
            // 
            // pnCanvas
            // 
            this.pnCanvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnCanvas.Location = new System.Drawing.Point(41, 129);
            this.pnCanvas.Name = "pnCanvas";
            this.pnCanvas.Size = new System.Drawing.Size(1948, 747);
            this.pnCanvas.TabIndex = 0;
            // 
            // numFindYear
            // 
            this.numFindYear.Location = new System.Drawing.Point(81, 22);
            this.numFindYear.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFindYear.Name = "numFindYear";
            this.numFindYear.Size = new System.Drawing.Size(120, 20);
            this.numFindYear.TabIndex = 1;
            // 
            // lbFindYear
            // 
            this.lbFindYear.AutoSize = true;
            this.lbFindYear.Location = new System.Drawing.Point(21, 24);
            this.lbFindYear.Name = "lbFindYear";
            this.lbFindYear.Size = new System.Drawing.Size(54, 13);
            this.lbFindYear.TabIndex = 2;
            this.lbFindYear.Text = "Najít rok: ";
            // 
            // lbEra
            // 
            this.lbEra.AutoSize = true;
            this.lbEra.Location = new System.Drawing.Point(975, 899);
            this.lbEra.Name = "lbEra";
            this.lbEra.Size = new System.Drawing.Size(13, 13);
            this.lbEra.TabIndex = 3;
            this.lbEra.Text = "0";
            // 
            // pnFindYear
            // 
            this.pnFindYear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnFindYear.Controls.Add(this.btnFind);
            this.pnFindYear.Controls.Add(this.rbNl);
            this.pnFindYear.Controls.Add(this.rbPrnl);
            this.pnFindYear.Controls.Add(this.numFindYear);
            this.pnFindYear.Controls.Add(this.lbFindYear);
            this.pnFindYear.Location = new System.Drawing.Point(41, 12);
            this.pnFindYear.Name = "pnFindYear";
            this.pnFindYear.Size = new System.Drawing.Size(331, 87);
            this.pnFindYear.TabIndex = 4;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(234, 50);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Vyhledat";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // rbNl
            // 
            this.rbNl.AutoSize = true;
            this.rbNl.Location = new System.Drawing.Point(169, 53);
            this.rbNl.Name = "rbNl";
            this.rbNl.Size = new System.Drawing.Size(41, 17);
            this.rbNl.TabIndex = 4;
            this.rbNl.TabStop = true;
            this.rbNl.Text = "N.l.";
            this.rbNl.UseVisualStyleBackColor = true;
            // 
            // rbPrnl
            // 
            this.rbPrnl.AutoSize = true;
            this.rbPrnl.Location = new System.Drawing.Point(81, 53);
            this.rbPrnl.Name = "rbPrnl";
            this.rbPrnl.Size = new System.Drawing.Size(53, 17);
            this.rbPrnl.TabIndex = 3;
            this.rbPrnl.TabStop = true;
            this.rbPrnl.Text = "Př.n.l.";
            this.rbPrnl.UseVisualStyleBackColor = true;
            // 
            // pnLine
            // 
            this.pnLine.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnLine.Controls.Add(this.btnDeleteLine);
            this.pnLine.Controls.Add(this.btnDetailLine);
            this.pnLine.Controls.Add(this.panel2);
            this.pnLine.Controls.Add(this.panel1);
            this.pnLine.Controls.Add(this.numLineTo);
            this.pnLine.Controls.Add(this.numLineFrom);
            this.pnLine.Controls.Add(this.btnCreateLine);
            this.pnLine.Location = new System.Drawing.Point(378, 12);
            this.pnLine.Name = "pnLine";
            this.pnLine.Size = new System.Drawing.Size(653, 87);
            this.pnLine.TabIndex = 5;
            // 
            // btnDeleteLine
            // 
            this.btnDeleteLine.Location = new System.Drawing.Point(465, 49);
            this.btnDeleteLine.Name = "btnDeleteLine";
            this.btnDeleteLine.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteLine.TabIndex = 10;
            this.btnDeleteLine.Text = "Smazat osu";
            this.btnDeleteLine.UseVisualStyleBackColor = true;
            this.btnDeleteLine.Click += new System.EventHandler(this.btnDeleteLine_Click_1);
            // 
            // btnDetailLine
            // 
            this.btnDetailLine.Location = new System.Drawing.Point(384, 50);
            this.btnDetailLine.Name = "btnDetailLine";
            this.btnDetailLine.Size = new System.Drawing.Size(75, 23);
            this.btnDetailLine.TabIndex = 9;
            this.btnDetailLine.Text = "Detail osy";
            this.btnDetailLine.UseVisualStyleBackColor = true;
            this.btnDetailLine.Click += new System.EventHandler(this.btnDetailLine_Click_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbLinePrnlTo);
            this.panel2.Controls.Add(this.rbLineNlTo);
            this.panel2.Location = new System.Drawing.Point(114, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 37);
            this.panel2.TabIndex = 8;
            // 
            // rbLinePrnlTo
            // 
            this.rbLinePrnlTo.AutoSize = true;
            this.rbLinePrnlTo.Location = new System.Drawing.Point(3, 6);
            this.rbLinePrnlTo.Name = "rbLinePrnlTo";
            this.rbLinePrnlTo.Size = new System.Drawing.Size(53, 17);
            this.rbLinePrnlTo.TabIndex = 6;
            this.rbLinePrnlTo.TabStop = true;
            this.rbLinePrnlTo.Text = "Př.n.l.";
            this.rbLinePrnlTo.UseVisualStyleBackColor = true;
            // 
            // rbLineNlTo
            // 
            this.rbLineNlTo.AutoSize = true;
            this.rbLineNlTo.Location = new System.Drawing.Point(56, 7);
            this.rbLineNlTo.Name = "rbLineNlTo";
            this.rbLineNlTo.Size = new System.Drawing.Size(41, 17);
            this.rbLineNlTo.TabIndex = 7;
            this.rbLineNlTo.TabStop = true;
            this.rbLineNlTo.Text = "N.l.";
            this.rbLineNlTo.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbLinePrnlFrom);
            this.panel1.Controls.Add(this.rbLineNlFrom);
            this.panel1.Location = new System.Drawing.Point(114, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 30);
            this.panel1.TabIndex = 6;
            // 
            // rbLinePrnlFrom
            // 
            this.rbLinePrnlFrom.AutoSize = true;
            this.rbLinePrnlFrom.Location = new System.Drawing.Point(3, 6);
            this.rbLinePrnlFrom.Name = "rbLinePrnlFrom";
            this.rbLinePrnlFrom.Size = new System.Drawing.Size(53, 17);
            this.rbLinePrnlFrom.TabIndex = 4;
            this.rbLinePrnlFrom.TabStop = true;
            this.rbLinePrnlFrom.Text = "Př.n.l.";
            this.rbLinePrnlFrom.UseVisualStyleBackColor = true;
            // 
            // rbLineNlFrom
            // 
            this.rbLineNlFrom.AutoSize = true;
            this.rbLineNlFrom.Location = new System.Drawing.Point(56, 6);
            this.rbLineNlFrom.Name = "rbLineNlFrom";
            this.rbLineNlFrom.Size = new System.Drawing.Size(41, 17);
            this.rbLineNlFrom.TabIndex = 5;
            this.rbLineNlFrom.TabStop = true;
            this.rbLineNlFrom.Text = "N.l.";
            this.rbLineNlFrom.UseVisualStyleBackColor = true;
            // 
            // numLineTo
            // 
            this.numLineTo.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLineTo.Location = new System.Drawing.Point(18, 50);
            this.numLineTo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineTo.Name = "numLineTo";
            this.numLineTo.Size = new System.Drawing.Size(90, 20);
            this.numLineTo.TabIndex = 8;
            // 
            // numLineFrom
            // 
            this.numLineFrom.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLineFrom.Location = new System.Drawing.Point(18, 17);
            this.numLineFrom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLineFrom.Name = "numLineFrom";
            this.numLineFrom.Size = new System.Drawing.Size(90, 20);
            this.numLineFrom.TabIndex = 6;
            // 
            // btnCreateLine
            // 
            this.btnCreateLine.Location = new System.Drawing.Point(239, 50);
            this.btnCreateLine.Name = "btnCreateLine";
            this.btnCreateLine.Size = new System.Drawing.Size(139, 23);
            this.btnCreateLine.TabIndex = 0;
            this.btnCreateLine.Text = "Vytvořit časovou osu";
            this.btnCreateLine.UseVisualStyleBackColor = true;
            this.btnCreateLine.Click += new System.EventHandler(this.btnCreateLine_Click);
            // 
            // pnBubble
            // 
            this.pnBubble.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnBubble.Controls.Add(this.panel5);
            this.pnBubble.Controls.Add(this.numBubbleYear);
            this.pnBubble.Controls.Add(this.btnCreateBubble);
            this.pnBubble.Location = new System.Drawing.Point(1037, 12);
            this.pnBubble.Name = "pnBubble";
            this.pnBubble.Size = new System.Drawing.Size(424, 87);
            this.pnBubble.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rbBubblePrnl);
            this.panel5.Controls.Add(this.rbBubbleNl);
            this.panel5.Location = new System.Drawing.Point(139, 29);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(108, 30);
            this.panel5.TabIndex = 6;
            // 
            // rbBubblePrnl
            // 
            this.rbBubblePrnl.AutoSize = true;
            this.rbBubblePrnl.Location = new System.Drawing.Point(3, 6);
            this.rbBubblePrnl.Name = "rbBubblePrnl";
            this.rbBubblePrnl.Size = new System.Drawing.Size(53, 17);
            this.rbBubblePrnl.TabIndex = 4;
            this.rbBubblePrnl.TabStop = true;
            this.rbBubblePrnl.Text = "Př.n.l.";
            this.rbBubblePrnl.UseVisualStyleBackColor = true;
            // 
            // rbBubbleNl
            // 
            this.rbBubbleNl.AutoSize = true;
            this.rbBubbleNl.Location = new System.Drawing.Point(56, 6);
            this.rbBubbleNl.Name = "rbBubbleNl";
            this.rbBubbleNl.Size = new System.Drawing.Size(41, 17);
            this.rbBubbleNl.TabIndex = 5;
            this.rbBubbleNl.TabStop = true;
            this.rbBubbleNl.Text = "N.l.";
            this.rbBubbleNl.UseVisualStyleBackColor = true;
            // 
            // numBubbleYear
            // 
            this.numBubbleYear.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBubbleYear.Location = new System.Drawing.Point(20, 35);
            this.numBubbleYear.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBubbleYear.Name = "numBubbleYear";
            this.numBubbleYear.Size = new System.Drawing.Size(90, 20);
            this.numBubbleYear.TabIndex = 6;
            // 
            // btnCreateBubble
            // 
            this.btnCreateBubble.Location = new System.Drawing.Point(272, 32);
            this.btnCreateBubble.Name = "btnCreateBubble";
            this.btnCreateBubble.Size = new System.Drawing.Size(139, 23);
            this.btnCreateBubble.TabIndex = 0;
            this.btnCreateBubble.Text = "Vytvořit bublinu";
            this.btnCreateBubble.UseVisualStyleBackColor = true;
            this.btnCreateBubble.Click += new System.EventHandler(this.btnCreateBubble_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.Location = new System.Drawing.Point(1793, 19);
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(105, 23);
            this.btnSaveProject.TabIndex = 11;
            this.btnSaveProject.Text = "Uložit projekt";
            this.btnSaveProject.UseVisualStyleBackColor = true;
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // btnSaveProjectAs
            // 
            this.btnSaveProjectAs.Location = new System.Drawing.Point(1793, 48);
            this.btnSaveProjectAs.Name = "btnSaveProjectAs";
            this.btnSaveProjectAs.Size = new System.Drawing.Size(105, 23);
            this.btnSaveProjectAs.TabIndex = 12;
            this.btnSaveProjectAs.Text = "Uložit projekt jako";
            this.btnSaveProjectAs.UseVisualStyleBackColor = true;
            this.btnSaveProjectAs.Click += new System.EventHandler(this.btnSaveProjectAs_Click);
            // 
            // listBoxLines
            // 
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.Location = new System.Drawing.Point(1467, 13);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.Size = new System.Drawing.Size(320, 82);
            this.listBoxLines.TabIndex = 13;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1910, 879);
            this.Controls.Add(this.listBoxLines);
            this.Controls.Add(this.btnSaveProjectAs);
            this.Controls.Add(this.btnSaveProject);
            this.Controls.Add(this.pnBubble);
            this.Controls.Add(this.pnLine);
            this.Controls.Add(this.pnFindYear);
            this.Controls.Add(this.lbEra);
            this.Controls.Add(this.pnCanvas);
            this.Name = "Editor";
            this.Text = "Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numFindYear)).EndInit();
            this.pnFindYear.ResumeLayout(false);
            this.pnFindYear.PerformLayout();
            this.pnLine.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLineFrom)).EndInit();
            this.pnBubble.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBubbleYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnCanvas;
        private System.Windows.Forms.NumericUpDown numFindYear;
        private System.Windows.Forms.Label lbFindYear;
        private System.Windows.Forms.Label lbEra;
        private System.Windows.Forms.Panel pnFindYear;
        private System.Windows.Forms.RadioButton rbNl;
        private System.Windows.Forms.RadioButton rbPrnl;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Panel pnLine;
        private System.Windows.Forms.Button btnCreateLine;
        private System.Windows.Forms.NumericUpDown numLineTo;
        private System.Windows.Forms.NumericUpDown numLineFrom;
        private System.Windows.Forms.RadioButton rbLineNlTo;
        private System.Windows.Forms.RadioButton rbLinePrnlTo;
        private System.Windows.Forms.RadioButton rbLineNlFrom;
        private System.Windows.Forms.RadioButton rbLinePrnlFrom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnBubble;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbBubblePrnl;
        private System.Windows.Forms.RadioButton rbBubbleNl;
        private System.Windows.Forms.NumericUpDown numBubbleYear;
        private System.Windows.Forms.Button btnCreateBubble;
        private System.Windows.Forms.Button btnDetailLine;
        private System.Windows.Forms.Button btnDeleteLine;
        private System.Windows.Forms.Button btnSaveProject;
        private System.Windows.Forms.Button btnSaveProjectAs;
        private System.Windows.Forms.ListBox listBoxLines;
    }
}