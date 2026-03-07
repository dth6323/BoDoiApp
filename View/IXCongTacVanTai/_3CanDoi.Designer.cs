using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    partial class _3CanDoi
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel root;
        private Panel pnlMain;
        private TableLayoutPanel mainLayout;
        private Panel pnlHeader;
        private TableLayoutPanel content;

        private Button btnLeft;
        private Button btnRight;

        private FlowLayoutPanel form;

        TextBox vtbi_from;
        TextBox vtbi_to;

        TextBox vtle_from;
        TextBox vtle_to;

        TextBox danquan_from;
        TextBox danquan_to;

        TextBox xetho_count;
        TextBox xetho_from;
        TextBox xetho_to;

        TextBox tongkha_from;
        TextBox tongkha_to;

        TextBox tong_to;

        TextBox txtKetLuan;

        Button btnBack;
        Button btnHome;
        Button btnSave;
        Button btnNext;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            root = new TableLayoutPanel();
            pnlMain = new Panel();
            mainLayout = new TableLayoutPanel();
            pnlHeader = new Panel();
            content = new TableLayoutPanel();
            btnLeft = new Button();
            btnRight = new Button();
            form = new FlowLayoutPanel();

            SuspendLayout();

            // ROOT
            root.Dock = DockStyle.Fill;
            root.RowCount = 4;
            root.ColumnCount = 1;
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            Label lblSub = new Label();
            lblSub.Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật";
            lblSub.Dock = DockStyle.Fill;
            lblSub.Font = new Font("Times New Roman", 11);
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            root.Controls.Add(lblSub, 0, 0);

            Label lblTitle = new Label();
            lblTitle.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT";
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.BackColor = Color.FromArgb(255, 242, 204);
            lblTitle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            root.Controls.Add(lblTitle, 0, 1);

            // MAIN PANEL
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.BorderStyle = BorderStyle.FixedSingle;
            pnlMain.Padding = new Padding(10);
            root.Controls.Add(pnlMain, 0, 2);

            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            pnlMain.Controls.Add(mainLayout);

            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.BackColor = Color.FromArgb(180, 198, 231);

            Label lblHeader = new Label();
            lblHeader.Text = "IX. Công tác vận tải";
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lblHeader.Padding = new Padding(12, 0, 0, 0);
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            pnlHeader.Controls.Add(lblHeader);

            mainLayout.Controls.Add(pnlHeader, 0, 0);

            // CONTENT
            content.Dock = DockStyle.Fill;
            content.ColumnCount = 3;
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
            mainLayout.Controls.Add(content, 0, 1);

            btnLeft.Text = "◄";
            btnLeft.Dock = DockStyle.Fill;
            btnLeft.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            btnLeft.ForeColor = Color.Red;
            btnLeft.Click += btnBack_Click;
            content.Controls.Add(btnLeft, 0, 0);

            form.Dock = DockStyle.Fill;
            form.AutoScroll = true;
            form.FlowDirection = FlowDirection.TopDown;
            form.WrapContents = false;
            form.Padding = new Padding(10);
            content.Controls.Add(form, 1, 0);

            btnRight.Text = "►";
            btnRight.Dock = DockStyle.Fill;
            btnRight.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            btnRight.ForeColor = Color.Red;
            btnRight.Click += btnNext_Click;
            content.Controls.Add(btnRight, 2, 0);

            // SECTION
            Label lblSection = new Label();
            lblSection.Text = "3. Cân đối";
            lblSection.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            lblSection.AutoSize = true;
            form.Controls.Add(lblSection);

            // TABLE
            TableLayoutPanel table = new TableLayoutPanel();
            table.ColumnCount = 5;
            table.AutoSize = true;

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));

            form.Controls.Add(table);

            AddRow(table, "- Khả năng của VTĐbi:", out vtbi_from, out vtbi_to);
            AddRow(table, "- Khả năng của VTĐle:", out vtle_from, out vtle_to);
            AddRow(table, "- Khả năng của LL dân quân:", out danquan_from, out danquan_to);

            AddXeThoRow(table);

            AddRow(table, "- Tổng khả năng vận chuyển:", out tongkha_from, out tongkha_to);

            AddSingleRow(table, "- Tổng khối lượng vận chuyển:", out tong_to);

            // KET LUAN

            Panel pnlKet = new Panel();
            pnlKet.Width = 500;
            pnlKet.Height = 40;
            pnlKet.Margin = new Padding(50, 20, 3, 0);

            Label lblKet = new Label();
            lblKet.Text = "Kết luận:";
            lblKet.AutoSize = true;
            lblKet.Location = new Point(0, 12);

            txtKetLuan = new TextBox();
            txtKetLuan.Width = 380;
            txtKetLuan.Location = new Point(80, 10);

            pnlKet.Controls.Add(lblKet);
            pnlKet.Controls.Add(txtKetLuan);

            form.Controls.Add(pnlKet);

            this.Controls.Add(root);
            this.Dock = DockStyle.Fill;

            ResumeLayout(false);
        }

        private void AddRow(TableLayoutPanel table, string text,
            out TextBox txtFrom, out TextBox txtTo)
        {
            int r = table.RowCount++;

            Label lbl = new Label
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(3, 8, 3, 3)
            };
            Label lblTu = new Label { Text = "Từ",
                Margin = new Padding(3, 4, 3, 3)
            };
            Label lblDen = new Label { Text = "đến",
                Margin = new Padding(3, 4, 3, 3)
            };

            txtFrom = new TextBox { Width = 70 };
            txtTo = new TextBox { Width = 70 };

            table.Controls.Add(lbl, 0, r);
            table.Controls.Add(lblTu, 1, r);
            table.Controls.Add(txtFrom, 2, r);
            table.Controls.Add(lblDen, 3, r);
            table.Controls.Add(txtTo, 4, r);
        }

        private void AddXeThoRow(TableLayoutPanel table)
        {
            int r = table.RowCount++;

            Label lbl = new Label
            {
                Text = "- Xe thồ:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
            };
            xetho_count = new TextBox { Width = 50 };
            xetho_from = new TextBox
            {
                Width = 70,
            };

            xetho_to = new TextBox
            {
                Width = 70,
            };
            table.Controls.Add(lbl, 0, r);
            table.Controls.Add(xetho_count, 1, r);
            table.Controls.Add(new Label { Text = "Từ",
                Margin = new Padding(3, 4, 3, 3)
            }, 2, r);
            table.Controls.Add(xetho_from, 3, r);
            table.Controls.Add(xetho_to, 4, r);
        }

        private void AddSingleRow(TableLayoutPanel table, string text,
            out TextBox txt)
        {
            int r = table.RowCount++;

            Label lbl = new Label
            {
                Text = text,
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(3, 8, 3, 3)
            };
            txt = new TextBox { Width = 80 };

            table.Controls.Add(lbl, 0, r);
            table.Controls.Add(new Label { Text = "" }, 1, r);
            table.Controls.Add(txt, 2, r);
        }
    }
}