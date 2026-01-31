using BoDoiApp.DataLayer;
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

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ThuYeu : UserControl
    {
        private const string EXCEL_PATH = @"D:\document\Thaiha\BoDoiApp\Resources\Sheet\Book1.xlsx";
        public ThuYeu()
        {
            InitializeComponent();
        }

        private void ThuYeu_Load(object sender, EventArgs e)
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("File Excel không tồn tại tại đường dẫn: " + EXCEL_PATH, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                reoGridControl1.Load(EXCEL_PATH);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[1];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChuYeuData.UpdateHangLoat(reoGridControl1, "thu yeu");
        }
    }
}
