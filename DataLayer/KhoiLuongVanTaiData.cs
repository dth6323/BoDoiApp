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
    internal class KhoiLuongVanTaiData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        private static readonly int[] DataRows =
        {
            8,9,10,
            12,13,14
        };

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
                        string deleteSql = "DELETE FROM KhoiLuongVanTai WHERE UserId=@User";

                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO KhoiLuongVanTai
                        (TT,B,C,D,E,F,G,H,I,J,K,L,M,UserId)
                        VALUES
                        (@TT,@B,@C,@D,@E,@F,@G,@H,@I,@J,@K,@L,@M,@User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.Add("@TT", System.Data.DbType.Int32);

                            cmd.Parameters.Add("@B", System.Data.DbType.Double);
                            cmd.Parameters.Add("@C", System.Data.DbType.Double);
                            cmd.Parameters.Add("@D", System.Data.DbType.Double);
                            cmd.Parameters.Add("@E", System.Data.DbType.Double);
                            cmd.Parameters.Add("@F", System.Data.DbType.Double);
                            cmd.Parameters.Add("@G", System.Data.DbType.Double);
                            cmd.Parameters.Add("@H", System.Data.DbType.Double);
                            cmd.Parameters.Add("@I", System.Data.DbType.Double);
                            cmd.Parameters.Add("@J", System.Data.DbType.Double);
                            cmd.Parameters.Add("@K", System.Data.DbType.Double);
                            cmd.Parameters.Add("@L", System.Data.DbType.Double);

                            cmd.Parameters.Add("@M", System.Data.DbType.Double);

                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            int tt = 1;

                            for (var row = 5; row<= 13; row++)
                            {
                                cmd.Parameters["@TT"].Value = tt++;

                                cmd.Parameters["@B"].Value = GetDouble(ws.GetCellData(row, 1));
                                cmd.Parameters["@C"].Value = GetDouble(ws.GetCellData(row, 2));
                                cmd.Parameters["@D"].Value = GetDouble(ws.GetCellData(row, 3));
                                cmd.Parameters["@E"].Value = GetDouble(ws.GetCellData(row, 4));
                                cmd.Parameters["@F"].Value = GetDouble(ws.GetCellData(row, 5));
                                cmd.Parameters["@G"].Value = GetDouble(ws.GetCellData(row, 6));
                                cmd.Parameters["@H"].Value = GetDouble(ws.GetCellData(row, 7));
                                cmd.Parameters["@I"].Value = GetDouble(ws.GetCellData(row, 8));
                                cmd.Parameters["@J"].Value = GetDouble(ws.GetCellData(row, 9));
                                cmd.Parameters["@K"].Value = GetDouble(ws.GetCellData(row, 10));
                                cmd.Parameters["@L"].Value = GetDouble(ws.GetCellData(row, 11));

                                cmd.Parameters["@M"].Value = GetDouble(ws.GetCellData(row, 12));

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
        public static void LoadCongNguoi(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection("Data Source=data.db;Version=3;"))
            {
                connection.Open();

                string sql = @"SELECT CEIL(cong_nguoi / 2.0)
                       FROM kehoach_baodam_quany WHERE User=@User
                       LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        var ws = grid.CurrentWorksheet;

                        // L6 = row 5 col 11
                        ws.SetCellData(5, 11, result);
                    }
                }
            }
        }
        private static void SetIfUnlocked(Worksheet ws, int row, int col, object value)
            {
                if (!ws.Cells[row, col].IsReadOnly)
                {
                    ws.SetCellData(row, col, value);
                }
            }
        public static void LoadAll(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT *
                       FROM KhoiLuongVanTai
                       WHERE UserId=@User
                       ORDER BY TT";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        LoadCongNguoi(grid);
                        int row = 5; // dòng bắt đầu giống SaveAll

                        while (reader.Read())
                        {
                            SetIfUnlocked(ws, row, 1, reader["B"]);
                            SetIfUnlocked(ws, row, 2, reader["C"]);
                            SetIfUnlocked(ws, row, 3, reader["D"]);
                            SetIfUnlocked(ws, row, 4, reader["E"]);
                            SetIfUnlocked(ws, row, 5, reader["F"]);
                            SetIfUnlocked(ws, row, 6, reader["G"]);
                            SetIfUnlocked(ws, row, 7, reader["H"]);
                            SetIfUnlocked(ws, row, 8, reader["I"]);
                            SetIfUnlocked(ws, row, 9, reader["J"]);
                            SetIfUnlocked(ws, row, 10, reader["K"]);
                            SetIfUnlocked(ws, row, 11, reader["L"]);
                            SetIfUnlocked(ws, row, 12, reader["M"]);

                            row++;
                        }
                    }
                }
            }
        }
    
    }
}
