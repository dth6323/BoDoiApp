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
            this.lblPassword = new System.Windows.Forms.Label();

            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();

            this.btn_dn = new System.Windows.Forms.Button();
            this.btn_dk = new System.Windows.Forms.Button();

            // UserControl
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(this.tblRoot);

            // tblRoot
            this.tblRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoot.ColumnCount = 3;
            this.tblRoot.RowCount = 3;
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoot.Controls.Add(this.pnlLogin, 1, 1);

            // pnlLogin
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLogin.Size = new System.Drawing.Size(420, 280);
            this.pnlLogin.Controls.Add(this.tblLogin);

            // tblLogin
            this.tblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLogin.ColumnCount = 2;
            this.tblLogin.RowCount = 5;

            this.tblLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tblLogin.Controls.Add(this.lblTitle, 0, 0);
            this.tblLogin.SetColumnSpan(this.lblTitle, 2);

            this.tblLogin.Controls.Add(this.lblUsername, 0, 1);
            this.tblLogin.Controls.Add(this.txt_username, 1, 1);

            this.tblLogin.Controls.Add(this.lblPassword, 0, 2);
            this.tblLogin.Controls.Add(this.txt_password, 1, 2);

            this.tblLogin.Controls.Add(this.btn_dn, 0, 3);
            this.tblLogin.Controls.Add(this.btn_dk, 1, 3);

            // Labels
            this.lblTitle.AutoSize = true;
            this.lblTitle.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblUsername.AutoSize = true;
            this.lblUsername.Text = "Tên đăng nhập";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblPassword.AutoSize = true;
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // TextBox
            this.txt_username.Dock = System.Windows.Forms.DockStyle.Fill;

            this.txt_password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_password.PasswordChar = '●';

            // Buttons
            this.btn_dn.Text = "Đăng nhập";
            this.btn_dn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_dn.Click += new System.EventHandler(this.btn_dn_Click);

            this.btn_dk.Text = "Đăng ký";
            this.btn_dk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_dk.Click += new System.EventHandler(this.btn_dk_Click);
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
    }
}
