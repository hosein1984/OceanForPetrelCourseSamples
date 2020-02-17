namespace OceanCoursePlugin._16_UISettingDialogPage
{
    partial class XYZObjectSettingPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.RadiusTextBox = new System.Windows.Forms.TextBox();
            this.ZTextBox = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z";
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(161, 84);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(100, 22);
            this.XTextBox.TabIndex = 3;
            // 
            // RadiusTextBox
            // 
            this.RadiusTextBox.Location = new System.Drawing.Point(161, 168);
            this.RadiusTextBox.Name = "RadiusTextBox";
            this.RadiusTextBox.Size = new System.Drawing.Size(100, 22);
            this.RadiusTextBox.TabIndex = 4;
            // 
            // ZTextBox
            // 
            this.ZTextBox.Location = new System.Drawing.Point(161, 140);
            this.ZTextBox.Name = "ZTextBox";
            this.ZTextBox.Size = new System.Drawing.Size(100, 22);
            this.ZTextBox.TabIndex = 5;
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(161, 112);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(100, 22);
            this.YTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Radius";
            // 
            // XYZObjectSettingPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.ZTextBox);
            this.Controls.Add(this.RadiusTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "XYZObjectSettingPage";
            this.Size = new System.Drawing.Size(328, 352);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.TextBox RadiusTextBox;
        private System.Windows.Forms.TextBox ZTextBox;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.Label label4;
    }
}
