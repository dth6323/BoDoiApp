using BoDoiApp.Resources;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BaoDamVuKhi : UserControl
    {
        private readonly string ConnectioString = "Data Source=data2.db;Version=3;";
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");

        public BaoDamVuKhi()
        {
            CreateTable();
            InitializeComponent();
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
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[2];
            if(IsDataExists(Properties.Settings.Default.Username))
            {
                LoadDataFromDatabase();
            }
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
            NavigationService.Navigate(new TiepNhanBoSung());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ""4_1_ChiTieu"" (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId NVARCHAR(10) NOT NULL,
    LoaiTBKT TEXT NOT NULL,
    DVT TEXT NOT NULL,
    NhuCau INTEGER NOT NULL,
    HienCo_Plus INTEGER NOT NULL,
    HienCo_Kbd INTEGER NOT NULL,
    HienCo_Kt INTEGER NOT NULL,
    HienCo_SoTot INTEGER NOT NULL,
    PCTQD INTEGER NOT NULL,
    BoSung INTEGER NOT NULL
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
            for (int i = 2; i <= 7; i++)
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
                            for (int i = 2; i <= 7; i++)
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
                            throw;
                        }
                    }
                }
        }
        
        private void LoadDataFromDatabase()
        {
            string userId = Properties.Settings.Default.Username;
            int startRow = 2;   
            string sql = @"SELECT LoaiTBKT, DVT, NhuCau, HienCo_Plus, HienCo_Kbd, HienCo_Kt, HienCo_SoTot, PCTQD, BoSung 
                           FROM ""4_1_ChiTieu"" 
                           WHERE UserId = @UserId;";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = command.ExecuteReader())
                {
                    int currentRow = startRow;
                    while (reader.Read())
                    {
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 0, reader["LoaiTBKT"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 1, reader["DVT"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 2, reader["NhuCau"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 3, reader["HienCo_Plus"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 4, reader["HienCo_Kbd"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 5, reader["HienCo_Kt"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 6, reader["HienCo_SoTot"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 7, reader["PCTQD"].ToString());
                        reoGridControl1.CurrentWorksheet.SetCellData(currentRow, 8, reader["BoSung"].ToString());
                        currentRow++;
                    }
                }
            }
        }
    }
}
