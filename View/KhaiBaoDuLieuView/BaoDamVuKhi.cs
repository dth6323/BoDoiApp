using System;
using System.IO;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BaoDamVuKhi : Form
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");

        public BaoDamVuKhi()
        {
            InitializeComponent();
        }

        private void BaoDamVuKhi_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[2];
        }
    }
}
