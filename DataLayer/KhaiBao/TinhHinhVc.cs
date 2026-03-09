using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer.KhaiBao
{
    internal class TinhHinhVcData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";
        private static readonly HashSet<int> SkipRows = new HashSet<int>
            {
                0,1,2,13,14,20,25,27
            };
        private static double GetDouble(object value)
        {
            if (value == null) return 0;

            if (double.TryParse(value.ToString(), out double result))
                return result;

            return 0;
        }

        private static string GetString(object value)
        {
            if (value == null) return "";
            return value.ToString();
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
                        string deleteSql = "DELETE FROM KhaiBaoTinhHinhVc WHERE User=@User";
                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO KhaiBaoTinhHinhVc
                (tt, khod, donVi, cong, ghiChu, User)
                VALUES
                (@tt, @khod, @donVi, @cong, @ghiChu, @User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.Add("@tt", System.Data.DbType.Int32);
                            cmd.Parameters.Add("@khod", System.Data.DbType.Double);
                            cmd.Parameters.Add("@donVi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@cong", System.Data.DbType.Double);
                            cmd.Parameters.Add("@ghiChu", System.Data.DbType.String);
                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            int tt = 0;

                            int maxRow = 29;

                            for (int row = 0; row < maxRow; row++)
                            {
                                if (SkipRows.Contains(row)) continue;

                                cmd.Parameters["@tt"].Value = tt++;
                                cmd.Parameters["@khod"].Value = GetDouble(ws.GetCellData(row,3));
                                cmd.Parameters["@donVi"].Value = GetDouble(ws.GetCellData(row,4));
                                cmd.Parameters["@cong"].Value = GetDouble(ws.GetCellData(row,5));
                                cmd.Parameters["@ghiChu"].Value = ws.GetCellData(row, 6)?.ToString() ?? "";
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
        public static void LoadAllCell(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM KhaiBaoTinhHinhVc WHERE User=@User ORDER BY tt";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 0;

                        while (reader.Read())
                        {
                            while (SkipRows.Contains(row))
                                row++;

                            ws.SetCellData(row, 3, reader["khod"]);
                            ws.SetCellData(row, 4, reader["donVi"]);
                            ws.SetCellData(row, 5, reader["cong"]);
                            ws.SetCellData(row, 6, reader["ghiChu"]);

                            row++;
                        }
                    }
                }
            }
        }
        public static void LoadAll(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM KhaiBaoTinhHinhVc WHERE User=@User ORDER BY tt";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 0;

                        while (reader.Read())
                        {
                            while (SkipRows.Contains(row))
                                row++;

                            ws.SetCellData(row, 3, reader["khod"]);
                            ws.SetCellData(row, 4, reader["donVi"]);
                            ws.SetCellData(row, 5, reader["cong"]);
                            ws.SetCellData(row, 6, reader["ghiChu"]);

                            row++;
                        }
                    }
                }
            }
        }
    }
}