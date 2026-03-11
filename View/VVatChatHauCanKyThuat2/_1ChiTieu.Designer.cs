using System.Drawing;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
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

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel headerLayout;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.TableLayoutPanel footerLayout;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();

            this.label11 = new System.Windows.Forms.Label();

            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // =========================
            // MAIN LAYOUT
            // =========================

            this.mainLayout.ColumnCount = 1;
            this.mainLayout.RowCount = 3;
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));

            // =========================
            // HEADER
            // =========================

            this.headerLayout.ColumnCount = 1;
            this.headerLayout.RowCount = 1;
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            this.headerLayout.Controls.Add(this.label11, 0, 0);

            // =========================
            // CONTENT
            // =========================

            this.contentLayout.ColumnCount = 1;
            this.contentLayout.RowCount = 5;
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            for (int i = 0; i < 5; i++)
            {
                this.contentLayout.RowStyles.Add(
                    new System.Windows.Forms.RowStyle(
                        System.Windows.Forms.SizeType.Percent, 20F));
            }

            SetupButton(this.button8, "Toàn trận");
            SetupButton(this.button4, "HƯỚNG CHỦ YẾU");
            SetupButton(this.button5, "HƯỚNG THỨ YẾU");
            SetupButton(this.button6, "LỰC LƯỢNG DỰ BỊ");
            SetupButton(this.button7, "LỰC LƯỢNG CÒN LẠI");
            button8.BackColor = Color.FromArgb(41, 128, 185);
            button4.BackColor = Color.FromArgb(39, 174, 96);
            button5.BackColor = Color.FromArgb(155, 89, 182);
            button6.BackColor = Color.FromArgb(241, 196, 15);
            button7.BackColor = Color.FromArgb(230, 126, 34);
            this.button8.Click += new System.EventHandler(this.button8_Click);
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button6.Click += new System.EventHandler(this.button6_Click);
            this.button7.Click += new System.EventHandler(this.button7_Click);

            this.contentLayout.Controls.Add(this.button8, 0, 0);
            this.contentLayout.Controls.Add(this.button4, 0, 1);
            this.contentLayout.Controls.Add(this.button5, 0, 2);
            this.contentLayout.Controls.Add(this.button6, 0, 3);
            this.contentLayout.Controls.Add(this.button7, 0, 4);

            // =========================
            // FOOTER
            // =========================

            this.footerLayout.ColumnCount = 3;
            this.footerLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            this.footerLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.footerLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.footerLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));

            SetupButton(this.button1, "Trở về");
            SetupButton(this.button2, "Trang chủ");
            SetupButton(this.button3, "Tiếp Theo");
            button1.BackColor = Color.FromArgb(231, 76, 60);
            button2.BackColor = Color.FromArgb(52, 152, 219);
            button3.BackColor = Color.FromArgb(46, 204, 113);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button3.Click += new System.EventHandler(this.button3_Click);

            this.footerLayout.Controls.Add(this.button1, 0, 0);
            this.footerLayout.Controls.Add(this.button2, 1, 0);
            this.footerLayout.Controls.Add(this.button3, 2, 0);

            // =========================
            // ADD TO MAIN
            // =========================

            this.mainLayout.Controls.Add(this.headerLayout, 0, 0);
            this.mainLayout.Controls.Add(this.contentLayout, 0, 1);
            this.mainLayout.Controls.Add(this.footerLayout, 0, 2);

            this.Controls.Add(this.mainLayout);

            this.Name = "_1ChiTieu";
            this.Size = new System.Drawing.Size(1137, 576);

            this.ResumeLayout(false);
        }

        private void SetupButton(System.Windows.Forms.Button btn, string text) { btn.Text = text; btn.Dock = System.Windows.Forms.DockStyle.Fill; btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold); }
    }
}