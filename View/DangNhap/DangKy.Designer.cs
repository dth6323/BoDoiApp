namespace BoDoiApp.View.DangNhap
{
    partial class DangKy
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
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.tblRegister = new System.Windows.Forms.TableLayoutPanel();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblFullname = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblRePassword = new System.Windows.Forms.Label();

            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_fullname = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_repassword = new System.Windows.Forms.TextBox();

            this.btn_reg = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();

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
            this.tblRoot.Controls.Add(this.pnlRegister, 1, 1);

            // pnlRegister
            this.pnlRegister.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRegister.Padding = new System.Windows.Forms.Padding(20);
            this.pnlRegister.Size = new System.Drawing.Size(460, 380);
            this.pnlRegister.Controls.Add(this.tblRegister);

            // tblRegister
            this.tblRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRegister.ColumnCount = 2;
            this.tblRegister.RowCount = 7;

            this.tblRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblRegister.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tblRegister.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.tblRegister.Controls.Add(this.lblTitle, 0, 0);
            this.tblRegister.SetColumnSpan(this.lblTitle, 2);

            this.tblRegister.Controls.Add(this.lblUsername, 0, 1);
            this.tblRegister.Controls.Add(this.txt_username, 1, 1);

            this.tblRegister.Controls.Add(this.lblFullname, 0, 2);
            this.tblRegister.Controls.Add(this.txt_fullname, 1, 2);

            this.tblRegister.Controls.Add(this.lblPassword, 0, 3);
            this.tblRegister.Controls.Add(this.txt_password, 1, 3);

            this.tblRegister.Controls.Add(this.lblRePassword, 0, 4);
            this.tblRegister.Controls.Add(this.txt_repassword, 1, 4);

            this.tblRegister.Controls.Add(this.btn_reg, 0, 5);
            this.tblRegister.Controls.Add(this.btn_back, 1, 5);

            // Labels
            // Labels
            this.lblTitle.AutoSize = true;
            this.lblUsername.AutoSize = true;
            this.lblFullname.AutoSize = true;
            this.lblPassword.AutoSize = true;
            this.lblRePassword.AutoSize = true;

            this.lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;

            this.lblUsername.Text = "Tên đăng nhập";
            this.lblFullname.Text = "Họ và tên";
            this.lblPassword.Text = "Mật khẩu";
            this.lblRePassword.Text = "Nhập lại mật khẩu";

            this.lblUsername.TextAlign =
            this.lblFullname.TextAlign =
            this.lblPassword.TextAlign =
            this.lblRePassword.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;


            // TextBox
            this.txt_username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_fullname.Dock = System.Windows.Forms.DockStyle.Fill;

            this.txt_password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_password.PasswordChar = '●';

            this.txt_repassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_repassword.PasswordChar = '●';

            // Buttons
            this.btn_reg.Text = "Đăng ký";
            this.btn_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_reg.Click += new System.EventHandler(this.btn_reg_Click);

            this.btn_back.Text = "Quay lại";
            this.btn_back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblRoot;
        private System.Windows.Forms.Panel pnlRegister;
        private System.Windows.Forms.TableLayoutPanel tblRegister;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblFullname;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblRePassword;

        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_fullname;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_repassword;

        private System.Windows.Forms.Button btn_reg;
        private System.Windows.Forms.Button btn_back;
    }
}
