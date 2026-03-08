using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.Baovehaucankythuat
{
    public partial class _1DukienTinhHuong : UserControl
    {
        private float currentFontSize = 11f;

        // ===== BUTTON DECLARE =====
        private Button btnNext;
        private Button btnBack;
        private Button btnHome;
        private Button btnSave;

        private RichTextBox txt;

        public _1DukienTinhHuong()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _1DukienTinhHuong_Load;
        }

        private void _1DukienTinhHuong_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT =====
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
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(main, 0, 1);

            main.Controls.Add(MakeHeader("X. Bảo đảm hậu cần, kỹ thuật", true), 0, 0);
            main.Controls.Add(MakeHeader("I. Dự kiến tình huống có thể xảy ra", false), 0, 1);

            // ===== CONTENT =====
            TableLayoutPanel content = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            main.Controls.Add(content, 0, 2);

            // ===== TEXT AREA =====
            Panel box = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 30, 10, 30)
            };
            content.Controls.Add(box, 1, 0);

            txt = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 12),
                BorderStyle = BorderStyle.None
            };
            box.Controls.Add(txt);

            // ===== NEXT BUTTON =====
            btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.Red
            };
            content.Controls.Add(btnNext, 2, 0);

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

            // ===== BACK BUTTON =====
            btnBack = new Button
            {
                Text = "Trở về",
                BackColor = Color.FromArgb(252, 213, 180),
                Dock = DockStyle.Left,
                Width = 100
            };
            bottom.Controls.Add(btnBack, 0, 0);

            // ===== HOME BUTTON =====
            btnHome = new Button
            {
                Text = "Trang\nchủ",
                BackColor = Color.Yellow,
                Dock = DockStyle.Fill,
                Height = 45
            };
            bottom.Controls.Add(btnHome, 1, 0);

            // ===== SAVE BUTTON =====
            btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(189, 215, 238),
                Dock = DockStyle.Right,
                Width = 100
            };
            bottom.Controls.Add(btnSave, 2, 0);

            // ===== ADD EVENT =====
            btnNext.Click += BtnNext_Click;
            btnBack.Click += BtnBack_Click;
            btnHome.Click += BtnHome_Click;
            btnSave.Click += BtnSave_Click;
        }

        // ===== EVENTS =====

        private void BtnNext_Click(object sender, EventArgs e)
        {
            Savedata(txt.Text);
            NavigationService.Navigate(() => new _2BienPhap());
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Savedata(txt.Text);
            MessageBox.Show("Đã lưu dữ liệu");
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

        private void Savedata(string content)
        {
            var dataLayer = new RichTextBoxData();
            dataLayer.SaveOrUpdate(
                Constants.CURRENT_USER_ID_VALUE,
                content,
                "X_DuKien"
            );
        }
    }
}