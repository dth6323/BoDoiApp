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
using unvell.ReoGrid;
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
            reoGridControl1.SheetTabVisible = false;
            var sheet2 = reoGridControl1.CurrentWorksheet;
            LoadSummaryFromDB();
            sheet2.Ranges[new RangePosition(0, 0, 20, 5)].IsReadonly = true;
            sheet2.HideColumns(5, sheet2.ColumnCount - 5);

            // Ẩn từ dòng 82 trở đi
            sheet2.HideRows(7, sheet2.RowCount - 7);
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
            NavigationService.Navigate(() => new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsDataExist())
            {
                var result = MessageBox.Show(
                    "Dữ liệu đã tồn tại. Bạn có muốn cập nhật lại không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    UpdateBulkData();
                }
            }
            else
            {
                SaveData();
            }
            NavigationService.Navigate(() => new PhanCapVatLieu());
        }
        private void LoadSummaryFromDB()
        {
            var ws = reoGridControl1.CurrentWorksheet;

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                // ===== Load từ bảng dan_report =====
                string sqlReport = @"SELECT nhu_cau_tl, hien_co_dv_tl, hien_co_kho_tl
                             FROM dan_report
                             WHERE tt = 1 AND userId = @User";

                using (var cmd = new SQLiteCommand(sqlReport, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            int nhuCau = Convert.ToInt32(rd["nhu_cau_tl"]);
                            int hienCoDv = Convert.ToInt32(rd["hien_co_dv_tl"]);
                            int hienCoKho = Convert.ToInt32(rd["hien_co_kho_tl"]);

                            ws.SuspendUIUpdates();
                            ws.SetCellData(2, 2, hienCoDv + hienCoKho); // C3
                            ws.SetCellData(2, 3, nhuCau);
                            ws.ResumeUIUpdates();            // D3
                        }
                    }
                }

                // ===== Load từ bảng VCHCVTKT =====
                string sql = @"SELECT Row, Col, Value
                       FROM VCHCVTKT
                       WHERE UserId = @User
                       AND Row IN (7,13,18,20)
                       AND Col IN (5,15)";

                using (var cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);

                    using (var rd = cmd.ExecuteReader())
                    {
                        ws.SuspendUIUpdates();

                        while (rd.Read())
                        {
                            int r = Convert.ToInt32(rd["Row"]);
                            int c = Convert.ToInt32(rd["Col"]);
                            double v = Convert.ToDouble(rd["Value"]);

                            int excelRow = r + 1;

                            if (c == 15)
                            {
                                if (excelRow == 8) ws[3, 2] = v;
                                if (excelRow == 14) ws[4, 2] = v;
                                if (excelRow == 19) ws[5, 2] = v;
                                if (excelRow == 21) ws[6, 2] = v;
                            }

                            if (c == 5)
                            {
                                if (excelRow == 8) ws[3, 3] = v;
                                if (excelRow == 14) ws[4, 3] = v;
                                if (excelRow == 19) ws[5, 3] = v;
                                if (excelRow == 21) ws[6, 3] = v;
                            }
                        }

                        ws.ResumeUIUpdates();
                    }
                }
            }
        }
 
        private void CreateTable()
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS ""5_1_VatTu"" (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    Loai TEXT  NULL,
    DVT TEXT  NULL,
    QUYDINH INTEGER  NULL,
    HIENCO INTEGER  NULL,
    BOSUNG INTEGER  NULL
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

                if (string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(dvt))
                    continue;

                int quydinh = 0;
                int hienco = 0;
                int bosung = 0;

                int.TryParse(worksheet.GetCellData(row, 2)?.ToString(), out quydinh);
                int.TryParse(worksheet.GetCellData(row, 3)?.ToString(), out hienco);
                int.TryParse(worksheet.GetCellData(row, 4)?.ToString(), out bosung);

                AddData(loai, dvt, quydinh, hienco, bosung);
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

    if (string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(dvt))
        continue;

    int quydinh = 0;
    int hienco = 0;
    int bosung = 0;

    int.TryParse(worksheet.GetCellData(row, 2)?.ToString(), out quydinh);
    int.TryParse(worksheet.GetCellData(row, 3)?.ToString(), out hienco);
    int.TryParse(worksheet.GetCellData(row, 4)?.ToString(), out bosung);

    command.Parameters.Clear();
    command.Parameters.AddWithValue("@UserId", Properties.Settings.Default.Username);
    command.Parameters.AddWithValue("@Loai", loai);
    command.Parameters.AddWithValue("@DVT", dvt);
    command.Parameters.AddWithValue("@QUYDINH", quydinh);
    command.Parameters.AddWithValue("@HIENCO", hienco);
    command.Parameters.AddWithValue("@BOSUNG", bosung);

    command.ExecuteNonQuery();
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
