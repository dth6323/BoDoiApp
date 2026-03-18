using System;
using System.Windows.Forms;
using System.Drawing;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    partial class _3YDinh
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.TableLayoutPanel();
            this.lblDocTitle = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1100, 65);
            this.panelHeader.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1100, 65);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "PHẦN MỀM HỖ TRỢ TẬP BÀI BẢO ĐẢM HẬU CẦN, KỸ THUẬT TIỂU ĐOÀN BỘ BINH CHIẾN ĐẤU";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.ColumnCount = 3;
            this.panelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.panelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.panelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.panelFooter.Controls.Add(this.button1, 0, 0);
            this.panelFooter.Controls.Add(this.button2, 1, 0);
            this.panelFooter.Controls.Add(this.button3, 2, 0);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 545);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.RowCount = 1;
            this.panelFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelFooter.Size = new System.Drawing.Size(1100, 55);
            this.panelFooter.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(360, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Trở về";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(110)))), ((int)(((byte)(253)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(369, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(360, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Dự Kiến";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(135)))), ((int)(((byte)(84)))));
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(735, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(362, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "Tiếp";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panelBody
            // 
            this.panelBody.ColumnCount = 1;
            this.panelBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelBody.Controls.Add(this.lblDocTitle, 0, 0);
            this.panelBody.Controls.Add(this.lblSection, 0, 1);
            this.panelBody.Controls.Add(this.lblSub, 0, 2);
            this.panelBody.Controls.Add(this.richTextBox1, 0, 3);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 65);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(30, 20, 30, 10);
            this.panelBody.RowCount = 4;
            this.panelBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelBody.Size = new System.Drawing.Size(1100, 480);
            this.panelBody.TabIndex = 0;
            this.panelBody.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBody_Paint);
            // 
            // lblDocTitle
            // 
            this.lblDocTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDocTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.lblDocTitle.Location = new System.Drawing.Point(30, 20);
            this.lblDocTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblDocTitle.Name = "lblDocTitle";
            this.lblDocTitle.Size = new System.Drawing.Size(1040, 23);
            this.lblDocTitle.TabIndex = 0;
            this.lblDocTitle.Text = "Dự kiến kế hoạch bảo đảm hậu cần - kỹ thuật";
            this.lblDocTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSection
            // 
            this.lblSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSection.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblSection.Location = new System.Drawing.Point(30, 53);
            this.lblSection.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(1040, 23);
            this.lblSection.TabIndex = 1;
            this.lblSection.Text = "VII. Bảo đảm quân y";
            // 
            // lblSub
            // 
            this.lblSub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSub.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.lblSub.Location = new System.Drawing.Point(30, 80);
            this.lblSub.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(1040, 23);
            this.lblSub.TabIndex = 2;
            this.lblSub.Text = "3. Ý định bảo đảm";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(33, 112);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(1034, 355);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // _3YDinh
            // 
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelFooter);
            this.Font = new System.Drawing.Font("Times New Roman", 11F);
            this.Name = "_3YDinh";
            this.Size = new System.Drawing.Size(1100, 600);
            this.Load += new System.EventHandler(this._3YDinh_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TableLayoutPanel panelFooter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel panelBody;
        private System.Windows.Forms.Label lblDocTitle;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}