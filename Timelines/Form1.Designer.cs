namespace Timelines
{
    partial class Form1
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
            this.btnStartEditor = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartEditor
            // 
            this.btnStartEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartEditor.Location = new System.Drawing.Point(174, 153);
            this.btnStartEditor.Name = "btnStartEditor";
            this.btnStartEditor.Size = new System.Drawing.Size(144, 24);
            this.btnStartEditor.TabIndex = 0;
            this.btnStartEditor.Text = "Nový projekt";
            this.btnStartEditor.UseVisualStyleBackColor = true;
            this.btnStartEditor.Click += new System.EventHandler(this.btnStartEditor_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(174, 192);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(144, 24);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Otevřít";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 392);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnStartEditor);
            this.Name = "Form1";
            this.Text = "TimeLine";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartEditor;
        private System.Windows.Forms.Button btnOpen;
    }
}

