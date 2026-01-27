using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VIBaoDamSinhHoat
{
    public partial class _3BaoDamONghi : Form
    {
        private float currentFontSize = 11f;

        public _3BaoDamONghi()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void _3BaoDamONghi_Load(object sender, EventArgs e)
        {
            // ===== FORM CONFIG =====
            this.Text = "Phần mềm hỗ trợ tập bài bảo đảm hậu cần";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 500);
            this.MinimumSize = new Size(900, 400);
            this.AutoScaleMode = AutoScaleMode.None;

            // ===== MAIN LAYOUT =====
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            this.Controls.Add(layout);

            // ===== TITLE =====
            Label lblTitle = new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Times New Roman", 12, FontStyle.Bold)
            };
            layout.Controls.Add(lblTitle, 0, 0);

            // ===== CONTENT PANEL =====
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            layout.Controls.Add(pnlMain, 0, 1);

            // ===== HEADER =====
            Label lblHeader = new Label
            {
                Text = "VI. Bảo đảm sinh hoạt",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.FromArgb(217, 225, 242),
                Font = new Font("Times New Roman", 12, FontStyle.Bold)
            };

            Label lblContent = new Label
            {
                Text = "3. Bảo đảm ở, ngủ nghỉ",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Times New Roman", 11)
            };

            // ===== TEXTBOX =====
            TextBox txtInput = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical,
                Text = "(Học viên nhập văn bản)"
            };

            // ===== PANEL MŨI TÊN TRÁI =====
            Panel pnlArrowLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 60
            };

            Button btnPrev = new Button
            {
                Text = "◀",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };
            btnPrev.Click += (s, e2) =>
            {
                new _2BaoDamMac().Show();
                this.Hide();
            };
            pnlArrowLeft.Controls.Add(btnPrev);

            // ===== ADD CONTROLS =====
            pnlMain.Controls.Add(txtInput);
            pnlMain.Controls.Add(lblContent);
            pnlMain.Controls.Add(lblHeader);
            pnlMain.Controls.Add(pnlArrowLeft);

            // ===== BOTTOM BUTTONS =====
            TableLayoutPanel pnlButton = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            layout.Controls.Add(pnlButton, 0, 2);

            Button btnBack = new Button
            {
                Text = "Trở về",
                Anchor = AnchorStyles.Left
            };

            Button btnHome = new Button
            {
                Text = "Trang chủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            };

            Button btnSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.Right
            };

            pnlButton.Controls.Add(btnBack, 0, 0);
            pnlButton.Controls.Add(btnHome, 1, 0);
            pnlButton.Controls.Add(btnSave, 2, 0);
        }

        // ===== ZOOM CTRL + / CTRL - =====
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                currentFontSize++;
                UpdateFontRecursive(this);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                if (currentFontSize > 8)
                    currentFontSize--;
                UpdateFontRecursive(this);
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
