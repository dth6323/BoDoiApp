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
    public partial class DanVatChatVatTu : Form
    {
        private const string EXCEL_PATH = @"D:\document\Thaiha\BoDoiApp\Resources\Sheet\Book1.xlsx";
        public DanVatChatVatTu()
        {
            InitializeComponent();
        }

        private void DanVatChatVatTu_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[3];
        }
    }
}
