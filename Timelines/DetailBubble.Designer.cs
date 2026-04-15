namespace Timelines
{
    partial class DetailBubble
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnDate = new System.Windows.Forms.Panel();
            this.pbEditPencil = new System.Windows.Forms.PictureBox();
            this.pbEditPencil2 = new System.Windows.Forms.PictureBox();
            this.pnDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditPencil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditPencil2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(574, 82);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(429, 38);
            this.txtName.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(75, 140);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(928, 493);
            this.txtDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(71, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Datum: ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(928, 653);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Uložit";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnDate
            // 
            this.pnDate.Controls.Add(this.pbEditPencil);
            this.pnDate.Location = new System.Drawing.Point(130, 82);
            this.pnDate.Name = "pnDate";
            this.pnDate.Size = new System.Drawing.Size(273, 52);
            this.pnDate.TabIndex = 13;
            // 
            // pbEditPencil
            // 
            this.pbEditPencil.BackgroundImage = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil.Image = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil.InitialImage = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil.Location = new System.Drawing.Point(248, 3);
            this.pbEditPencil.Name = "pbEditPencil";
            this.pbEditPencil.Size = new System.Drawing.Size(15, 15);
            this.pbEditPencil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEditPencil.TabIndex = 3;
            this.pbEditPencil.TabStop = false;
            this.pbEditPencil.Click += new System.EventHandler(this.pbEditPencil_Click);
            // 
            // pbEditPencil2
            // 
            this.pbEditPencil2.BackgroundImage = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil2.Image = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil2.InitialImage = global::Timelines.Properties.Resources.edit_pencil_png;
            this.pbEditPencil2.Location = new System.Drawing.Point(999, 71);
            this.pbEditPencil2.Name = "pbEditPencil2";
            this.pbEditPencil2.Size = new System.Drawing.Size(15, 15);
            this.pbEditPencil2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEditPencil2.TabIndex = 4;
            this.pbEditPencil2.TabStop = false;
            this.pbEditPencil2.Click += new System.EventHandler(this.pbEditPencil2_Click);
            // 
            // DetailBubble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 688);
            this.Controls.Add(this.pbEditPencil2);
            this.Controls.Add(this.pnDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Name = "DetailBubble";
            this.Text = "DetailBubble";
            this.pnDate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEditPencil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditPencil2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnDate;
        private System.Windows.Forms.PictureBox pbEditPencil;
        private System.Windows.Forms.PictureBox pbEditPencil2;
    }
}