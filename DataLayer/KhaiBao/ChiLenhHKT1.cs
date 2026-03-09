using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.DataLayer.KhaiBao
{
    internal class ChiLenhHKT1Data
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        private static readonly HashSet<int> SkipRows = new HashSet<int>
{
    1,2,3,14,15,21,26,29
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
                        string deleteSql = "DELETE FROM ChiLenhHkt1 WHERE User=@User";

                        using (var deleteCmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            deleteCmd.ExecuteNonQuery();
                        }

                        var ws = grid.CurrentWorksheet;

                        string insertSql = @"INSERT INTO ChiLenhHkt1
                        (tt,qddc,pc04n,pcscd,gdcb,gdcd,User)
                        VALUES
                        (@tt,@qddc,@pc04n,@pcscd,@gdcb,@gdcd,@User)";

                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.Add("@tt", System.Data.DbType.Int32);
                            cmd.Parameters.Add("@qddc", System.Data.DbType.Double);
                            cmd.Parameters.Add("@pc04n", System.Data.DbType.Double);
                            cmd.Parameters.Add("@pcscd", System.Data.DbType.Double);
                            cmd.Parameters.Add("@gdcb", System.Data.DbType.Double);
                            cmd.Parameters.Add("@gdcd", System.Data.DbType.Double);
                            cmd.Parameters.Add("@User", System.Data.DbType.String);

                            int tt = 0;

                            for (int row = 1; row <= 28; row++)
                            {
                                if (SkipRows.Contains(row)) continue;

                                cmd.Parameters["@tt"].Value = tt++;

                                    cmd.Parameters["@qddc"].Value = GetDouble(ws.GetCellData(row, 3));
                                    cmd.Parameters["@pc04n"].Value = GetDouble(ws.GetCellData(row, 4));
                                    cmd.Parameters["@pcscd"].Value = GetDouble(ws.GetCellData(row, 5));
                                    cmd.Parameters["@gdcb"].Value = GetDouble(ws.GetCellData(row, 6));
                                    cmd.Parameters["@gdcd"].Value = GetDouble(ws.GetCellData(row, 7));
                                

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

                string sql = "SELECT * FROM ChiLenhHkt1 WHERE User=@User ORDER BY tt";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 1;

                        while (reader.Read())
                        {
                            while (SkipRows.Contains(row))
                                row++;

                            ws.SetCellData(row, 3, reader["qddc"]);
                            ws.SetCellData(row, 4, reader["pc04n"]);
                            ws.SetCellData(row, 5, reader["pcscd"]);
                            ws.SetCellData(row, 6, reader["gdcb"]);
                            ws.SetCellData(row, 7, reader["gdcd"]);


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

                string sql = "SELECT * FROM ChiLenhHkt1 WHERE User=@User ORDER BY tt";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ws = grid.CurrentWorksheet;
                        int row = 1;

                        while (reader.Read())
                        {
                            while (SkipRows.Contains(row))
                                row++;

                                ws.SetCellData(row, 3, reader["qddc"]);
                                ws.SetCellData(row, 4, reader["pc04n"]);
                                ws.SetCellData(row, 5, reader["pcscd"]);
                                ws.SetCellData(row, 6, reader["gdcb"]);
                                ws.SetCellData(row, 7, reader["gdcd"]);
                            

                            row++;
                        }
                    }
                }
            }
        }
    }
}