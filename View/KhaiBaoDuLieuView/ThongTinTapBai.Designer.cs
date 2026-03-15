using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class ThongTinTapBai
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing" ‌󠁢󠁢󠁢󠁦󠁮󠁢󠁢󠁢󠁦󠁬󠁢󠁢󠁢󠁦󠁯󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁦󠁣󠁢󠁢󠁢󠁥󠁺󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁦󠁧󠁢󠁢󠁢󠁥󠁵󠁢󠁢󠁢󠁦󠁨󠁢󠁢󠁢󠁥󠁵󠁢󠁢󠁢󠁥󠁿󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁥󠁸󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁦󠁬󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁦󠁩󠁢󠁢󠁢󠁦󠁯󠁢󠁢󠁢󠁦󠁬󠁢󠁢󠁢󠁥󠁷󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁦󠁢󠁢󠁢󠁢󠁦󠁩󠁢󠁢󠁢󠁦󠁯󠁢󠁢󠁢󠁦󠁦󠁢󠁢󠁢󠁥󠁸󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁥󠁶󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁥󠁸󠁢󠁢󠁢󠁦󠁣󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁦󠁪󠁢󠁢󠁢󠁦󠁩󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁥󠁸󠁢󠁢󠁢󠁤󠁩󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁦󠁩󠁢󠁢󠁢󠁦󠁮󠁢󠁢󠁢󠁦󠁢󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁦󠁬󠁢󠁢󠁢󠁦󠁱󠁢󠁢󠁢󠁦󠁣󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁣󠁴󠁢󠁢󠁢󠁣󠁨󠁢󠁢󠁢󠁥󠁺󠁢󠁢󠁢󠁥󠁵󠁢󠁢󠁢󠁦󠁦󠁢󠁢󠁢󠁦󠁭󠁢󠁢󠁢󠁥󠁹󠁢󠁢󠁢󠁣󠁶󠁡/param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font inputFont = new Font("Segoe UI", 10F);

            this.BackColor = Color.FromArgb(245, 247, 250);

            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.Padding = new Padding(40);

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));

            for (int i = 0; i < 10; i++)
                mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            // HEADER
            label8 = new Label();
            label8.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN - KỸ THUẬT";
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label8.TextAlign = ContentAlignment.MiddleCenter;
            label8.BackColor = Color.FromArgb(52, 152, 219);
            label8.ForeColor = Color.White;
            label8.Height = 70;

            mainLayout.Controls.Add(label8, 0, 0);
            mainLayout.SetColumnSpan(label8, 2);

            // TÊN VĂN KIỆN
            label1 = new Label();
            label1.Text = "Tên văn kiện";
            label1.Font = labelFont;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Dock = DockStyle.Fill;

            txt_tenvankien = new TextBox();
            txt_tenvankien.Font = inputFont;
            txt_tenvankien.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label1, 0, 1);
            mainLayout.Controls.Add(txt_tenvankien, 1, 1);

            // VỊ TRÍ CHỈ HUY
            label2 = new Label();
            label2.Text = "Vị trí chỉ huy";
            label2.Font = labelFont;
            label2.Dock = DockStyle.Fill;

            txt_vtch = new TextBox();
            txt_vtch.Font = inputFont;
            txt_vtch.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label2, 0, 2);
            mainLayout.Controls.Add(txt_vtch, 1, 2);

            // THỜI GIAN
            label3 = new Label();
            label3.Text = "Thời gian";
            label3.Font = labelFont;
            label3.Dock = DockStyle.Fill;

            txt_thoigian = new TextBox();
            txt_thoigian.Font = inputFont;
            txt_thoigian.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label3, 0, 3);
            mainLayout.Controls.Add(txt_thoigian, 1, 3);

            // MẢNH BẢN CHẮP
            // MẢNH BẢN CHẮP
label4 = new Label();
            label4.Text = "Mảnh bản chắp";
            label4.Font = labelFont;
            label4.Dock = DockStyle.Fill;

            // Grid 2x2
            TableLayoutPanel mapPanel = new TableLayoutPanel();
            mapPanel.Dock = DockStyle.Fill;
            mapPanel.RowCount = 2;
            mapPanel.ColumnCount = 2;

            mapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mapPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            mapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mapPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            txt_m1 = new TextBox();
            txt_m2 = new TextBox();
            txt_m3 = new TextBox();
            txt_m4 = new TextBox();

            txt_m1.Font = inputFont;
            txt_m2.Font = inputFont;
            txt_m3.Font = inputFont;
            txt_m4.Font = inputFont;

            txt_m1.Dock = DockStyle.Fill;
            txt_m2.Dock = DockStyle.Fill;
            txt_m3.Dock = DockStyle.Fill;
            txt_m4.Dock = DockStyle.Fill;

            mapPanel.Controls.Add(txt_m1, 0, 0);
            mapPanel.Controls.Add(txt_m2, 1, 0);
            mapPanel.Controls.Add(txt_m3, 0, 1);
            mapPanel.Controls.Add(txt_m4, 1, 1);

            mainLayout.Controls.Add(label4, 0, 4);
            mainLayout.Controls.Add(mapPanel, 1, 4);

            // TỶ LỆ
            label9 = new Label();
            label9.Text = "Tỷ lệ";
            label9.Font = labelFont;
            label9.Dock = DockStyle.Fill;

            txt_tyle = new TextBox();
            txt_tyle.Font = inputFont;
            txt_tyle.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label9, 0, 5);
            mainLayout.Controls.Add(txt_tyle, 1, 5);

            // NĂM
            label6 = new Label();
            label6.Text = "Năm";
            label6.Font = labelFont;
            label6.Dock = DockStyle.Fill;

            txt_nam = new TextBox();
            txt_nam.Font = inputFont;
            txt_nam.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label6, 0, 6);
            mainLayout.Controls.Add(txt_nam, 1, 6);

            // CHỈ HUY HCKT
            label5 = new Label();
            label5.Text = "Chỉ huy hậu cần kỹ thuật";
            label5.Font = labelFont;
            label5.Dock = DockStyle.Fill;

            txt_chhckt = new TextBox();
            txt_chhckt.Font = inputFont;
            txt_chhckt.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label5, 0, 7);
            mainLayout.Controls.Add(txt_chhckt, 1, 7);

            // NGƯỜI THAY THẾ
            label7 = new Label();
            label7.Text = "Người thay thế";
            label7.Font = labelFont;
            label7.Dock = DockStyle.Fill;

            txt_nguoithaythe = new TextBox();
            txt_nguoithaythe.Font = inputFont;
            txt_nguoithaythe.Dock = DockStyle.Fill;

            mainLayout.Controls.Add(label7, 0, 8);
            mainLayout.Controls.Add(txt_nguoithaythe, 1, 8);

            this.Controls.Add(mainLayout);
        }

        #endregion

        private System.Windows.Forms.TextBox txt_tenvankien;
        private System.Windows.Forms.TextBox txt_vtch;
        private System.Windows.Forms.TextBox txt_thoigian;
        private System.Windows.Forms.TextBox txt_m1;
        private System.Windows.Forms.TextBox txt_m2;
        private System.Windows.Forms.TextBox txt_m3;
        private System.Windows.Forms.TextBox txt_m4;
        private System.Windows.Forms.TextBox txt_tyle;
        private System.Windows.Forms.TextBox txt_chhckt;
        private System.Windows.Forms.TextBox txt_nam;
        private System.Windows.Forms.TextBox txt_nguoithaythe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label9;
    }
}