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
    public partial class QuyDinhDuTruTieuThuBoSungVatChat : Form
    {
        QuyDinhDuTruTieuThuBoSungData quyDinhDuTruTieuThuBoSungData = new QuyDinhDuTruTieuThuBoSungData();
        bool isAddingNew = true;
        public QuyDinhDuTruTieuThuBoSungVatChat()
        {
            InitializeComponent();
        }

        private void QuyDinhDuTruTieuThuBoSungVatChat_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("I", "Đạn");
            dataGridView1.Rows.Add("1", "SMPK 12,7 mm", "Cơ Số");
            dataGridView1.Rows.Add("2", "Đạn BB nhóm 1");
            dataGridView1.Rows.Add("-", "súng ngắn", "Cơ Số");
            dataGridView1.Rows.Add("-", "Tiểu liên", "Cơ Số");
            dataGridView1.Rows.Add("-", "Trung liên", "Cơ Số");
            dataGridView1.Rows.Add("-", "Đại liên", "Cơ Số");
            dataGridView1.Rows.Add("-", "B41", "Cơ Số");
            dataGridView1.Rows.Add("3", "Đạn BB nhóm 2");
            dataGridView1.Rows.Add("-", "Co100mm", "Cơ Số");
            dataGridView1.Rows.Add("-", "Co82mm", "Cơ Số");
            dataGridView1.Rows.Add("-", "Co60mm", "Cơ Số");
            dataGridView1.Rows.Add("-", "SPG-9", "Cơ Số");
            dataGridView1.Rows.Add("4", "Lựu đạn", "Cơ Số");
            dataGridView1.Rows.Add("II", "Vật chất hậu cần");
            dataGridView1.Rows.Add("1", "Gạo", "Ngày ăn");
            dataGridView1.Rows.Add("2", "Thực phẩm");
            dataGridView1.Rows.Add("3", "Lương khô");
            dataGridView1.Rows.Add("4", "Đường sữa TB", "%QS");
            dataGridView1.Rows.Add("5", "QLCĐ", "%QS");
            dataGridView1.Rows.Add("6", "QTCĐ", "%QS");
            dataGridView1.Rows.Add("7", "Túi y sỹ", "Túi");
            dataGridView1.Rows.Add("8", "Túi y tá");
            dataGridView1.Rows.Add("9", "Túi cứu thương");
            dataGridView1.Rows.Add("10", "Băng cá nhân", "C/người");
            dataGridView1.Rows.Add("11", "Dầu thắp", "Ngày");
            dataGridView1.Rows.Add("III", "Vật tư kỹ thuật", "Tấn");
            DataTable dt = quyDinhDuTruTieuThuBoSungData.LayThongTin();
            if (dt.Rows.Count > 0)
            {
                isAddingNew = false;
                for (int i = 0, j = 1; i < dt.Rows.Count; i++, j++)
                {
                    try
                    {
                        if (j == 8 || j == 14) j++;
                        DataRow row = dt.Rows[i];
                        string qddt = row["QuyDinhDuTru"]?.ToString() ?? "";
                        string pc0400 = row["PhaiCo0400N"]?.ToString() ?? "";
                        string pcscd = row["PhaiCSCD"]?.ToString() ?? "";
                        string cdcb = row["TieuThuGDCB"]?.ToString() ?? "";
                        string cdcd = row["TieuThuGDCD"]?.ToString() ?? "";
                        dataGridView1.Rows[j].Cells[3].Value = qddt;
                        dataGridView1.Rows[j].Cells[4].Value = pc0400;
                        dataGridView1.Rows[j].Cells[5].Value = pcscd;
                        dataGridView1.Rows[j].Cells[6].Value = cdcb;
                        dataGridView1.Rows[j].Cells[7].Value = cdcd;
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAddingNew)
            {
                quyDinhDuTruTieuThuBoSungData.ThemHangLoat(dataGridView1);
                isAddingNew = false;
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                quyDinhDuTruTieuThuBoSungData.SuaHangLoat(dataGridView1);
                MessageBox.Show("Sửa thành công");
            }
        }
    }
}
