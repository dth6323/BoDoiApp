namespace BoDoiApp.form
{
    partial class dn
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Designer

        private void InitializeComponent()
        {
            this.tblRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tblLogin = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_dn = new System.Windows.Forms.Button();
            this.btn_dk = new System.Windows.Forms.Button();
            this.tblRoot.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.tblLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblRoot
            // 
            this.tblRoot.ColumnCount = 3;
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.Controls.Add(this.pnlLogin, 1, 1);
            this.tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoot.Location = new System.Drawing.Point(0, 0);
            this.tblRoot.Name = "tblRoot";
            this.tblRoot.RowCount = 3;
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.Size = new System.Drawing.Size(1253, 693);
            this.tblRoot.TabIndex = 0;
            // 
            // pnlLogin
            // 
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.tblLogin);
            this.pnlLogin.Location = new System.Drawing.Point(416, 206);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLogin.Size = new System.Drawing.Size(420, 280);
            this.pnlLogin.TabIndex = 0;
            // 
            // tblLogin
            // 
            this.tblLogin.ColumnCount = 2;
            this.tblLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogin.Controls.Add(this.lblTitle, 0, 0);
            this.tblLogin.Controls.Add(this.lblUsername, 0, 1);
            this.tblLogin.Controls.Add(this.txt_username, 1, 1);
            this.tblLogin.Controls.Add(this.lblPassword, 0, 2);
            this.tblLogin.Controls.Add(this.txt_password, 1, 2);
            this.tblLogin.Controls.Add(this.btn_dn, 0, 3);
            this.tblLogin.Controls.Add(this.btn_dk, 1, 3);
            this.tblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLogin.Location = new System.Drawing.Point(20, 20);
            this.tblLogin.Name = "tblLogin";
            this.tblLogin.RowCount = 5;
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogin.Size = new System.Drawing.Size(378, 238);
            this.tblLogin.TabIndex = 0;
            this.tblLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.tblLogin_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.tblLogin.SetColumnSpan(this.lblTitle, 2);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(372, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(3, 40);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(116, 20);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Tên đăng nhập";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_username
            // 
            this.txt_username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_username.Location = new System.Drawing.Point(143, 43);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(232, 26);
            this.txt_username.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 20);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_password
            // 
            this.txt_password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_password.Location = new System.Drawing.Point(143, 83);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '●';
            this.txt_password.Size = new System.Drawing.Size(232, 26);
            this.txt_password.TabIndex = 4;
            // 
            // btn_dn
            // 
            this.btn_dn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_dn.Location = new System.Drawing.Point(3, 123);
            this.btn_dn.Name = "btn_dn";
            this.btn_dn.Size = new System.Drawing.Size(134, 39);
            this.btn_dn.TabIndex = 5;
            this.btn_dn.Text = "Đăng nhập";
            this.btn_dn.Click += new System.EventHandler(this.btn_dn_Click);
            // 
            // btn_dk
            // 
            this.btn_dk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_dk.Location = new System.Drawing.Point(143, 123);
            this.btn_dk.Name = "btn_dk";
            this.btn_dk.Size = new System.Drawing.Size(232, 39);
            this.btn_dk.TabIndex = 6;
            this.btn_dk.Text = "Đăng ký";
            this.btn_dk.Click += new System.EventHandler(this.btn_dk_Click);
            // 
            // dn
            // 
            this.Controls.Add(this.tblRoot);
            this.Name = "dn";
            this.Size = new System.Drawing.Size(1253, 693);
            this.tblRoot.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.tblLogin.ResumeLayout(false);
            this.tblLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblRoot;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TableLayoutPanel tblLogin;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;

        private System.Windows.Forms.Button btn_dn;
        private System.Windows.Forms.Button btn_dk;

        private void tblLogin_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
    }
}
