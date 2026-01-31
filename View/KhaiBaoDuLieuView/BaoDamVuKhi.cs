using System;
using System.IO;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BaoDamVuKhi : UserControl
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

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new TiepNhanBoSung());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
