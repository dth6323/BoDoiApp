using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.Outline;
using unvell.ReoGrid.Graphics;
namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ChuYeu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "QuanSo.xlsx");
        private string BoPhan = "chu yeu";
        private int EndRow = 0;
        public ChuYeu(string boPhan)
        {
            BoPhan = boPhan;
            InitializeComponent();
        }
        public static void LockSheet(ReoGridControl grid)
        {
            var ws = grid.CurrentWorksheet;
            if (ws == null) return;

            for (int r = 0; r < ws.RowCount; r++)
            {
                for (int c = 0; c < ws.ColumnCount; c++)
                {
                    ws.Cells[r, c].IsReadOnly = !IsCellAllowed(r, c);
                }
            }
        }

        private static bool IsCellAllowed(int row, int col)
        {
            // lock hàng 1 → 7 và 9
            if (row <= 6 || row == 8)
                return false;
            if (row == 7 && col == 0)
                return false;

            return true;
        }
        private void ChuYeu_Load(object sender, EventArgs e)
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("File Excel không tồn tại tại đường dẫn: " + EXCEL_PATH, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                reoGridControl1.Load(EXCEL_PATH);

                switch (BoPhan)
                {
                    case "Hướng Chủ yếu":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[1];
                        EndRow = 18;
                        break;
                    case "Hướng Thứ Yếu":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[2];
                        EndRow = 10;
                        break;
                    case "Phòng ngự phía sau":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[3];
                        EndRow = 9;
                        break;
                    case "Tieu Doan":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[0];
                        EndRow = 18;
                        break;
                    case "LL còn lại":
                        EndRow = 18;
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[4];
                        LoadForOther();
                        break;
                    default: break;
                }
                LockSheet(reoGridControl1);
                
                if (IsDataExists(BoPhan) && BoPhan != "LL còn lại")
                {
                    LoadDataWithUser();
                }
                var sheet2 = reoGridControl1.CurrentWorksheet;
                sheet2.HideColumns(15, sheet2.ColumnCount - 15);
                sheet2.HideColumns(25, sheet2.ColumnCount - 25);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetCellIfUnlocked(Worksheet ws, int row, int col, object value)
        {
            if (!ws.Cells[row, col].IsReadOnly)
            {
                ws.SetCellData(row, col, value);
            }
        }

        private bool IsDataExists(string boPhan)
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM trangkithuat WHERE User = @User AND option = @Option";
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                    command.Parameters.AddWithValue("@Option", boPhan);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (IsDataExists(BoPhan))
            {
                ChuYeuData.UpdateHangLoat(reoGridControl1, BoPhan);
            }
            else
            {
                ChuYeuData.ThemHangLoat(reoGridControl1, BoPhan);
            }
        }

        private void LoadDataWithUser()
        {
            string sql = "SELECT * FROM trangkithuat WHERE User = @User AND option = @Option";

            try
            {
                using (var connection = new SQLiteConnection("Data Source=data.db;Version=3;"))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                        command.Parameters.AddWithValue("@Option", BoPhan);

                        using (var reader = command.ExecuteReader())
                        {
                            var ws = reoGridControl1.CurrentWorksheet;
                            int row = 5;

                            while (reader.Read())
                            {
                                // bỏ qua các dòng header trong sheet
                                if (row == 5 || row == 6 || row == 7 || row == 16)
                                {
                                    row++;
                                }

                                SetCellIfUnlocked(ws, row, 0, reader["ll"]?.ToString());
                                SetCellIfUnlocked(ws, row, 1, reader["quan_so"]?.ToString());
                                SetCellIfUnlocked(ws, row, 2, reader["sn"]?.ToString());
                                SetCellIfUnlocked(ws, row, 3, reader["tl"]?.ToString());
                                SetCellIfUnlocked(ws, row, 4, reader["trl"]?.ToString());
                                SetCellIfUnlocked(ws, row, 5, reader["dl"]?.ToString());
                                SetCellIfUnlocked(ws, row, 6, reader["b41_m79"]?.ToString());
                                SetCellIfUnlocked(ws, row, 7, reader["luu_dan"]?.ToString());
                                SetCellIfUnlocked(ws, row, 8, reader["coi_60"]?.ToString());
                                SetCellIfUnlocked(ws, row, 9, reader["coi_82"]?.ToString());
                                SetCellIfUnlocked(ws, row, 10, reader["coi_100"]?.ToString());
                                SetCellIfUnlocked(ws, row, 11, reader["pct_spg9"]?.ToString());
                                SetCellIfUnlocked(ws, row, 12, reader["phao_pk_127"]?.ToString());
                                SetCellIfUnlocked(ws, row, 13, reader["ons"]?.ToString());
                                SetCellIfUnlocked(ws, row, 14, reader["db"]?.ToString());

                                row++;
                            }
                        }
                    }
                }

                reoGridControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }
        private void LoadForOther()
        {
            // ✅ Truyền đúng option cho từng bộ phận
            var tieuDoanData = ArrayData("Tieu Doan", "dBB");
            var chuYeuData = ArrayData("Hướng Chủ yếu", "Tổng");
            var thuYeuData = ArrayData("Hướng Thứ Yếu", "Tổng");
            var duBiData = ArrayData("Phòng ngự phía sau", "Tổng");

            var llConLai = new int[15];
            for (int i = 0; i < 15; i++)
            {
                llConLai[i] = tieuDoanData[i] - chuYeuData[i] - thuYeuData[i] - duBiData[i];
            }

            bool flag = IsDataExists("LL còn lại");

            if (!flag)
            {
                var ws = reoGridControl1.CurrentWorksheet;
                // ✅ Ghi vào đúng row tương ứng trong sheet (kiểm tra row nào là dòng tổng)
                for (int i = 0; i < 15; i++)
                {
                    ws.SetCellData(6, i+1, llConLai[i]); // xác nhận lại row index đúng
                }
            }
            else
            {
                LoadDataWithUser();
            }
        }
        
        private int[] ArrayData(string option, string columnName)
        {
            string sql =
                "SELECT ll, quan_so, sn, tl, trl, dl, b41_m79, luu_dan, coi_60, coi_82, coi_100, pct_spg9, phao_pk_127, ons, db " +
                "FROM trangkithuat " +
                "WHERE User = @User AND option = @Option AND ll = @ColumnName";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@Option", option);
                    command.Parameters.AddWithValue("@ColumnName", columnName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int[] data = new int[15];
                            for (int i = 1; i < 16; i++)
                            {
                                data[i-1] = reader.IsDBNull(i) ? 0 : Convert.ToInt32(reader.GetValue(i));
                            }
                            return data;
                        } 
                    }
                }
                return new int[15];
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
