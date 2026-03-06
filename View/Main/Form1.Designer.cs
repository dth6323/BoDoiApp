using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.root = new TableLayoutPanel();
            this.buttonGrid = new TableLayoutPanel();

            this.btn_kbdl = new Button();
            this.btn_dkkhbdhckt = new Button();
            this.btn_khbdhckt = new Button();
            this.btn_xuatkh = new Button();
            this.btn_xuatdukien = new Button();
            this.btn_thoat = new Button();

            this.root.SuspendLayout();
            this.buttonGrid.SuspendLayout();
            this.SuspendLayout();

            // ================= ROOT =================
            this.root.ColumnCount = 3;
            this.root.RowCount = 3;
            this.root.Dock = DockStyle.Fill;
            this.root.BackColor = Color.WhiteSmoke;

            this.root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.root.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            this.root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            this.root.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.root.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.root.Controls.Add(this.buttonGrid, 1, 1);

            // ================= GRID BUTTON =================
            this.buttonGrid.ColumnCount = 3;
            this.buttonGrid.RowCount = 2;
            this.buttonGrid.AutoSize = true;
            this.buttonGrid.Anchor = AnchorStyles.None;
            this.buttonGrid.BackColor = Color.White;
            this.buttonGrid.Padding = new Padding(20);

            this.buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            this.buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.buttonGrid.Controls.Add(this.btn_kbdl, 0, 0);
            this.buttonGrid.Controls.Add(this.btn_dkkhbdhckt, 1, 0);
            this.buttonGrid.Controls.Add(this.btn_khbdhckt, 2, 0);
            this.buttonGrid.Controls.Add(this.btn_xuatkh, 0, 1);
            this.buttonGrid.Controls.Add(this.btn_xuatdukien, 1, 1);
            this.buttonGrid.Controls.Add(this.btn_thoat, 2, 1);

            // ================= STYLE BUTTON =================
            Font btnFont = new Font("Segoe UI", 11F, FontStyle.Bold);

            void StyleButton(Button btn, Color color)
            {
                btn.Dock = DockStyle.Fill;
                btn.Margin = new Padding(10);
                btn.MinimumSize = new Size(160, 100);
                btn.Font = btnFont;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = color;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.MiddleCenter;
            }

            // ===== Button 1 =====
            this.btn_kbdl.Text = "Khai báo dữ liệu";
            StyleButton(this.btn_kbdl, Color.FromArgb(52, 152, 219));
            this.btn_kbdl.Click += new System.EventHandler(this.btn_kbdl_Click);

            // ===== Button 2 =====
            this.btn_dkkhbdhckt.Text = "Dự kiến kế hoạch\nbảo đảm hậu cần kỹ thuật";
            StyleButton(this.btn_dkkhbdhckt, Color.FromArgb(46, 204, 113));
            this.btn_dkkhbdhckt.Click += new System.EventHandler(this.btn_dkkhbdhckt_Click);

            // ===== Button 3 =====
            this.btn_khbdhckt.Text = "Kế hoạch bảo đảm\nhậu cần kỹ thuật";
            StyleButton(this.btn_khbdhckt, Color.FromArgb(155, 89, 182));
            this.btn_khbdhckt.Click += new System.EventHandler(this.btn_khbdhckt_Click);

            // ===== Button 4 =====
            this.btn_xuatkh.Text = "Xuất báo cáo\nDự kiến";
            StyleButton(this.btn_xuatkh, Color.FromArgb(231, 76, 60));
            this.btn_xuatkh.Click += new System.EventHandler(this.button5_Click);

            // ===== Button 5 =====
            this.btn_xuatdukien.Text = "Xuất báo cáo\nKế hoạch";
            StyleButton(this.btn_xuatdukien, Color.FromArgb(241, 196, 15));
            this.btn_xuatdukien.Click += new System.EventHandler(this.btn_xuatbaocaodukien_Click);

            // ===== Button 6 =====
            this.btn_thoat.Text = "Thông tin người dùng";
            StyleButton(this.btn_thoat, Color.FromArgb(52, 73, 94));
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);

            // ================= FORM =================
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Controls.Add(this.root);
            this.BackColor = Color.White;
            this.Size = new Size(1137, 576);

            this.root.ResumeLayout(false);
            this.buttonGrid.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel root;
        private TableLayoutPanel buttonGrid;

        private Button btn_kbdl;
        private Button btn_dkkhbdhckt;
        private Button btn_khbdhckt;
        private Button btn_xuatkh;
        private Button btn_xuatdukien;
        private Button btn_thoat;
    }
}