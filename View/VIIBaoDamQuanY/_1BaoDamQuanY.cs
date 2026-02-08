using BoDoiApp.DataLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    public partial class _1BaoDamQuanY : UserControl
    {
        private float currentFontSize = 11f;

        private readonly BaoDamQuanYData dataLayer = new BaoDamQuanYData();
        private const string SectionKey = "BaoDamQuanY_1";
        private TextBox txtInput;

        public _1BaoDamQuanY()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            AutoScaleMode = AutoScaleMode.None;
        }

        private void _1BaoDamQuanY_Load(object sender, EventArgs e)
        {
            Controls.Clear();

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            Controls.Add(root);

            // ===== TITLE =====
            root.Controls.Add(new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            // ===== MAIN =====
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            root.Controls.Add(main, 0, 1);

            // ===== CONTENT =====
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            content.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
            content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.Controls.Add(content, 0, 0);

            content.Controls.Add(CreateHeader("VII. Bảo đảm quân y", 35), 0, 0);
            content.Controls.Add(CreateHeader("1. Dự kiến tỷ lệ thương, bệnh binh", 30, false), 0, 1);

            TableLayoutPanel table = CreateTable();
            table.Dock = DockStyle.Fill;
            content.Controls.Add(table, 0, 2);

            // ===== TEXT INPUT (LƯU + LOAD) =====
            txtInput = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 11),
                ScrollBars = ScrollBars.Vertical
            };
            content.Controls.Add(txtInput, 0, 3);

            // ===== LOAD DATA =====
            var savedContent = dataLayer.LayThongTin(SectionKey);
            if (!string.IsNullOrWhiteSpace(savedContent))
            {
                txtInput.Text = savedContent;
            }

            // ===== ARROW =====
            Button btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };
            btnNext.Click += (s, e2) =>
                MessageBox.Show("Sang mục VII.2 (chưa tạo)");
            main.Controls.Add(btnNext, 1, 0);

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

            bottom.Controls.Add(new Button { Text = "Trở về", Anchor = AnchorStyles.Left }, 0, 0);
            bottom.Controls.Add(new Button { Text = "Trang chủ", BackColor = Color.Yellow }, 1, 0);

            Button btnSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.Right
            };
            btnSave.Click += (s, e2) =>
            {
                var contentText = txtInput.Text;
                var existing = dataLayer.LayThongTin(SectionKey);

                if (string.IsNullOrWhiteSpace(existing))
                    dataLayer.ThemThongTin(contentText, SectionKey);
                else
                    dataLayer.CapNhatThongTin(contentText, SectionKey);
            };

            bottom.Controls.Add(btnSave, 2, 0);
        }

        // ===== TABLE =====
        TableLayoutPanel CreateTable()
        {
            TableLayoutPanel tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 9,
                RowCount = 6,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
            for (int i = 0; i < 7; i++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));

            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            for (int i = 0; i < 4; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            tbl.Controls.Add(MakeHeader("TT"), 0, 0);
            tbl.Controls.Add(MakeHeader("Nội dung"), 1, 0);
            tbl.Controls.Add(MakeHeader("Quân số"), 2, 0);
            tbl.Controls.Add(MakeHeader("TB"), 3, 0);
            tbl.Controls.Add(MakeHeader("TB"), 4, 0);
            tbl.Controls.Add(MakeHeader("TBBH"), 5, 0);
            tbl.Controls.Add(MakeHeader("TBBH"), 6, 0);
            tbl.Controls.Add(MakeHeader("BB"), 7, 0);
            tbl.Controls.Add(MakeHeader("BB"), 8, 0);

            tbl.Controls.Add(MakeHeader("%QS"), 3, 1);
            tbl.Controls.Add(MakeHeader("Số người"), 4, 1);
            tbl.Controls.Add(MakeHeader("%QS"), 5, 1);
            tbl.Controls.Add(MakeHeader("Số người"), 6, 1);
            tbl.Controls.Add(MakeHeader("%QS"), 7, 1);
            tbl.Controls.Add(MakeHeader("Số người"), 8, 1);

            AddRow(tbl, 2, "1", "Toàn trận");
            AddRow(tbl, 3, "", "Hướng chủ yếu");
            AddRow(tbl, 4, "", "Hướng thứ yếu");
            AddRow(tbl, 5, "2", "Ngày cao nhất");

            return tbl;
        }

        void AddRow(TableLayoutPanel tbl, int row, string tt, string text)
        {
            tbl.Controls.Add(new Label { Text = tt, TextAlign = ContentAlignment.MiddleCenter }, 0, row);
            tbl.Controls.Add(new Label { Text = text, TextAlign = ContentAlignment.MiddleLeft }, 1, row);
        }

        Label MakeHeader(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(217, 225, 242)
            };
        }

        Label CreateHeader(string text, int height, bool blue = true)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = height,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                BackColor = blue ? Color.FromArgb(217, 225, 242) : Color.Transparent
            };
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

        void UpdateFont(Control c)
        {
            c.Font = new Font("Times New Roman", currentFontSize, c.Font.Style);
            foreach (Control child in c.Controls)
                UpdateFont(child);
        }
    }
}
