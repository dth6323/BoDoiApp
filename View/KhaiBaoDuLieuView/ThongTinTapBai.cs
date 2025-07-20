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
            if (dt.Rows.Count > 0)
            {
                isAddingNew = false;
                DataRow row = dt.Rows[0];
                txt_tdbt.Text = row["tendaubai"]?.ToString() ?? "";
                txt_sch.Text = row["sochuy"]?.ToString() ?? "";
                txt_bdtb.Text = row["bandotapbai"]?.ToString() ?? "";
                txt_m1.Text = row["manh1"]?.ToString() ?? "";
                txt_m2.Text = row["manh2"]?.ToString() ?? "";
                txt_m3.Text = row["manh3"]?.ToString() ?? "";
                txt_m4.Text = row["manh4"]?.ToString() ?? "";
                txt_chtd.Text = row["chihuyduan"]?.ToString() ?? "";
                txt_chhc.Text = row["chihuyhaucan"]?.ToString() ?? "";
                txt_chtdtt.Text = row["chihuyduan_tt"]?.ToString() ?? "";
                txt_chhctt.Text = row["chihuyhaucan_tt"]?.ToString() ?? "";
                txt_ct.Text = row["captren"]?.ToString() ?? "";
                txt_cm.Text = row["capminh"]?.ToString() ?? "";
                // ... các textbox khác tương tự
            }
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {    // Khởi tạo đối tượng ThongTinTapBai

            // Lấy dữ liệu từ các TextBox (cho phép trống)
            string tenDauBai = txt_tdbt.Text ?? "";
            string soChuy = txt_sch.Text ?? "";
            string banDoTapBai = txt_bdtb.Text ?? "";
            string manh1 = txt_m1.Text ?? "";
            string manh2 = txt_m2.Text ?? "";
            string manh3 = txt_m3.Text ?? "";
            string manh4 = txt_m4.Text ?? "";
            string chiHuyTieuDoan = txt_chtd.Text ?? "";
            string chiHuyHauCan = txt_chhc.Text ?? "";
            string chiHuyTieuDoanTT = txt_chtdtt.Text ?? "";
            string chiHuyHauCanTT = txt_chhctt.Text ?? "";
            string capTren = txt_ct.Text ?? "";
            string capMinh = txt_cm.Text ?? "";

            bool result;

            // Kiểm tra nếu tất cả TextBox đều trống thì thêm mới, ngược lại thì cập nhật
            if (isAddingNew==true)
            {
                // Thêm mới thông tin
                result = thongTinTapBai.ThemThongTin(tenDauBai, soChuy, banDoTapBai, manh1, manh2, manh3, manh4,
                    chiHuyTieuDoan, chiHuyHauCan, chiHuyTieuDoanTT, chiHuyHauCanTT, capTren, capMinh);
                if (result)
                {
                    MessageBox.Show("Thêm thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Cập nhật thông tin dựa trên User
                result = thongTinTapBai.CapNhatThongTin(tenDauBai, soChuy, banDoTapBai, manh1, manh2, manh3, manh4,
                    chiHuyTieuDoan, chiHuyHauCan, chiHuyTieuDoanTT, chiHuyHauCanTT, capTren, capMinh);
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


    }
}
