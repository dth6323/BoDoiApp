using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class PhanCapVatLieu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");
        public PhanCapVatLieu()
        {
            CreateTable();
            InitializeComponent();
        }

        private void PhanCapVatLieu_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(EXCEL_PATH);
            reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[4];
            if (IsDataExist())
            {
                LoadDataFromDatabase();
            }
        }

       
        private void LoadDataFromDatabase()
        {
            var userName = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userName))
                return;

            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null)
                throw new InvalidOperationException("Current worksheet is not available.");

            // Column mapping (0-based). Keep consistent with Save/Update methods.
            const int COL_TT = 0;
            const int COL_LOAIVATCHAT = 1;
            const int COL_DVT = 2;

            const int COL_PC_TDQ_KHOD = 3;
            const int COL_PC_TDQ_DONVI = 4;
            const int COL_PC_TDQ_PLUS = 5;

            const int COL_PC_SCD_KHOD = 6;
            const int COL_PC_SCD_DONVI = 7;
            const int COL_PC_SCD_PLUS = 8;

            const int startRow = 2;

            try
            {
                var dt = new DataTable();

                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                using (var command = connection.CreateCommand())
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    connection.Open();

                    command.CommandText = @"
        SELECT
            vc.TT,
            vc.LoaiVatChat,
            vc.DVT,
            vc.PC_TDQ_KhoD,
            vc.PC_TDQ_DonVi,
            vc.PC_TDQ_Plus,
            vc.PC_SCD_KhoD,
            vc.PC_SCD_DonVi,
            vc.PC_SCD_Plus
        FROM VatChat vc
        INNER JOIN Users u ON u.Id = vc.UserId
        WHERE u.UserName = @UserName
        ORDER BY vc.TT ASC;";

                    command.Parameters.AddWithValue("@UserName", userName.Trim());
                    adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                    return;

                // Ensure enough rows exist in sheet
                int requiredRows = startRow + dt.Rows.Count;
                if (sheet.RowCount < requiredRows)
                {
                    sheet.AppendRows(requiredRows - sheet.RowCount);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];
                    int r = startRow + i;

                    sheet[r, COL_TT] = row["TT"] == DBNull.Value ? null : row["TT"];
                    sheet[r, COL_LOAIVATCHAT] = row["LoaiVatChat"] == DBNull.Value ? string.Empty : Convert.ToString(row["LoaiVatChat"]);
                    sheet[r, COL_DVT] = row["DVT"] == DBNull.Value ? string.Empty : Convert.ToString(row["DVT"]);

                    sheet[r, COL_PC_TDQ_KHOD] = row["PC_TDQ_KhoD"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_KhoD"]);
                    sheet[r, COL_PC_TDQ_DONVI] = row["PC_TDQ_DonVi"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_DonVi"]);
                    sheet[r, COL_PC_TDQ_PLUS] = row["PC_TDQ_Plus"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_TDQ_Plus"]);

                    sheet[r, COL_PC_SCD_KHOD] = row["PC_SCD_KhoD"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_KhoD"]);
                    sheet[r, COL_PC_SCD_DONVI] = row["PC_SCD_DonVi"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_DonVi"]);
                    sheet[r, COL_PC_SCD_PLUS] = row["PC_SCD_Plus"] == DBNull.Value ? 0 : Convert.ToInt32(row["PC_SCD_Plus"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load data failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            var userId = Properties.Settings.Default.Username;
            if (IsDataExist())
            {
                UpdateBulkDataFromCell(userId);
            }
            else
            {
                SaveDataFromReoGridToVatChat(userId);
            }
            NavigationService.Navigate(new TiepNhanBoXungV());
        }

        private bool IsDataExist()
        {
            // Checks whether VatChat has any rows for the current user (by username)
            string userName = Properties.Settings.Default.Username;
            if (string.IsNullOrWhiteSpace(userName)) return false;

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                // Assumes UserId in VatChat maps to Users.Id, and Users has UserName.
                command.CommandText = @"
        SELECT EXISTS(
        SELECT 1
        FROM VatChat vc
        INNER JOIN Users u ON u.Id = vc.UserId
        WHERE u.UserName = @UserName
        LIMIT 1
        );";
                command.Parameters.AddWithValue("@UserName", userName.Trim());

                object result = command.ExecuteScalar();
                return Convert.ToInt32(result) == 1;
            }
        }

        private void CreateTable()
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                string sql = @"CREATE TABLE IF NOT EXISTS VatChat (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        UserId TEXT NOT NULL,
        TT INTEGER  NULL,
        LoaiVatChat TEXT  NULL,
        DVT TEXT  NULL,

        PC_TDQ_KhoD INTEGER DEFAULT 0,
        PC_TDQ_DonVi INTEGER DEFAULT 0,
        PC_TDQ_Plus INTEGER DEFAULT 0,

        PC_SCD_KhoD INTEGER DEFAULT 0,
        PC_SCD_DonVi INTEGER DEFAULT 0,
        PC_SCD_Plus INTEGER DEFAULT 0
    );";
                connection.Open();
                var command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
            }
        }

        private long AddVatChat(
            string userId,
            int tt,
            string loaiVatChat,
            string dvt,
            int pcTdqKhoD = 0,
            int pcTdqDonVi = 0,
            int pcTdqPlus = 0,
            int pcScdKhoD = 0,
            int pcScdDonVi = 0,
            int pcScdPlus = 0)
        {
            if (string.IsNullOrWhiteSpace(loaiVatChat)) throw new ArgumentException("LoaiVatChat is required.", nameof(loaiVatChat));
            if (string.IsNullOrWhiteSpace(dvt)) throw new ArgumentException("DVT is required.", nameof(dvt));

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
    INSERT INTO VatChat
    (UserId, TT, LoaiVatChat, DVT,
     PC_TDQ_KhoD, PC_TDQ_DonVi, PC_TDQ_Plus,
     PC_SCD_KhoD, PC_SCD_DonVi, PC_SCD_Plus)
    VALUES
    (@UserId, @TT, @LoaiVatChat, @DVT,
     @PC_TDQ_KhoD, @PC_TDQ_DonVi, @PC_TDQ_Plus,
     @PC_SCD_KhoD, @PC_SCD_DonVi, @PC_SCD_Plus);
    SELECT last_insert_rowid();";

                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@TT", tt);
                command.Parameters.AddWithValue("@LoaiVatChat", loaiVatChat.Trim());
                command.Parameters.AddWithValue("@DVT", dvt.Trim());

                command.Parameters.AddWithValue("@PC_TDQ_KhoD", pcTdqKhoD);
                command.Parameters.AddWithValue("@PC_TDQ_DonVi", pcTdqDonVi);
                command.Parameters.AddWithValue("@PC_TDQ_Plus", pcTdqPlus);

                command.Parameters.AddWithValue("@PC_SCD_KhoD", pcScdKhoD);
                command.Parameters.AddWithValue("@PC_SCD_DonVi", pcScdDonVi);
                command.Parameters.AddWithValue("@PC_SCD_Plus", pcScdPlus);

                connection.Open();

                object result = command.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0L : Convert.ToInt64(result);
            }
        }

        private int SaveDataFromReoGridToVatChat(string userId, int startRow = 2)
        {
            if (userId == null) throw new ArgumentOutOfRangeException(nameof(userId));

            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null) throw new InvalidOperationException("Current worksheet is not available.");

            // Column mapping (0-based). Adjust if your sheet layout differs.
            const int COL_TT = 0;
            const int COL_LOAIVATCHAT = 1;
            const int COL_DVT = 2;

            const int COL_PC_TDQ_KHOD = 3;
            const int COL_PC_TDQ_DONVI = 4;
            const int COL_PC_TDQ_PLUS = 5;

            const int COL_PC_SCD_KHOD = 6;
            const int COL_PC_SCD_DONVI = 7;
            const int COL_PC_SCD_PLUS = 8;

            int inserted = 0;

            // Fallback scan range (kept conservative). Change to match your sheet.
            int maxRowsToScan = Math.Max(0, sheet.RowCount);
            if (maxRowsToScan <= 0) maxRowsToScan = 2000;

            try
            {
                for (int r = startRow; r < maxRowsToScan; r++)
                {
                    string loaiVatChat = GetCellString(sheet, r, COL_LOAIVATCHAT);
                    string dvt = GetCellString(sheet, r, COL_DVT);

                    if (string.IsNullOrWhiteSpace(loaiVatChat) && string.IsNullOrWhiteSpace(dvt))
                        continue; // ignore empty rows

                    if (string.IsNullOrWhiteSpace(loaiVatChat) || string.IsNullOrWhiteSpace(dvt))
                        continue; // enforce required fields for AddVatChat

                    int tt = GetCellInt(sheet, r, COL_TT);

                    int pcTdqKhoD = GetCellInt(sheet, r, COL_PC_TDQ_KHOD);
                    int pcTdqDonVi = GetCellInt(sheet, r, COL_PC_TDQ_DONVI);
                    int pcTdqPlus = GetCellInt(sheet, r, COL_PC_TDQ_PLUS);

                    int pcScdKhoD = GetCellInt(sheet, r, COL_PC_SCD_KHOD);
                    int pcScdDonVi = GetCellInt(sheet, r, COL_PC_SCD_DONVI);
                    int pcScdPlus = GetCellInt(sheet, r, COL_PC_SCD_PLUS);

                    long id = AddVatChat(
                        userId,
                        tt,
                        loaiVatChat,
                        dvt,
                        pcTdqKhoD,
                        pcTdqDonVi,
                        pcTdqPlus,
                        pcScdKhoD,
                        pcScdDonVi,
                        pcScdPlus);

                    if (id > 0) inserted++;
                }

                return inserted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save data failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return inserted;
            }
        }

        private static string GetCellString(unvell.ReoGrid.Worksheet sheet, int row, int col)
        {
            var cell = sheet.GetCell(row, col);
            var data = cell == null ? null : cell.Data;
            return data == null ? string.Empty : Convert.ToString(data).Trim();
        }

        private static int GetCellInt(unvell.ReoGrid.Worksheet sheet, int row, int col)
        {
            var cell = sheet.GetCell(row, col);
            var data = cell == null ? null : cell.Data;
            if (data == null) return 0;

            if (data is int) return (int)data;
            if (data is long) return unchecked((int)(long)data);
            if (data is double) return (int)Math.Round((double)data);

            int v;
            return int.TryParse(Convert.ToString(data), out v) ? v : 0;
        }



        private int UpdateBulkDataFromCell(string userId, int startRow = 2)
        {
            if (userId == null) throw new ArgumentOutOfRangeException(nameof(userId));

            var sheet = reoGridControl1?.CurrentWorksheet;
            if (sheet == null) throw new InvalidOperationException("Current worksheet is not available.");

            // Column mapping (0-based). Keep consistent with SaveDataFromReoGridToVatChat.
            const int COL_TT = 0;
            const int COL_LOAIVATCHAT = 1;
            const int COL_DVT = 2;

            const int COL_PC_TDQ_KHOD = 3;
            const int COL_PC_TDQ_DONVI = 4;
            const int COL_PC_TDQ_PLUS = 5;

            const int COL_PC_SCD_KHOD = 6;
            const int COL_PC_SCD_DONVI = 7;
            const int COL_PC_SCD_PLUS = 8;

            int updated = 0;

            // Conservative scan limit
            int maxRowsToScan = Math.Max(0, sheet.RowCount);
            if (maxRowsToScan <= 0) maxRowsToScan = 2000;

            try
            {
                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();

                    using (var tx = connection.BeginTransaction())
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = tx;
                        command.CommandText = @"
        UPDATE VatChat
        SET
          LoaiVatChat = @LoaiVatChat,
          DVT = @DVT,
          PC_TDQ_KhoD = @PC_TDQ_KhoD,
          PC_TDQ_DonVi = @PC_TDQ_DonVi,
          PC_TDQ_Plus = @PC_TDQ_Plus,
          PC_SCD_KhoD = @PC_SCD_KhoD,
          PC_SCD_DonVi = @PC_SCD_DonVi,
          PC_SCD_Plus = @PC_SCD_Plus
        WHERE UserId = @UserId AND TT = @TT;";

                        // prepare parameters once
                        command.Parameters.Add("@LoaiVatChat", DbType.String);
                        command.Parameters.Add("@DVT", DbType.String);

                        command.Parameters.Add("@PC_TDQ_KhoD", DbType.Int32);
                        command.Parameters.Add("@PC_TDQ_DonVi", DbType.Int32);
                        command.Parameters.Add("@PC_TDQ_Plus", DbType.Int32);

                        command.Parameters.Add("@PC_SCD_KhoD", DbType.Int32);
                        command.Parameters.Add("@PC_SCD_DonVi", DbType.Int32);
                        command.Parameters.Add("@PC_SCD_Plus", DbType.Int32);

                        command.Parameters.Add("@UserId", DbType.Int32);
                        command.Parameters.Add("@TT", DbType.Int32);

                        for (int r = startRow; r < maxRowsToScan; r++)
                        {
                            string loaiVatChat = GetCellString(sheet, r, COL_LOAIVATCHAT);
                            string dvt = GetCellString(sheet, r, COL_DVT);

                            // ignore empty rows
                            if (string.IsNullOrWhiteSpace(loaiVatChat) && string.IsNullOrWhiteSpace(dvt))
                                continue;

                            // enforce required fields to avoid writing bad data
                            if (string.IsNullOrWhiteSpace(loaiVatChat) || string.IsNullOrWhiteSpace(dvt))
                                continue;

                            int tt = GetCellInt(sheet, r, COL_TT);
                            if (tt <= 0) continue;

                            command.Parameters["@LoaiVatChat"].Value = loaiVatChat.Trim();
                            command.Parameters["@DVT"].Value = dvt.Trim();

                            command.Parameters["@PC_TDQ_KhoD"].Value = GetCellInt(sheet, r, COL_PC_TDQ_KHOD);
                            command.Parameters["@PC_TDQ_DonVi"].Value = GetCellInt(sheet, r, COL_PC_TDQ_DONVI);
                            command.Parameters["@PC_TDQ_Plus"].Value = GetCellInt(sheet, r, COL_PC_TDQ_PLUS);

                            command.Parameters["@PC_SCD_KhoD"].Value = GetCellInt(sheet, r, COL_PC_SCD_KHOD);
                            command.Parameters["@PC_SCD_DonVi"].Value = GetCellInt(sheet, r, COL_PC_SCD_DONVI);
                            command.Parameters["@PC_SCD_Plus"].Value = GetCellInt(sheet, r, COL_PC_SCD_PLUS);

                            command.Parameters["@UserId"].Value = userId;
                            command.Parameters["@TT"].Value = tt;

                            updated += command.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                return updated;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bulk update failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return updated;
            }
        }

    }
}
