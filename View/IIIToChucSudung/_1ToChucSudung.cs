using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.IIIToChucSudung
{
    public partial class _1ToChucSudung : UserControl
    {
        public _1ToChucSudung()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu và chuyển sang bước tiếp theo?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (IsDataExistForCurrentUser())
                    {
                        UpdateDataFromReoGrid();
                    }
                    else
                    {
                        SaveDataFromReoGrid();
                    }

                    MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                return;
            }
            NavigationService.Navigate(new _2BoTri());
        }

        private void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ToChucSuDungLucLuong (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    TT INTEGER NOT NULL,
    NoiDung TEXT NOT NULL,
    QS_SQ INTEGER DEFAULT 0,
    QS_QNCN INTEGER DEFAULT 0,
    QS_HSQ_BS INTEGER DEFAULT 0,
    QS_Plus INTEGER DEFAULT 0,
    VK_VuKhi INTEGER DEFAULT 0,
    VK_XeMay INTEGER DEFAULT 0,
    VK_TBKhac INTEGER DEFAULT 0,
    HC_KT_QS INTEGER DEFAULT 0,
    HC_KT_TB INTEGER DEFAULT 0,
    TangCuong_QS INTEGER DEFAULT 0,
    TangCuong_TB INTEGER DEFAULT 0
);";
             using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
             {
                 connection.Open();
                 var command = new SQLiteCommand(sql, connection);
                 command.ExecuteNonQuery();
            }
        }

        private void _1ToChucSudung_Load(object sender, EventArgs e)
        {
            CreateTable();

            // Ensure we are on the expected sheet.
            if (reoGridControl1 != null && reoGridControl1.Worksheets != null && reoGridControl1.Worksheets.Count > 6)
            {
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[6];
            }

            if (IsDataExistForCurrentUser())
            {
                try
                {
                    LoadDataFromDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDataFromDatabase()
        {
            var userId = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userId))
                return;

            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[6];
            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null)
                throw new InvalidOperationException("Current worksheet is not available.");

            const int startRow = 4;

            const int COL_TT = 0;
            const int COL_NOIDUNG = 1;
            const int COL_QS_SQ = 2;
            const int COL_QS_QNCN = 3;
            const int COL_QS_HSQ_BS = 4;
            const int COL_QS_PLUS = 5;
            const int COL_VK_VUKHI = 6;
            const int COL_VK_XEMAY = 7;
            const int COL_VK_TBK = 8;
            const int COL_HC_KT_QS = 9;
            const int COL_HC_KT_TB = 10;
            const int COL_TANGCUONG_QS = 11;
            const int COL_TANGCUONG_TB = 12;

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = @"
SELECT
  TT,
  NoiDung,
  QS_SQ,
  QS_QNCN,
  QS_HSQ_BS,
  QS_Plus,
  VK_VuKhi,
  VK_XeMay,
  VK_TBKhac,
  HC_KT_QS,
  HC_KT_TB,
  TangCuong_QS,
  TangCuong_TB
FROM ToChucSuDungLucLuong
WHERE UserId = @UserId
ORDER BY TT ASC;";
                command.Parameters.AddWithValue("@UserId", userId.Trim());

                using (var reader = command.ExecuteReader())
                {
                    int row = startRow;
                    while (reader.Read())
                    {
                        sheet.SetCellData(row, COL_TT, reader["TT"] == DBNull.Value ? null : (object)Convert.ToInt32(reader["TT"]));
                        sheet.SetCellData(row, COL_NOIDUNG, reader["NoiDung"] == DBNull.Value ? string.Empty : Convert.ToString(reader["NoiDung"]));

                        sheet.SetCellData(row, COL_QS_SQ, reader["QS_SQ"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QS_SQ"]));
                        sheet.SetCellData(row, COL_QS_QNCN, reader["QS_QNCN"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QS_QNCN"]));
                        sheet.SetCellData(row, COL_QS_HSQ_BS, reader["QS_HSQ_BS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QS_HSQ_BS"]));
                        sheet.SetCellData(row, COL_QS_PLUS, reader["QS_Plus"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QS_Plus"]));

                        sheet.SetCellData(row, COL_VK_VUKHI, reader["VK_VuKhi"] == DBNull.Value ? 0 : Convert.ToInt32(reader["VK_VuKhi"]));
                        sheet.SetCellData(row, COL_VK_XEMAY, reader["VK_XeMay"] == DBNull.Value ? 0 : Convert.ToInt32(reader["VK_XeMay"]));
                        sheet.SetCellData(row, COL_VK_TBK, reader["VK_TBKhac"] == DBNull.Value ? 0 : Convert.ToInt32(reader["VK_TBKhac"]));

                        sheet.SetCellData(row, COL_HC_KT_QS, reader["HC_KT_QS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["HC_KT_QS"]));
                        sheet.SetCellData(row, COL_HC_KT_TB, reader["HC_KT_TB"] == DBNull.Value ? 0 : Convert.ToInt32(reader["HC_KT_TB"]));

                        sheet.SetCellData(row, COL_TANGCUONG_QS, reader["TangCuong_QS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TangCuong_QS"]));
                        sheet.SetCellData(row, COL_TANGCUONG_TB, reader["TangCuong_TB"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TangCuong_TB"]));

                        row++;
                    }
                }
            }

            reoGridControl1.Refresh();
        }

        private void SaveDataFromReoGrid()
        {
            var userId = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userId))
                throw new InvalidOperationException("Username is not set.");
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[6];
            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null)
                throw new InvalidOperationException("Current worksheet is not available.");

            const int startRow = 4;
            const int startCol = 0;
            const int endCol = 12; // inclusive

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = tx;
                    command.CommandText = @"
INSERT INTO ToChucSuDungLucLuong
(UserId, TT, NoiDung,
 QS_SQ, QS_QNCN, QS_HSQ_BS, QS_Plus,
 VK_VuKhi, VK_XeMay, VK_TBKhac,
 HC_KT_QS, HC_KT_TB,
 TangCuong_QS, TangCuong_TB)
VALUES
(@UserId, @TT, @NoiDung,
 @QS_SQ, @QS_QNCN, @QS_HSQ_BS, @QS_Plus,
 @VK_VuKhi, @VK_XeMay, @VK_TBKhac,
 @HC_KT_QS, @HC_KT_TB,
 @TangCuong_QS, @TangCuong_TB);";

                    command.Parameters.Add("@UserId", DbType.String);
                    command.Parameters.Add("@TT", DbType.Int32);
                    command.Parameters.Add("@NoiDung", DbType.String);

                    command.Parameters.Add("@QS_SQ", DbType.Int32);
                    command.Parameters.Add("@QS_QNCN", DbType.Int32);
                    command.Parameters.Add("@QS_HSQ_BS", DbType.Int32);
                    command.Parameters.Add("@QS_Plus", DbType.Int32);

                    command.Parameters.Add("@VK_VuKhi", DbType.Int32);
                    command.Parameters.Add("@VK_XeMay", DbType.Int32);
                    command.Parameters.Add("@VK_TBKhac", DbType.Int32);

                    command.Parameters.Add("@HC_KT_QS", DbType.Int32);
                    command.Parameters.Add("@HC_KT_TB", DbType.Int32);

                    command.Parameters.Add("@TangCuong_QS", DbType.Int32);
                    command.Parameters.Add("@TangCuong_TB", DbType.Int32);

                    for (int r = startRow; r < Math.Max(sheet.RowCount, startRow + 1); r++)
                    {
                        // Stop when the entire row (0..12) has no data.
                        if (IsRowEmpty(sheet, r, startCol, endCol))
                            break;

                        int tt = GetCellInt(sheet, r, 0);
                        string noiDung = GetCellString(sheet, r, 1);

                        // If the row has some data elsewhere but TT/NoiDung is missing, skip it.
                        if (tt <= 0 || string.IsNullOrWhiteSpace(noiDung))
                            continue;

                        command.Parameters["@UserId"].Value = userId.Trim();
                        command.Parameters["@TT"].Value = tt;
                        command.Parameters["@NoiDung"].Value = noiDung.Trim();

                        command.Parameters["@QS_SQ"].Value = GetCellInt(sheet, r, 2);
                        command.Parameters["@QS_QNCN"].Value = GetCellInt(sheet, r, 3);
                        command.Parameters["@QS_HSQ_BS"].Value = GetCellInt(sheet, r, 4);
                        command.Parameters["@QS_Plus"].Value = GetCellInt(sheet, r, 5);

                        command.Parameters["@VK_VuKhi"].Value = GetCellInt(sheet, r, 6);
                        command.Parameters["@VK_XeMay"].Value = GetCellInt(sheet, r, 7);
                        command.Parameters["@VK_TBKhac"].Value = GetCellInt(sheet, r, 8);

                        command.Parameters["@HC_KT_QS"].Value = GetCellInt(sheet, r, 9);
                        command.Parameters["@HC_KT_TB"].Value = GetCellInt(sheet, r, 10);

                        command.Parameters["@TangCuong_QS"].Value = GetCellInt(sheet, r, 11);
                        command.Parameters["@TangCuong_TB"].Value = GetCellInt(sheet, r, 12);

                        command.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
        }

        private void UpdateDataFromReoGrid()
        {
            var userId = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userId))
                throw new InvalidOperationException("Username is not set.");

            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[6];
            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null)
                throw new InvalidOperationException("Current worksheet is not available.");

            const int startRow = 4;
            const int startCol = 0;
            const int endCol = 12; // inclusive

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                using (var tx = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = tx;
                    command.CommandText = @"
UPDATE ToChucSuDungLucLuong
SET
  NoiDung = @NoiDung,
  QS_SQ = @QS_SQ,
  QS_QNCN = @QS_QNCN,
  QS_HSQ_BS = @QS_HSQ_BS,
  QS_Plus = @QS_Plus,
  VK_VuKhi = @VK_VuKhi,
  VK_XeMay = @VK_XeMay,
  VK_TBKhac = @VK_TBKhac,
  HC_KT_QS = @HC_KT_QS,
  HC_KT_TB = @HC_KT_TB,
  TangCuong_QS = @TangCuong_QS,
  TangCuong_TB = @TangCuong_TB
WHERE UserId = @UserId AND TT = @TT;";

                    command.Parameters.Add("@NoiDung", DbType.String);

                    command.Parameters.Add("@QS_SQ", DbType.Int32);
                    command.Parameters.Add("@QS_QNCN", DbType.Int32);
                    command.Parameters.Add("@QS_HSQ_BS", DbType.Int32);
                    command.Parameters.Add("@QS_Plus", DbType.Int32);

                    command.Parameters.Add("@VK_VuKhi", DbType.Int32);
                    command.Parameters.Add("@VK_XeMay", DbType.Int32);
                    command.Parameters.Add("@VK_TBKhac", DbType.Int32);

                    command.Parameters.Add("@HC_KT_QS", DbType.Int32);
                    command.Parameters.Add("@HC_KT_TB", DbType.Int32);

                    command.Parameters.Add("@TangCuong_QS", DbType.Int32);
                    command.Parameters.Add("@TangCuong_TB", DbType.Int32);

                    command.Parameters.Add("@UserId", DbType.String);
                    command.Parameters.Add("@TT", DbType.Int32);

                    for (int r = startRow; r < Math.Max(sheet.RowCount, startRow + 1); r++)
                    {
                        if (IsRowEmpty(sheet, r, startCol, endCol))
                            break;

                        int tt = GetCellInt(sheet, r, 0);
                        string noiDung = GetCellString(sheet, r, 1);

                        if (tt <= 0 || string.IsNullOrWhiteSpace(noiDung))
                            continue;

                        command.Parameters["@NoiDung"].Value = noiDung.Trim();

                        command.Parameters["@QS_SQ"].Value = GetCellInt(sheet, r, 2);
                        command.Parameters["@QS_QNCN"].Value = GetCellInt(sheet, r, 3);
                        command.Parameters["@QS_HSQ_BS"].Value = GetCellInt(sheet, r, 4);
                        command.Parameters["@QS_Plus"].Value = GetCellInt(sheet, r, 5);

                        command.Parameters["@VK_VuKhi"].Value = GetCellInt(sheet, r, 6);
                        command.Parameters["@VK_XeMay"].Value = GetCellInt(sheet, r, 7);
                        command.Parameters["@VK_TBKhac"].Value = GetCellInt(sheet, r, 8);

                        command.Parameters["@HC_KT_QS"].Value = GetCellInt(sheet, r, 9);
                        command.Parameters["@HC_KT_TB"].Value = GetCellInt(sheet, r, 10);

                        command.Parameters["@TangCuong_QS"].Value = GetCellInt(sheet, r, 11);
                        command.Parameters["@TangCuong_TB"].Value = GetCellInt(sheet, r, 12);

                        command.Parameters["@UserId"].Value = userId.Trim();
                        command.Parameters["@TT"].Value = tt;

                        int affected = command.ExecuteNonQuery();
                        if (affected == 0)
                        {
                            // If no row exists for this TT yet, insert it.
                            using (var insertCmd = connection.CreateCommand())
                            {
                                insertCmd.Transaction = tx;
                                insertCmd.CommandText = @"
INSERT INTO ToChucSuDungLucLuong
(UserId, TT, NoiDung,
 QS_SQ, QS_QNCN, QS_HSQ_BS, QS_Plus,
 VK_VuKhi, VK_XeMay, VK_TBKhac,
 HC_KT_QS, HC_KT_TB,
 TangCuong_QS, TangCuong_TB)
VALUES
(@UserId, @TT, @NoiDung,
 @QS_SQ, @QS_QNCN, @QS_HSQ_BS, @QS_Plus,
 @VK_VuKhi, @VK_XeMay, @VK_TBKhac,
 @HC_KT_QS, @HC_KT_TB,
 @TangCuong_QS, @TangCuong_TB);";

                                insertCmd.Parameters.AddWithValue("@UserId", userId.Trim());
                                insertCmd.Parameters.AddWithValue("@TT", tt);
                                insertCmd.Parameters.AddWithValue("@NoiDung", noiDung.Trim());

                                insertCmd.Parameters.AddWithValue("@QS_SQ", GetCellInt(sheet, r, 2));
                                insertCmd.Parameters.AddWithValue("@QS_QNCN", GetCellInt(sheet, r, 3));
                                insertCmd.Parameters.AddWithValue("@QS_HSQ_BS", GetCellInt(sheet, r, 4));
                                insertCmd.Parameters.AddWithValue("@QS_Plus", GetCellInt(sheet, r, 5));

                                insertCmd.Parameters.AddWithValue("@VK_VuKhi", GetCellInt(sheet, r, 6));
                                insertCmd.Parameters.AddWithValue("@VK_XeMay", GetCellInt(sheet, r, 7));
                                insertCmd.Parameters.AddWithValue("@VK_TBKhac", GetCellInt(sheet, r, 8));

                                insertCmd.Parameters.AddWithValue("@HC_KT_QS", GetCellInt(sheet, r, 9));
                                insertCmd.Parameters.AddWithValue("@HC_KT_TB", GetCellInt(sheet, r, 10));

                                insertCmd.Parameters.AddWithValue("@TangCuong_QS", GetCellInt(sheet, r, 11));
                                insertCmd.Parameters.AddWithValue("@TangCuong_TB", GetCellInt(sheet, r, 12));

                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tx.Commit();
                }
            }
        }

        private bool IsDataExistForCurrentUser()
        {
            var userId = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userId)) return false;

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(1) FROM ToChucSuDungLucLuong WHERE UserId = @UserId;";
                command.Parameters.AddWithValue("@UserId", userId.Trim());
                var result = command.ExecuteScalar();
                return Convert.ToInt64(result) > 0;
            }
        }

        private static bool IsRowEmpty(unvell.ReoGrid.Worksheet sheet, int row, int startCol, int endCol)
        {
            for (int c = startCol; c <= endCol; c++)
            {
                var data = sheet.GetCellData(row, c);
                if (data != null && !string.IsNullOrWhiteSpace(Convert.ToString(data)))
                    return false;
            }
            return true;
        }

        private static string GetCellString(unvell.ReoGrid.Worksheet sheet, int row, int col)
        {
            var data = sheet.GetCellData(row, col);
            return data == null ? string.Empty : Convert.ToString(data).Trim();
        }

        private static int GetCellInt(unvell.ReoGrid.Worksheet sheet, int row, int col)
        {
            var data = sheet.GetCellData(row, col);
            if (data == null) return 0;

            if (data is int) return (int)data;
            if (data is long) return unchecked((int)(long)data);
            if (data is double) return (int)Math.Round((double)data);

            int v;
            return int.TryParse(Convert.ToString(data), out v) ? v : 0;
        }
    }
}
