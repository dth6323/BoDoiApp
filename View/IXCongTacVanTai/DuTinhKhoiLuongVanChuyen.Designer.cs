using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    partial class DuTinhKhoiLuongVanChuyen
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private GroupBox groupBox1;

        private Label lblSection;
        private Label lblTong;
        private Label lblChuanBi;
        private Label lblChienDau;

        public TextBox txtTong;
        public TextBox txtTongVCHC;
        public TextBox txtTongVCKT;

        public TextBox txtChuanBi;
        public TextBox txtChuanBiVCHC;
        public TextBox txtChuanBiVCKT;

        public TextBox txtChienDau;
        public TextBox txtChienDauVCHC;
        public TextBox txtChienDauVCKT;

        private Button btnBack;
        private Button btnHome;
        private Button btnSave;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.lblChuanBi = new System.Windows.Forms.Label();
            this.lblChienDau = new System.Windows.Forms.Label();
            this.txtTong = new System.Windows.Forms.TextBox();
            this.txtTongVCHC = new System.Windows.Forms.TextBox();
            this.txtTongVCKT = new System.Windows.Forms.TextBox();
            this.txtChuanBi = new System.Windows.Forms.TextBox();
            this.txtChuanBiVCHC = new System.Windows.Forms.TextBox();
            this.txtChuanBiVCKT = new System.Windows.Forms.TextBox();
            this.txtChienDau = new System.Windows.Forms.TextBox();
            this.txtChienDauVCHC = new System.Windows.Forms.TextBox();
            this.txtChienDauVCKT = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(850, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSection);
            this.groupBox1.Controls.Add(this.lblTong);
            this.groupBox1.Controls.Add(this.lblChuanBi);
            this.groupBox1.Controls.Add(this.lblChienDau);
            this.groupBox1.Controls.Add(this.txtTong);
            this.groupBox1.Controls.Add(this.txtTongVCHC);
            this.groupBox1.Controls.Add(this.txtTongVCKT);
            this.groupBox1.Controls.Add(this.txtChuanBi);
            this.groupBox1.Controls.Add(this.txtChuanBiVCHC);
            this.groupBox1.Controls.Add(this.txtChuanBiVCKT);
            this.groupBox1.Controls.Add(this.txtChienDau);
            this.groupBox1.Controls.Add(this.txtChienDauVCHC);
            this.groupBox1.Controls.Add(this.txtChienDauVCKT);
            this.groupBox1.Location = new System.Drawing.Point(40, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 230);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IX. Công tác vận tải";
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(20, 25);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(166, 13);
            this.lblSection.TabIndex = 0;
            this.lblSection.Text = "2. Dự tính khối lượng vận chuyển";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Location = new System.Drawing.Point(20, 60);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(164, 13);
            this.lblTong.TabIndex = 1;
            this.lblTong.Text = "Khối lượng vận chuyển toàn trận:";
            // 
            // lblChuanBi
            // 
            this.lblChuanBi.AutoSize = true;
            this.lblChuanBi.Location = new System.Drawing.Point(40, 100);
            this.lblChuanBi.Name = "lblChuanBi";
            this.lblChuanBi.Size = new System.Drawing.Size(109, 13);
            this.lblChuanBi.TabIndex = 2;
            this.lblChuanBi.Text = "+ Giai đoạn chuẩn bị:";
            // 
            // lblChienDau
            // 
            this.lblChienDau.AutoSize = true;
            this.lblChienDau.Location = new System.Drawing.Point(40, 140);
            this.lblChienDau.Name = "lblChienDau";
            this.lblChienDau.Size = new System.Drawing.Size(116, 13);
            this.lblChienDau.TabIndex = 3;
            this.lblChienDau.Text = "+ Giai đoạn chiến đấu:";
            // 
            // txtTong
            // 
            this.txtTong.Location = new System.Drawing.Point(220, 60);
            this.txtTong.Name = "txtTong";
            this.txtTong.Size = new System.Drawing.Size(100, 20);
            this.txtTong.TabIndex = 4;
            // 
            // txtTongVCHC
            // 
            this.txtTongVCHC.Location = new System.Drawing.Point(380, 60);
            this.txtTongVCHC.Name = "txtTongVCHC";
            this.txtTongVCHC.Size = new System.Drawing.Size(100, 20);
            this.txtTongVCHC.TabIndex = 5;
            // 
            // txtTongVCKT
            // 
            this.txtTongVCKT.Location = new System.Drawing.Point(540, 60);
            this.txtTongVCKT.Name = "txtTongVCKT";
            this.txtTongVCKT.Size = new System.Drawing.Size(100, 20);
            this.txtTongVCKT.TabIndex = 6;
            // 
            // txtChuanBi
            // 
            this.txtChuanBi.Location = new System.Drawing.Point(220, 100);
            this.txtChuanBi.Name = "txtChuanBi";
            this.txtChuanBi.Size = new System.Drawing.Size(100, 20);
            this.txtChuanBi.TabIndex = 7;
            // 
            // txtChuanBiVCHC
            // 
            this.txtChuanBiVCHC.Location = new System.Drawing.Point(380, 100);
            this.txtChuanBiVCHC.Name = "txtChuanBiVCHC";
            this.txtChuanBiVCHC.Size = new System.Drawing.Size(100, 20);
            this.txtChuanBiVCHC.TabIndex = 8;
            // 
            // txtChuanBiVCKT
            // 
            this.txtChuanBiVCKT.Location = new System.Drawing.Point(540, 100);
            this.txtChuanBiVCKT.Name = "txtChuanBiVCKT";
            this.txtChuanBiVCKT.Size = new System.Drawing.Size(100, 20);
            this.txtChuanBiVCKT.TabIndex = 9;
            // 
            // txtChienDau
            // 
            this.txtChienDau.Location = new System.Drawing.Point(220, 140);
            this.txtChienDau.Name = "txtChienDau";
            this.txtChienDau.Size = new System.Drawing.Size(100, 20);
            this.txtChienDau.TabIndex = 10;
            // 
            // txtChienDauVCHC
            // 
            this.txtChienDauVCHC.Location = new System.Drawing.Point(380, 140);
            this.txtChienDauVCHC.Name = "txtChienDauVCHC";
            this.txtChienDauVCHC.Size = new System.Drawing.Size(100, 20);
            this.txtChienDauVCHC.TabIndex = 11;
            // 
            // txtChienDauVCKT
            // 
            this.txtChienDauVCKT.Location = new System.Drawing.Point(540, 140);
            this.txtChienDauVCKT.Name = "txtChienDauVCKT";
            this.txtChienDauVCKT.Size = new System.Drawing.Size(100, 20);
            this.txtChienDauVCKT.TabIndex = 12;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(40, 320);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(90, 30);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "← Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(140, 320);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(90, 30);
            this.btnHome.TabIndex = 3;
            this.btnHome.Text = "🏠 Home";
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(650, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // DuTinhKhoiLuongVanChuyen
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.btnSave);
            this.Name = "DuTinhKhoiLuongVanChuyen";
            this.Size = new System.Drawing.Size(850, 400);
            this.Load += new System.EventHandler(this.DuTinhKhoiLuongVanChuyen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}