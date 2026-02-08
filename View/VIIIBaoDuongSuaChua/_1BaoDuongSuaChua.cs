using BoDoiApp.DataLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class _1BaoDuongSuaChua : UserControl
    {
        private float currentFontSize = 11f;

        private readonly BaoDuongSuaChuaData dataLayer = new BaoDuongSuaChuaData();
        private const string SectionKey = "BaoDuongSuaChua_1";
        private TextBox txtInput;

        public _1BaoDuongSuaChua()
        {
            InitializeComponent();
            this.Load += _1BaoDuongSuaChua_Load;
        }

        private void _1BaoDuongSuaChua_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.AutoScaleMode = AutoScaleMode.None;
            this.Controls.Clear();

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
                Text = "VIII. Bảo dưỡng, sửa chữa",
                Dock = DockStyle.Top,
                Height = 35,
                BackColor = Color.FromArgb(198, 224, 180),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblContent = new Label
            {
                Text = "1. Bảo dưỡng",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // ===== TEXTBOX =====
            txtInput = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical
            };

            // ===== LOAD DATA =====
            var savedContent = dataLayer.LayThongTin(SectionKey);
            if (!string.IsNullOrWhiteSpace(savedContent))
            {
                txtInput.Text = savedContent;
            }

            // ===== ARROW RIGHT =====
            Panel pnlArrowRight = new Panel
            {
                Dock = DockStyle.Right,
                Width = 60
            };

            Button btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };
            btnNext.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _2SuaChua());
            };
            pnlArrowRight.Controls.Add(btnNext);

            // ===== ADD CONTROLS =====
            pnlMain.Controls.Add(txtInput);
            pnlMain.Controls.Add(lblContent);
            pnlMain.Controls.Add(lblHeader);
            pnlMain.Controls.Add(pnlArrowRight);

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
                BackColor = Color.FromArgb(252, 213, 180),
                Anchor = AnchorStyles.Left
            };
            btnBack.Click += (s, e2) => NavigationService.Back();

            Button btnHome = new Button
            {
                Text = "Trang\nchủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            };

            Button btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(189, 215, 238),
                Anchor = AnchorStyles.Right
            };
            btnSave.Click += (s, e2) =>
            {
                if (dataLayer.TonTai(SectionKey))
                    dataLayer.CapNhatThongTin(txtInput.Text, SectionKey);
                else
                    dataLayer.ThemThongTin(txtInput.Text, SectionKey);
            };

            pnlButton.Controls.Add(btnBack, 0, 0);
            pnlButton.Controls.Add(btnHome, 1, 0);
            pnlButton.Controls.Add(btnSave, 2, 0);
        }

        // ===== ZOOM CTRL + / - =====
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
