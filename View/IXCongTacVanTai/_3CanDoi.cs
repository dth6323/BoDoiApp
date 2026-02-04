using BoDoiApp.View.VICongTacVanTai;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class _3CanDoi : UserControl
    {
        private float currentFontSize = 11f;

        public _3CanDoi()
        {
            InitializeComponent();
            Load += _3CanDoi_Load;
        }

        private void _3CanDoi_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            AutoScaleMode = AutoScaleMode.None;

            // ROOT
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            Controls.Add(root);

            // SUBTITLE
            root.Controls.Add(new Label
            {
                Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            // TITLE
            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 1);

            // MAIN PANEL
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
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlMain.Controls.Add(mainLayout);

            // HEADER
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(180, 198, 231)
            };
            pnlHeader.Controls.Add(new Label
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
            pnlHeader.Controls.Add(btnClose);

            mainLayout.Controls.Add(pnlHeader, 0, 0);

            // CONTENT
            var content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
            mainLayout.Controls.Add(content, 0, 1);

            // LEFT ARROW
            Button left = CreateArrow("◄");
            left.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _2DuTinhKhoiLuongVanChuyen());
            };
            content.Controls.Add(left, 0, 0);

            // FORM (sửa)
            var form = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10)
            };
            content.Controls.Add(form, 1, 0);

            AddRangeRow(form, "Khả năng của VTB/d là:");
            AddRangeRow(form, "Khả năng của VTB/e là:");
            AddRangeRow(form, "Khả năng của LL dân quân là:");
            AddRangeRow(form, "Xe thồ:", true);
            AddRangeRow(form, "Tổng khả năng vận chuyển:");

            // Kết luận
            var lblKetLuan = new Label
            {
                Text = "* Kết luận:",
                AutoSize = true,
                Font = new Font("Times New Roman", 11),
                Margin = new Padding(20, 20, 0, 5)
            };
            form.Controls.Add(lblKetLuan);

            var txtKetLuan = new TextBox
            {
                Width = 600,
                Height = 60,
                Multiline = true,
                Font = new Font("Times New Roman", 11),
                TextAlign = HorizontalAlignment.Center,
                Text = "(Học viên nhập văn bản)",
                Margin = new Padding(20, 0, 0, 10)
            };
            form.Controls.Add(txtKetLuan);

            // RIGHT ARROW
            Button right = CreateArrow("►");
            right.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _4YdinhVanChuyen());
            };
            content.Controls.Add(right, 2, 0);

            // BUTTONS
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

        private void AddRangeRow(FlowLayoutPanel parent, string labelText, bool hasCount = false)
        {
            var row = new FlowLayoutPanel
            {
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 8)
            };

            row.Controls.Add(new Label
            {
                Text = labelText,
                Width = 230,
                Font = new Font("Times New Roman", 11),
                Margin = new Padding(20, 3, 0, 0)
            });

            if (hasCount)
            {
                row.Controls.Add(new TextBox { Width = 60, Margin = new Padding(10, 0, 5, 0) });
                row.Controls.Add(new Label { Text = "xe", AutoSize = true, Margin = new Padding(5, 3, 20, 0) });
            }

            row.Controls.Add(new Label { Text = "Từ", AutoSize = true, Margin = new Padding(10, 3, 5, 0) });
            row.Controls.Add(new TextBox { Width = 80, Margin = new Padding(5, 0, 5, 0) });
            row.Controls.Add(new Label { Text = "đến", AutoSize = true, Margin = new Padding(10, 3, 5, 0) });
            row.Controls.Add(new TextBox { Width = 80, Margin = new Padding(5, 0, 0, 0) });

            parent.Controls.Add(row);
        }

        private Button CreateArrow(string text) => new Button
        {
            Text = text,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            ForeColor = Color.Red,
            FlatStyle = FlatStyle.Flat
        };

        private Control CreateButton(string text, Color color, bool bold = false)
        {
            var b = new Button
            {
                Text = text,
                Width = 100,
                Height = 35,
                BackColor = color,
                Font = new Font("Times New Roman", bold ? 10 : 11, bold ? FontStyle.Bold : FontStyle.Regular)
            };

            var p = new Panel { Dock = DockStyle.Fill };
            p.Controls.Add(b);
            b.Location = new Point((p.Width - b.Width) / 2, 10);

            p.Resize += (s, e) => b.Location = new Point((p.Width - b.Width) / 2, 10);

            return p;
        }
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