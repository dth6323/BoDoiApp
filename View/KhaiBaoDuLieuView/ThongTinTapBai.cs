using BoDoiApp.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ThongTinTapBai : Form
    {
        ThongTinTapBaiData thongTinTapBai = new ThongTinTapBaiData();
        bool isAddingNew = true;
        int currentId = 0;
        public ThongTinTapBai()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThongTinTapBai_Load(object sender, EventArgs e)
        {
            DataTable dt = thongTinTapBai.LayThongTin();

            if (dt != null && dt.Rows.Count > 0)
            {
                isAddingNew = false;
                DataRow row = dt.Rows[0];

                currentId = Convert.ToInt32(row["id"]);

                txt_tenvankien.Text = row["thongtintapbai"]?.ToString() ?? "";
                txt_vtch.Text = row["vitrichihuy"]?.ToString() ?? "";
                txt_thoigian.Text = row["thoigian"]?.ToString() ?? "";

                txt_m1.Text = row["manh1"]?.ToString() ?? "";
                txt_m2.Text = row["manh2"]?.ToString() ?? "";
                txt_m3.Text = row["manh3"]?.ToString() ?? "";
                txt_m4.Text = row["manh4"]?.ToString() ?? "";

            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string thongTinTapBai = txt_tenvankien.Text;
            string viTriChiHuy = txt_vtch.Text;
            string thoiGian = txt_thoigian.Text;

            string manh1 = txt_m1.Text;
            string manh2 = txt_m2.Text;
            string manh3 = txt_m3.Text;
            string manh4 = txt_m4.Text;
            string tyle = txt_tyle.Text;
            string nam = txt_nam.Text;
            string chiHuyHCKT = txt_chhckt.Text;
            string nguoiThayThe = txt_nguoithaythe.Text;

            bool result;

            if (isAddingNew)
            {
                result = this.thongTinTapBai.ThemThongTin(
                    thongTinTapBai, viTriChiHuy, thoiGian,
                    manh1, manh2, manh3, manh4,
                    tyle, nam, chiHuyHCKT, nguoiThayThe
                );

                if (result)
                    MessageBox.Show("Thêm thông tin thành công!", "Thành công");

                isAddingNew = false;
            }
            else
            {
                result = this.thongTinTapBai.CapNhatThongTin(
                    thongTinTapBai, viTriChiHuy, thoiGian,
                    manh1, manh2, manh3, manh4,
                    tyle, nam, chiHuyHCKT, nguoiThayThe
                );

                if (result)
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (FormMana.KhaiBaoDuLieu == null)
            {
                MessageBox.Show("Form KhaiBaoDuLieu đang NULL");
                return;
            }

            FormMana.KhaiBaoDuLieu.Show();
            this.Hide();
        }



    }
}