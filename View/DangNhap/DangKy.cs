using BoDoiApp.DataLayer;
using System;
using System.Windows.Forms;

namespace BoDoiApp.View.DangNhap
{
    public partial class DangKy : UserControl
    {
        private readonly User _user;

        public DangKy()
        {
            InitializeComponent();
            _user = new User();
        }

        private void btn_reg_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string fullName = txt_fullname.Text.Trim();
            string password = txt_password.Text;
            string rePassword = txt_repassword.Text;

            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(rePassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (password != rePassword)
            {
                MessageBox.Show(
                    "Mật khẩu nhập lại không khớp!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txt_repassword.Focus();
                return;
            }

            try
            {
                // Register(username, password, fullName)
                _user.Register(username, password, fullName);

                MessageBox.Show(
                    "Đăng ký thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                NavigationService.Navigate(new form.dn());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đăng ký thất bại: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new form.dn());
        }
    }
}
