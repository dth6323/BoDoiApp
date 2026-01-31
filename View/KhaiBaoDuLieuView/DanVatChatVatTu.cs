using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class DanVatChatVatTu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");
        public DanVatChatVatTu()
        {
            InitializeComponent();
        }

        private void DanVatChatVatTu_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[3];
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
            NavigationService.Navigate(new PhanCapVatLieu());
        }
    }
}
