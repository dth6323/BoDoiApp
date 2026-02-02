using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class _4YdinhVanChuyen : UserControl
    {
        private float currentFontSize = 11f;
        private float zoomFactor = 1.0f;

        public _4YdinhVanChuyen()
        {
            InitializeComponent();
            Load += _4YdinhVanChuyen_Load;
        }

        private void _4YdinhVanChuyen_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            AutoScaleMode = AutoScaleMode.None;
            Controls.Clear();

            /* ================= ROOT ================= */
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(root);

            /* ================= SUB TITLE ================= */
            root.Controls.Add(new Label
            {
                Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            /* ================= TITLE ================= */
            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 1);

            /* ================= MAIN ================= */
            var pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            root.Controls.Add(pnlMain, 0, 2);

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlMain.Controls.Add(mainLayout);

            /* ================= LEFT ARROW ================= */
            var btnBack = CreateArrow("◄");
            btnBack.Click += (s, ev) =>
            {
                NavigationService.Navigate(new _3CanDoi());
            };
            mainLayout.Controls.Add(btnBack, 0, 0);

            /* ================= CONTENT ================= */
            var contentPanel = new Panel { Dock = DockStyle.Fill };
            mainLayout.Controls.Add(contentPanel, 1, 0);

            var contentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            contentPanel.Controls.Add(contentLayout);

            /* ===== HEADER IX ===== */
            var header = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(180, 198, 231)
            };

            header.Controls.Add(new Label
            {
                Text = "IX. Công tác vận tải",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                Padding = new Padding(12, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            });

            var btnClose = new Button
            {
                Text = "X",
                Dock = DockStyle.Right,
                Width = 30,
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            header.Controls.Add(btnClose);

            contentLayout.Controls.Add(header, 0, 0);

            /* ===== SECTION TITLE ===== */
            contentLayout.Controls.Add(new Label
            {
                Text = "4. Ý định vận chuyển",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 0),
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 1);

            /* ===== TEXT AREA ===== */
            var txtYdinh = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Font = new Font("Times New Roman", 11),
                Text = "(Học viên nhập văn bản)",
                Margin = new Padding(20, 10, 20, 20),
                ScrollBars = ScrollBars.Vertical
            };
            contentLayout.Controls.Add(txtYdinh, 0, 2);

            /* ================= RIGHT EMPTY ================= */
            mainLayout.Controls.Add(new Panel(), 2, 0);

            /* ================= BOTTOM BUTTONS ================= */
            var buttons = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                Padding = new Padding(10)
            };
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            buttons.Controls.Add(CreateButton("Trở về", Color.FromArgb(255, 200, 150)), 0, 0);
            buttons.Controls.Add(CreateButton("Trang\nchủ", Color.Yellow, true), 1, 0);
            buttons.Controls.Add(CreateButton("Lưu", Color.FromArgb(173, 216, 230)), 2, 0);

            root.Controls.Add(buttons, 0, 3);
        }

        /* ================= COMMON ================= */
        private Button CreateArrow(string text) => new Button
        {
            Text = text,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 36, FontStyle.Bold),
            ForeColor = Color.Red,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.Transparent,
            FlatAppearance = { BorderSize = 0 }
        };

        private Control CreateButton(string text, Color color, bool bold = false)
        {
            var b = new Button
            {
                Text = text,
                Width = 120,
                Height = 40,
                BackColor = color,
                Font = new Font("Times New Roman", bold ? 12 : 11,
                    bold ? FontStyle.Bold : FontStyle.Regular)
            };

            var p = new Panel { Dock = DockStyle.Fill };
            p.Controls.Add(b);
            p.Resize += (s, e) =>
                b.Location = new Point((p.Width - b.Width) / 2, (p.Height - b.Height) / 2);

            return p;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                zoomFactor += 0.1f;
                Scale(new SizeF(1.1f, 1.1f));
                return true;
            }
            if (keyData == (Keys.Control | Keys.Subtract))
            {
                zoomFactor -= 0.1f;
                Scale(new SizeF(0.9f, 0.9f));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
