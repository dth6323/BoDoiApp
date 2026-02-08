using BoDoiApp.DataLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class _3CanDoiVaYdinhBaoDam : UserControl
    {
        private float currentFontSize = 11f;

        private readonly BaoDuongSuaChuaData dataLayer = new BaoDuongSuaChuaData();
        private const string KeyCanDoi = "BaoDuongSuaChua_3_CanDoi";
        private const string KeyYDinh = "BaoDuongSuaChua_3_YDinh";

        private TextBox txtCanDoi;
        private TextBox txtYdinh;

        public _3CanDoiVaYdinhBaoDam()
        {
            InitializeComponent();
            this.Load += _3CanDoiVaYdinhBaoDam_Load;
        }

        private void _3CanDoiVaYdinhBaoDam_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            AutoScaleMode = AutoScaleMode.None;
            BuildUI();
        }

        private void BuildUI()
        {
            Controls.Clear();

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            Controls.Add(root);

            // ===== TITLE =====
            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            // ===== MAIN BORDER =====
            Panel border = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            root.Controls.Add(border, 0, 1);

            // ===== HEADER =====
            border.Controls.Add(new Label
            {
                Text = "VIII. Bảo dưỡng, sửa chữa",
                Dock = DockStyle.Top,
                Height = 35,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(198, 224, 180),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            });

            // ===== CONTENT (TABLELAYOUT FIX) =====
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(40, 15, 40, 15),
                ColumnCount = 1,
                RowCount = 4
            };
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));   // lblB
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 140));  // txtCanDoi
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));   // lblC
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // txtYdinh
            border.Controls.Add(content);

            // ===== b. CÂN ĐỐI =====
            Label lblB = new Label
            {
                Text = "2. Sửa chữa\nb. Cân đối",
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Dock = DockStyle.Fill
            };
            content.Controls.Add(lblB, 0, 0);

            txtCanDoi = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical
            };
            content.Controls.Add(txtCanDoi, 0, 1);

            // ===== c. Ý ĐỊNH BẢO ĐẢM =====
            Label lblC = new Label
            {
                Text = "c. Ý định bảo đảm",
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 0)
            };
            content.Controls.Add(lblC, 0, 2);

            txtYdinh = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical
            };
            content.Controls.Add(txtYdinh, 0, 3);

            // ===== LOAD DATA =====
            var canDoiSaved = dataLayer.LayThongTin(KeyCanDoi);
            if (!string.IsNullOrWhiteSpace(canDoiSaved))
                txtCanDoi.Text = canDoiSaved;

            var ydinhSaved = dataLayer.LayThongTin(KeyYDinh);
            if (!string.IsNullOrWhiteSpace(ydinhSaved))
                txtYdinh.Text = ydinhSaved;

            // ===== ARROWS =====
            Button btnLeft = new Button
            {
                Text = "◀",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Size = new Size(60, 140),
                Location = new Point(10, 220)
            };
            btnLeft.Click += (s, e) => NavigationService.Back();
            border.Controls.Add(btnLeft);

            Button btnRight = new Button
            {
                Text = "▶",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Size = new Size(60, 140),
                Anchor = AnchorStyles.Right
            };
            border.Controls.Add(btnRight);

            border.Resize += (s, e) =>
            {
                btnRight.Location = new Point(border.Width - 70, 220);
            };

            // ===== BOTTOM =====
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            root.Controls.Add(bottom, 0, 2);
            Button back = new Button
            {
                Text = "Trở về",
                BackColor = Color.FromArgb(252, 213, 180),
                Anchor = AnchorStyles.Left,
                Width = 100
            }; 
            back.Click += (s, e) => NavigationService.Back();

            bottom.Controls.Add(back, 0, 0);

            bottom.Controls.Add(new Button
            {
                Text = "Trang\nchủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None,
                Width = 100
            }, 1, 0);

            Button btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(189, 215, 238),
                Anchor = AnchorStyles.Right,
                Width = 100
            };
            btnSave.Click += (s, e) =>
            {
                if (dataLayer.TonTai(KeyCanDoi))
                    dataLayer.CapNhatThongTin(txtCanDoi.Text, KeyCanDoi);
                else
                    dataLayer.ThemThongTin(txtCanDoi.Text, KeyCanDoi);

                if (dataLayer.TonTai(KeyYDinh))
                    dataLayer.CapNhatThongTin(txtYdinh.Text, KeyYDinh);
                else
                    dataLayer.ThemThongTin(txtYdinh.Text, KeyYDinh);
            };


            bottom.Controls.Add(btnSave, 2, 0);
        }


        // ===== SAVE HELPER =====
        private void SaveText(string key, string value)
        {
            var exist = dataLayer.LayThongTin(key);
            if (string.IsNullOrWhiteSpace(exist))
                dataLayer.ThemThongTin(value, key);
            else
                dataLayer.CapNhatThongTin(value, key);
        }

        // ===== ZOOM =====
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                currentFontSize++;
                UpdateFont(this);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Subtract))
            {
                if (currentFontSize > 8) currentFontSize--;
                UpdateFont(this);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateFont(Control c)
        {
            c.Font = new Font("Times New Roman", currentFontSize, c.Font.Style);
            foreach (Control child in c.Controls)
                UpdateFont(child);
        }
    }
}
