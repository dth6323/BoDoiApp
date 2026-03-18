using BoDoiApp.DataLayer;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Drawing;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class _1BaoDuongSuaChua : UserControl
    {
        private float currentFontSize = 11f;

        private readonly BaoDuongSuaChuaData dataLayer = new BaoDuongSuaChuaData();
        private const string SectionKey = "BaoDuongSuaChua_1";
        private TextBox txtInput;
        private int PART = 0;
        public _1BaoDuongSuaChua(int part = 0)
        {
            PART = part;
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

            
            // ===== ADD CONTROLS =====
            pnlMain.Controls.Add(txtInput);
            pnlMain.Controls.Add(lblContent);
            pnlMain.Controls.Add(lblHeader);

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
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White
            };
            btnBack.Click += (s, e2) => NavigationService.Back();

            Button btnHome = new Button
            {
                Text = "Dự Kiến",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White
            };
            if (PART == 1) btnHome.Text = "Kế Hoạch";
            Button btnSave = new Button
            {
                Text = "Tiếp",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(25, 135, 84),
                ForeColor = Color.White
            };
            if (PART == 0)
            {
                btnSave.Click += (s, e2) =>
                {
                    if (dataLayer.TonTai(SectionKey))
                        dataLayer.CapNhatThongTin(txtInput.Text, SectionKey);
                    else
                        dataLayer.ThemThongTin(txtInput.Text, SectionKey);
                    NavigationService.Navigate(() => new _2SuaChua());
                };
            }
            else
            {
                btnSave.Click += (s, e2) =>
                {
                    if (dataLayer.TonTai(SectionKey))
                        dataLayer.CapNhatThongTin(txtInput.Text, SectionKey);
                    else
                        dataLayer.ThemThongTin(txtInput.Text, SectionKey);
                    NavigationService.Navigate(() => new KeHoachSuaChua());
                };
            }

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
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                if (currentFontSize > 8)
                    currentFontSize--;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
