using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.Baovehaucankythuat
{
    public partial class _2BienPhap : UserControl
    {
        private float currentFontSize = 11f;

        public _2BienPhap()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _2BienPhap_Load;
        }

        private void _2BienPhap_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
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
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                Padding = new Padding(10)
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(main, 0, 1);

            main.Controls.Add(MakeHeader("X. Bảo vệ hậu cần - kỹ thuật"), 0, 0);
            main.Controls.Add(MakeHeader("2. Biện pháp"), 0, 1);

            // ===== CONTENT =====
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));
            main.Controls.Add(content, 0, 2);

            // ===== LEFT ARROW =====
            Button btnPrev = new Button
            {
                Dock = DockStyle.Fill,
                Text = "◀",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Red
            };
            btnPrev.Click += (s, e2) =>
            {
                NavigationService.Navigate(new _1DukienTinhHuong());
            };
            content.Controls.Add(btnPrev, 0, 0);

            // ===== TEXT BOX =====
            Panel box = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5, 10, 5, 10)
            };
            content.Controls.Add(box, 1, 0);

            RichTextBox txt = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 12),
                BorderStyle = BorderStyle.None
            };
            box.Controls.Add(txt);

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

            bottom.Controls.Add(MakeBottomButton("Trở về", Color.FromArgb(252, 213, 180), DockStyle.Left), 0, 0);
            bottom.Controls.Add(MakeBottomButton("Trang\nchủ", Color.Yellow, DockStyle.Fill), 1, 0);
            bottom.Controls.Add(MakeBottomButton("Lưu", Color.FromArgb(189, 215, 238), DockStyle.Right), 2, 0);
        }

        private Label MakeHeader(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Button MakeBottomButton(string text, Color color, DockStyle dock)
        {
            return new Button
            {
                Text = text,
                BackColor = color,
                Dock = dock,
                Width = 100,
                Height = 40
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

        private void UpdateFont(Control c)
        {
            c.Font = new Font("Times New Roman", currentFontSize, c.Font.Style);
            foreach (Control child in c.Controls)
                UpdateFont(child);
        }
    }
}
