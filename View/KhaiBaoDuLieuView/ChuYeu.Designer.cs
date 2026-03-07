namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    partial class ChuYeu
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();

            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // reoGridControl1
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 40;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.TabIndex = 0;

            // tableLayoutPanel1
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Height = 45;

            // button1 (Trở về)
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button1.Text = "Trở về";
            this.button1.Width = 100;
            this.button1.Height = 30;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // button3 (Trang chủ)
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Text = "Trang chủ";
            this.button3.Width = 110;
            this.button3.Height = 30;
            this.button3.Click += new System.EventHandler(this.button3_Click);

            // button2 (Lưu)
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Text = "Lưu";
            this.button2.Width = 100;
            this.button2.Height = 30;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            // ChuYeu
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reoGridControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChuYeu";
            this.Size = new System.Drawing.Size(853, 468);
            this.Load += new System.EventHandler(this.ChuYeu_Load);

            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}