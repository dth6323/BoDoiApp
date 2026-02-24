using BoDoiApp.Resources;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.IIIToChucSudung
{
    public partial class _2BoTri : UserControl
    {
        public _2BoTri()
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
            if (MessageBox.Show("Bạn có chắc chắn muốn lưu dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {

                var userId = Properties.Settings.Default.Username;
                if (string.IsNullOrWhiteSpace(userId))
                    throw new InvalidOperationException("Username is not set.");

                if (IsDataExistForCurrentUser(userId))
                {
                    UpdateData(userId);
                }
                else
                {
                    AddData(userId);
                }

                MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void _2BoTri_Load(object sender, EventArgs e)
        {
            try
            {
                CreateTable();

                var userId = Properties.Settings.Default.Username;
                if (string.IsNullOrWhiteSpace(userId))
                    return;

                if (IsDataExistForCurrentUser(userId))
                {
                    LoadData(userId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateTable()
        {
            const string sql = @"CREATE TABLE IF NOT EXISTS ToChucSuDungBoTriHCKT (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ViTriChinhThuc TEXT,
    TramQY TEXT,
    BepAn TEXT,
    VTB TEXT,
    KhoDan1 TEXT,
    KhoDan2 TEXT,
    ViTriDuBi TEXT
);";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool IsDataExistForCurrentUser(string userId)
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(1) FROM ToChucSuDungBoTriHCKT WHERE UserId = @UserId;";
                command.Parameters.AddWithValue("@UserId", userId.Trim());
                var result = command.ExecuteScalar();
                return Convert.ToInt64(result) > 0;
            }
        }

        private void AddData(string userId)
        {
            const string sql = @"
INSERT INTO ToChucSuDungBoTriHCKT
(UserId, ViTriChinhThuc, TramQY, BepAn, VTB, KhoDan1, KhoDan2, ViTriDuBi)
VALUES
(@UserId, @ViTriChinhThuc, @TramQY, @BepAn, @VTB, @KhoDan1, @KhoDan2, @ViTriDuBi);";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    FillParams(command, userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateData(string userId)
        {
            const string sql = @"
UPDATE ToChucSuDungBoTriHCKT
SET
  ViTriChinhThuc = @ViTriChinhThuc,
  TramQY = @TramQY,
  BepAn = @BepAn,
  VTB = @VTB,
  KhoDan1 = @KhoDan1,
  KhoDan2 = @KhoDan2,
  ViTriDuBi = @ViTriDuBi
WHERE UserId = @UserId;";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    FillParams(command, userId);
                    int affected = command.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        // fallback: record might have been deleted since the existence check
                        AddData(userId);
                    }
                }
            }
        }

        private void LoadData(string userId)
        {
            const string sql = @"
SELECT ViTriChinhThuc, TramQY, BepAn, VTB, KhoDan1, KhoDan2, ViTriDuBi
FROM ToChucSuDungBoTriHCKT
WHERE UserId = @UserId
LIMIT 1;";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId.Trim());

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return;

                    textBox1.Text = reader["ViTriChinhThuc"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ViTriChinhThuc"]);
                    textBox2.Text = reader["TramQY"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TramQY"]);
                    textBox3.Text = reader["BepAn"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BepAn"]);
                    textBox4.Text = reader["VTB"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VTB"]);
                    textBox5.Text = reader["KhoDan1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KhoDan1"]);
                    textBox6.Text = reader["KhoDan2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KhoDan2"]);
                    richTextBox1.Text = reader["ViTriDuBi"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ViTriDuBi"]);
                }
            }
        }

        private void FillParams(SQLiteCommand command, string userId)
        {
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@UserId", userId.Trim());
            command.Parameters.AddWithValue("@ViTriChinhThuc", (textBox1.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@TramQY", (textBox2.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@BepAn", (textBox3.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@VTB", (textBox4.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@KhoDan1", (textBox5.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@KhoDan2", (textBox6.Text ?? string.Empty).Trim());
            command.Parameters.AddWithValue("@ViTriDuBi", (richTextBox1.Text ?? string.Empty).Trim());
        }
    }
}
