using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.DataLayer
{
    internal class QuyDinhDuTruTieuThuBoSungData
    {
        private const string connectionString = "Data Source=data.db;Version=3;";

        public QuyDinhDuTruTieuThuBoSungData()
        {
        }

        public void ThemHangLoat(DataGridView dgv)
        {
            for (int i = 1,j=0; i < dgv.Rows.Count; i++,j++)
            {
                if (i == 8 || i == 14) continue;
                double? duTru = TryParseNullable(dgv.Rows[i].Cells[3].Value);
                double? co0400 = TryParseNullable(dgv.Rows[i].Cells[4].Value);
                double? cscd = TryParseNullable(dgv.Rows[i].Cells[5].Value);
                double? gdcb = TryParseNullable(dgv.Rows[i].Cells[6].Value);
                double? gdcd = TryParseNullable(dgv.Rows[i].Cells[7].Value);
                ThemThongTin(j, duTru, co0400, cscd, gdcb, gdcd);
            }
        }

        public void SuaHangLoat(DataGridView dgv)
        {
            for (int i = 1,j=0; i < dgv.Rows.Count; i++,j++)
            {
                if (i == 8 || i == 14) continue;
                double? duTru = TryParseNullable(dgv.Rows[i].Cells[3].Value);
                double? co0400 = TryParseNullable(dgv.Rows[i].Cells[4].Value);
                double? cscd = TryParseNullable(dgv.Rows[i].Cells[5].Value);
                double? gdcb = TryParseNullable(dgv.Rows[i].Cells[6].Value);
                double? gdcd = TryParseNullable(dgv.Rows[i].Cells[7].Value);
                CapNhatThongTin(j, duTru, co0400, cscd, gdcb, gdcd);
            }
        }

        public bool ThemThongTin(int vcId, double? duTru, double? co0400, double? cscd, double? gdcb, double? gdcd)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO QuyDinhDuTruTieuThuVoSung 
                                   (UserId,vcId, QuyDinhDuTru, PhaiCo0400N, PhaiCSCD, TieuThuGDCB, TieuThuGDCD)
                                   VALUES (@UserId,@vcId, @duTru, @co0400, @cscd, @gdcb, @gdcd)";
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@vcId", vcId);
                        command.Parameters.AddWithValue("@duTru", duTru.HasValue ? (object)duTru.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@co0400", co0400.HasValue ? (object)co0400.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@cscd", cscd.HasValue ? (object)cscd.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@gdcb", gdcb.HasValue ? (object)gdcb.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@gdcd", gdcd.HasValue ? (object)gdcd.Value : DBNull.Value);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqliteException ex)
            {
                MessageBox.Show($"Lỗi thêm Quy định vật chất: {ex.Message}\nCode: {ex.ErrorCode}");
                return false;
            }
        }

        public bool CapNhatThongTin(int vcId, double? duTru, double? co0400, double? cscd, double? gdcb, double? gdcd)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE QuyDinhDuTruTieuThuVoSung 
                                   SET QuyDinhDuTru = @duTru,
                                       PhaiCo0400N = @co0400,
                                       PhaiCSCD = @cscd,
                                       TieuThuGDCB = @gdcb,
                                       TieuThuGDCD = @gdcd
                                   WHERE vcId = @vcId and UserId = @id";
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@vcId", vcId);
                        command.Parameters.AddWithValue("@id", Properties.Settings.Default.Username);
                        command.Parameters.AddWithValue("@duTru", duTru.HasValue ? (object)duTru.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@co0400", co0400.HasValue ? (object)co0400.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@cscd", cscd.HasValue ? (object)cscd.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@gdcb", gdcb.HasValue ? (object)gdcb.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@gdcd", gdcd.HasValue ? (object)gdcd.Value : DBNull.Value);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqliteException ex)
            {
                MessageBox.Show($"Lỗi cập nhật Quy định vật chất: {ex.Message}");
                return false;
            }
        }

        public DataTable LayThongTin()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM QuyDinhDuTruTieuThuVoSung WHERE UserId = @UserId";
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                        using (var reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                MessageBox.Show($"Lỗi lấy dữ liệu Quy định vật chất: {ex.Message}");
                return null;
            }
            return dt;
        }

        // Hàm phụ để ép kiểu double? từ object
        private double? TryParseNullable(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            if (double.TryParse(val.ToString(), out double result))
                return result;
            return null;
        }
    }
}
