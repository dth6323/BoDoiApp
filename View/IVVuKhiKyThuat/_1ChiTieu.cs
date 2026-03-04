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
        private static string EXCEL_PATH = Path.Combine(Basedir, "Resources", "Sheet", "Book1.xlsx");
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
            LoadExcel();

        }

        private void LoadExcel()
        {
            LoadSumOfAllSection();
            LoadSection(13, 23, "Hướng Chủ yếu", UserId, new string[] { "phao_pk_127", "coi_82", "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(24, 34, "Hướng Thứ Yếu", UserId, new string[] { "phao_pk_127", "coi_82", "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(35, 45, "Phòng ngự phía sau", UserId, new string[] { "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(46, 56, "LL còn lại", UserId, new string[] { "phao_pk_127", "coi_100", "pct_spg9", "b41_m79", "tl", "sn", "luu_dan" });
        }

        private int[] SumOfChuYeuData()
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
        FROM trangkithuat WHERE User = @UserId;";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@UserId", UserId);
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
            var arraySum = SumOfChuYeuData();
            string sql = @"
        SELECT    
             option,
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
        FROM trangkithuat
        WHERE User = @UserId AND ll = '1. Trong biên chế'
        GROUP BY option";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);
                    var reader = command.ExecuteReader();
                    int index = 2;
                    string[] columnName = new string[] { "sum_phao_pk_127", "sum_coi_100", "sum_coi_82", "sum_coi_60", "sum_pct_spg9", "sum_b41_m79", "sum_dl", "sum_trl", "sum_tl", "sum_sn", "sum_luu_dan" };
                    int i = 0;
                    reader.Read();
                    foreach (var col in columnName)
                    {
                        var value = reader[col]?.ToString() ?? "0";
                        reoGridControl1.CurrentWorksheet.SetCellData(index, 3, value);
                        i++;
                        index++;

                    }
                    int k = 1;
                    for (int j = 2; j <= 12; j++)
                    {
                        reoGridControl1.CurrentWorksheet.SetCellData(j, 4, arraySum[k++]);
                    }
                }
            }
        }

        private void LoadSection(int rowStart, int rowEnd, string section, string userId, string[] columnNames)
        {
            string sql = $"SELECT * FROM trangkithuat WHERE User = '{userId}' AND option = '{section}' AND ll = '1. Trong biên chế'";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    reader.Read();

                    for (int row = rowStart; row <= rowEnd; row++)
                    {
                        int i = 0, j = 0;
                        var value = reader[columnNames[j++]]?.ToString() ?? "0";
                        var arraySum = ChuYeuData.SumOfChuYeuData(section);
                        reoGridControl1.CurrentWorksheet.SetCellData(row, 3, value);
                        if (i != 0)
                        {
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 4, arraySum[columnNames[i++]]);
                        }
                    }
                }

            }

        }

    }
}

