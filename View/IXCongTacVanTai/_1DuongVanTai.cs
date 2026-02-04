using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VICongTacVanTai
{
    public partial class _1DuongVanTai : UserControl
    {
        private float currentFontSize = 11f;

        public _1DuongVanTai()
        {
            InitializeComponent();
            this.Load += _1DuongVanTai_Load;
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                // Reset zoom khi quay lại
                currentFontSize = 11f;
                UpdateFontRecursive(this);
            }
        }
        private void _1DuongVanTai_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.AutoScaleMode = AutoScaleMode.None;

            /* ================= ROOT LAYOUT ================= */
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            this.Controls.Add(layout);

            /* ================= TITLE ================= */
            Label lblTitle = new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            layout.Controls.Add(lblTitle, 0, 0);

            /* ================= MAIN PANEL ================= */
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            layout.Controls.Add(pnlMain, 0, 1);

            /* ================= USE TABLELAYOUTPANEL FOR MAIN CONTENT ================= */
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38)); // IX header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // Section
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Content

            pnlMain.Controls.Add(mainLayout);

            /* ================= HEADER IX ================= */
            Label lblHeader = new Label
            {
                Text = "IX. Công tác vận tải",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(180, 198, 231),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0)
            };
            mainLayout.Controls.Add(lblHeader, 0, 0);

            /* ================= SECTION ================= */
            Label lblSection = new Label
            {
                Text = "1. Đường vận tải",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                Padding = new Padding(12, 5, 0, 0)
            };
            mainLayout.Controls.Add(lblSection, 0, 1);

            /* ================= CONTENT WRAPPER ================= */
            // 🔥 Tạo TableLayoutPanel để chia cột cho table và arrow
            TableLayoutPanel pnlContentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(12),
                BackColor = Color.White
            };
            pnlContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pnlContentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));

            mainLayout.Controls.Add(pnlContentLayout, 0, 2);

            /* ================= TABLE ================= */
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Times New Roman", 11),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Width = 60,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Đường vận tải"
            });

            for (int i = 1; i <= 5; i++)
                dgv.Rows.Add(i, "");

            pnlContentLayout.Controls.Add(dgv, 0, 0);

            /* ================= ARROW RIGHT ================= */
            Button btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _2DuTinhKhoiLuongVanChuyen());
            };
            pnlContentLayout.Controls.Add(btnNext, 1, 0);

            /* ================= BOTTOM BUTTONS ================= */
            TableLayoutPanel pnlButton = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                Padding = new Padding(10)
            };
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.Controls.Add(pnlButton, 0, 2);

            Button btnBack = new Button { Text = "Trở về", Anchor = AnchorStyles.Left, Width = 100 };
            Button btnHome = new Button { Text = "Trang chủ", BackColor = Color.Yellow, Anchor = AnchorStyles.None, Width = 100 };
            Button btnSave = new Button { Text = "Lưu", Anchor = AnchorStyles.Right, Width = 100 };

            pnlButton.Controls.Add(btnBack, 0, 0);
            pnlButton.Controls.Add(btnHome, 1, 0);
            pnlButton.Controls.Add(btnSave, 2, 0);
        }

        /* ===== ZOOM CTRL + / - ===== */
        private float zoomFactor = 1.0f;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                zoomFactor += 0.1f;
                this.Scale(new SizeF(1.1f, 1.1f));
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                zoomFactor -= 0.1f;
                this.Scale(new SizeF(0.9f, 0.9f));
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateFontRecursive(Control control)
        {
            control.Font = new Font("Times New Roman", currentFontSize, control.Font.Style);
            foreach (Control child in control.Controls)
                UpdateFontRecursive(child);
        }
    }
}