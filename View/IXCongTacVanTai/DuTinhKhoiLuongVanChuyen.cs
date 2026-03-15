using BoDoiApp.Resources;
using System;
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
            LoadCalculatedValues();
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


        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, System.EventArgs e)
        {
            SaveDuTinhKhoiLuong();
            NavigationService.Navigate(() => new _3CanDoi());

        }

        private void btnHome_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void btnBack_Click(object sender, System.EventArgs e)
        {
            NavigationService.Back();
        }


        private double GetValue(SQLiteConnection conn, int row, int col)
        {
            string sql = @"SELECT Value 
                   FROM VCHCVTKT
                   WHERE Row = @Row 
                   AND Col = @Col 
                   AND UserId = @UserId
                   LIMIT 1";

            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Row", row);
                cmd.Parameters.AddWithValue("@Col", col);
                cmd.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                var result = cmd.ExecuteScalar();

                if (result != null && double.TryParse(result.ToString(), out double value))
                    return value;
            }

            return 0;
        }
        private void LoadCalculatedValues()
        {
            using (var conn = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();

                // ===== MID (VCHC) =====
                double midTong =
                    GetValue(conn, 7, 25);

                double midChuanBi =
                    GetValue(conn, 8, 19) +
                    GetValue(conn, 8, 20) +
                    GetValue(conn, 8, 21) +
                    GetValue(conn, 8, 22);

                double midChienDau =
                    GetValue(conn, 9, 23) +
                    GetValue(conn, 9, 24);

                // ===== RIGHT (VCKT) =====
                double rightTong = 0;
                double rightChuanBi = 0;
                double rightChienDau = 0;

                string sql = @"SELECT
        th_no_sung_tl + kh_truoc_no_sung_kho_tl + kh_truoc_no_sung_dv_tl as a,
        kh_truoc_no_sung_kho_tl + kh_truoc_no_sung_dv_tl as b,
        th_no_sung_tl as c
        FROM dan_report
        WHERE tt = 1 AND userId = @UserId";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", Constants.CURRENT_USER_ID_VALUE);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rightTong = Convert.ToDouble(reader["a"]);
                            rightChuanBi = Convert.ToDouble(reader["b"]);
                            rightChienDau = Convert.ToDouble(reader["c"]);
                        }
                    }
                }

                // ===== MID =====
                txtTongVCHC.Text = midTong.ToString();
                txtChuanBiVCHC.Text = midChuanBi.ToString();
                txtChienDauVCHC.Text = midChienDau.ToString();

                // ===== RIGHT =====
                txtTongVCKT.Text = rightTong.ToString();
                txtChuanBiVCKT.Text = rightChuanBi.ToString();
                txtChienDauVCKT.Text = rightChienDau.ToString();

                // ===== LEFT = MID + RIGHT =====
                txtTong.Text = (midTong + rightTong).ToString();
                txtChuanBi.Text = (midChuanBi + rightChuanBi).ToString();
                txtChienDau.Text = (midChienDau + rightChienDau).ToString();
            }
        }


    }
}