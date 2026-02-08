using BoDoiApp.DataLayer;
using BoDoiApp.View;
using BoDoiApp.View.DangNhap;
using System;
using System.Windows.Forms;

namespace BoDoiApp.form
{
    public partial class dn : UserControl
    {
        private readonly User _user;

        public dn()
        {
            InitializeComponent();
            _user = new User();
        }

        private void btn_dn_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string password = txt_password.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                if (_user.Login(username, password))
                {
                    Properties.Settings.Default.IsLoggedIn = true;
                    Properties.Settings.Default.Username = username;
                    Properties.Settings.Default.Save();

                    NavigationService.Navigate(new Form1());
                }
                else
                {
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không đúng!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Có lỗi xảy ra: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btn_dk_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new DangKy());
        }
    }
}
