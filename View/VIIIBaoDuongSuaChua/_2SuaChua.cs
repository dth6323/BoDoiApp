using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class _2SuaChua : UserControl
    {
        private float currentFontSize = 11f;

        public _2SuaChua()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _2SuaChua_Load;
        }

        private void _2SuaChua_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT (1280x720 theo MainForm) =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
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

            // ===== MAIN =====
            Panel main = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            root.Controls.Add(main, 0, 1);

            main.Controls.Add(MakeHeader("VIII. Bảo dưỡng, sửa chữa", true));
            main.Controls.Add(MakeHeader("2. Sửa chữa", false));
            main.Controls.Add(MakeHeader("a. Dự kiến tỷ lệ vũ khí trang bị kỹ thuật hư hỏng", false));

            // ===== CONTENT =====
            Panel content = new Panel { Dock = DockStyle.Fill };
            main.Controls.Add(content);

            // ===== TABLE =====
            TableLayoutPanel table = CreateTable();
            table.Location = new Point(120, 100);
            content.Controls.Add(table);

            // ===== LEFT ARROW =====
            Button btnPrev = new Button
            {
                Text = "◀",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Size = new Size(60, 140),
                Location = new Point(20, 250)
            };
            btnPrev.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _1BaoDuongSuaChua());
            };
            content.Controls.Add(btnPrev);

            // ===== RIGHT ARROW =====
            Button btnNext = new Button
            {
                Text = "▶",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Size = new Size(60, 140),
                Anchor = AnchorStyles.Right
            };
            content.Controls.Add(btnNext);

            content.Resize += (s, e2) =>
            {
                btnNext.Location = new Point(content.Width - 80, 250);
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

            bottom.Controls.Add(new Button
            {
                Text = "Trở về",
                BackColor = Color.FromArgb(252, 213, 180),
                Anchor = AnchorStyles.Left
            }, 0, 0);

            bottom.Controls.Add(new Button
            {
                Text = "Trang\nchủ",
                BackColor = Color.Yellow,
                Anchor = AnchorStyles.None
            }, 1, 0);

            bottom.Controls.Add(new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(189, 215, 238),
                Anchor = AnchorStyles.Right
            }, 2, 0);
        }

        // ===== TABLE =====
        private TableLayoutPanel CreateTable()
        {
            TableLayoutPanel tbl = new TableLayoutPanel
            {
                Size = new Size(1000, 260),
                ColumnCount = 10,
                RowCount = 7,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            for (int i = 0; i < 6; i++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 6));

            AddHeader(tbl, "TT", 0, 0, 1, 2);
            AddHeader(tbl, "Loại VKTBKT", 1, 0, 1, 2);
            AddHeader(tbl, "Số lượng", 2, 0, 1, 2);
            AddHeader(tbl, "Tỷ lệ (%)", 3, 0, 1, 2);
            AddHeader(tbl, "Tổng số TBKT hư hỏng", 4, 0, 6, 1);

            string[] sub = { "Nhẹ", "Vừa", "Nặng", "Hủy", "+", "" };
            for (int i = 0; i < 6; i++)
                AddHeader(tbl, sub[i], 4 + i, 1, 1, 1);

            AddRow(tbl, 2, "1", "SMPK 12,7", "9", "8", "1", "0", "0", "0", "1");
            AddRow(tbl, 3, "2", "SPG-9", "9", "10", "1", "0", "0", "0", "1");
            AddRow(tbl, 4, "3", "Súng ngắn", "36", "4", "1", "0", "0", "0", "1");
            AddRow(tbl, 5, "4", "Tiểu liên", "439", "4", "9", "5", "3", "1", "18");
            AddRow(tbl, 6, "5", "Trung liên", "27", "4", "1", "0", "0", "0", "1");

            return tbl;
        }

        private void AddHeader(TableLayoutPanel tbl, string text, int col, int row, int colSpan, int rowSpan)
        {
            Label lbl = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(252, 228, 214)
            };
            tbl.Controls.Add(lbl, col, row);
            tbl.SetColumnSpan(lbl, colSpan);
            tbl.SetRowSpan(lbl, rowSpan);
        }

        private void AddRow(TableLayoutPanel tbl, int row, params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
                tbl.Controls.Add(new Label
                {
                    Text = values[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(221, 235, 247)
                }, i, row);
        }

        private Label MakeHeader(string text, bool green)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                BackColor = green ? Color.FromArgb(198, 224, 180) : Color.Transparent
            };
        }

        // ===== ZOOM CTRL =====
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
