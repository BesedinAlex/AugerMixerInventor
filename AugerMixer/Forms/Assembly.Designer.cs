namespace AugerMixer.Forms
{
    partial class Assembly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assembly));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BuildButton = new System.Windows.Forms.Button();
            this.Hold = new System.Windows.Forms.Button();
            this.Bottom = new System.Windows.Forms.Button();
            this.Screw = new System.Windows.Forms.Button();
            this.Top = new System.Windows.Forms.Button();
            this.MainBody = new System.Windows.Forms.Button();
            this.InventorControl = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BuildButton);
            this.groupBox1.Controls.Add(this.Hold);
            this.groupBox1.Controls.Add(this.Bottom);
            this.groupBox1.Controls.Add(this.Screw);
            this.groupBox1.Controls.Add(this.Top);
            this.groupBox1.Controls.Add(this.MainBody);
            this.groupBox1.Controls.Add(this.InventorControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 103);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Меню";
            // 
            // BuildButton
            // 
            this.BuildButton.Image = global::AugerMixer.Properties.Resources.iam_icon;
            this.BuildButton.Location = new System.Drawing.Point(515, 19);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(79, 78);
            this.BuildButton.TabIndex = 6;
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.Build_Click);
            // 
            // Hold
            // 
            this.Hold.Location = new System.Drawing.Point(430, 19);
            this.Hold.Name = "Hold";
            this.Hold.Size = new System.Drawing.Size(79, 78);
            this.Hold.TabIndex = 5;
            this.Hold.Text = "Создание боковых опор";
            this.Hold.UseVisualStyleBackColor = true;
            this.Hold.Click += new System.EventHandler(this.Hold_Click);
            // 
            // Bottom
            // 
            this.Bottom.Location = new System.Drawing.Point(345, 19);
            this.Bottom.Name = "Bottom";
            this.Bottom.Size = new System.Drawing.Size(79, 78);
            this.Bottom.TabIndex = 4;
            this.Bottom.Text = "Создание корпуса для выгрузки материалов";
            this.Bottom.UseVisualStyleBackColor = true;
            this.Bottom.Click += new System.EventHandler(this.Bottom_Click);
            // 
            // Screw
            // 
            this.Screw.Location = new System.Drawing.Point(260, 19);
            this.Screw.Name = "Screw";
            this.Screw.Size = new System.Drawing.Size(79, 78);
            this.Screw.TabIndex = 3;
            this.Screw.Text = "Создание шнека";
            this.Screw.UseVisualStyleBackColor = true;
            this.Screw.Click += new System.EventHandler(this.Auger_Click);
            // 
            // Top
            // 
            this.Top.Location = new System.Drawing.Point(175, 19);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(79, 78);
            this.Top.TabIndex = 2;
            this.Top.Text = "Создание крышки";
            this.Top.UseVisualStyleBackColor = true;
            this.Top.Click += new System.EventHandler(this.Top_Click);
            // 
            // MainBody
            // 
            this.MainBody.Location = new System.Drawing.Point(90, 19);
            this.MainBody.Name = "MainBody";
            this.MainBody.Size = new System.Drawing.Size(79, 78);
            this.MainBody.TabIndex = 1;
            this.MainBody.Text = "Создание конического корпуса";
            this.MainBody.UseVisualStyleBackColor = true;
            this.MainBody.Click += new System.EventHandler(this.MainBody_Click);
            // 
            // InventorControl
            // 
            this.InventorControl.AutoSize = true;
            this.InventorControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InventorControl.Image = global::AugerMixer.Properties.Resources.Inventor_Icon;
            this.InventorControl.Location = new System.Drawing.Point(6, 19);
            this.InventorControl.Name = "InventorControl";
            this.InventorControl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.InventorControl.Size = new System.Drawing.Size(78, 78);
            this.InventorControl.TabIndex = 0;
            this.InventorControl.UseVisualStyleBackColor = true;
            this.InventorControl.Click += new System.EventHandler(this.InventorControl_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AugerMixer.Properties.Resources.Main_Drawing;
            this.pictureBox1.Location = new System.Drawing.Point(12, 121);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(601, 430);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Assembly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 567);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Assembly";
            this.Text = "Планетарно-шнековой смеситель в Inventor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InventorControl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.Button Hold;
        private new System.Windows.Forms.Button Bottom;
        private System.Windows.Forms.Button Screw;
        private new System.Windows.Forms.Button Top;
        private System.Windows.Forms.Button MainBody;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

