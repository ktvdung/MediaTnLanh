namespace MediaTinLanh
{
    partial class frmTextToPP
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocaltion = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbFonts = new System.Windows.Forms.ComboBox();
            this.cbbSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbStyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường dẫn lưu files:";
            // 
            // txtLocaltion
            // 
            this.txtLocaltion.Location = new System.Drawing.Point(110, 6);
            this.txtLocaltion.Name = "txtLocaltion";
            this.txtLocaltion.Size = new System.Drawing.Size(497, 20);
            this.txtLocaltion.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(12, 79);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(676, 359);
            this.txtContent.TabIndex = 2;
            this.txtContent.Text = "";
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(613, 4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Font chữ:";
            // 
            // cbbFonts
            // 
            this.cbbFonts.FormattingEnabled = true;
            this.cbbFonts.Items.AddRange(new object[] {
            "Calibri",
            "Helvetica CE 35 Thin",
            "Arial",
            "Times New Roman"});
            this.cbbFonts.Location = new System.Drawing.Point(110, 41);
            this.cbbFonts.Name = "cbbFonts";
            this.cbbFonts.Size = new System.Drawing.Size(116, 21);
            this.cbbFonts.TabIndex = 5;
            // 
            // cbbSize
            // 
            this.cbbSize.FormattingEnabled = true;
            this.cbbSize.Location = new System.Drawing.Point(291, 41);
            this.cbbSize.Name = "cbbSize";
            this.cbbSize.Size = new System.Drawing.Size(116, 21);
            this.cbbSize.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cỡ chữ:";
            // 
            // cbbStyle
            // 
            this.cbbStyle.FormattingEnabled = true;
            this.cbbStyle.Items.AddRange(new object[] {
            "In đậm",
            "In nghiêng",
            "Gạch chân"});
            this.cbbStyle.Location = new System.Drawing.Point(471, 41);
            this.cbbStyle.Name = "cbbStyle";
            this.cbbStyle.Size = new System.Drawing.Size(116, 21);
            this.cbbStyle.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kiểu chữ:";
            // 
            // frmTextToPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 440);
            this.Controls.Add(this.cbbStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbFonts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtLocaltion);
            this.Controls.Add(this.label1);
            this.Name = "frmTextToPP";
            this.Text = "frmTextToPP";
            this.Load += new System.EventHandler(this.frmTextToPP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocaltion;
        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbFonts;
        private System.Windows.Forms.ComboBox cbbSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbStyle;
        private System.Windows.Forms.Label label4;
    }
}