using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    partial class DuongVanT
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.Label lblDuongVanTai;

        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt3;
        private System.Windows.Forms.TextBox txt4;

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDuongVanTai = new System.Windows.Forms.Label();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.txt4 = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();

            System.Windows.Forms.Panel bottomPanel = new System.Windows.Forms.Panel();

            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // ================= TITLE =================
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Height = 50;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            // ================= GROUPBOX =================
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Height = 220;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Text = "IX. Công tác vận tải";

            // label
            this.lblDuongVanTai.AutoSize = true;
            this.lblDuongVanTai.Location = new System.Drawing.Point(20, 35);
            this.lblDuongVanTai.Text = "1. Đường vận tải";

            // ================= TEXTBOX =================
            int left = 60;
            int width = 1100;

            this.txt1.Location = new System.Drawing.Point(left, 70);
            this.txt1.Width = width;
            this.txt1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.txt2.Location = new System.Drawing.Point(left, 100);
            this.txt2.Width = width;
            this.txt2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.txt3.Location = new System.Drawing.Point(left, 130);
            this.txt3.Width = width;
            this.txt3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.txt4.Location = new System.Drawing.Point(left, 160);
            this.txt4.Width = width;
            this.txt4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // add textbox vào groupbox
            this.groupBox1.Controls.Add(this.lblDuongVanTai);
            this.groupBox1.Controls.Add(this.txt1);
            this.groupBox1.Controls.Add(this.txt2);
            this.groupBox1.Controls.Add(this.txt3);
            this.groupBox1.Controls.Add(this.txt4);

            // ================= BOTTOM PANEL =================
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Height = 60;

            // ================= BUTTON BACK =================
            btnBack.Text = "Trở về";
            btnBack.Size = new Size(100, 40);
            btnBack.Location = new Point(20, 10);
            btnBack.Anchor = AnchorStyles.Left;

            btnHome.Text = "Trang chủ";
            btnHome.Size = new Size(120, 40);
            btnHome.Location = new Point((bottomPanel.Width - btnHome.Width) / 2, 10);
            btnHome.Anchor = AnchorStyles.None;

            btnSave.Text = "Tiếp";
            btnSave.Size = new Size(100, 40);
            btnSave.Location = new Point(bottomPanel.Width - 120, 10);
            btnSave.Anchor = AnchorStyles.Right;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            bottomPanel.Controls.Add(btnBack);
            bottomPanel.Controls.Add(btnHome);
            bottomPanel.Controls.Add(btnSave);

            this.Controls.Add(bottomPanel);

            // ================= ADD CONTROL =================
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(this.lblTitle);

            // ================= USERCONTROL =================
            this.Name = "DuongVanT";
            this.Size = new System.Drawing.Size(1341, 724);
            this.Load += new System.EventHandler(this.DuongVanT_Load);

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}