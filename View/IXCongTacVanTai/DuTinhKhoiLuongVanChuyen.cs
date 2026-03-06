using BoDoiApp.Resources;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.IXCongTacVanTai
{
    public partial class DuTinhKhoiLuongVanChuyen : UserControl
    {
        public DuTinhKhoiLuongVanChuyen()
        {
            InitializeComponent();
            this.Load += DuTinhKhoiLuongVanChuyen_Load;
        }

        private void DuTinhKhoiLuongVanChuyen_Load(object sender, System.EventArgs e)
        {
            LoadDuTinhKhoiLuong();
        }

        private double GetDouble(string text)
        {
            if (double.TryParse(text, out double result))
                return result;

            return 0;
        }


        private void SaveDuTinhKhoiLuong()
        {
            using (var conn = new SQLiteConnection("Data Source=data.db"))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM du_tinh_khoi_luong WHERE UserId=@UserId";
                var checkCmd = new SQLiteCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                long count = (long)checkCmd.ExecuteScalar();

                string query;

                if (count == 0)
                {
                    query = @"INSERT INTO du_tinh_khoi_luong
                    (UserId,KhoiLuongToanTran,KhoiLuongGiaiDoanChuanBi,KhoiLuongGiaiDoanChienDau,
                    VCHCToanTran,VCHCChuanBi,VCHCChienDau,
                    VCKTToanTran,VCKTChuanBi,VCKTChienDau)
                    VALUES
                    (@UserId,@Tong,@ChuanBi,@ChienDau,
                    @VCHCTong,@VCHCChuanBi,@VCHCChienDau,
                    @VCKTTong,@VCKTChuanBi,@VCKTChienDau)";
                }
                else
                {
                    query = @"UPDATE du_tinh_khoi_luong SET
                    KhoiLuongToanTran=@Tong,
                    KhoiLuongGiaiDoanChuanBi=@ChuanBi,
                    KhoiLuongGiaiDoanChienDau=@ChienDau,
                    VCHCToanTran=@VCHCTong,
                    VCHCChuanBi=@VCHCChuanBi,
                    VCHCChienDau=@VCHCChienDau,
                    VCKTToanTran=@VCKTTong,
                    VCKTChuanBi=@VCKTChuanBi,
                    VCKTChienDau=@VCKTChienDau
                    WHERE UserId=@UserId";
                }

                var cmd = new SQLiteCommand(query, conn);

                cmd.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                cmd.Parameters.AddWithValue("@Tong", GetDouble(txtTong.Text));
                cmd.Parameters.AddWithValue("@ChuanBi", GetDouble(txtChuanBi.Text));
                cmd.Parameters.AddWithValue("@ChienDau", GetDouble(txtChienDau.Text));

                cmd.Parameters.AddWithValue("@VCHCTong", GetDouble(txtTongVCHC.Text));
                cmd.Parameters.AddWithValue("@VCHCChuanBi", GetDouble(txtChuanBiVCHC.Text));
                cmd.Parameters.AddWithValue("@VCHCChienDau", GetDouble(txtChienDauVCHC.Text));

                cmd.Parameters.AddWithValue("@VCKTTong", GetDouble(txtTongVCKT.Text));
                cmd.Parameters.AddWithValue("@VCKTChuanBi", GetDouble(txtChuanBiVCKT.Text));
                cmd.Parameters.AddWithValue("@VCKTChienDau", GetDouble(txtChienDauVCKT.Text));

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Đã lưu dữ liệu");
        }

        private void LoadDuTinhKhoiLuong()
        {
            using (var conn = new SQLiteConnection("Data Source=data.db"))
            {
                conn.Open();

                string query = "SELECT * FROM du_tinh_khoi_luong WHERE UserId=@UserId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTong.Text = reader["KhoiLuongToanTran"]?.ToString();
                            txtChuanBi.Text = reader["KhoiLuongGiaiDoanChuanBi"]?.ToString();
                            txtChienDau.Text = reader["KhoiLuongGiaiDoanChienDau"]?.ToString();

                            txtTongVCHC.Text = reader["VCHCToanTran"]?.ToString();
                            txtChuanBiVCHC.Text = reader["VCHCChuanBi"]?.ToString();
                            txtChienDauVCHC.Text = reader["VCHCChienDau"]?.ToString();

                            txtTongVCKT.Text = reader["VCKTToanTran"]?.ToString();
                            txtChuanBiVCKT.Text = reader["VCKTChuanBi"]?.ToString();
                            txtChienDauVCKT.Text = reader["VCKTChienDau"]?.ToString();
                        }
                    }
                }
            }
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, System.EventArgs e)
        {
            SaveDuTinhKhoiLuong();
            NavigationService.Navigate(new _3CanDoi());

        }

        private void btnHome_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            NavigationService.Back();
        }
    }
}