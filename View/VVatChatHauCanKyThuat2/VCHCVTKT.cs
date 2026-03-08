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


                reoGridControl1.CurrentWorksheet = sheet2;
                sheet2.Recalculate();
                reoGridControl1.SheetTabVisible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LỖI: " + ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VCHCVTKTDATA.SaveAll(reoGridControl1);

            NavigationService.Navigate(() => new TiepNhanBoXungV());
        }
    }
}
