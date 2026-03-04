using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer
{
    public static class ChuYeuData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public static bool ThemThongTin(
            string ll,
            string quanSo,
            string sn, string tl, string trl, string dl, string b41_m79,
            string luuDan,
            string coi60, string coi82, string coi100,
            string pctSpg9,string on,string db,
            string phaoPk127, string option)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO trangkithuat 
                              (ll,quan_so, sn, tl, trl, dl, b41_m79,
                               luu_dan, coi_60, coi_82, coi_100,
                               pct_spg9, phao_pk_127,ons,db,
                               User, option)
                               VALUES
                              (@ll,@quan_so, @sn, @tl, @trl, @dl, @b41_m79,
                               @luu_dan, @coi_60, @coi_82, @coi_100,
                               @pct_spg9, @phao_pk_127, @ons, @db, 
                               @User, @option)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ll", ll ?? "");
                        command.Parameters.AddWithValue("@quan_so", quanSo ?? "");
                        command.Parameters.AddWithValue("@ons",on ?? "");
                        command.Parameters.AddWithValue("@db",db ?? "");
                        command.Parameters.AddWithValue("@sn", sn ?? "");
                        command.Parameters.AddWithValue("@tl", tl ?? "");
                        command.Parameters.AddWithValue("@trl", trl ?? "");
                        command.Parameters.AddWithValue("@dl", dl ?? "");
                        command.Parameters.AddWithValue("@b41_m79", b41_m79 ?? "");

                        command.Parameters.AddWithValue("@luu_dan", luuDan ?? "");
                        command.Parameters.AddWithValue("@coi_60", coi60 ?? "");
                        command.Parameters.AddWithValue("@coi_82", coi82 ?? "");
                        command.Parameters.AddWithValue("@coi_100", coi100 ?? "");

                        command.Parameters.AddWithValue("@pct_spg9", pctSpg9 ?? "");
                        command.Parameters.AddWithValue("@phao_pk_127", phaoPk127 ?? "");

                        command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@option", option);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi thêm thông tin: {ex.Message}\nError Code: {ex.ErrorCode}");
                return false;
            }
        }


        public static void ThemHangLoat(ReoGridControl data, string option, int endRow)
        {
            for (int i = 5; i <= endRow; i++)
            {
                string ll = data.CurrentWorksheet.GetCellData(i,0)?.ToString();
                string quanSo = data.CurrentWorksheet.GetCellData(i, 1)?.ToString();
                string sn = data.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                string tl = data.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                string trl = data.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                string dl = data.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                string b41_m79 = data.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                string luuDan = data.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                string coi60 = data.CurrentWorksheet.GetCellData(i, 8)?.ToString();
                string coi82 = data.CurrentWorksheet.GetCellData(i, 9)?.ToString();
                string coi100 = data.CurrentWorksheet.GetCellData(i, 10)?.ToString();
                string pctSpg9 = data.CurrentWorksheet.GetCellData(i, 11)?.ToString();
                string phaoPk127 = data.CurrentWorksheet.GetCellData(i, 12)?.ToString();
                string on = data.CurrentWorksheet.GetCellData(i, 13)?.ToString();
                string db = data.CurrentWorksheet.GetCellData(i, 14)?.ToString();
                ThemThongTin(
                    ll,
                    quanSo,
                    sn, tl, trl, dl, b41_m79,
                    luuDan,
                    coi60, coi82, coi100,
                    pctSpg9,on,db,
                    phaoPk127,
                    option);
            }
        }

        private static void UpdateThongTin(
            string ll,
            string quanSo,
            string sn, string tl, string trl, string dl, string b41_m79,
            string luuDan,
            string coi60, string coi82, string coi100,
            string pctSpg9,string on, string db,
            string phaoPk127, string option)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {   
                    connection.Open();
                    string sql = @"UPDATE trangkithuat SET 
                                       quan_so = @quan_so, sn = @sn, tl = @tl, trl = @trl, dl = @dl, b41_m79 = @b41_m79,
                                       luu_dan = @luu_dan, coi_60 = @coi_60, coi_82 = @coi_82, coi_100 = @coi_100,
                                       pct_spg9 = @pct_spg9, phao_pk_127 = @phao_pk_127, ons = @ons, db = @db,
                                       WHERE ll = @ll AND User = @User AND option = @option";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ll", ll ?? "");
                        command.Parameters.AddWithValue("@quan_so", quanSo ?? "");
                        command.Parameters.AddWithValue("@ons", on ?? "");
                        command.Parameters.AddWithValue("@db", db ?? "");
                        command.Parameters.AddWithValue("@sn", sn ?? "");
                        command.Parameters.AddWithValue("@tl", tl ?? "");
                        command.Parameters.AddWithValue("@trl", trl ?? "");
                        command.Parameters.AddWithValue("@dl", dl ?? "");
                        command.Parameters.AddWithValue("@b41_m79", b41_m79 ?? "");
                        command.Parameters.AddWithValue("@luu_dan", luuDan ?? "");
                        command.Parameters.AddWithValue("@coi_60", coi60 ?? "");
                        command.Parameters.AddWithValue("@coi_82", coi82 ?? "");
                        command.Parameters.AddWithValue("@coi_100", coi100 ?? "");
                        command.Parameters.AddWithValue("@pct_spg9", pctSpg9 ?? "");
                        command.Parameters.AddWithValue("@phao_pk_127", phaoPk127 ?? "");
                        command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@option", option);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật thông tin: {ex.Message}\nError Code: {ex.ErrorCode}");
            }
        }
        public static void UpdateHangLoat(ReoGridControl data, string option, int endRow)
        {
            for (int i = 5; i <= endRow; i++)
            {
                string ll = data.CurrentWorksheet.GetCellData(i, 0)?.ToString();
                string quanSo = data.CurrentWorksheet.GetCellData(i, 1)?.ToString();
                string sn = data.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                string tl = data.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                string trl = data.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                string dl = data.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                string b41_m79 = data.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                string luuDan = data.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                string coi60 = data.CurrentWorksheet.GetCellData(i, 8)?.ToString();
                string coi82 = data.CurrentWorksheet.GetCellData(i, 9)?.ToString();
                string coi100 = data.CurrentWorksheet.GetCellData(i, 10)?.ToString();
                string pctSpg9 = data.CurrentWorksheet.GetCellData(i, 11)?.ToString();
                string phaoPk127 = data.CurrentWorksheet.GetCellData(i, 12)?.ToString();
                string on = data.CurrentWorksheet.GetCellData(i, 13)?.ToString();
                string db = data.CurrentWorksheet.GetCellData(i, 14)?.ToString();
                UpdateThongTin(
                    ll,
                    quanSo,
                    sn, tl, trl, dl, b41_m79,
                    luuDan,
                    coi60, coi82, coi100,
                    pctSpg9,on,db,
                    phaoPk127,
                    option);
            }
        }

        public static Dictionary<string,int> SumOfChuYeuData(string option)
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
        FROM trangkithuat
        WHERE ""User"" = @userId
        AND ""option"" = @option;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@userId", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@option", option ?? "");

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var result = new Dictionary<string, int>
                        {
                            { "sum_quan_so", 0 },
                            { "sum_sn", 0 },
                            { "sum_tl", 0 },
                            { "sum_trl", 0 },
                            { "sum_dl", 0 },
                            { "sum_b41_m79", 0 },
                            { "sum_luu_dan", 0 },
                            { "sum_coi_60", 0 },
                            { "sum_coi_82", 0 },
                            { "sum_coi_100", 0 },
                            { "sum_pct_spg9", 0 },
                            { "sum_phao_pk_127", 0 }
                        };

                        while (reader.Read())
                        {
                            foreach (var key in result.Keys.ToList())
                            {
                                var value = reader[key]?.ToString() ?? "0";
                                if (int.TryParse(value, out int intValue))
                                {
                                    result[key] = intValue;
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tính tổng: {ex.Message}\nError Code: {ex.ErrorCode}");
                return new Dictionary<string, int>();
            }
        }
    }
}
