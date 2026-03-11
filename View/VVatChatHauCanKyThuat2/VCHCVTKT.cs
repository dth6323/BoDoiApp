using BoDoiApp.DataLayer;
using BoDoiApp.DataLayer.KhaiBao;
using BoDoiApp.View.KhaiBaoDuLieuView;
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

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class VCHCVTKT : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Test.xlsx");
        public VCHCVTKT()
        {
            InitializeComponent();
        }

        private void VCHCVTKT_Load(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(EXCEL_PATH))
                    return;

                reoGridControl1.Load(EXCEL_PATH);

                // Recalc sheet QSTBKT trước (sheet được tham chiếu)
                // Sau đó mới recalc sheet VCHCVTKT
                var sheet2 = reoGridControl1.Worksheets["VCHCVTKT"];
                if (sheet2 == null)
                {
                    MessageBox.Show("Không tìm thấy sheet tên 'VCHCVTKT'!");
                    return;
                }

                 

                VCHCVTKTDATA.LoadAll(reoGridControl1);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["QSTBKT"];
                VCHCVTKTDATA.LoadTrangKiThuat(reoGridControl1);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["ChiLenhHCKT"];
                ChiLenhHKT1Data.LoadAllCell(reoGridControl1);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["PhanCapVCHC"];
                PhanCapVatLieuData.LoadAllVatChat(reoGridControl1);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets["TinhHinhVC"];
                TinhHinhVcData.LoadAllCell(reoGridControl1);
                reoGridControl1.CurrentWorksheet = sheet2;
                sheet2.Recalculate();
                VCHCVTKTDATA.LockSheet(reoGridControl1);
                sheet2.HideColumns(29, sheet2.ColumnCount - 29);

                // Ẩn từ dòng 82 trở đi
                sheet2.HideRows(81, sheet2.RowCount - 81);
                reoGridControl1.SheetTabVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LỖI: " + ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VCHCVTKTDATA.SaveAll(reoGridControl1);
            NavigationService.Navigate(() => new BienPhapDamBao());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }
    }
}
