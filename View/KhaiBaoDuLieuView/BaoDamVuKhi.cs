using BoDoiApp.Resources;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BaoDamVuKhi : UserControl
    {
        // Các dòng thực sự chứa dữ liệu trong Excel
        private readonly int[] DATA_ROWS = { 2, 3, 4,5,6, 7, 8,9, 10, 11,12 };
        private readonly int[] DATA_COLS = { 3,6,7 };
        private readonly string ConnectioString = "Data Source=data.db;Version=3;";
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");

        public BaoDamVuKhi()
        {
            InitializeComponent();
            CreateTable();
        }

        private bool IsDataExists(string userId)
        {
            using (var connection = new SQLiteConnection(ConnectioString))
            {
                connection.Open();
                string sql = @"SELECT COUNT(*) FROM ""4_1_ChiTieu"" WHERE UserId = @UserId;";
                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
            ;
            
        }
        private void BaoDamVuKhi_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            var sheet = reoGridControl1.Worksheets[2];
            reoGridControl1.CurrentWorksheet = sheet;
            reoGridControl1.SheetTabVisible = false;
            if (IsDataExists(Properties.Settings.Default.Username))
            {
                LoadDataFromDatabase();
            }
            LoadTrangKiThuatToColumnC();

            // ===== Khóa toàn bộ sheet =====
            for (int row = 0; row < sheet.RowCount; row++)
            {
                for (int col = 0; col < sheet.ColumnCount; col++)
                {
                    sheet.Cells[row, col].IsReadOnly = true;
                }
            }
            // ===== 2. Mở khóa D3-D13 =====
            for (int row = 2; row <= 12; row++) // 3 → 13
            {
                sheet.Cells[row, 3].IsReadOnly = false; // cột D
            }

            // ===== 3. Mở khóa G3-G13 =====
            for (int row = 2; row <= 12; row++)
            {
                sheet.Cells[row, 6].IsReadOnly = false; // cột G
            }

            // ===== 4. Mở khóa H3-H13 =====
            for (int row = 2; row <= 12; row++)
            {
                sheet.Cells[row, 7].IsReadOnly = false; // cột H
            }

            // ===== Ẩn cột từ J trở đi =====
            sheet.HideColumns(9, sheet.ColumnCount - 9);

            // ===== Ẩn dòng từ 14 trở đi =====
            sheet.HideRows(13, sheet.RowCount - 13);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(() => new Form1());
        }
        private void LoadTrangKiThuatToColumnC()
        {
            string userId = Properties.Settings.Default.Username;

            string sql = @"SELECT 
                    sn, tl, trl, dl, b41_m79,
                    luu_dan, coi_60, coi_82, coi_100,
                    pct_spg9, phao_pk_127
                   FROM trangkithuat
                   WHERE User = @UserId 
                   AND ll = 'Tổng' 
                   AND option = 'Tieu Doan';";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object[] values =
                            {
                        reader["sn"],
                        reader["tl"],
                        reader["trl"],
                        reader["dl"],
                        reader["b41_m79"],
                        reader["luu_dan"],
                        reader["coi_60"],
                        reader["coi_82"],
                        reader["coi_100"],
                        reader["pct_spg9"],
                        reader["phao_pk_127"]
                    };

                            for (int i = 0; i < DATA_ROWS.Length && i < values.Length; i++)
                            {
                                int row = DATA_ROWS[i];
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 2, values[i]);
                            }
                        }
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (IsDataExists(Properties.Settings.Default.Username))
            {
                BulkUpdateData();
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SaveData();
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            NavigationService.Navigate(() => new TiepNhanBoSung());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""4_1_ChiTieu"" (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId NVARCHAR(10) NOT NULL,
    LoaiTBKT TEXT  NULL,
    DVT TEXT  NULL,
    NhuCau INTEGER  NULL,
    HienCo_Plus INTEGER  NULL,
    HienCo_Kbd INTEGER  NULL,
    HienCo_Kt INTEGER  NULL,
    HienCo_SoTot INTEGER  NULL,
    PCTQD INTEGER  NULL,
    BoSung INTEGER  NULL
);";
            using (var connection = new SQLiteConnection(ConnectioString))
            {
                connection.Open();
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            ;
        }

        private void SaveData()
        {
            foreach (int i in DATA_ROWS)
            {
                string loai = reoGridControl1.CurrentWorksheet.GetCellData(i, 0)?.ToString();
                string dvt = reoGridControl1.CurrentWorksheet.GetCellData(i, 1)?.ToString();
                string nhucau = reoGridControl1.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                string plus = reoGridControl1.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                string kbd = reoGridControl1.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                string kt = reoGridControl1.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                string sotot = reoGridControl1.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                string pctqd = reoGridControl1.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                string bosung = reoGridControl1.CurrentWorksheet.GetCellData(i, 8)?.ToString();

                AddData(loai, dvt, nhucau, plus, kbd, kt, sotot, pctqd, bosung);
            }
        }

        private void AddData(string loai, string dvt, string nhucau, string plus, string kbd, string kt, string sotot, string pctqd, string bosung)
        {
            using (var connection = new SQLiteConnection(ConnectioString))
            {
                connection.Open();
                string UserId = Properties.Settings.Default.Username;
                string sql = @"
                    INSERT INTO ""4_1_ChiTieu""
                    (
                        UserId,
                        LoaiTBKT,
                        DVT,
                        NhuCau,
                        HienCo_Plus,
                        HienCo_Kbd,
                        HienCo_Kt,
                        HienCo_SoTot,
                        PCTQD,
                        BoSung
                    )
                    VALUES
                    (
                        @UserId,
                        @LoaiTBKT,
                        @DVT,
                        @NhuCau,
                        @HienCo_Plus,
                        @HienCo_Kbd,
                        @HienCo_Kt,
                        @HienCo_SoTot,
                        @PCTQD,
                        @BoSung
                    );";
                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@LoaiTBKT", loai);
                command.Parameters.AddWithValue("@DVT", dvt);
                command.Parameters.AddWithValue("@NhuCau", nhucau);
                command.Parameters.AddWithValue("@HienCo_Plus", plus);
                command.Parameters.AddWithValue("@HienCo_Kbd", kbd);
                command.Parameters.AddWithValue("@HienCo_Kt", kt);
                command.Parameters.AddWithValue("@HienCo_SoTot", sotot);
                command.Parameters.AddWithValue("@PCTQD", pctqd);
                command.Parameters.AddWithValue("@BoSung", bosung);
                command.ExecuteNonQuery();
            }
            ;
            
        }

        private void BulkUpdateData()
        {
                using (var connection = new SQLiteConnection(ConnectioString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            for (int i = 2; i <= DATA_ROWS.Length; i++)
                            {
                                string loai = reoGridControl1.CurrentWorksheet.GetCellData(i, 0)?.ToString();
                                string dvt = reoGridControl1.CurrentWorksheet.GetCellData(i, 1)?.ToString();
                                string nhucau = reoGridControl1.CurrentWorksheet.GetCellData(i, 2)?.ToString();
                                string plus = reoGridControl1.CurrentWorksheet.GetCellData(i, 3)?.ToString();
                                string kbd = reoGridControl1.CurrentWorksheet.GetCellData(i, 4)?.ToString();
                                string kt = reoGridControl1.CurrentWorksheet.GetCellData(i, 5)?.ToString();
                                string sotot = reoGridControl1.CurrentWorksheet.GetCellData(i, 6)?.ToString();
                                string pctqd = reoGridControl1.CurrentWorksheet.GetCellData(i, 7)?.ToString();
                                string bosung = reoGridControl1.CurrentWorksheet.GetCellData(i, 8)?.ToString();

                                string sql = @"
            UPDATE ""4_1_ChiTieu""
            SET
                LoaiTBKT = @LoaiTBKT,
                DVT = @DVT,
                NhuCau = @NhuCau,
                HienCo_Plus = @HienCo_Plus,
                HienCo_Kbd = @HienCo_Kbd,
                HienCo_Kt = @HienCo_Kt,
                HienCo_SoTot = @HienCo_SoTot,
                PCTQD = @PCTQD,
                BoSung = @BoSung
            WHERE UserId = @UserId AND LoaiTBKT = @LoaiTBKT;";

                                var command = new SQLiteCommand(sql, connection, transaction);
                                command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                                command.Parameters.AddWithValue("@LoaiTBKT", loai);
                                command.Parameters.AddWithValue("@DVT", dvt);
                                command.Parameters.AddWithValue("@NhuCau", nhucau);
                                command.Parameters.AddWithValue("@HienCo_Plus", plus);
                                command.Parameters.AddWithValue("@HienCo_Kbd", kbd);
                                command.Parameters.AddWithValue("@HienCo_Kt", kt);
                                command.Parameters.AddWithValue("@HienCo_SoTot", sotot);
                                command.Parameters.AddWithValue("@PCTQD", pctqd);
                                command.Parameters.AddWithValue("@BoSung", bosung);
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi cập nhập dữ liệu hãy đảm bảo tất cả các ô đều được nhập dữ liệu");
                            return; 
                        }
                    }
                }
        }

        private void LoadDataFromDatabase()
        {
            string userId = Properties.Settings.Default.Username;

            string sql = @"SELECT LoaiTBKT, DVT, NhuCau, HienCo_Plus, HienCo_Kbd, 
                   HienCo_Kt, HienCo_SoTot, PCTQD, BoSung 
                   FROM ""4_1_ChiTieu"" 
                   WHERE UserId = @UserId;";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    int index = 0;

                    while (reader.Read() && index < DATA_ROWS.Length)
                    {
                        int row = DATA_ROWS[index];

                        if (Array.Exists(DATA_COLS, c => c == 0))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 0, reader["LoaiTBKT"]);

                        if (Array.Exists(DATA_COLS, c => c == 1))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 1, reader["DVT"]);

                        if (Array.Exists(DATA_COLS, c => c == 2))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 2, reader["NhuCau"]);

                        if (Array.Exists(DATA_COLS, c => c == 3))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 3, reader["HienCo_Plus"]);

                        if (Array.Exists(DATA_COLS, c => c == 4))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 4, reader["HienCo_Kbd"]);

                        if (Array.Exists(DATA_COLS, c => c == 5))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 5, reader["HienCo_Kt"]);

                        if (Array.Exists(DATA_COLS, c => c == 6))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 6, reader["HienCo_SoTot"]);

                        if (Array.Exists(DATA_COLS, c => c == 7))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 7, reader["PCTQD"]);

                        if (Array.Exists(DATA_COLS, c => c == 8))
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 8, reader["BoSung"]);

                        index++;
                    }
                }
            }
        }
    }
}
