namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class TinhHinhTacDong
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
            this.headerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.mainLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();

            // ================= MAIN LAYOUT =================

            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowCount = 4;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;

            // ================= HEADER =================

            this.headerPanel.ColumnCount = 1;
            this.headerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.label11.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            this.headerPanel.Controls.Add(this.label11, 0, 0);

            // ================= TITLE PANEL =================

            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titlePanel.Padding = new System.Windows.Forms.Padding(20);

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật";
            this.label1.Location = new System.Drawing.Point(400, 10);

            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Text = "I. Đánh giá tình hình tác động đến hậu cần - kỹ thuật";
            this.label2.Location = new System.Drawing.Point(40, 50);

            this.titlePanel.Controls.Add(this.label1);
            this.titlePanel.Controls.Add(this.label2);

            // ================= RICH TEXT =================

            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(40, 10, 40, 10);
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // ================= BUTTON PANEL =================

            this.buttonPanel.ColumnCount = 3;
            this.buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            // BUTTON 1

            this.button1.Text = "Trở về";
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button1.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // BUTTON 2

            this.button2.Text = "Trang Chủ";
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button2.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            // BUTTON 3

            this.button3.Text = "Lưu";
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button3.BackColor = System.Drawing.Color.FromArgb(25, 135, 84);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Click += new System.EventHandler(this.button3_Click);

            this.buttonPanel.Controls.Add(this.button1, 0, 0);
            this.buttonPanel.Controls.Add(this.button2, 1, 0);
            this.buttonPanel.Controls.Add(this.button3, 2, 0);

            // ================= ADD MAIN =================

            this.mainLayout.Controls.Add(this.headerPanel, 0, 0);
            this.mainLayout.Controls.Add(this.titlePanel, 0, 1);
            this.mainLayout.Controls.Add(this.richTextBox1, 0, 2);
            this.mainLayout.Controls.Add(this.buttonPanel, 0, 3);

            // ================= FORM =================

            this.Controls.Add(this.mainLayout);
            this.Name = "TinhHinhTacDong";
            this.Size = new System.Drawing.Size(1280, 720);
            this.Load += new System.EventHandler(this.TinhHinhTacDong_Load);

            this.mainLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel headerPanel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TableLayoutPanel buttonPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}