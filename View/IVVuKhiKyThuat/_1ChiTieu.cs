using BoDoiApp.Resources;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace BoDoiApp.View.IVVuKhiKyThuat
{
    public partial class _1ChiTieu : UserControl
    {
        private static string Basedir = AppDomain.CurrentDomain.BaseDirectory;
        private static string EXCEL_PATH = Path.Combine(Basedir, "Resources", "Sheet", "ChiTieu.xlsx");
        private string UserId = Constants.CURRENT_USER_ID_VALUE;
        public _1ChiTieu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
           NavigationService.Back();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Form1());

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
           
            NavigationService.Navigate(new _2BienPhapBaoDam());
        }

        private void _1ChiTieu_Load(object sender, System.EventArgs e)
        {
           reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[7];

        }

        private void LoadExcel()
        {
           
        }
        
        private void LoadSection(int rowStart,int rowEnd,string section,string userId)
        {
            string sql = $"SELECT Value FROM trangkithuat WHERE User = '{userId}' AND option = '{section}'";
            using (var connection = new SqlConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string value = reader.GetString(0);
                            for (int i = rowStart; i <= rowEnd; i++)
                            {
                                reoGridControl1.CurrentWorksheet.SetCellData(i, 1, value);
                            }
                        }
                    }
                }
            }
        }
    }
}
