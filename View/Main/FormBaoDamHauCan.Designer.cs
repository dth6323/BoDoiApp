using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.Main
{
    partial class FormBaoDamHauCan
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel layout;
        private TableLayoutPanel pnlMain;
        private TableLayoutPanel menuLayout;
        private TableLayoutPanel pnlBottom;

        private Label lblTitle;
        private Label lblHeader;

        private Button btnBack;
        private Button btnHome;
        private Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            layout = new TableLayoutPanel();
            pnlMain = new TableLayoutPanel();
            menuLayout = new TableLayoutPanel();
            pnlBottom = new TableLayoutPanel();

            lblTitle = new Label();
            lblHeader = new Label();

            btnBack = new Button();
            btnHome = new Button();
            btnSave = new Button();

            // ===== layout chính =====
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 3;
            layout.ColumnCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            // ===== TITLE =====
            lblTitle.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN - KỸ THUẬT";
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.BackColor = Color.FromArgb(255, 242, 204);
            lblTitle.Font = titleFont;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ===== MAIN =====
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.ColumnCount = 3;
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 900));
            pnlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // ===== MENU LAYOUT =====
            menuLayout.Dock = DockStyle.Top;
            menuLayout.AutoSize = true;
            menuLayout.ColumnCount = 1;

            // HEADER
            lblHeader.Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật";
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Height = 40;
            lblHeader.BackColor = Color.FromArgb(255, 242, 204);
            lblHeader.Font = headerFont;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;

            menuLayout.Controls.Add(lblHeader);

            // ===== MENU DATA =====
            var menus = new (string text, string tag, Color color)[]
            {
                ("I. Đánh giá tình hình", "I_DANH_GIA", Color.FromArgb(226,239,218)),
                ("II. Nhiệm vụ", "II_NHIEM_VU", Color.FromArgb(221,235,247)),
                ("III. Tổ chức sử dụng", "III_TO_CHUC", Color.FromArgb(242,220,219)),
                ("IV. Vũ khí", "IV_VU_KHI", Color.FromArgb(217,225,242)),
                ("V. Vật chất", "V_VAT_CHAT", Color.FromArgb(255,229,204)),
                ("VI. Sinh hoạt", "VI_SINH_HOAT", Color.FromArgb(226,239,218)),
                ("VII. Quân y", "VII_QUAN_Y", Color.FromArgb(222,235,247)),
                ("VIII. Bảo dưỡng", "VIII_BAO_DUONG", Color.FromArgb(242,220,219)),
                ("IX. Vận tải", "IX_VAN_TAI", Color.FromArgb(221,235,247)),
                ("X. Bảo vệ", "X_BAO_VE", Color.FromArgb(217,225,242)),
                ("XI. Chỉ huy", "XI_CHI_HUY", Color.FromArgb(255,229,204)),
                ("Kết luận", "KET_LUAN", Color.Gold)
            };

            // ADD BUTTON (KHÔNG LAG)
            foreach (var m in menus)
            {
                var btn = CreateMenuButton(m.text, m.tag, m.color);
                menuLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
                menuLayout.Controls.Add(btn);
            }

            pnlMain.Controls.Add(menuLayout, 1, 0);

            // ===== BOTTOM =====
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.ColumnCount = 3;
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            btnBack.Text = "Trở về";
            btnBack.Font = btnFont;
            btnBack.Dock = DockStyle.Left;
            btnBack.Click += (s, e) => NavigationService.Back();

            btnHome.Text = "Trang chủ";
            btnHome.Font = btnFont;
            btnHome.BackColor = Color.Yellow;
            btnHome.Dock = DockStyle.Fill;

            btnSave.Text = "Lưu";
            btnSave.Font = btnFont;
            btnSave.Dock = DockStyle.Right;

            pnlBottom.Controls.Add(btnBack, 0, 0);
            pnlBottom.Controls.Add(btnHome, 1, 0);
            pnlBottom.Controls.Add(btnSave, 2, 0);

            // ===== ADD =====
            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(pnlMain, 0, 1);
            layout.Controls.Add(pnlBottom, 0, 2);

            this.Controls.Add(layout);
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.Transparent;

            this.ResumeLayout(false);
        }
    }
}