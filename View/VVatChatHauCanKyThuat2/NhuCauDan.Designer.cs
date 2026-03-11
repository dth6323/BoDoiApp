using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    partial class NhuCauDan
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
        private System.Windows.Forms.TableLayoutPanel footerLayout;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;

        private unvell.ReoGrid.ReoGridControl reoGridControl1;

        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();

            this.label11 = new System.Windows.Forms.Label();

            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();

            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();

            this.SuspendLayout();

            // ======================
            // MAIN LAYOUT
            // ======================

            this.mainLayout.Dock = DockStyle.Fill;
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

            this.headerLayout.Controls.Add(label11, 0, 0);

            // ======================
            // REO GRID
            // ======================

            this.reoGridControl1.Dock = DockStyle.Fill;
            this.reoGridControl1.BackColor = Color.White;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.SheetTabVisible = true;

            // ======================
            // FOOTER BUTTONS
            // ======================

            this.footerLayout.Dock = DockStyle.Fill;
            this.footerLayout.ColumnCount = 3;

            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            this.footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            SetupButton(button1, "Trở về", Color.FromArgb(231, 76, 60));
            SetupButton(button2, "Trang Chủ", Color.FromArgb(52, 152, 219));
            SetupButton(button3, "Lưu", Color.FromArgb(46, 204, 113));

            button1.Click += new EventHandler(button1_Click);
            button2.Click += new EventHandler(button2_Click);
            button3.Click += new EventHandler(button3_Click);

            this.footerLayout.Controls.Add(button1, 0, 0);
            this.footerLayout.Controls.Add(button2, 1, 0);
            this.footerLayout.Controls.Add(button3, 2, 0);

            // ======================
            // ADD LAYOUT
            // ======================

            this.mainLayout.Controls.Add(headerLayout, 0, 0);
            this.mainLayout.Controls.Add(reoGridControl1, 0, 1);
            this.mainLayout.Controls.Add(footerLayout, 0, 2);

            this.Controls.Add(mainLayout);

            this.Name = "NhuCauDan";
            this.Size = new Size(1280, 720);
            this.Load += new EventHandler(NhuCauDan_Load);

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