using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing.Text;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;

namespace BoDoiApp.View.IVVuKhiKyThuat
{
    public partial class _1ChiTieu : UserControl
    {
        private static string Basedir = AppDomain.CurrentDomain.BaseDirectory;
        private static string EXCEL_PATH = Path.Combine(Basedir, "Resources", "Sheet", "Book1.xlsx");
        private string UserId = Constants.CURRENT_USER_ID_VALUE;
        public _1ChiTieu()
        {
            CreateDatabase();
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Form1());

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn chuyển sang phần Biện Pháp Bảo Đảm không? Dữ liệu hiện tại sẽ được lưu lại.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (IsDataExists())
                {
                    UpdateData();
                }else
                {
                    InsertData();
                }
            }
                
                
                NavigationService.Navigate(new _2BienPhapBaoDam());
        }

        private void _1ChiTieu_Load(object sender, System.EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[7];
            LoadExcel();

        }
        private bool IsDataExists()
        {
            string sql = "SELECT COUNT(*) FROM tbkt_supply_plan WHERE userId = @UserId";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", UserId);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi kiểm tra dữ liệu: {ex.Message}\nError Code: {ex.ErrorCode}", "Lỗi SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadExcel()
        {
            LoadSumOfAllSection();
            LoadSection(13, 23, "Hướng Chủ Yếu", UserId, new string[] { "phao_pk_127", "coi_82", "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(24, 34, "Hướng Thứ Yếu", UserId, new string[] { "phao_pk_127", "coi_82", "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(35, 45, "Phòng ngự phía sau", UserId, new string[] { "coi_60", "pct_spg9", "b41_m79", "dl", "trl", "tl", "sn", "luu_dan" });
            LoadSection(46, 56, "LL còn lại", UserId, new string[] { "phao_pk_127", "coi_100", "pct_spg9", "b41_m79", "tl", "sn", "luu_dan" });
        }

        private int[] SumOfChuYeuData()
        {

            string sql = @"
        SELECT    
             SUM(CAST(COALESCE(quan_so, 0) AS INTEGER))     AS sum_quan_so,    
             SUM(CAST(COALESCE(sn, 0) AS INTEGER))          AS sum_sn,    
             SUM(CAST(COALESCE(tl, 0) AS INTEGER))          AS sum_tl,    
             SUM(CAST(COALESCE(trl, 0) AS INTEGER))         AS sum_trl,    
             SUM(CAST(COALESCE(dl, 0) AS INTEGER))          AS sum_dl,    
             SUM(CAST(COALESCE(b41_m79, 0) AS INTEGER))     AS sum_b41_m79,    
             SUM(CAST(COALESCE(luu_dan, 0) AS INTEGER))     AS sum_luu_dan,    
             SUM(CAST(COALESCE(coi_60, 0) AS INTEGER))      AS sum_coi_60,    
             SUM(CAST(COALESCE(coi_82, 0) AS INTEGER))      AS sum_coi_82,    
             SUM(CAST(COALESCE(coi_100, 0) AS INTEGER))     AS sum_coi_100,    
             SUM(CAST(COALESCE(pct_spg9, 0) AS INTEGER))    AS sum_pct_spg9,    
             SUM(CAST(COALESCE(phao_pk_127, 0) AS INTEGER)) AS sum_phao_pk_127                
        FROM trangkithuat WHERE User = @UserId;";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@UserId", UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        int GetInt32OrZero(int ordinal)
                        {
                            return reader.IsDBNull(ordinal) ? 0 : Convert.ToInt32(reader.GetValue(ordinal));
                        }

                        while (reader.Read())
                        {
                            return new[]
                            {
                                    GetInt32OrZero(0),   // quan_so
                                    GetInt32OrZero(11),  // phao_pk_127
                                    GetInt32OrZero(9),   // coi_100
                                    GetInt32OrZero(8),   // coi_82
                                    GetInt32OrZero(7),   // coi_60
                                    GetInt32OrZero(10),  // pct_spg9
                                    GetInt32OrZero(5),   // b41_m79
                                    GetInt32OrZero(4),   // dl
                                    GetInt32OrZero(3),   // trl
                                    GetInt32OrZero(2),   // tl
                                    GetInt32OrZero(1),   // sn
                                    GetInt32OrZero(6),   // luu_dan
                                };
                        }

                        return new int[12];
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tính tổng: {ex.Message}\nError Code: {ex.ErrorCode}");
                return new int[12];
            }
        }

        private void LoadSumOfAllSection()
        {
            var arraySum = SumOfChuYeuData();
            string sql = @"
        SELECT    
             option,
             SUM(CAST(COALESCE(quan_so, 0) AS INTEGER))     AS sum_quan_so,    
             SUM(CAST(COALESCE(sn, 0) AS INTEGER))          AS sum_sn,    
             SUM(CAST(COALESCE(tl, 0) AS INTEGER))          AS sum_tl,    
             SUM(CAST(COALESCE(trl, 0) AS INTEGER))         AS sum_trl,    
             SUM(CAST(COALESCE(dl, 0) AS INTEGER))          AS sum_dl,    
             SUM(CAST(COALESCE(b41_m79, 0) AS INTEGER))     AS sum_b41_m79,    
             SUM(CAST(COALESCE(luu_dan, 0) AS INTEGER))     AS sum_luu_dan,    
             SUM(CAST(COALESCE(coi_60, 0) AS INTEGER))      AS sum_coi_60,    
             SUM(CAST(COALESCE(coi_82, 0) AS INTEGER))      AS sum_coi_82,    
             SUM(CAST(COALESCE(coi_100, 0) AS INTEGER))     AS sum_coi_100,    
             SUM(CAST(COALESCE(pct_spg9, 0) AS INTEGER))    AS sum_pct_spg9,    
             SUM(CAST(COALESCE(phao_pk_127, 0) AS INTEGER)) AS sum_phao_pk_127    
        FROM trangkithuat
        WHERE User = @UserId AND ll = '1. Trong biên chế'
        GROUP BY option";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);
                    var reader = command.ExecuteReader();
                    int index = 2;
                    string[] columnName = new string[] { "sum_phao_pk_127", "sum_coi_100", "sum_coi_82", "sum_coi_60", "sum_pct_spg9", "sum_b41_m79", "sum_dl", "sum_trl", "sum_tl", "sum_sn", "sum_luu_dan" };
                    int i = 0;
                    reader.Read();
                    foreach (var col in columnName)
                    {
                        var value = reader[col]?.ToString() ?? "0";
                        reoGridControl1.CurrentWorksheet.SetCellData(index, 3, value);
                        i++;
                        index++;

                    }
                    int k = 1;
                    for (int j = 2; j <= 12; j++)
                    {
                        reoGridControl1.CurrentWorksheet.SetCellData(j, 4, arraySum[k++]);
                    }
                }
            }
        }

        // Fix for CS1503: Argument 2: cannot convert from 'out object' to 'out int'
        // Change the declaration of sumVal from 'object' to 'int' in LoadSection

        private void LoadSection(int rowStart, int rowEnd, string section, string userId, string[] columnNames)
        {
            try
            {
                string sql = "SELECT * FROM trangkithuat WHERE User = @UserId AND option = @Section AND ll = '1. Trong biên chế'";

                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Section", section);

                    using (var reader = command.ExecuteReader())
                    {
                        var arraySum = ChuYeuData.SumOfChuYeuData(section);
                        if (!reader.Read())
                        {
                            // No rows found => fill with zeros to avoid exceptions and keep UI stable.
                            for (int row = rowStart; row <= rowEnd; row++)
                            {
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 3, "0");
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 4, "0");
                            }
                            return;
                        }

                        // Compute sums once per section (was recomputed inside the loop).

                        int maxRows = Math.Min(rowEnd - rowStart + 1, columnNames.Length);
                        for (int idx = 0; idx < maxRows; idx++)
                        {
                            int row = rowStart + idx;
                            string col = columnNames[idx];
                            if (row == 7 || row == 8 || row == 9 || row == 10) continue;

                            var value = reader[col] != null ? reader[col].ToString() : "0";
                            if (string.IsNullOrWhiteSpace(value)) value = "0";

                            reoGridControl1.CurrentWorksheet.SetCellData(row, 3, value);

                            int sumVal;
                            if (arraySum != null && arraySum.TryGetValue("sum_" +col, out sumVal))
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 4, sumVal.ToString());
                            else
                                reoGridControl1.CurrentWorksheet.SetCellData(row, 4, "0");
                        }

                        // If there are more template rows than columns => pad with zeros.
                        for (int row = rowStart + maxRows; row <= rowEnd; row++)
                        {
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 3, "0");
                            reoGridControl1.CurrentWorksheet.SetCellData(row, 4, "0");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi tải dữ liệu '{section}': {ex.Message}\nError Code: {ex.ErrorCode}",
                    "Lỗi SQLite",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi tải dữ liệu '{section}': {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CreateDatabase()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS tbkt_supply_plan (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userId TEXT NOT NULL,
    ten_tbkt TEXT,
    don_vi_tinh TEXT,
    nhu_cau INTEGER,
    hien_co_tong_so INTEGER,
    hien_co_so_tot INTEGER,
    hien_co_kbq INTEGER,
    hien_co_kt INTEGER,
    phai_co_truoc_cd INTEGER,
    bo_sung_so_luong INTEGER,
    bo_sung_thoi_gian TEXT,
    bo_sung_dia_diem TEXT,
    bo_sung_phuong_thuc TEXT
);";        
            using(var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertData()
        {
            string sql = @"INSERT INTO tbkt_supply_plan 
                (userId, ten_tbkt, don_vi_tinh, nhu_cau, hien_co_tong_so, hien_co_so_tot, 
                 hien_co_kbq, hien_co_kt, phai_co_truoc_cd, bo_sung_so_luong, 
                 bo_sung_thoi_gian, bo_sung_dia_diem, bo_sung_phuong_thuc) 
                VALUES 
                (@UserId, @TenTbkt, @DonViTinh, @NhuCau, @HienCoTongSo, @HienCoSoTot, 
                 @HienCoKbq, @HienCoKt, @PhaiCoTruocCd, @BoSungSoLuong, 
                 @BoSungThoiGian, @BoSungDiaDiem, @BoSungPhuongThuc)";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            for (int row = 2; row <= 46; row++)
                            {
                                using (var command = new SQLiteCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@UserId", UserId);
                                    command.Parameters.AddWithValue("@TenTbkt", reoGridControl1.CurrentWorksheet.GetCellData(row, 1)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@DonViTinh", reoGridControl1.CurrentWorksheet.GetCellData(row, 2)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@NhuCau", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 3)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoTongSo", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 4)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoSoTot", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 5)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoKbq", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 6)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoKt", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 7)?.ToString()));
                                    command.Parameters.AddWithValue("@PhaiCoTruocCd", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 8)?.ToString()));
                                    command.Parameters.AddWithValue("@BoSungSoLuong", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 9)?.ToString()));
                                    command.Parameters.AddWithValue("@BoSungThoiGian", reoGridControl1.CurrentWorksheet.GetCellData(row, 10)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@BoSungDiaDiem", reoGridControl1.CurrentWorksheet.GetCellData(row, 11)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@BoSungPhuongThuc", reoGridControl1.CurrentWorksheet.GetCellData(row, 12)?.ToString() ?? "");

                                    command.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lưu dữ liệu: {ex.Message}\nError Code: {ex.ErrorCode}", "Lỗi SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ParseIntOrZero(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            return int.TryParse(value, out int result) ? result : 0;
        }
        private void UpdateData()
        {
            string sql = @"UPDATE tbkt_supply_plan 
                    SET ten_tbkt = @TenTbkt, 
                        don_vi_tinh = @DonViTinh, 
                        nhu_cau = @NhuCau, 
                        hien_co_tong_so = @HienCoTongSo, 
                        hien_co_so_tot = @HienCoSoTot, 
                        hien_co_kbq = @HienCoKbq, 
                        hien_co_kt = @HienCoKt, 
                        phai_co_truoc_cd = @PhaiCoTruocCd, 
                        bo_sung_so_luong = @BoSungSoLuong, 
                        bo_sung_thoi_gian = @BoSungThoiGian, 
                        bo_sung_dia_diem = @BoSungDiaDiem, 
                        bo_sung_phuong_thuc = @BoSungPhuongThuc
                    WHERE userId = @UserId AND id = @Id";

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            for (int row = 2; row <= 46; row++)
                            {
                                // Calculate the ID based on row position (assuming sequential IDs starting from 1)
                                int recordId = row - 1;

                                using (var command = new SQLiteCommand(sql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@UserId", UserId);
                                    command.Parameters.AddWithValue("@Id", recordId);
                                    command.Parameters.AddWithValue("@TenTbkt", reoGridControl1.CurrentWorksheet.GetCellData(row, 1)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@DonViTinh", reoGridControl1.CurrentWorksheet.GetCellData(row, 2)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@NhuCau", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 3)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoTongSo", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 4)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoSoTot", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 5)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoKbq", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 6)?.ToString()));
                                    command.Parameters.AddWithValue("@HienCoKt", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 7)?.ToString()));
                                    command.Parameters.AddWithValue("@PhaiCoTruocCd", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 8)?.ToString()));
                                    command.Parameters.AddWithValue("@BoSungSoLuong", ParseIntOrZero(reoGridControl1.CurrentWorksheet.GetCellData(row, 9)?.ToString()));
                                    command.Parameters.AddWithValue("@BoSungThoiGian", reoGridControl1.CurrentWorksheet.GetCellData(row, 10)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@BoSungDiaDiem", reoGridControl1.CurrentWorksheet.GetCellData(row, 11)?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@BoSungPhuongThuc", reoGridControl1.CurrentWorksheet.GetCellData(row, 12)?.ToString() ?? "");

                                    command.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật dữ liệu: {ex.Message}\nError Code: {ex.ErrorCode}", "Lỗi SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

