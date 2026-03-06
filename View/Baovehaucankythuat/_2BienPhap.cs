using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.Baovehaucankythuat
{
    public partial class _2BienPhap : UserControl
    {
        private float currentFontSize = 11f;

        // ===== CONTROLS =====
        private Button btnPrev;
        private Button btnBack;
        private Button btnHome;
        private Button btnSave;
        private RichTextBox txt;

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

            // ===== PREV BUTTON =====
            btnPrev = new Button
            {
                Dock = DockStyle.Fill,
                Text = "◀",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Red
            };
            content.Controls.Add(btnPrev, 0, 0);

            // ===== TEXT AREA =====
            Panel box = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5, 10, 5, 10)
            };
            content.Controls.Add(box, 1, 0);

            txt = new RichTextBox
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

            // ===== BACK =====
            btnBack = MakeBottomButton("Trở về", Color.FromArgb(252, 213, 180), DockStyle.Left);
            bottom.Controls.Add(btnBack, 0, 0);

            // ===== HOME =====
            btnHome = MakeBottomButton("Trang\nchủ", Color.Yellow, DockStyle.Fill);
            bottom.Controls.Add(btnHome, 1, 0);

            // ===== SAVE =====
            btnSave = MakeBottomButton("Lưu", Color.FromArgb(189, 215, 238), DockStyle.Right);
            bottom.Controls.Add(btnSave, 2, 0);

            // ===== EVENTS =====
            btnPrev.Click += BtnPrev_Click;
            btnBack.Click += BtnBack_Click;
            btnHome.Click += BtnHome_Click;
            btnSave.Click += BtnSave_Click;
        }

        // ===== EVENTS =====

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new _1DukienTinhHuong());
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Savedata(txt.Text);
            MessageBox.Show("Đã lưu dữ liệu");
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

        private void Savedata(string content)
        {
            var dataLayer = new RichTextBoxData();
            dataLayer.AddData(Constants.CURRENT_USER_ID_VALUE, content, "X_BienPhap");
        }
    }
}