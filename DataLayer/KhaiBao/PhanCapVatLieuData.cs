using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer.KhaiBao
{
    internal class PhanCapVatLieuData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        private static readonly HashSet<int> DataRows = new HashSet<int>
        {
            5,6,7,8,9,
            11,12,13,14,
            16,17
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
                        string deleteSql = "DELETE FROM VatChat WHERE UserId=@User";

                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO VatChat
                        (TT,LoaiVatChat,DVT,
                        PC_TDQ_KhoD,PC_TDQ_DonVi,PC_TDQ_Plus,
                        PC_SCD_KhoD,PC_SCD_DonVi,PC_SCD_Plus,
                        UserId)
                        VALUES
                        (@TT,@LoaiVatChat,@DVT,
                        @PC_TDQ_KhoD,@PC_TDQ_DonVi,@PC_TDQ_Plus,
                        @PC_SCD_KhoD,@PC_SCD_DonVi,@PC_SCD_Plus,
                        @User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.Add("@TT", System.Data.DbType.Int32);
                            cmd.Parameters.Add("@LoaiVatChat", System.Data.DbType.String);
                            cmd.Parameters.Add("@DVT", System.Data.DbType.String);

                            cmd.Parameters.Add("@PC_TDQ_KhoD", System.Data.DbType.Double);
                            cmd.Parameters.Add("@PC_TDQ_DonVi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@PC_TDQ_Plus", System.Data.DbType.Double);

                            cmd.Parameters.Add("@PC_SCD_KhoD", System.Data.DbType.Double);
                            cmd.Parameters.Add("@PC_SCD_DonVi", System.Data.DbType.Double);
                            cmd.Parameters.Add("@PC_SCD_Plus", System.Data.DbType.Double);

                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            int tt = 1;

                            for (int row = 0; row <= 50; row++)
                            {
                                if (!DataRows.Contains(row)) continue;

                                cmd.Parameters["@TT"].Value = tt++;

                                cmd.Parameters["@LoaiVatChat"].Value = ws.GetCellData(row, 1)?.ToString();
                                cmd.Parameters["@DVT"].Value = ws.GetCellData(row, 2)?.ToString();

                                cmd.Parameters["@PC_TDQ_KhoD"].Value = GetDouble(ws.GetCellData(row, 3));
                                cmd.Parameters["@PC_TDQ_DonVi"].Value = GetDouble(ws.GetCellData(row, 4));
                                cmd.Parameters["@PC_TDQ_Plus"].Value = GetDouble(ws.GetCellData(row, 5));

                                cmd.Parameters["@PC_SCD_KhoD"].Value = GetDouble(ws.GetCellData(row, 6));
                                cmd.Parameters["@PC_SCD_DonVi"].Value = GetDouble(ws.GetCellData(row, 7));
                                cmd.Parameters["@PC_SCD_Plus"].Value = GetDouble(ws.GetCellData(row, 8));

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

        public static void LoadAllVatChat(ReoGridControl grid)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT *
                       FROM VatChat
                       WHERE UserId=@User
                       ORDER BY TT";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;

                        int index = 0;
                        int[] rows = { 5, 6, 7, 8, 9, 11, 12, 13, 14, 16, 17 };

                        while (reader.Read())
                        {
                            if (index >= rows.Length) break;

                            int row = rows[index];

                            ws.SetCellData(row, 3, reader["PC_TDQ_KhoD"]);   // D
                            ws.SetCellData(row, 4, reader["PC_TDQ_DonVi"]);  // E
                            ws.SetCellData(row, 5, reader["PC_TDQ_Plus"]);   // F

                            ws.SetCellData(row, 6, reader["PC_SCD_KhoD"]);   // G
                            ws.SetCellData(row, 7, reader["PC_SCD_DonVi"]);  // H
                            ws.SetCellData(row, 8, reader["PC_SCD_Plus"]);   // I

                            index++;
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

                string sql = @"SELECT *
                               FROM VatChat
                               WHERE UserId=@User
                               ORDER BY TT";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;

                        int index = 0;
                        int[] rows = { 5, 6, 7, 8, 9, 11, 12, 13, 14, 16, 17 };

                        while (reader.Read())
                        {
                            if (index >= rows.Length) break;

                            int row = rows[index];

                            ws.SetCellData(row, 4, reader["PC_TDQ_DonVi"]);
                            ws.SetCellData(row, 7, reader["PC_SCD_DonVi"]);

                            index++;
                        }
                    }
                }
            }
        }
    }
}