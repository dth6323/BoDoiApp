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
    internal class KeHoachBaoDamQuanY
    {
        private const string connectionString = "Data Source=data.db;Version=3;";
        private static double GetDouble(object value)
        {
            if (value == null) return 0;

            if (double.TryParse(value.ToString(), out double result))
                return result;

            return 0;
        }
        public static void SaveAll(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Xóa dữ liệu cũ theo user
                        string deleteSql = "DELETE FROM kehoach_baodam_quany WHERE User=@User";
                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO kehoach_baodam_quany
                (quan_so, tb_qs, tb_nguoi, tbhh_qs, tbhh_nguoi,
                 bb_qs, bb_nguoi, cong_nguoi, cang_bo, tu_di, tong, User)
                VALUES
                (@quan_so, @tb_qs, @tb_nguoi, @tbhh_qs, @tbhh_nguoi,
                 @bb_qs, @bb_nguoi, @cong_nguoi,@cang_bo,@tu_di,@tong, @User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.Add("@quan_so", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tb_qs", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tb_nguoi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tbhh_qs", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tbhh_nguoi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@bb_qs", System.Data.DbType.Double);
                            cmd.Parameters.Add("@bb_nguoi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@cong_nguoi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@cang_bo", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tu_di", System.Data.DbType.Double);
                            cmd.Parameters.Add("@tong", System.Data.DbType.Double);
                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            // Dòng 4 → 12 (index 3 → 11)
                            for (int row = 4; row <= 11; row++)
                            {
                                cmd.Parameters["@quan_so"].Value = GetDouble(ws.GetCellData(row, 2));
                                cmd.Parameters["@tb_qs"].Value = GetDouble(ws.GetCellData(row, 3));
                                cmd.Parameters["@tb_nguoi"].Value = GetDouble(ws.GetCellData(row, 4));
                                cmd.Parameters["@tbhh_qs"].Value = GetDouble(ws.GetCellData(row, 5));
                                cmd.Parameters["@tbhh_nguoi"].Value = GetDouble(ws.GetCellData(row, 6));
                                cmd.Parameters["@bb_qs"].Value = GetDouble(ws.GetCellData(row, 7));
                                cmd.Parameters["@bb_nguoi"].Value = GetDouble(ws.GetCellData(row, 8));
                                cmd.Parameters["@cong_nguoi"].Value = GetDouble(ws.GetCellData(row, 9));
                                cmd.Parameters["@cang_bo"].Value = GetDouble(ws.GetCellData(row, 10));
                                cmd.Parameters["@tu_di"].Value = GetDouble(ws.GetCellData(row, 10));
                                cmd.Parameters["@tong"].Value = GetDouble(ws.GetCellData(row, 11));
                                cmd.Parameters["@User"].Value = Properties.Settings.Default.Username;

                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Lưu thành công!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }
        public static void LoadAll(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM kehoach_baodam_quany WHERE User=@User";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 4;

                        while (reader.Read() && row <= 11)
                        {
                            ws.SetCellData(row, 2, reader["quan_so"]);
                            ws.SetCellData(row, 3, reader["tb_qs"]);
                            ws.SetCellData(row, 4, reader["tb_nguoi"]);
                            ws.SetCellData(row, 5, reader["tbhh_qs"]);
                            ws.SetCellData(row, 6, reader["tbhh_nguoi"]);
                            ws.SetCellData(row, 7, reader["bb_qs"]);
                            ws.SetCellData(row, 8, reader["bb_nguoi"]);
                            ws.SetCellData(row, 9, reader["cong_nguoi"]);
                            ws.SetCellData(row, 10, reader["cang_bo"]);
                            ws.SetCellData(row, 11, reader["tu_di"]);
                            ws.SetCellData(row, 12, reader["tong"]);

                            row++;
                        }
                    }
                }
            }
        }
    }
}
