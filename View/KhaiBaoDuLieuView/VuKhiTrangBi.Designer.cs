namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class VuKhiTrangBi
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dự kiến kế hoạch đảm bảo hậu cần - kỹ thuật";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 356);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 94);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Trở về";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(336, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 49);
            this.button2.TabIndex = 0;
            this.button2.Text = "Trang chủ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(698, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 49);
            this.button3.TabIndex = 0;
            this.button3.Text = "Lưu";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(73, 82);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(660, 249);
            this.reoGridControl1.TabIndex = 2;
            this.reoGridControl1.Text = "reoGridControl1";
            // 
            // VuKhiTrangBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reoGridControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "VuKhiTrangBi";
            this.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU\t\t\t\t" +
    "\t\t\t\t\t\t\t\t\tPHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHI" +
    "ẾN ĐẤU\t\t\t\t\t\t\t\t\t\t\t\t\t";
            this.Load += new System.EventHandler(this.VuKhiTrangBi_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
    }
}