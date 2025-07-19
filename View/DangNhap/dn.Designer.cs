namespace BoDoiApp.form
{
    partial class dn
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
            this.txt_tk = new System.Windows.Forms.TextBox();
            this.txt_mk = new System.Windows.Forms.TextBox();
            this.btn_dk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_dn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_tk
            // 
            this.txt_tk.Location = new System.Drawing.Point(209, 101);
            this.txt_tk.Name = "txt_tk";
            this.txt_tk.Size = new System.Drawing.Size(151, 20);
            this.txt_tk.TabIndex = 0;
            // 
            // txt_mk
            // 
            this.txt_mk.Location = new System.Drawing.Point(209, 127);
            this.txt_mk.Name = "txt_mk";
            this.txt_mk.Size = new System.Drawing.Size(151, 20);
            this.txt_mk.TabIndex = 0;
            // 
            // btn_dk
            // 
            this.btn_dk.Location = new System.Drawing.Point(285, 173);
            this.btn_dk.Name = "btn_dk";
            this.btn_dk.Size = new System.Drawing.Size(75, 23);
            this.btn_dk.TabIndex = 1;
            this.btn_dk.Text = "Đăng ký";
            this.btn_dk.UseVisualStyleBackColor = true;
            this.btn_dk.Click += new System.EventHandler(this.btn_dk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tài khoản";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_dn
            // 
            this.btn_dn.Location = new System.Drawing.Point(204, 173);
            this.btn_dn.Name = "btn_dn";
            this.btn_dn.Size = new System.Drawing.Size(75, 23);
            this.btn_dn.TabIndex = 1;
            this.btn_dn.Text = "Đăng nhập";
            this.btn_dn.UseVisualStyleBackColor = true;
            this.btn_dn.Click += new System.EventHandler(this.btn_dn_Click);
            // 
            // dn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 275);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_dn);
            this.Controls.Add(this.btn_dk);
            this.Controls.Add(this.txt_mk);
            this.Controls.Add(this.txt_tk);
            this.Name = "dn";
            this.Text = "dn";
            this.Load += new System.EventHandler(this.dn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_tk;
        private System.Windows.Forms.TextBox txt_mk;
        private System.Windows.Forms.Button btn_dk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_dn;
    }
}