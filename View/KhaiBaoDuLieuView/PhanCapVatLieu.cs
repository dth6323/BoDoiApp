using BoDoiApp.DataLayer.KhaiBao;
using BoDoiApp.Resources;
using BoDoiApp.View.TinhHinhDonVi;
using System;
using System.IO;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class PhanCapVatLieu : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book3.xlsx");

        public PhanCapVatLieu()
        {
            InitializeComponent();
        }

        private void PhanCapVatLieu_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);

            // Sheet 1
            reoGridControl1.CurrentWorksheet =
                reoGridControl1.Worksheets["ChiLenhHCKT"];

            ChiLenhHKT1Data.LoadAll(reoGridControl1);

            // Sheet 2
            var sheet2 = reoGridControl1.Worksheets["PhanCapVCHC"];
            reoGridControl1.CurrentWorksheet = sheet2;

            PhanCapVatLieuData.LoadAll(reoGridControl1);

            reoGridControl1.SheetTabVisible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Lưu dữ liệu
            PhanCapVatLieuData.SaveAll(reoGridControl1);

            NavigationService.Navigate(() => new TiepNhanBoXungV());
        }
    }
}