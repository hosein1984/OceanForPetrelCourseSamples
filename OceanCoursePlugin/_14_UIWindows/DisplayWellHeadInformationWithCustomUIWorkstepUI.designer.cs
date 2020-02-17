namespace OceanCoursePlugin._14_UIWindows
{
    partial class DisplayWellHeadInformationWithCustomUIWorkstepUI
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
            this.WellDropper = new Slb.Ocean.Petrel.UI.DropTarget();
            this.WellPresenter = new Slb.Ocean.Petrel.UI.Controls.PresentationBox();
            this.WellHeadXTextBox = new Slb.Ocean.Petrel.UI.Controls.UnitTextBox();
            this.WellHeadYTextBox = new Slb.Ocean.Petrel.UI.Controls.UnitTextBox();
            this.ApplyButton = new Slb.Ocean.Petrel.UI.Controls.BasicButton();
            this.OkButton = new Slb.Ocean.Petrel.UI.Controls.BasicButton();
            this.CancelButton = new Slb.Ocean.Petrel.UI.Controls.BasicButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WellHeadXUnit = new System.Windows.Forms.Label();
            this.WellHeadYUnit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WellDropper
            // 
            this.WellDropper.AllowDrop = true;
            this.WellDropper.Location = new System.Drawing.Point(229, 60);
            this.WellDropper.Name = "WellDropper";
            this.WellDropper.Size = new System.Drawing.Size(26, 23);
            this.WellDropper.TabIndex = 0;
            // 
            // WellPresenter
            // 
            this.WellPresenter.Location = new System.Drawing.Point(267, 60);
            this.WellPresenter.Name = "WellPresenter";
            this.WellPresenter.Size = new System.Drawing.Size(100, 25);
            this.WellPresenter.TabIndex = 1;
            // 
            // WellHeadXTextBox
            // 
            this.WellHeadXTextBox.Location = new System.Drawing.Point(229, 104);
            this.WellHeadXTextBox.Name = "WellHeadXTextBox";
            this.WellHeadXTextBox.Size = new System.Drawing.Size(100, 22);
            this.WellHeadXTextBox.TabIndex = 2;
            // 
            // WellHeadYTextBox
            // 
            this.WellHeadYTextBox.Location = new System.Drawing.Point(229, 137);
            this.WellHeadYTextBox.Name = "WellHeadYTextBox";
            this.WellHeadYTextBox.Size = new System.Drawing.Size(100, 22);
            this.WellHeadYTextBox.TabIndex = 3;
            // 
            // ApplyButton
            // 
            this.ApplyButton.BackColor = System.Drawing.Color.White;
            this.ApplyButton.Location = new System.Drawing.Point(94, 203);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(83, 32);
            this.ApplyButton.TabIndex = 4;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = false;
            // 
            // OkButton
            // 
            this.OkButton.BackColor = System.Drawing.Color.White;
            this.OkButton.Location = new System.Drawing.Point(200, 203);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(91, 32);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = false;
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(310, 203);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(87, 32);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select a Well To Disaply its Well Head Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Well Head X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Input Well Head";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Well Head Y";
            // 
            // WellHeadXUnit
            // 
            this.WellHeadXUnit.AutoSize = true;
            this.WellHeadXUnit.Location = new System.Drawing.Point(335, 107);
            this.WellHeadXUnit.Name = "WellHeadXUnit";
            this.WellHeadXUnit.Size = new System.Drawing.Size(0, 17);
            this.WellHeadXUnit.TabIndex = 11;
            // 
            // WellHeadYUnit
            // 
            this.WellHeadYUnit.AutoSize = true;
            this.WellHeadYUnit.Location = new System.Drawing.Point(335, 140);
            this.WellHeadYUnit.Name = "WellHeadYUnit";
            this.WellHeadYUnit.Size = new System.Drawing.Size(0, 17);
            this.WellHeadYUnit.TabIndex = 12;
            // 
            // DisplayWellHeadInformationWithCustomUIWorkstepUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WellHeadYUnit);
            this.Controls.Add(this.WellHeadXUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.WellHeadYTextBox);
            this.Controls.Add(this.WellHeadXTextBox);
            this.Controls.Add(this.WellPresenter);
            this.Controls.Add(this.WellDropper);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DisplayWellHeadInformationWithCustomUIWorkstepUI";
            this.Size = new System.Drawing.Size(461, 258);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Slb.Ocean.Petrel.UI.DropTarget WellDropper;
        private Slb.Ocean.Petrel.UI.Controls.PresentationBox WellPresenter;
        private Slb.Ocean.Petrel.UI.Controls.UnitTextBox WellHeadXTextBox;
        private Slb.Ocean.Petrel.UI.Controls.UnitTextBox WellHeadYTextBox;
        private Slb.Ocean.Petrel.UI.Controls.BasicButton ApplyButton;
        private Slb.Ocean.Petrel.UI.Controls.BasicButton OkButton;
        private Slb.Ocean.Petrel.UI.Controls.BasicButton CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label WellHeadXUnit;
        private System.Windows.Forms.Label WellHeadYUnit;
    }
}
