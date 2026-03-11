using BoDoiApp.DataLayer;
using BoDoiApp.DataLayer.KhaiBao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class KeHoach93 : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "test3.xlsx");
        public KeHoach93()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            KeHoachVanChuyenData.SaveAll(reoGridControl1);
            NavigationService.Navigate(() => new _4YdinhVanChuyen());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }
        private void KeHoach93_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(EXCEL_PATH))
                    return;

                reoGridControl1.Load(EXCEL_PATH);

                // Recalc sheet QSTBKT trước (sheet được tham chiếu)
                // Sau đó mới recalc sheet VCHCVTKT



                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["KhoiLuongVanTai"];
                KhoiLuongVanTaiData.LoadAll(reoGridControl1);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["DuKienTiLeTBBB"];
                KeHoachBaoDamQuanYData.LoadAll(reoGridControl1);
                var sheet2 = reoGridControl1.Worksheets["KeHoachVanChuyen"];
                reoGridControl1.CurrentWorksheet = sheet2;
                KeHoachVanChuyenData.LoadAll(reoGridControl1);
                sheet2.Recalculate();
                if (sheet2 == null)
                {
                    MessageBox.Show("Không tìm thấy sheet tên 'VCHCVTKT'!");
                    return;
                }
                KeHoachVanChuyenData.LockSheet(reoGridControl1);
                sheet2.HideColumns(13, sheet2.ColumnCount - 13);

                // Ẩn từ dòng 82 trở đi
                sheet2.HideRows(61, sheet2.RowCount - 61);
                reoGridControl1.SheetTabVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LỖI: " + ex.ToString());
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
