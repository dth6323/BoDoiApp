using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using BoDoiApp.View.VIIIBaoDuongSuaChua;
using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.VIIBaoDamQuanY
{
    public partial class _1BaoDamQuanY : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book3.xlsx");

        private ReoGridControl reoGridControl1;
        private void LoadTrangKiThuatToColumnC()
        {
            string userId = Properties.Settings.Default.Username;

            string sql = @"SELECT quan_so, option
                   FROM trangkithuat
                   WHERE ll = 'Tổng'
                   AND User = @UserId";

            var ws = reoGridControl1.CurrentWorksheet;

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string option = reader["option"].ToString();
                            object quanSo = reader["quan_so"];

                            // map DB -> Excel
                            if (option == "Tieu Doan")
                                option = "Toàn Trận";

                            for (int row = 0; row < ws.RowCount; row++)
                            {
                                object cellValue = ws.GetCellData(row, 1); // cột B

                                if (cellValue != null &&
                                    cellValue.ToString().Trim() == option.Trim())
                                {
                                    ws.SetCellData(row, 2, quanSo); // cột C
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public _1BaoDamQuanY()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _1BaoDamQuanY_Load;
        }

        private void _1BaoDamQuanY_Load(object sender, EventArgs e)
        {
            Controls.Clear();
            AutoScaleMode = AutoScaleMode.None;

            // ===== ROOT =====
            TableLayoutPanel root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            Controls.Add(root);

            // ===== TITLE =====
            root.Controls.Add(new Label
            {
                Text = "BẢO ĐẢM QUÂN Y",
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.FromArgb(255, 242, 204),
                Font = new System.Drawing.Font("Times New Roman", 13, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            }, 0, 0);

            // ===== MAIN =====
            Panel main = new Panel { Dock = DockStyle.Fill };
            root.Controls.Add(main, 0, 1);

            // ===== REOGRID =====
            reoGridControl1 = new ReoGridControl
            {
                Dock = DockStyle.Fill
            };
            main.Controls.Add(reoGridControl1);

            LoadExcelAndData();

            // ===== BOTTOM =====
            // ===== BOTTOM PANEL =====
            // ===== BOTTOM =====
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1
            };

            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));

            root.Controls.Add(bottom, 0, 2);

            // ===== STYLE CHUNG =====
            Font btnFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            // ===== NÚT TRỞ VỀ =====
            Button btnBack = new Button
            {
                Text = "Trở về",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White
            };
            btnBack.FlatAppearance.BorderSize = 0;

            btnBack.Click += (s, ev) =>
            {
                NavigationService.Back();
            };

            bottom.Controls.Add(btnBack, 0, 0);

            // ===== NÚT TRANG CHỦ =====
            Button btnHome = new Button
            {
                Text = "Trang chủ",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont,
                BackColor = Color.FromArgb(13, 110, 253),
                ForeColor = Color.White
            };
            btnHome.FlatAppearance.BorderSize = 0;

            btnHome.Click += (s, ev) =>
            {
                NavigationService.Navigate(() => new Form1());
            };

            bottom.Controls.Add(btnHome, 1, 0);

            // ===== NÚT LƯU =====
            Button btnSave = new Button
            {
                Text = "Tiếp",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = btnFont,
                BackColor = Color.FromArgb(25, 135, 84),
                ForeColor = Color.White
            };
            btnSave.FlatAppearance.BorderSize = 0;

            btnSave.Click += (s, ev) =>
            {
                BaoDamQuanYData.SaveAll(reoGridControl1);
                NavigationService.Navigate(() => new Form1());
            };
            bottom.Controls.Add(btnSave, 2, 0);


            // ===== PANEL PHẢI (CHO NÚT TIẾP) =====


            LoadTrangKiThuatToColumnC();
        }
        private void LoadExcelAndData()
        {
            if (!File.Exists(EXCEL_PATH))
            {
                MessageBox.Show("Không tìm thấy file Excel");
                return;
            }

            reoGridControl1.Load(EXCEL_PATH);

            // ===== Chọn sheet sửa chữa =====
            reoGridControl1.CurrentWorksheet =
                reoGridControl1.Worksheets["BaoDamQuanY"];

            var ws = reoGridControl1.CurrentWorksheet;

            // ===== 1. Khóa toàn bộ sheet =====
            for (int row = 0; row < ws.RowCount; row++)
            {
                for (int col = 0; col < ws.ColumnCount; col++)
                {
                    ws.Cells[row, col].IsReadOnly = true;
                }
            }

            // ===== 2. Mở khóa D5-D12 =====
            for (int row = 4; row <= 11; row++) // 5 → 12 (index bắt đầu từ 0)
            {
                ws.Cells[row, 3].IsReadOnly = false; // Cột D (index 3)
            }

            // ===== 3. Mở khóa F5-F12 =====
            for (int row = 4; row <= 11; row++)
            {
                ws.Cells[row, 5].IsReadOnly = false; // Cột F (index 5)
            }

            // ===== 4. Mở khóa H5-H12 =====
            for (int row = 4; row <= 11; row++)
            {
                ws.Cells[row, 7].IsReadOnly = false; // Cột H (index 7)
            }

            // Ẩn sheet tab
            reoGridControl1.SheetTabVisible = false;
            ws.HideColumns(10, ws.ColumnCount - 10);

            // Ẩn dòng 15 trở đi
            ws.HideRows(10, ws.RowCount - 10);
            // Load dữ liệu DB
            BaoDamQuanYData.LoadAll(reoGridControl1);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            BaoDamQuanYData.SaveAll(reoGridControl1);
        }
    }
}