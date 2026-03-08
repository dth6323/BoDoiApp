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
    internal class VCHCVTKTDATA
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        static int[] rows_O = { 9, 10, 11, 12, 13, 15, 16, 17, 18, 20, 21 };

        static int[] rows_FGHJN =
        {
            30,31,32,36,
            45,46,47,
            51,
            60,61,62,
            66
        };

        static int[] rows_P = { 45, 46, 47 };

        static int[] rows_U = { 45, 46, 47, 60, 61, 62 };

        static int[] rows_Zblock = CreateRange(7, 81);

        static int[] CreateRange(int start, int end)
        {
            List<int> list = new List<int>();
            for (int i = start; i <= end; i++) list.Add(i);
            return list.ToArray();
        }

        static double GetDouble(object v)
        {
            if (v == null) return 0;
            if (double.TryParse(v.ToString(), out double d)) return d;
            return 0;
        }

        public static void SaveAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        string delete = "DELETE FROM VCHCVTKT WHERE UserId=@User";

                        using (var cmd = new SQLiteCommand(delete, con, tran))
                        {
                            cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                            cmd.ExecuteNonQuery();
                        }

                        string insert = @"INSERT INTO VCHCVTKT
                        (Row,Col,Value,UserId)
                        VALUES (@Row,@Col,@Value,@User)";

                        using (var cmd = new SQLiteCommand(insert, con, tran))
                        {
                            cmd.Parameters.Add("@Row");
                            cmd.Parameters.Add("@Col");
                            cmd.Parameters.Add("@Value");
                            cmd.Parameters.Add("@User");

                            void SaveCell(int r, int c)
                            {
                                cmd.Parameters["@Row"].Value = r;
                                cmd.Parameters["@Col"].Value = c;
                                cmd.Parameters["@Value"].Value = GetDouble(ws.GetCellData(r, c));
                                cmd.Parameters["@User"].Value = Properties.Settings.Default.Username;

                                cmd.ExecuteNonQuery();
                            }

                            foreach (var r in rows_O)
                                SaveCell(r, 14);

                            foreach (var r in rows_FGHJN)
                            {
                                SaveCell(r, 5);
                                SaveCell(r, 6);
                                SaveCell(r, 7);
                                SaveCell(r, 9);
                                SaveCell(r, 13);
                                SaveCell(r, 17);
                            }

                            foreach (var r in rows_P)
                                SaveCell(r, 15);

                            foreach (var r in rows_U)
                                SaveCell(r, 20);

                            foreach (var r in rows_Zblock)
                            {
                                SaveCell(r, 25);
                                SaveCell(r, 26);
                                SaveCell(r, 27);
                                SaveCell(r, 28);
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Lưu thành công");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public static void LoadAll(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string sql = @"SELECT Row,Col,Value
                               FROM VCHCVTKT
                               WHERE UserId=@User";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int r = Convert.ToInt32(rd["Row"]);
                            int c = Convert.ToInt32(rd["Col"]);

                            ws.SetCellData(r, c, rd["Value"]);
                        }
                    }
                }
            }
        }
    }
}