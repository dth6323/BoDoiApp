namespace BoDoiApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.btn_kbdl = new System.Windows.Forms.Button();
            this.btn_pbnv = new System.Windows.Forms.Button();
            this.btn_dkkhbdhckt = new System.Windows.Forms.Button();
            this.btn_khts = new System.Windows.Forms.Button();
            this.btn_khbdhckt = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_kbdl
            // 
            this.btn_kbdl.Location = new System.Drawing.Point(4, 5);
            this.btn_kbdl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_kbdl.Name = "btn_kbdl";
            this.btn_kbdl.Size = new System.Drawing.Size(198, 221);
            this.btn_kbdl.TabIndex = 0;
            this.btn_kbdl.Text = "Khai báo dữ liệu";
            this.btn_kbdl.UseVisualStyleBackColor = true;
            this.btn_kbdl.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_pbnv
            // 
            this.btn_pbnv.Location = new System.Drawing.Point(210, 5);
            this.btn_pbnv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_pbnv.Name = "btn_pbnv";
            this.btn_pbnv.Size = new System.Drawing.Size(201, 221);
            this.btn_pbnv.TabIndex = 0;
            this.btn_pbnv.Text = "Phổ biến nhiệm vụ";
            this.btn_pbnv.UseVisualStyleBackColor = true;
            this.btn_pbnv.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_dkkhbdhckt
            // 
            this.btn_dkkhbdhckt.Location = new System.Drawing.Point(419, 5);
            this.btn_dkkhbdhckt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_dkkhbdhckt.Name = "btn_dkkhbdhckt";
            this.btn_dkkhbdhckt.Size = new System.Drawing.Size(217, 221);
            this.btn_dkkhbdhckt.TabIndex = 0;
            this.btn_dkkhbdhckt.Text = "Dự kiến kế hoạch bảo đảm hậu cần, kỹ thuật";
            this.btn_dkkhbdhckt.UseVisualStyleBackColor = true;
            this.btn_dkkhbdhckt.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_khts
            // 
            this.btn_khts.Location = new System.Drawing.Point(644, 5);
            this.btn_khts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_khts.Name = "btn_khts";
            this.btn_khts.Size = new System.Drawing.Size(218, 221);
            this.btn_khts.TabIndex = 0;
            this.btn_khts.Text = "Kế hoạch trinh sát";
            this.btn_khts.UseVisualStyleBackColor = true;
            this.btn_khts.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_khbdhckt
            // 
            this.btn_khbdhckt.Location = new System.Drawing.Point(870, 5);
            this.btn_khbdhckt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_khbdhckt.Name = "btn_khbdhckt";
            this.btn_khbdhckt.Size = new System.Drawing.Size(229, 221);
            this.btn_khbdhckt.TabIndex = 0;
            this.btn_khbdhckt.Text = "Kế hoạch bảo đảm hậu cần kỹ thuật";
            this.btn_khbdhckt.UseVisualStyleBackColor = true;
            this.btn_khbdhckt.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_kbdl);
            this.flowLayoutPanel1.Controls.Add(this.btn_pbnv);
            this.flowLayoutPanel1.Controls.Add(this.btn_dkkhbdhckt);
            this.flowLayoutPanel1.Controls.Add(this.btn_khts);
            this.flowLayoutPanel1.Controls.Add(this.btn_khbdhckt);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(218, 156);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1197, 265);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1876, 1050);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_kbdl;
        private System.Windows.Forms.Button btn_pbnv;
        private System.Windows.Forms.Button btn_dkkhbdhckt;
        private System.Windows.Forms.Button btn_khts;
        private System.Windows.Forms.Button btn_khbdhckt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

