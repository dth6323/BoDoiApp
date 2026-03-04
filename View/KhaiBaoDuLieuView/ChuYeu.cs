using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using unvell.ReoGrid.Outline;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ChuYeu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "QS.xlsx");
        private string BoPhan = "chu yeu";
        private int EndRow = 0;
        public ChuYeu(string boPhan)
        {
            BoPhan = boPhan;
            InitializeComponent();
        }

        private void ChuYeu_Load(object sender, EventArgs e)
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("File Excel không tồn tại tại đường dẫn: " + EXCEL_PATH, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                reoGridControl1.Load(EXCEL_PATH);
                switch (BoPhan)
                {
                    case "Hướng Chủ Yếu":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[1];
                        EndRow = 16;
                        break;
                    case "Hướng Thứ Yếu":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[2];
                        EndRow = 11;
                        break;
                    case "Dự bị cơ động":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[3];
                        EndRow = 9;
                        break;
                    case "Tieu Doan":
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[0];
                        EndRow = 18;
                        break;
                    case "LL còn lại":
                        EndRow = 18;
                        reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[4];
                        LoadForOther();
                        break;
                    default: break;
                }
                if (IsDataExists(BoPhan) && BoPhan != "LL còn lại")
                {
                    LoadDataWithUser();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsDataExists(string boPhan)
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM trangkithuat WHERE User = @User AND option = @Option";
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User", Properties.Settings.Default.Username);
                    command.Parameters.AddWithValue("@Option", boPhan);
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn lưu dữ liệu không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (IsDataExists(BoPhan))
            {
                ChuYeuData.UpdateHangLoat(reoGridControl1, BoPhan, EndRow);
            }
            else
            {
                ChuYeuData.ThemHangLoat(reoGridControl1, BoPhan, EndRow);
            }
        }

        private void LoadDataWithUser()
        {
            reoGridControl1.Load(EXCEL_PATH);

            string sql = $"SELECT * FROM trangkithuat WHERE User = @User AND option = @Option";
            try
            {
                using (var connection = new System.Data.SQLite.SQLiteConnection("Data Source=data.db;Version=3;"))
                {
                    connection.Open();
                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                        command.Parameters.AddWithValue("@Option", BoPhan);
                        using (var reader = command.ExecuteReader())
                        {
                            var ws = reoGridControl1.CurrentWorksheet;
                            int startRow = 5;
                            int row = startRow;
                            while (reader.Read())
                            {
                                ws.SetCellData(row, 0, reader["ll"]?.ToString());
                                ws.SetCellData(row, 1, reader["quan_so"]?.ToString());
                                ws.SetCellData(row, 2, reader["sn"]?.ToString());
                                ws.SetCellData(row, 3, reader["tl"]?.ToString());
                                ws.SetCellData(row, 4, reader["trl"]?.ToString());
                                ws.SetCellData(row, 5, reader["dl"]?.ToString());
                                ws.SetCellData(row, 6, reader["b41_m79"]?.ToString());
                                ws.SetCellData(row, 7, reader["luu_dan"]?.ToString());
                                ws.SetCellData(row, 8, reader["coi_60"]?.ToString());
                                ws.SetCellData(row, 9, reader["coi_82"]?.ToString());
                                ws.SetCellData(row, 10, reader["coi_100"]?.ToString());
                                ws.SetCellData(row, 11, reader["pct_spg9"]?.ToString());
                                ws.SetCellData(row, 12, reader["phao_pk_127"]?.ToString());
                                ws.SetCellData(row, 13, reader["ons"]?.ToString());
                                ws.SetCellData(row, 14, reader["db"]?.ToString());
                                row++;
                            }
                        }
                    }
                }
                reoGridControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }
        private void LoadForOther()
        {
            var tieuDoanData = ArrayData(BoPhan, "dBB");
            var chuYeuData = ArrayData(BoPhan, "Tổng");
            var thuYeuData = ArrayData(BoPhan, "Tổng");
            var duBiData = ArrayData(BoPhan, "Tổng");
            var llConLai = new int[15];
            for(int i = 0; i < 15; i++)
            {
                llConLai[i] = tieuDoanData[i] - chuYeuData[i] - thuYeuData[i] - duBiData[i];
            }
            string sql = $"SELECT * FROM trangkithuat WHERE User = '{Constants.CURRENT_USER_ID_VALUE}' AND option = 'LL còn lại'";
            bool flag = false;
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using(var command = new SQLiteCommand(sql, connection))
                {
                    flag = command.ExecuteScalar() != null;

                }
            }
            if (!flag)
            {
                for(int i =0; i < 15; i++)
                {
                    reoGridControl1.CurrentWorksheet.SetCellData(6, i, llConLai[i]);
                }
            }else
            {
                LoadDataWithUser();
            }
            

        }

        private int[] ArrayData(string option, string columnName)
        {
            string sql =
                "SELECT ll, quan_so, sn, tl, trl, dl, b41_m79, luu_dan, coi_60, coi_82, coi_100, pct_spg9, phao_pk_127, ons, db " +
                "FROM trangkithuat " +
                "WHERE User = @User AND option = @Option AND ll = @ColumnName";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User", Constants.CURRENT_USER_ID_VALUE);
                    command.Parameters.AddWithValue("@Option", option);
                    command.Parameters.AddWithValue("@ColumnName", columnName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int[] data = new int[15];
                            for (int i = 0; i < 15; i++)
                            {
                                data[i] = reader.IsDBNull(i) ? 0 : Convert.ToInt32(reader.GetValue(i));
                            }
                            return data;
                        }
                    }
                }
                return new int[15];
            }
        }
    }
}
