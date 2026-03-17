using BoDoiApp.DataLayer;
using BoDoiApp.View.Main;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BoDoiApp.View.VIBaoDamSinhHoat
{
    public partial class _1BaoDamAnUong : UserControl
    {
        private float currentFontSize = 11f;
        private readonly BaoDamSinhHoatData dataLayer = new BaoDamSinhHoatData();
        private const string SectionKey = "BaoDamSinhHoat_AnUong";
        private TextBox txtInput;
        private int PART = 0;
        public _1BaoDamAnUong(int part = 0)
        {
            this.PART = part;
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }

        private void _1BaoDamAnUong_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

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
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblContent = new Label
            {
                Text = "1. Bảo đảm ăn uống",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtInput = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical
            };



            // ===== ADD CONTROLS =====
            pnlMain.Controls.Add(txtInput);
            pnlMain.Controls.Add(lblContent);
            pnlMain.Controls.Add(lblHeader);

            // ===== BOTTOM BUTTONS =====
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

            // ===== STYLE CHUNG =====
            Font btnFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // ===== BACK =====
            Button btnBack = new Button
            {
                Text = "Trở về",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e2) => NavigationService.Back();

            // ===== HOME =====
            Button btnHome = new Button
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont
            };
            btnHome.FlatAppearance.BorderSize = 0;

            if (PART == 0)
            {
                btnHome.Text = "Dự kiến";
                btnHome.Click += (s, e2) =>
                {
                    NavigationService.Navigate(() => new FormBaoDamHauCan());
                };
            }
            else
            {
                btnHome.Text = "Kế hoạch";
                btnHome.Click += (s, e2) =>
                {
                    NavigationService.Navigate(() => new FormKeHoach());
                };
            }

            // ===== SAVE / NEXT =====
            Button btnSave = new Button
            {
                Text = "Tiếp",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(25, 135, 84),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont
            };
            btnSave.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, e2) =>
            {
                var txt = pnlMain.Controls.OfType<TextBox>().FirstOrDefault();
                if (txt == null || string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Vui lòng nhập nội dung trước khi lưu!");
                    return;
                }
                var existing = dataLayer.LayThongTin(SectionKey);

                if (string.IsNullOrWhiteSpace(existing))
                    dataLayer.ThemThongTin(txt.Text, SectionKey);
                else
                    dataLayer.CapNhatThongTin(txt.Text, SectionKey);

                NavigationService.Navigate(() => new _2BaoDamMac());
            };

            // ===== ADD =====
            pnlButton.Controls.Add(btnBack, 0, 0);
            pnlButton.Controls.Add(btnHome, 1, 0);
            pnlButton.Controls.Add(btnSave, 2, 0);
            var savedContent = dataLayer.LayThongTin(SectionKey);
            if (!string.IsNullOrWhiteSpace(savedContent))
            {
                txtInput.Text = savedContent;
            }
        }

        // ===== ZOOM CTRL + / - =====
        private float zoomFactor = 1.0f;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                ZoomText(1);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                ZoomText(-1);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ZoomText(float delta)
        {
            currentFontSize = Math.Max(8, currentFontSize + delta);
            txtInput.Font = new Font(
                txtInput.Font.FontFamily,
                currentFontSize,
                txtInput.Font.Style
            );
        }


    }
}
