using BoDoiApp.Resources;
using BoDoiApp.View.VICongTacVanTai;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class _3CanDoi : UserControl
    {
        private float zoomFactor = 1.0f;

        public _3CanDoi()
        {
            InitializeComponent();
            this.Load += _3CanDoi_Load;
        }

        private void _3CanDoi_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // =============================
        // LOAD DATA
        // =============================
        private void LoadData()
        {
            try
            {
                string sql = "SELECT * FROM candoivt WHERE UserId = @UserId LIMIT 1";

                using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
                {
                    connection.Open();

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vtbi_from.Text = reader["vtbi_from"]?.ToString() ?? "";
                                vtbi_to.Text = reader["vtbi_to"]?.ToString() ?? "";

                                vtle_from.Text = reader["vtle_from"]?.ToString() ?? "";
                                vtle_to.Text = reader["vtle_to"]?.ToString() ?? "";

                                danquan_from.Text = reader["danquan_from"]?.ToString() ?? "";
                                danquan_to.Text = reader["danquan_to"]?.ToString() ?? "";

                                xetho_count.Text = reader["xetho_count"]?.ToString() ?? "";
                                xetho_from.Text = reader["xetho_from"]?.ToString() ?? "";
                                xetho_to.Text = reader["xetho_to"]?.ToString() ?? "";

                                tongkha_from.Text = reader["tongkha_from"]?.ToString() ?? "";
                                tongkha_to.Text = reader["tongkha_to"]?.ToString() ?? "";

                                tong_to.Text = reader["tong_to"]?.ToString() ?? "";

                                txtKetLuan.Text = reader["ketluan"]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // =============================
        // SAVE DATA
        // =============================
        private void SaveAllData(string userId)
        {
            string checkSql = "SELECT COUNT(*) FROM candoivt WHERE UserId = @UserId";

            string insertSql = @"
INSERT INTO candoivt
(UserId,
 vtbi_from, vtbi_to,
 vtle_from, vtle_to,
 danquan_from, danquan_to,
 xetho_count,
 xetho_from, xetho_to,
 tongkha_from, tongkha_to,
 tong_to,
 ketluan)
VALUES
(@UserId,
 @vtbi_from, @vtbi_to,
 @vtle_from, @vtle_to,
 @danquan_from, @danquan_to,
 @xetho_count,
 @xetho_from, @xetho_to,
 @tongkha_from, @tongkha_to,
 @tong_to,
 @ketluan)";

            string updateSql = @"
UPDATE candoivt SET
    vtbi_from = @vtbi_from,
    vtbi_to = @vtbi_to,

    vtle_from = @vtle_from,
    vtle_to = @vtle_to,

    danquan_from = @danquan_from,
    danquan_to = @danquan_to,

    xetho_count = @xetho_count,

    xetho_from = @xetho_from,
    xetho_to = @xetho_to,

    tongkha_from = @tongkha_from,
    tongkha_to = @tongkha_to,

    tong_to = @tong_to,

    ketluan = @ketluan
WHERE UserId = @UserId";

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                bool exists;

                using (var checkCommand = new SQLiteCommand(checkSql, connection))
                {
                    checkCommand.Parameters.AddWithValue("@UserId", userId);
                    exists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                }

                string sql = exists ? updateSql : insertSql;

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    command.Parameters.AddWithValue("@vtbi_from", vtbi_from?.Text ?? "");
                    command.Parameters.AddWithValue("@vtbi_to", vtbi_to?.Text ?? "");

                    command.Parameters.AddWithValue("@vtle_from", vtle_from?.Text ?? "");
                    command.Parameters.AddWithValue("@vtle_to", vtle_to?.Text ?? "");

                    command.Parameters.AddWithValue("@danquan_from", danquan_from?.Text ?? "");
                    command.Parameters.AddWithValue("@danquan_to", danquan_to?.Text ?? "");

                    command.Parameters.AddWithValue("@xetho_count", xetho_count?.Text ?? "");

                    command.Parameters.AddWithValue("@xetho_from", xetho_from?.Text ?? "");
                    command.Parameters.AddWithValue("@xetho_to", xetho_to?.Text ?? "");

                    command.Parameters.AddWithValue("@tongkha_from", tongkha_from?.Text ?? "");
                    command.Parameters.AddWithValue("@tongkha_to", tongkha_to?.Text ?? "");

                    command.Parameters.AddWithValue("@tong_to", tong_to?.Text ?? "");

                    command.Parameters.AddWithValue("@ketluan", txtKetLuan?.Text ?? "");

                    command.ExecuteNonQuery();
                }
            }
        }

        // =============================
        // BUTTON SAVE
        // =============================
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllData(Constants.CURRENT_USER_ID_VALUE);

                MessageBox.Show(
                    "Lưu dữ liệu thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // =============================
        // BUTTON BACK
        // =============================
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllData(Constants.CURRENT_USER_ID_VALUE);
                NavigationService.Navigate(() => new DuTinhKhoiLuongVanChuyen());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // =============================
        // BUTTON NEXT
        // =============================
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllData(Constants.CURRENT_USER_ID_VALUE);
                NavigationService.Navigate(() => new _4YdinhVanChuyen());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // =============================
        // BUTTON HOME
        // =============================
        private void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllData(Constants.CURRENT_USER_ID_VALUE);
                NavigationService.Navigate(() => new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // =============================
        // ZOOM UI
        // =============================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Add))
            {
                zoomFactor += 0.1f;
                this.Scale(new SizeF(1.1f, 1.1f));
                return true;
            }

            if (keyData == (Keys.Control | Keys.Subtract))
            {
                zoomFactor -= 0.1f;
                this.Scale(new SizeF(0.9f, 0.9f));
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =============================
        // FONT UPDATE
        // =============================
        private void UpdateFontRecursive(Control control)
        {
            control.Font = new Font("Times New Roman", 11f, control.Font.Style);

            foreach (Control child in control.Controls)
            {
                UpdateFontRecursive(child);
            }
        }
    }
}