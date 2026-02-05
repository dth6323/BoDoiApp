using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class DanVatChatVatTu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");
        public DanVatChatVatTu()
        {
            CreateTable();
            InitializeComponent();
        }

        private void DanVatChatVatTu_Load(object sender, EventArgs e)
        {   
            
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[3];
            if(IsDataExist())
            {
                LoadDataFromDatabse();
            }
        }

        private bool IsDataExist()
        {
            string userId = Properties.Settings.Default.Username;
            string sql = @"SELECT COUNT(*) FROM ""5_1_VatTu"" WHERE UserId = @UserId;";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
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
            SaveData();
            NavigationService.Navigate(new PhanCapVatLieu());
        }

        private bool LoadDataFromDatabse()
        {
            string userId = Properties.Settings.Default.Username;
            string sql = @"SELECT Loai, DVT, QUYDINH, HIENCO, BOSUNG 
                           FROM ""5_1_VatTu"" 
                           WHERE UserId = @UserId;";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        var worksheet = reoGridControl1.CurrentWorksheet;
                        int row = 1; // Assuming first row is header
                        while (reader.Read())
                        {
                            worksheet.SetCellData(row, 0, reader["Loai"]?.ToString());
                            worksheet.SetCellData(row, 1, reader["DVT"]?.ToString());
                            worksheet.SetCellData(row, 2, reader["QUYDINH"]?.ToString());
                            worksheet.SetCellData(row, 3, reader["HIENCO"]?.ToString());
                            worksheet.SetCellData(row, 4, reader["BOSUNG"]?.ToString());
                            row++;
                        }
                    }
                }
            }
            return false;
        }
        private void CreateTable()
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS ""5_1_VatTu"" (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    Loai TEXT NOT NULL,
    DVT TEXT NOT NULL,
    QUYDINH INTEGER NOT NULL,
    HIENCO INTEGER NOT NULL,
    BOSUNG INTEGER NOT NULL
);";
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }
        private void AddData(string loai, string dvt, int quydinh, int hienco, int bosung)
        {
            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    var sql = @"INSERT INTO ""5_1_VatTu"" (UserId, Loai, DVT, QUYDINH, HIENCO, BOSUNG)
                            VALUES (@UserId, @Loai, @DVT, @QUYDINH, @HIENCO, @BOSUNG);";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                    command.Parameters.AddWithValue("@Loai", loai);
                    command.Parameters.AddWithValue("@DVT", dvt);
                    command.Parameters.AddWithValue("@QUYDINH", quydinh);
                    command.Parameters.AddWithValue("@HIENCO", hienco);
                    command.Parameters.AddWithValue("@BOSUNG", bosung);
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi thêm thông tin: {ex.Message}\nError Code: {ex.ErrorCode}");
            }
        }
        private void SaveData()
        {
            var worksheet = reoGridControl1.CurrentWorksheet;
            int rowCount = worksheet.RowCount;
            for (int row = 1; row < rowCount; row++)
            {
                var loai = worksheet.GetCellData(row, 0)?.ToString();
                var dvt = worksheet.GetCellData(row, 1)?.ToString();
                var quydinhObj = worksheet.GetCellData(row, 2);
                var hiencoObj = worksheet.GetCellData(row, 3);
                var bosungObj = worksheet.GetCellData(row, 4);
                if (string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(dvt) ||
                    quydinhObj == null || hiencoObj == null || bosungObj == null)
                {
                    continue; // Skip empty or incomplete rows
                }
                if (int.TryParse(quydinhObj.ToString(), out int quydinh) &&
                    int.TryParse(hiencoObj.ToString(), out int hienco) &&
                    int.TryParse(bosungObj.ToString(), out int bosung))
                {
                    AddData(loai, dvt, quydinh, hienco, bosung);
                }
                else
                {
                    MessageBox.Show($"Dữ liệu không hợp lệ ở dòng {row + 1}. Vui lòng kiểm tra lại.");
                }
            }
            MessageBox.Show("Dữ liệu đã được lưu thành công.");
        }
        private void UpdateBulkData()
        {
            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var sql = @"UPDATE ""5_1_VatTu"" 
                                    SET DVT = @DVT, QUYDINH = @QUYDINH, HIENCO = @HIENCO, BOSUNG = @BOSUNG
                                    WHERE Loai = @Loai AND UserId = @UserId;";
                        var command = new SQLiteCommand(sql, connection);
                        command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                        var worksheet = reoGridControl1.CurrentWorksheet;
                        int rowCount = worksheet.RowCount;
                        for (int row = 1; row < rowCount; row++)
                        {
                            var loai = worksheet.GetCellData(row, 0)?.ToString();
                            var dvt = worksheet.GetCellData(row, 1)?.ToString();
                            var quydinhObj = worksheet.GetCellData(row, 2);
                            var hiencoObj = worksheet.GetCellData(row, 3);
                            var bosungObj = worksheet.GetCellData(row, 4);
                            if (string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(dvt) ||
                                quydinhObj == null || hiencoObj == null || bosungObj == null)
                            {
                                continue; // Skip empty or incomplete rows
                            }
                            if (int.TryParse(quydinhObj.ToString(), out int quydinh) &&
                                int.TryParse(hiencoObj.ToString(), out int hienco) &&
                                int.TryParse(bosungObj.ToString(), out int bosung))
                            {
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
                                command.Parameters.AddWithValue("@Loai", loai);
                                command.Parameters.AddWithValue("@DVT", dvt);
                                command.Parameters.AddWithValue("@QUYDINH", quydinh);
                                command.Parameters.AddWithValue("@HIENCO", hienco);
                                command.Parameters.AddWithValue("@BOSUNG", bosung);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật thông tin: {ex.Message}\nError Code: {ex.ErrorCode}");
            }
        }
    }
}
