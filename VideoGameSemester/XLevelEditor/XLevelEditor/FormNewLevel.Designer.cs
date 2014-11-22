namespace XLevelEditor
{
    partial class FormNewLevel
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
            this.lblLevelName = new System.Windows.Forms.Label();
            this.tbLevelName = new System.Windows.Forms.TextBox();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.mtbWidth = new System.Windows.Forms.MaskedTextBox();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.mtbHeight = new System.Windows.Forms.MaskedTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMapName = new System.Windows.Forms.Label();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblLevelName
            // 
            this.lblLevelName.AutoSize = true;
            this.lblLevelName.Location = new System.Drawing.Point(10, 12);
            this.lblLevelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLevelName.Name = "lblLevelName";
            this.lblLevelName.Size = new System.Drawing.Size(67, 13);
            this.lblLevelName.TabIndex = 0;
            this.lblLevelName.Text = "Level Name:";
            // 
            // tbLevelName
            // 
            this.tbLevelName.Location = new System.Drawing.Point(80, 10);
            this.tbLevelName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbLevelName.Name = "tbLevelName";
            this.tbLevelName.Size = new System.Drawing.Size(76, 20);
            this.tbLevelName.TabIndex = 1;
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Location = new System.Drawing.Point(16, 61);
            this.lblMapWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(62, 13);
            this.lblMapWidth.TabIndex = 4;
            this.lblMapWidth.Text = "Map Width:";
            // 
            // mtbWidth
            // 
            this.mtbWidth.Location = new System.Drawing.Point(80, 58);
            this.mtbWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mtbWidth.Mask = "0000";
            this.mtbWidth.Name = "mtbWidth";
            this.mtbWidth.Size = new System.Drawing.Size(35, 20);
            this.mtbWidth.TabIndex = 5;
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Location = new System.Drawing.Point(10, 86);
            this.lblMapHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(65, 13);
            this.lblMapHeight.TabIndex = 6;
            this.lblMapHeight.Text = "Map Height:";
            // 
            // mtbHeight
            // 
            this.mtbHeight.Location = new System.Drawing.Point(80, 84);
            this.mtbHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mtbHeight.Mask = "0000";
            this.mtbHeight.Name = "mtbHeight";
            this.mtbHeight.Size = new System.Drawing.Size(35, 20);
            this.mtbHeight.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(10, 115);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 19);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(80, 115);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 19);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(13, 34);
            this.lblMapName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(62, 13);
            this.lblMapName.TabIndex = 2;
            this.lblMapName.Text = "Map Name:";
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(80, 34);
            this.tbMapName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(76, 20);
            this.tbMapName.TabIndex = 3;
            // 
            // FormNewLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 153);
            this.ControlBox = false;
            this.Controls.Add(this.tbMapName);
            this.Controls.Add(this.lblMapName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.mtbHeight);
            this.Controls.Add(this.lblMapHeight);
            this.Controls.Add(this.mtbWidth);
            this.Controls.Add(this.lblMapWidth);
            this.Controls.Add(this.tbLevelName);
            this.Controls.Add(this.lblLevelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormNewLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Level";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLevelName;
        private System.Windows.Forms.TextBox tbLevelName;
        private System.Windows.Forms.Label lblMapWidth;
        private System.Windows.Forms.MaskedTextBox mtbWidth;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.MaskedTextBox mtbHeight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.TextBox tbMapName;
    }
}