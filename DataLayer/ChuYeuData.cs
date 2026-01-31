using System;
using System.Collections.Generic;
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
        private const string connectionString = "Data Source=data2.db;Version=3;";

        public static bool ThemThongTin(
            string quanSo,
            string sn, string tl, string trl, string dl, string b41_m79,
            string luuDan,
            string coi60, string coi82, string coi100,
            string pctSpg9,
            string phaoPk127,string option)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO trangkithuat 
                          (quan_so, sn, tl, trl, dl, b41_m79,
                           luu_dan, coi_60, coi_82, coi_100,
                           pct_spg9, phao_pk_127,
                           User, option)
                           VALUES
                          (@quan_so, @sn, @tl, @trl, @dl, @b41_m79,
                           @luu_dan, @coi_60, @coi_82, @coi_100,
                           @pct_spg9, @phao_pk_127,
                           @User, @option)";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@quan_so", quanSo ?? "");

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
        

        public static void ThemHangLoat(ReoGridControl data,string option)
        {
            for(int i=6; i <= 16; i++)
            {
                string quanSo = data.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                string sn = data.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                string tl = data.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                string trl = data.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                string dl = data.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                string b41_m79 = data.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                string luuDan = data.CurrentWorksheet.GetCellData(i, 8)?.ToString();
                string coi60 = data.CurrentWorksheet.GetCellData(i, 9)?.ToString();
                string coi82 = data.CurrentWorksheet.GetCellData(i, 10)?.ToString();
                string coi100 = data.CurrentWorksheet.GetCellData(i, 11)?.ToString();
                string pctSpg9 = data.CurrentWorksheet.GetCellData(i, 12)?.ToString();
                string phaoPk127 = data.CurrentWorksheet.GetCellData(i, 13)?.ToString();
                ThemThongTin(
                    quanSo,
                    sn, tl, trl, dl, b41_m79,
                    luuDan,
                    coi60, coi82, coi100,
                    pctSpg9,
                    phaoPk127,
                    option);
            }
        }

        private static void UpdateThongTin(
            string quanSo,
            string sn, string tl, string trl, string dl, string b41_m79,
            string luuDan,
            string coi60, string coi82, string coi100,
            string pctSpg9,
            string phaoPk127, string option)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE trangkithuat SET 
                                   sn = @sn, tl = @tl, trl = @trl, dl = @dl, b41_m79 = @b41_m79,
                                   luu_dan = @luu_dan, coi_60 = @coi_60, coi_82 = @coi_82, coi_100 = @coi_100,
                                   pct_spg9 = @pct_spg9, phao_pk_127 = @phao_pk_127
                                   WHERE quan_so = @quan_so AND User = @User AND option = @option";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@quan_so", quanSo ?? "");
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
        public static void UpdateHangLoat(ReoGridControl data, string option)
        {
            for (int i = 6; i <= 16; i++)
            {
                string quanSo = data.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                string sn = data.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                string tl = data.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                string trl = data.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                string dl = data.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                string b41_m79 = data.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                string luuDan = data.CurrentWorksheet.GetCellData(i, 8)?.ToString();
                string coi60 = data.CurrentWorksheet.GetCellData(i, 9)?.ToString();
                string coi82 = data.CurrentWorksheet.GetCellData(i, 10)?.ToString();
                string coi100 = data.CurrentWorksheet.GetCellData(i, 11)?.ToString();
                string pctSpg9 = data.CurrentWorksheet.GetCellData(i, 12)?.ToString();
                string phaoPk127 = data.CurrentWorksheet.GetCellData(i, 13)?.ToString();
                UpdateThongTin(
                    quanSo,
                    sn, tl, trl, dl, b41_m79,
                    luuDan,
                    coi60, coi82, coi100,
                    pctSpg9,
                    phaoPk127,
                    option);
            }
        }

    }
}
