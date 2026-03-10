using System.Windows.Forms;

namespace BoDoiApp.View.IVVuKhiKyThuat
{
    partial class _1ChiTieu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();

            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();

            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();

            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();

            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.mainLayout.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // ================= MAIN LAYOUT =================

            this.mainLayout.ColumnCount = 1;
            this.mainLayout.RowCount = 3;
            this.mainLayout.Dock = DockStyle.Fill;

            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));

            // ================= HEADER =================

            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.Dock = DockStyle.Fill;

            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.label11.Dock = DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.label11.Text =
            "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);

            // ================= CONTENT =================

            this.contentLayout = new TableLayoutPanel();
            this.contentLayout.ColumnCount = 1;
            this.contentLayout.Dock = DockStyle.Fill;

            this.contentLayout.RowCount = 3;
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // label1
            this.label1.AutoSize = true;
            this.label1.Text = "1. Chỉ tiêu";
            this.label1.Dock = DockStyle.Top;

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Text = "IV. Bảo đảm vũ khí, trang bị kỹ thuật";
            this.label2.Dock = DockStyle.Top;

            // REOGRID
            this.reoGridControl1.Dock = DockStyle.Fill;
            this.reoGridControl1.BackColor = System.Drawing.Color.White;

            this.contentLayout.Controls.Add(this.label1, 0, 0);
            this.contentLayout.Controls.Add(this.label2, 0, 1);
            this.contentLayout.Controls.Add(this.reoGridControl1, 0, 2);

            // ================= FOOTER =================

            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.Dock = DockStyle.Fill;

            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));

            // buttons
            this.button1.Text = "Trở về";
            this.button2.Text = "Trang chủ";
            this.button3.Text = "Tiếp theo";

            this.button1.Anchor = AnchorStyles.Left;
            this.button2.Anchor = AnchorStyles.None;
            this.button3.Anchor = AnchorStyles.Right;

            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button3.Click += new System.EventHandler(this.button3_Click);

            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);

            // ================= ADD MAIN =================

            this.mainLayout.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.mainLayout.Controls.Add(this.contentLayout, 0, 1);
            this.mainLayout.Controls.Add(this.tableLayoutPanel1, 0, 2);

            this.Controls.Add(this.mainLayout);

            this.Name = "_1ChiTieu";
            this.Size = new System.Drawing.Size(1137, 576);

            this.Load += new System.EventHandler(this._1ChiTieu_Load);

            this.mainLayout.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private TableLayoutPanel contentLayout;
        private Label label11;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Button button2;
        private Button button3;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private Label label1;
        private Label label2;
    }
}