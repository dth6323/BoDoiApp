using BoDoiApp.Resources;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.IIIToChucSudung
{
    public partial class _1ToChucSudung : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string ExcelFilePath =
            Path.Combine(BaseDir, "Resources", "Sheet", "TinhHinhDonVi.xlsx");

        public _1ToChucSudung()
        {
            InitializeComponent();
        }

        private void _1ToChucSudung_Load(object sender, EventArgs e)
        {
            try
            {
                CreateTable();

                if (!File.Exists(ExcelFilePath))
                {
                    MessageBox.Show("Không tìm thấy file Excel:\n" + ExcelFilePath);
                    return;
                }

                try { reoGridControl1.Reset(); }
                catch (Exception ex) { MessageBox.Show("Lỗi Reset: " + ex.Message + "\n" + ex.StackTrace); return; }

                try { reoGridControl1.Load(ExcelFilePath); }
                catch (Exception ex) { MessageBox.Show("Lỗi Load: " + ex.Message + "\n" + ex.StackTrace); return; }

                var sheet = reoGridControl1.Worksheets["ToChucLucLuong"];
                if (sheet == null) { MessageBox.Show("Không tìm thấy sheet"); return; }

                reoGridControl1.CurrentWorksheet = sheet;
                reoGridControl1.SheetTabVisible = false;
                LockCells();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                SaveData();
                MessageBox.Show("Lưu thành công");
                NavigationService.Navigate(() => new _2BoTri());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SaveData();
            MessageBox.Show("Lưu thành công");
            NavigationService.Navigate(() => new _2BoTri());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        // ================= DATABASE =================

        private void CreateTable()
        {
            string sql = @"
CREATE TABLE IF NOT EXISTS ToChucSuDungLucLuong(
Id INTEGER PRIMARY KEY AUTOINCREMENT,
UserId TEXT,
RowIndex INTEGER,
NoiDung TEXT,
QS_SQ INTEGER,
QS_QNCN INTEGER,
QS_HSQ_BS INTEGER,
QS_Plus INTEGER,
VK_VuKhi INTEGER,
VK_XeMay INTEGER,
VK_TBKhac INTEGER,
HC_KT_QS INTEGER,
HC_KT_TB INTEGER,
TangCuong_QS INTEGER,
TangCuong_TB INTEGER,
UNIQUE(UserId,RowIndex)
);";
            using (var con = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(sql, con))
                    cmd.ExecuteNonQuery();
            }
        }

        // ================= LOAD =================

        private void LoadData()
        {
            var sheet = reoGridControl1.CurrentWorksheet;
            if (sheet == null) return;

            string userId = Properties.Settings.Default.Username;

            using (var con = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT * FROM ToChucSuDungLucLuong WHERE UserId=@u", con))
                {
                    cmd.Parameters.AddWithValue("@u", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int r = Convert.ToInt32(reader["RowIndex"]);
                            if (r < 0 || r >= sheet.RowCount) continue;

                            SetSafe(sheet, r, 1, reader["NoiDung"]);
                            SetSafe(sheet, r, 2, reader["QS_SQ"]);
                            SetSafe(sheet, r, 3, reader["QS_QNCN"]);
                            SetSafe(sheet, r, 4, reader["QS_HSQ_BS"]);
                            SetSafe(sheet, r, 5, reader["QS_Plus"]);
                            SetSafe(sheet, r, 6, reader["VK_VuKhi"]);
                            SetSafe(sheet, r, 7, reader["VK_XeMay"]);
                            SetSafe(sheet, r, 8, reader["VK_TBKhac"]);
                            SetSafe(sheet, r, 9, reader["HC_KT_QS"]);
                            SetSafe(sheet, r, 10, reader["HC_KT_TB"]);
                            SetSafe(sheet, r, 11, reader["TangCuong_QS"]);
                            SetSafe(sheet, r, 12, reader["TangCuong_TB"]);
                        }
                    }
                }
            }

            reoGridControl1.Refresh();
        }

        // ================= SAVE =================

        private void SaveData()
        {
            var sheet = reoGridControl1.CurrentWorksheet;
            if (sheet == null) return;

            string userId = Properties.Settings.Default.Username;

            using (var con = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    for (int r = 4; r <= 18; r++)
                    {
                        if (r >= sheet.RowCount) continue;
                        if (IsRowEmpty(sheet, r)) continue;

                        long count;
                        using (var checkCmd = new SQLiteCommand(
                            "SELECT COUNT(*) FROM ToChucSuDungLucLuong WHERE UserId=@u AND RowIndex=@row", con))
                        {
                            checkCmd.Parameters.AddWithValue("@u", userId);
                            checkCmd.Parameters.AddWithValue("@row", r);
                            count = (long)checkCmd.ExecuteScalar();
                        }

                        SQLiteCommand cmd = count > 0
                            ? new SQLiteCommand(@"
UPDATE ToChucSuDungLucLuong SET
NoiDung=@nd,
QS_SQ=@c2,
QS_QNCN=@c3,
QS_HSQ_BS=@c4,
QS_Plus=@c5,
VK_VuKhi=@c6,
VK_XeMay=@c7,
VK_TBKhac=@c8,
HC_KT_QS=@c9,
HC_KT_TB=@c10,
TangCuong_QS=@c11,
TangCuong_TB=@c12
WHERE UserId=@u AND RowIndex=@row", con)
                            : new SQLiteCommand(@"
INSERT INTO ToChucSuDungLucLuong
(UserId,RowIndex,NoiDung,
QS_SQ,QS_QNCN,QS_HSQ_BS,QS_Plus,
VK_VuKhi,VK_XeMay,VK_TBKhac,
HC_KT_QS,HC_KT_TB,
TangCuong_QS,TangCuong_TB)
VALUES(
@u,@row,@nd,
@c2,@c3,@c4,@c5,
@c6,@c7,@c8,
@c9,@c10,
@c11,@c12)", con);

                        cmd.Parameters.AddWithValue("@u", userId);
                        cmd.Parameters.AddWithValue("@row", r);
                        cmd.Parameters.AddWithValue("@nd", GetCellString(sheet, r, 1));
                        for (int c = 2; c <= 12; c++)
                            cmd.Parameters.AddWithValue("@c" + c, GetCellInt(sheet, r, c));

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
            }
        }

        // ================= LOCK =================

        private void LockCells()
        {
            var sheet = reoGridControl1.CurrentWorksheet;
            if (sheet == null) return;

            for (int r = 0; r < sheet.RowCount; r++)
                for (int c = 0; c < sheet.ColumnCount; c++)
                    sheet.Cells[r, c].IsReadOnly = true;

            for (int r = 4; r <= 18 && r < sheet.RowCount; r++)
                for (int c = 1; c <= 12 && c < sheet.ColumnCount; c++)
                    if (!IsSkipCell(r, c))
                        sheet.Cells[r, c].IsReadOnly = false;
        }

        private bool IsSkipCell(int row, int col)
        {
            if (col == 0) return true;

            if (row == 4 && col >= 2 && col <= 5) return true;

            if (col == 5 && row >= 5 && row <= 18) return true;

            if (row == 18 && col >= 2 && col <= 4) return true;

            if (row == 18 && col >= 9 && col <= 12) return true;

            // KHÓA thêm B,C,D,E từ dòng 6 -> 13
            if (row >= 6 && row <= 13 && col >= 1 && col <= 4) return true;

            return false;
        }

        // ================= SAFE CELL =================

        private void SetSafe(Worksheet sheet, int r, int c, object value)
        {
            try
            {
                if (c >= sheet.ColumnCount) return;
                sheet.SetCellData(r, c, value == DBNull.Value ? "" : value);
            }
            catch { }
        }

        // ================= HELPERS =================

        private static bool IsRowEmpty(Worksheet sheet, int row)
        {
            for (int c = 1; c <= 12; c++)
            {
                var data = sheet.GetCellData(row, c);
                if (data != null && !string.IsNullOrWhiteSpace(Convert.ToString(data)))
                    return false;
            }
            return true;
        }

        private static string GetCellString(Worksheet sheet, int r, int c)
        {
            var data = sheet.GetCellData(r, c);
            return data == null ? "" : Convert.ToString(data);
        }

        private static int GetCellInt(Worksheet sheet, int r, int c)
        {
            var data = sheet.GetCellData(r, c);
            int v;
            return int.TryParse(Convert.ToString(data), out v) ? v : 0;
        }
    }
}