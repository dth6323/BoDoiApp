using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing.Text;
using System.IO;
using System.Security.Policy;
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
            LoadSection(13, 23, "Hướng Chủ yếu", UserId);
            LoadSection(24, 34, "thu yeu", UserId);
            LoadSection(35, 45, "Phòng ngự phía sau", UserId);
            LoadSection(46, 56, "LL còn lại", UserId);
        }

        private int[] SumOfChuYeuData(string option)
        {

            string sql = @"
        SELECT    
             SUM(CAST(COALESCE(quan_so, 0) AS INTEGER))     AS sum_quan_so,    
             SUM(CAST(COALESCE(sn, 0) AS INTEGER))          AS sum_sn,    
             SUM(CAST(COALESCE(tl, 0) AS INTEGER))          AS sum_tl,    
             SUM(CAST(COALESCE(trl, 0) AS INTEGER))         AS sum_trl,    
             SUM(CAST(COALESCE(dl, 0) AS INTEGER))          AS sum_dl,    
             SUM(CAST(COALESCE(b41_m79, 0) AS INTEGER))     AS sum_b41_m79,    
             SUM(CAST(COALESCE(luu_dan, 0) AS INTEGER))     AS sum_luu_dan,    
             SUM(CAST(COALESCE(coi_60, 0) AS INTEGER))      AS sum_coi_60,    
             SUM(CAST(COALESCE(coi_82, 0) AS INTEGER))      AS sum_coi_82,    
             SUM(CAST(COALESCE(coi_100, 0) AS INTEGER))     AS sum_coi_100,    
             SUM(CAST(COALESCE(pct_spg9, 0) AS INTEGER))    AS sum_pct_spg9,    
             SUM(CAST(COALESCE(phao_pk_127, 0) AS INTEGER)) AS sum_phao_pk_127    
        FROM trangkithuat;";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        int GetInt32OrZero(int ordinal)
                        {
                            return reader.IsDBNull(ordinal) ? 0 : Convert.ToInt32(reader.GetValue(ordinal));
                        }

                        while (reader.Read())
                        {
                            return new[]
                            {
                                    GetInt32OrZero(0),   // quan_so
                                    GetInt32OrZero(11),  // phao_pk_127
                                    GetInt32OrZero(9),   // coi_100
                                    GetInt32OrZero(8),   // coi_82
                                    GetInt32OrZero(7),   // coi_60
                                    GetInt32OrZero(10),  // pct_spg9
                                    GetInt32OrZero(5),   // b41_m79
                                    GetInt32OrZero(4),   // dl
                                    GetInt32OrZero(3),   // trl
                                    GetInt32OrZero(2),   // tl
                                    GetInt32OrZero(1),   // sn
                                    GetInt32OrZero(6),   // luu_dan
                                };
                        }

                        return new int[12];
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tính tổng: {ex.Message}\nError Code: {ex.ErrorCode}");
                return new int[12];
            }
        }

        private void LoadSumOfAllSection()
        {
           
        }

        private void LoadSection(int rowStart, int rowEnd, string section, string userId)
        {
            string sql = $"SELECT Value FROM trangkithuat WHERE User = '{userId}' AND option = '{section}'";
            string[] columnName = new string[] { "phao_pk_127", "coi_100", "coi_82", "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" };
            using (var connection = new SqlConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    int index = 0;
                    while (reader.Read())
                    {
                        if (index == 1)
                        {

                            for (int row = rowStart; row <= rowEnd; row++)
                            {
                                int i = 1, j = 0;
                                var value = reader[columnName[j++]]?.ToString() ?? "0";
                                var arraySum = ChuYeuData.SumOfChuYeuData(section);
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 3, value);
                                if (i != 0)
                                {
                                    reoGridControl1.CurrentWorksheet.SetCellData(row, 4, arraySum[i++]);
                                }
                            }
                        }
                        index++;
                    }

                }

            }
        }
    }
}

