using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VICongTacVanTai
{
    public partial class _2DuTinhKhoiLuongVanChuyen : UserControl
    {
        private float currentFontSize = 11f;

        public _2DuTinhKhoiLuongVanChuyen()
        {
            InitializeComponent();
            this.Load += _2DuTinhKhoiLuongVanChuyen_Load;
        }

        private void _2DuTinhKhoiLuongVanChuyen_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.AutoScaleMode = AutoScaleMode.None;

            /* ================= ROOT LAYOUT ================= */
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30)); // Subtitle
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Title
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Main content
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60)); // Buttons
            this.Controls.Add(layout);

            /* ================= SUBTITLE ================= */
            Label lblSubtitle = new Label
            {
                Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleCenter
            };
            layout.Controls.Add(lblSubtitle, 0, 0);

            /* ================= TITLE ================= */
            Label lblTitle = new Label
            {
                Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 242, 204),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            layout.Controls.Add(lblTitle, 0, 1);

            /* ================= MAIN PANEL ================= */
            Panel pnlMain = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };
            layout.Controls.Add(pnlMain, 0, 2);

            /* ================= MAIN CONTENT LAYOUT ================= */
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlMain.Controls.Add(mainLayout);

            /* ================= HEADER IX with CLOSE BUTTON ================= */
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(180, 198, 231)
            };

            Label lblHeader = new Label
            {
                Text = "IX. Công tác vận tải",
                Dock = DockStyle.Fill,
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                BackColor = Color.Transparent
            };

            Button btnClose = new Button
            {
                Text = "X",
                Width = 30,
                Height = 30,
                Dock = DockStyle.Right,
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;

            pnlHeader.Controls.Add(lblHeader);
            pnlHeader.Controls.Add(btnClose);
            mainLayout.Controls.Add(pnlHeader, 0, 0);

            /* ================= SECTION ================= */
            Label lblSection = new Label
            {
                Text = "2. Dự tính khối lượng vận chuyển",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                Padding = new Padding(12, 5, 0, 0)
            };
            mainLayout.Controls.Add(lblSection, 0, 1);

            /* ================= CONTENT WITH ARROWS ================= */
            TableLayoutPanel pnlContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Padding = new Padding(12),
                BackColor = Color.White
            };
            pnlContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            pnlContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            pnlContent.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            mainLayout.Controls.Add(pnlContent, 0, 1);

            /* ================= LEFT ARROW ================= */
            Button btnPrev = new Button
            {
                Text = "◄",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            pnlContent.Controls.Add(btnPrev, 0, 0);

            /* ================= FORM CONTENT ================= */
            Panel pnlForm = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pnlContent.Controls.Add(pnlForm, 1, 0);

            // Form fields
            int yPos = 20;
            int labelWidth = 200;
            int textBoxWidth = 80;
            int spacing = 35;

            // Row 1: Khối lượng vận chuyển toàn trận
            CreateFormRow(pnlForm, "Khối lượng vận chuyển toàn trận:", yPos, labelWidth, textBoxWidth);
            yPos += spacing;

            // Row 2: Giai đoạn chuẩn bị
            CreateFormRow(pnlForm, "+ Giai đoạn chuẩn bị:", yPos, labelWidth, textBoxWidth);
            yPos += spacing;

            // Row 3: Giai đoạn chiến đấu
            CreateFormRow(pnlForm, "+ Giai đoạn chiến đấu:", yPos, labelWidth, textBoxWidth);

            /* ================= RIGHT ARROW ================= */
            Button btnNext = new Button
            {
                Text = "▶",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };
            btnNext.FlatAppearance.BorderSize = 0;
            pnlContent.Controls.Add(btnNext, 2, 0);

            /* ================= BOTTOM BUTTONS ================= */
            TableLayoutPanel pnlButton = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                Padding = new Padding(10)
            };
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnlButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            Button btnBack = new Button
            {
                Text = "Trở về",
                Width = 100,
                Height = 35,
                Font = new Font("Times New Roman", 11),
                BackColor = Color.FromArgb(255, 200, 150)
            };

            Button btnHome = new Button
            {
                Text = "Trang\nchủ",
                Width = 100,
                Height = 35,
                BackColor = Color.Yellow,
                Font = new Font("Times New Roman", 10, FontStyle.Bold)
            };

            Button btnSave = new Button
            {
                Text = "Lưu",
                Width = 100,
                Height = 35,
                Font = new Font("Times New Roman", 11),
                BackColor = Color.FromArgb(173, 216, 230)
            };

            Panel pnlLeft = new Panel { Dock = DockStyle.Fill };
            btnBack.Location = new Point(10, 10);
            pnlLeft.Controls.Add(btnBack);

            Panel pnlCenter = new Panel { Dock = DockStyle.Fill };
            btnHome.Location = new Point((pnlCenter.Width - btnHome.Width) / 2, 10);
            pnlCenter.Controls.Add(btnHome);

            Panel pnlRight = new Panel { Dock = DockStyle.Fill };
            pnlRight.Resize += (s, ev) => { btnSave.Location = new Point(pnlRight.Width - btnSave.Width - 10, 10); };
            pnlRight.Controls.Add(btnSave);

            pnlButton.Controls.Add(pnlLeft, 0, 0);
            pnlButton.Controls.Add(pnlCenter, 1, 0);
            pnlButton.Controls.Add(pnlRight, 2, 0);

            layout.Controls.Add(pnlButton, 0, 3);
        }

        private void CreateFormRow(Panel parent, string labelText, int yPos, int labelWidth, int textBoxWidth)
        {
            // Label
            Label lbl = new Label
            {
                Text = labelText,
                Location = new Point(20, yPos),
                Width = labelWidth,
                Font = new Font("Times New Roman", 11),
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(lbl);

            int xPos = labelWidth + 30;

            // TextBox 1
            TextBox txt1 = new TextBox
            {
                Location = new Point(xPos, yPos - 2),
                Width = textBoxWidth,
                Font = new Font("Times New Roman", 11),
                TextAlign = HorizontalAlignment.Center
            };
            parent.Controls.Add(txt1);
            xPos += textBoxWidth + 10;

            // Label "tấn"
            Label lblTon = new Label
            {
                Text = "tấn",
                Location = new Point(xPos, yPos),
                Width = 40,
                Font = new Font("Times New Roman", 11)
            };
            parent.Controls.Add(lblTon);
            xPos += 50;

            // Label "Trong đó:"
            Label lblTrongDo = new Label
            {
                Text = "Trong đó:",
                Location = new Point(xPos, yPos),
                Width = 70,
                Font = new Font("Times New Roman", 11)
            };
            parent.Controls.Add(lblTrongDo);
            xPos += 80;

            // Label "VCHC"
            Label lblVCHC = new Label
            {
                Text = "VCHC",
                Location = new Point(xPos, yPos),
                Width = 50,
                Font = new Font("Times New Roman", 11)
            };
            parent.Controls.Add(lblVCHC);
            xPos += 55;

            // TextBox 2
            TextBox txt2 = new TextBox
            {
                Location = new Point(xPos, yPos - 2),
                Width = textBoxWidth,
                Font = new Font("Times New Roman", 11),
                TextAlign = HorizontalAlignment.Center
            };
            parent.Controls.Add(txt2);
            xPos += textBoxWidth + 10;

            // Label "VCKT"
            Label lblVCKT = new Label
            {
                Text = "VCKT",
                Location = new Point(xPos, yPos),
                Width = 50,
                Font = new Font("Times New Roman", 11)
            };
            parent.Controls.Add(lblVCKT);
            xPos += 55;

            // TextBox 3
            TextBox txt3 = new TextBox
            {
                Location = new Point(xPos, yPos - 2),
                Width = textBoxWidth,
                Font = new Font("Times New Roman", 11),
                TextAlign = HorizontalAlignment.Center
            };
            parent.Controls.Add(txt3);
        }

        /* ===== ZOOM CTRL + / - ===== */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                currentFontSize++;
                UpdateFontRecursive(this);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                if (currentFontSize > 8)
                    currentFontSize--;
                UpdateFontRecursive(this);
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