using BoDoiApp.DataLayer;
using BoDoiApp.View;
using BoDoiApp.View.DangNhap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.form
{
    public partial class dn : UserControl
    {
        private readonly User u = new User();
        public dn()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_dn_Click(object sender, EventArgs e)
        {
            string username = txt_tk.Text;
            string password = txt_mk.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (u.Login(username, password))
            {
                // Lưu thông tin đăng nhập (ví dụ: sử dụng Properties.Settings)
                Properties.Settings.Default.IsLoggedIn = true;
                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Save();
                FormMana.Formmain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên người dùng hoặc mật khẩu không đúng!");
            }
        }

        private void dn_Load(object sender, EventArgs e)
        {

        }

        private void btn_dk_Click(object sender, EventArgs e)
        {
            FormMana.Dangky.Show();
            this.Hide();
        }
    }
}
