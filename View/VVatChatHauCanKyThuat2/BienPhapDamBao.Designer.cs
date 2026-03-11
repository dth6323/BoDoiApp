using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    partial class BienPhapDamBao
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel headerLayout;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.TableLayoutPanel footerLayout;

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.RichTextBox richTextBox1;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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

            this.richTextBox1 = new System.Windows.Forms.RichTextBox();

            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // MAIN LAYOUT
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 3;

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            // HEADER
            headerLayout.Dock = DockStyle.Fill;
            headerLayout.ColumnCount = 1;

            label11.Dock = DockStyle.Fill;
            label11.BackColor = Color.FromArgb(255, 192, 128);
            label11.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label11.TextAlign = ContentAlignment.MiddleCenter;
            label11.Text =
                "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";

            headerLayout.Controls.Add(label11);

            // CONTENT
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.ColumnCount = 1;
            contentLayout.RowCount = 4;

            contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            label1.Text = "Kế hoạch bảo đảm hậu cần - kỹ thuật";
            label1.Font = new Font("Segoe UI", 11F);
            label1.AutoSize = true;
            label1.Padding = new Padding(10);

            label2.Text = "V. Bảo đảm đạn, vật chất hậu cần, vật tư kỹ thuật";
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.AutoSize = true;
            label2.Padding = new Padding(10, 0, 0, 0);

            label3.Text = "2. Biện pháp bảo đảm";
            label3.Font = new Font("Segoe UI", 9F);
            label3.AutoSize = true;
            label3.Padding = new Padding(10, 0, 0, 0);

            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("Segoe UI", 10F);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;

            contentLayout.Controls.Add(label1);
            contentLayout.Controls.Add(label2);
            contentLayout.Controls.Add(label3);
            contentLayout.Controls.Add(richTextBox1);

            // FOOTER
            footerLayout.Dock = DockStyle.Fill;
            footerLayout.ColumnCount = 3;

            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            SetupButton(button1, "Trở về", Color.FromArgb(231, 76, 60));
            SetupButton(button2, "Trang chủ", Color.FromArgb(52, 152, 219));
            SetupButton(button3, "Lưu", Color.FromArgb(46, 204, 113));

            button1.Click += new EventHandler(button1_Click);
            button2.Click += new EventHandler(button2_Click);
            button3.Click += new EventHandler(button3_Click);

            footerLayout.Controls.Add(button1, 0, 0);
            footerLayout.Controls.Add(button2, 1, 0);
            footerLayout.Controls.Add(button3, 2, 0);

            // ADD LAYOUT
            mainLayout.Controls.Add(headerLayout, 0, 0);
            mainLayout.Controls.Add(contentLayout, 0, 1);
            mainLayout.Controls.Add(footerLayout, 0, 2);

            Controls.Add(mainLayout);

            Name = "BienPhapDamBao";
            Size = new Size(1280, 720);

            ResumeLayout(false);
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
            btn.Margin = new Padding(20, 5, 20, 5);
        }
    }
}