using BoDoiApp.DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class ChuYeu : UserControl
    {
        private const string EXCEL_PATH = @"D:\document\Thaiha\BoDoiApp\Resources\Sheet\Book1.xlsx";
        private string BoPhan = "chu yeu";
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
                if(Properties.Settings.Default.Username == "123")
                {
                    LoadDataWithUser();
                }else
                {
                    reoGridControl1.Load(EXCEL_PATH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChuYeuData.ThemHangLoat(reoGridControl1, BoPhan);
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
                                if (row > 16) break;
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
