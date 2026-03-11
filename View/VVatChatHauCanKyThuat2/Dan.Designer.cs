using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    partial class Dan
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        private unvell.ReoGrid.ReoGridControl reoGridControl1;

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();

            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();

            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();

            this.SuspendLayout();

            // ======================
            // MAIN LAYOUT
            // ======================

            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.RowCount = 3;

            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // ======================
            // HEADER
            // ======================

            this.headerLayout.Dock = DockStyle.Fill;
            this.headerLayout.ColumnCount = 1;

            this.label11.Dock = DockStyle.Fill;
            this.label11.BackColor = Color.FromArgb(255, 192, 128);
            this.label11.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.label11.TextAlign = ContentAlignment.MiddleCenter;
            this.label11.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            this.headerLayout.Controls.Add(this.label11, 0, 0);

            // ======================
            // CONTENT
            // ======================

            this.contentLayout.Dock = DockStyle.Fill;
            this.contentLayout.ColumnCount = 1;
            this.contentLayout.RowCount = 5;

            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.label1.Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật";
            this.label1.Font = new Font("Microsoft Sans Serif", 10F);
            this.label1.AutoSize = true;
            this.label1.Padding = new Padding(10);

            this.label2.Text = "V. Bảo đảm đạn, vật chất hậu cần, vật tư kỹ thuật";
            this.label2.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            this.label2.AutoSize = true;
            this.label2.Padding = new Padding(10, 0, 0, 0);

            this.label4.Text = "1. Chỉ tiêu";
            this.label4.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.label4.AutoSize = true;
            this.label4.Padding = new Padding(10, 0, 0, 0);

            this.label3.Text = "a. Đạn";
            this.label3.AutoSize = true;
            this.label3.Padding = new Padding(10, 0, 0, 0);

            // ======================
            // REO GRID
            // ======================

            this.reoGridControl1.Dock = DockStyle.Fill;
            this.reoGridControl1.BackColor = Color.White;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.SheetTabVisible = true;

            this.contentLayout.Controls.Add(label1, 0, 0);
            this.contentLayout.Controls.Add(label2, 0, 1);
            this.contentLayout.Controls.Add(label4, 0, 2);
            this.contentLayout.Controls.Add(label3, 0, 3);
            this.contentLayout.Controls.Add(reoGridControl1, 0, 4);

            // ======================
            // FOOTER BUTTONS
            // ======================

            this.footerLayout.Dock = DockStyle.Fill;
            this.footerLayout.ColumnCount = 3;

            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            SetupButton(button1, "Trở về", Color.FromArgb(231, 76, 60));
            SetupButton(button2, "Trang chủ", Color.FromArgb(52, 152, 219));
            SetupButton(button3, "Tiếp Theo", Color.FromArgb(46, 204, 113));

            button1.Click += new EventHandler(button1_Click);
            button3.Click += new EventHandler(button3_Click);

            this.footerLayout.Controls.Add(button1, 0, 0);
            this.footerLayout.Controls.Add(button2, 1, 0);
            this.footerLayout.Controls.Add(button3, 2, 0);

            // ======================
            // ADD LAYOUT
            // ======================

            this.mainLayout.Controls.Add(headerLayout, 0, 0);
            this.mainLayout.Controls.Add(contentLayout, 0, 1);
            this.mainLayout.Controls.Add(footerLayout, 0, 2);

            this.Controls.Add(mainLayout);

            this.Name = "Dan";
            this.Size = new Size(1280, 720);
            this.Load += new EventHandler(Dan_Load);

            this.ResumeLayout(false);
        }

        private void SetupButton(Button btn, string text, Color color)
        {
            btn.Text = text;
            btn.Dock = DockStyle.Fill;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }
    }
}