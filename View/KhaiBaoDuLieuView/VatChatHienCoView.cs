using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoDoiApp.DataLayer;
namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class VatChatHienCoView : Form
    {
        VatChatNguoiDungData vchc = new VatChatNguoiDungData();
        bool isAddingNew = true;
        public VatChatHienCoView()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void VatChatHienCoView_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("I", "Đạn");
            dataGridView1.Rows.Add("1", "Đạn BB nhóm I","CS");
            dataGridView1.Rows.Add("-", "súng ngắn","");
            dataGridView1.Rows.Add("-", "Tiểu liên","");
            dataGridView1.Rows.Add("-", "Trung liên","");
            dataGridView1.Rows.Add("-", "Đại liên","");
            dataGridView1.Rows.Add("-", "B41","");
            dataGridView1.Rows.Add("2", "Đạn BB nhóm II","CS");
            dataGridView1.Rows.Add("-", "Co60mm","");
            dataGridView1.Rows.Add("-", "Co82mm");
            dataGridView1.Rows.Add("-", "Co100mm");
            dataGridView1.Rows.Add("-", "SPG-9");
            dataGridView1.Rows.Add("3", "Đạn SMPK 12,7");
            dataGridView1.Rows.Add("4", "Lựu đạn");
            dataGridView1.Rows.Add("II", "Vật chất hậu cần");
            dataGridView1.Rows.Add("1", "Gạo","Ngày ăn");
            dataGridView1.Rows.Add("2", "Thực phẩm");
            dataGridView1.Rows.Add("3", "Lương khô");
            dataGridView1.Rows.Add("4", "Đường sữa TB","%QS");
            dataGridView1.Rows.Add("5", "QLCĐ");
            dataGridView1.Rows.Add("6", "QTCĐ");
            dataGridView1.Rows.Add("7", "Túi y sỹ","Túi");
            dataGridView1.Rows.Add("8", "Túi y tá");
            dataGridView1.Rows.Add("9", "Túi cứu thương");
            dataGridView1.Rows.Add("10", "Băng cá nhân","C/người");
            dataGridView1.Rows.Add("13", "Dầu thắp","Ngày");
            dataGridView1.Rows.Add("III", "Vật tư kỹ thuật","Tấn");

            DataTable dt = vchc.LayThongTin();
            if (dt.Rows.Count > 0)
            {
                isAddingNew = false;
                for (int i = 0, j = 2; i < dt.Rows.Count; i++, j++)
                {
                    try
                    {
                        if (j == 7 || j == 14) j++;
                        DataRow row = dt.Rows[i];
                        string soLuong = row["SoLuong"]?.ToString() ?? "";
                        string ghiChu = row["GhiChu"]?.ToString() ?? "";
                        dataGridView1.Rows[j].Cells[3].Value = soLuong;
                        dataGridView1.Rows[j].Cells[4].Value = ghiChu;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(" " + j);
                        return;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                vchc.ThemHangLoat(dataGridView1);
                isAddingNew = false;
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                vchc.SuaHangLoat(dataGridView1);
                MessageBox.Show("Sửa thành công");
            }
        }
    }
}
