using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Drawing;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    partial class _2CanDoi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            // ── Khởi tạo controls ──
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();

            this.panelFooter = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button(
                );
            this.button2 = new System.Windows.Forms.Button(
                );
            this.button3 = new System.Windows.Forms.Button();

            this.panelBody = new System.Windows.Forms.TableLayoutPanel();

            this.tableQYd = new System.Windows.Forms.TableLayoutPanel();
            this.lblQYd = new System.Windows.Forms.Label();
            this.txtQYdTu = new System.Windows.Forms.TextBox();
            this.lblQYdDen = new System.Windows.Forms.Label();
            this.txtQYdDen = new System.Windows.Forms.TextBox();

            this.tableQYe = new System.Windows.Forms.TableLayoutPanel();
            this.lblQYe = new System.Windows.Forms.Label();
            this.txtDYeTu = new System.Windows.Forms.TextBox();
            this.lblQYeDen = new System.Windows.Forms.Label();
            this.txtQYeDen = new System.Windows.Forms.TextBox();

            this.tableYte = new System.Windows.Forms.TableLayoutPanel();
            this.lblYte = new System.Windows.Forms.Label();
            this.txtYteTu = new System.Windows.Forms.TextBox();
            this.lblYteDen = new System.Windows.Forms.Label();
            this.txtYteDen = new System.Windows.Forms.TextBox();

            this.tableTong = new System.Windows.Forms.TableLayoutPanel();
            this.lblTong = new System.Windows.Forms.Label();
            this.txtTongTu = new System.Windows.Forms.TextBox();
            this.lblTongDen = new System.Windows.Forms.Label();
            this.txtTongDen = new System.Windows.Forms.TextBox();

            this.SuspendLayout();

            // ══════════════════════════════
            // HEADER
            // ══════════════════════════════
            this.lblHeader.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";
            this.lblHeader.Dock = DockStyle.Fill;
            this.lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            this.lblHeader.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            this.lblHeader.BackColor = Color.FromArgb(255, 192, 128);

            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 65;
            this.panelHeader.Controls.Add(this.lblHeader);

            // ══════════════════════════════
            // FOOTER (3 nút)
            // ══════════════════════════════
            this.panelFooter.Dock = DockStyle.Bottom;
            this.panelFooter.Height = 55;
            this.panelFooter.ColumnCount = 3;
            this.panelFooter.RowCount = 1;
            this.panelFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.panelFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.panelFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            this.panelFooter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));


            this.button1.Text = "Trở về";
            this.button1.Dock = DockStyle.Fill;
                this.button1.FlatStyle = FlatStyle.Flat;
                this.button1.BackColor = Color.FromArgb(108, 117, 125);
                this.button1.ForeColor = Color.White;
            this.button2.Text = "Dự Kiến";
            this.button2.Dock = DockStyle.Fill;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.BackColor = Color.FromArgb(13, 110, 253);
            this.button2.ForeColor = Color.White;
            this.button3.Text = "Tiếp";
            this.button3.Dock = DockStyle.Fill;
            this.button3.FlatStyle = FlatStyle.Flat;
            this.button3.BackColor = Color.FromArgb(25, 135, 84);
            this.button3.ForeColor = Color.White;
            this.button1.Click += new EventHandler(this.button1_Click);

            this.button2.Click += new EventHandler(this.button2_Click);

            this.button3.Click += new EventHandler(this.button3_Click);

            this.panelFooter.Controls.Add(this.button1, 0, 0);
            this.panelFooter.Controls.Add(this.button2, 1, 0);
            this.panelFooter.Controls.Add(this.button3, 2, 0);

            // ══════════════════════════════
            // BODY – TableLayoutPanel chứa 4 hàng input
            // ══════════════════════════════
            this.panelBody.Dock = DockStyle.Fill;
            this.panelBody.RowCount = 5;   // 4 hàng input + 1 padding dưới
            this.panelBody.ColumnCount = 1;
            this.panelBody.Padding = new Padding(20, 20, 20, 10);
            this.panelBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.panelBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.panelBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.panelBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            this.panelBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // spacer
            this.panelBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // ── Hàm tạo 1 hàng input ──
            BuildInputRow(
                this.tableQYd,
                this.lblQYd, "- Khả năng cấp cứu của QY/d là:",
                this.txtQYdTu, this.lblQYdDen, this.txtQYdDen);

            BuildInputRow(
                this.tableQYe,
                this.lblQYe, "- Khả năng cấp cứu của QY/e là:",
                this.txtDYeTu, this.lblQYeDen, this.txtQYeDen);

            BuildInputRow(
                this.tableYte,
                this.lblYte, "- Khả năng cấp cứu của 1 trạm y tế xã là:",
                this.txtYteTu, this.lblYteDen, this.txtYteDen);

            BuildInputRow(
                this.tableTong,
                this.lblTong, "- Tổng khả năng cấp cứu:",
                this.txtTongTu, this.lblTongDen, this.txtTongDen);

            this.panelBody.Controls.Add(this.tableQYd, 0, 0);
            this.panelBody.Controls.Add(this.tableQYe, 0, 1);
            this.panelBody.Controls.Add(this.tableYte, 0, 2);
            this.panelBody.Controls.Add(this.tableTong, 0, 3);

            // ══════════════════════════════
            // FORM gốc
            // ══════════════════════════════
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelFooter);

            this.Name = "_2CanDoi";
            this.Size = new Size(1100, 600);
            this.Font = new Font("Times New Roman", 11F);
            this.Load += new EventHandler(this._2CanDoi_Load);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Tạo 1 hàng: [Label mô tả | TextBox Từ | Label "Đến" | TextBox Đến]
        /// Tỉ lệ cột: 50% | 20% | 8% | 22%  → tự co giãn theo chiều rộng form
        /// </summary>
        private void BuildInputRow(
            TableLayoutPanel table,
            Label lblDesc, string descText,
            TextBox txtFrom, Label lblDen, TextBox txtTo)
        {
            table.ColumnCount = 4;
            table.RowCount = 1;
            table.Dock = DockStyle.Fill;
            table.Margin = new Padding(0, 4, 0, 4);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblDesc.Text = descText;
            lblDesc.Dock = DockStyle.Fill;
            lblDesc.TextAlign = ContentAlignment.MiddleLeft;
            lblDesc.Font = new Font("Times New Roman", 11F);

            txtFrom.Dock = DockStyle.Fill;
            txtFrom.Font = new Font("Times New Roman", 11F);

            lblDen.Text = "Đến";
            lblDen.Dock = DockStyle.Fill;
            lblDen.TextAlign = ContentAlignment.MiddleCenter;
            lblDen.Font = new Font("Times New Roman", 11F);

            txtTo.Dock = DockStyle.Fill;
            txtTo.Font = new Font("Times New Roman", 11F);

            table.Controls.Add(lblDesc, 0, 0);
            table.Controls.Add(txtFrom, 1, 0);
            table.Controls.Add(lblDen, 2, 0);
            table.Controls.Add(txtTo, 3, 0);
        }

        #endregion

        // ── Fields ──
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TableLayoutPanel panelFooter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel panelBody;

        private System.Windows.Forms.TableLayoutPanel tableQYd;
        private System.Windows.Forms.Label lblQYd;
        private System.Windows.Forms.TextBox txtQYdTu;
        private System.Windows.Forms.Label lblQYdDen;
        private System.Windows.Forms.TextBox txtQYdDen;

        private System.Windows.Forms.TableLayoutPanel tableQYe;
        private System.Windows.Forms.Label lblQYe;
        private System.Windows.Forms.TextBox txtDYeTu;
        private System.Windows.Forms.Label lblQYeDen;
        private System.Windows.Forms.TextBox txtQYeDen;

        private System.Windows.Forms.TableLayoutPanel tableYte;
        private System.Windows.Forms.Label lblYte;
        private System.Windows.Forms.TextBox txtYteTu;
        private System.Windows.Forms.Label lblYteDen;
        private System.Windows.Forms.TextBox txtYteDen;

        private System.Windows.Forms.TableLayoutPanel tableTong;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.TextBox txtTongTu;
        private System.Windows.Forms.Label lblTongDen;
        private System.Windows.Forms.TextBox txtTongDen;
    }
}