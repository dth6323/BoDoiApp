using BoDoiApp.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class QuanSoChienDau : Form
    {
        QuanSoChienDauData quanSoChienDauData = new QuanSoChienDauData();
        bool isAddingNew = true;
        public QuanSoChienDau()
        {
            InitializeComponent();
        }

        private void QuanSoChienDau_Load(object sender, EventArgs e)
        {
            DataTable dt = quanSoChienDauData.LayThongTin();
            if (dt.Rows.Count > 0)
            {
                isAddingNew = false;
                DataRow row = dt.Rows[0];
                txt_phdv1.Text = row["phienhieudonvi"]?.ToString() ?? "";
                txt_phdv2.Text = row["phdv1"]?.ToString() ?? "";
                txt_phdv3.Text = row["phdv1"]?.ToString() ?? "";
                txt_phdv4.Text = row["phdv1"]?.ToString() ?? "";
                txt_phdv5.Text = row["phdv1"]?.ToString() ?? "";
                txt_phdv6.Text = row["phdv1"]?.ToString() ?? "";
                txt_qscd1.Text = row["quansochiendau"]?.ToString() ?? "";
                txt_qscd2.Text = row["qscd1"]?.ToString() ?? "";
                txt_qscd3.Text = row["qscd1"]?.ToString() ?? "";
                txt_qscd4.Text = row["phdv1"]?.ToString() ?? "";
                txt_qscd5.Text = row["phdv1"]?.ToString() ?? "";
                txt_qscd6.Text = row["phdv1"]?.ToString() ?? "";
            }
        }
        private void txt_luu_Click(object sender, EventArgs e)
        {
            string phdv1 = txt_phdv1.Text ?? "";
            string phdv2 = txt_phdv2.Text ?? "";
            string phdv3 = txt_phdv3.Text  ?? "";
            string phdv4 = txt_phdv4.Text  ?? "";
            string phdv5 = txt_phdv5.Text  ?? "";
            string phdv6 = txt_phdv6.Text  ?? "";
            string qscd1 = txt_qscd1.Text  ?? "";
            string qscd2 = txt_qscd2.Text  ?? "";
            string qscd3 = txt_qscd3.Text  ?? "";
            string qscd4 = txt_qscd4.Text  ?? "";
            string qscd5 = txt_qscd5.Text  ?? "";
            string qscd6 = txt_qscd6.Text  ?? "";
            bool result;

            // Kiểm tra nếu tất cả TextBox đều trống thì thêm mới, ngược lại thì cập nhật
            if (isAddingNew == true)
            {
                // Thêm mới thông tin
                result = quanSoChienDauData.ThemThongTin(phdv1, phdv2, phdv3, phdv4, phdv5, phdv6,qscd1, qscd2, qscd3, qscd4, qscd5, qscd6);
                if (result)
                {
                    MessageBox.Show("Thêm thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                isAddingNew = false;
            }
            else
            {
                // Cập nhật thông tin dựa trên User
                result = quanSoChienDauData.CapNhatThongTin(phdv1, phdv2, phdv3, phdv4, phdv5, phdv6, qscd1, qscd2, qscd3, qscd4, qscd5, qscd6);
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            FormMana.KhaiBaoDuLieu.Show();
            this.Hide();
        }
    }
}
