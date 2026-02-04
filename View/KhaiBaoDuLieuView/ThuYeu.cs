using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ThuYeu : UserControl
    {
        private static readonly string BaseDir =
    AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book1.xlsx");
        public ThuYeu()
        {
            InitializeComponent();
        }

        private void ThuYeu_Load(object sender, EventArgs e)
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("File Excel không tồn tại tại đường dẫn: " + EXCEL_PATH, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if(Properties.Settings.Default.Username != null)
                {
                    LoadDataWithUser();
                }
                reoGridControl1.Load(EXCEL_PATH);
                reoGridControl1.CurrentWorksheet = reoGridControl1.Worksheets[1];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn quay lại không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            NavigationService.Back();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsDataExists("thu yeu"))
            {

                ChuYeuData.UpdateHangLoat(reoGridControl1, "thu yeu");
            }
            else
            {
                ChuYeuData.ThemHangLoat(reoGridControl1, "thu yeu");
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
        private void LoadDataWithUser()
        {
            reoGridControl1.Load(EXCEL_PATH);

            string sql = $"SELECT * FROM trangkithuat WHERE User = @User AND option = @Option";
            try
            {
                using (var connection = new System.Data.SQLite.SQLiteConnection("Data Source=data2.db;Version=3;"))
                {
                    connection.Open();
                    using (var command = new System.Data.SQLite.SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@User", "123");
                        command.Parameters.AddWithValue("@Option", "chu yeu");
                        using (var reader = command.ExecuteReader())
                        {
                            var ws = reoGridControl1.CurrentWorksheet;
                            int startRow = 5;
                            int row = startRow;
                            while (reader.Read())
                            {
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
                                row++;
                                if (row > 18) break;
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

    }
}
