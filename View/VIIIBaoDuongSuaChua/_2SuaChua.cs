using BoDoiApp.DataLayer;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class _2SuaChua : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book2.xlsx");

        private ReoGridControl reoGridControl1;

        public _2SuaChua()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Load += _2SuaChua_Load;
        }

        private void _2SuaChua_Load(object sender, EventArgs e)
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
                Text = "PHẦN SỬA CHỮA",
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
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3
            };
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            root.Controls.Add(bottom, 0, 2);

            // Trở về
            Button btnBack = new Button { Text = "Trở về" };
            btnBack.Click += (s, ev) =>
            {
                NavigationService.Navigate(new _1BaoDuongSuaChua());
            };
            bottom.Controls.Add(btnBack, 0, 0);

            // Trang chủ
            Button btnHome = new Button { Text = "Trang chủ" };
            btnHome.Click += (s, ev) =>
            {
                NavigationService.Navigate(new Form1());
            };
            bottom.Controls.Add(btnHome, 1, 0);

            // Lưu
            Button btnSave = new Button { Text = "Lưu" };
            btnSave.Click += BtnSave_Click;
            bottom.Controls.Add(btnSave, 2, 0);
        }

        // =============================
        // LOAD EXCEL + LOAD DATA
        // =============================
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
                reoGridControl1.Worksheets["SuaChua"];

            var ws = reoGridControl1.CurrentWorksheet;

            // Khóa hàng 1-3
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < ws.ColumnCount; col++)
                    ws.Cells[row, col].IsReadOnly = true;
            }

            // Khóa cột A,B,C
            for (int col = 0; col <= 2; col++)
            {
                for (int row = 0; row < ws.RowCount; row++)
                    ws.Cells[row, col].IsReadOnly = true;
            }

            // Khóa cột E-I
            for (int col = 4; col <= 8; col++)
            {
                for (int row = 0; row < ws.RowCount; row++)
                    ws.Cells[row, col].IsReadOnly = true;
            }
            ws.HideColumns(9, ws.ColumnCount - 9);

            // Ẩn dòng 15 trở đi
            ws.HideRows(14, ws.RowCount - 14);

            // Ẩn sheet tab
            reoGridControl1.SheetTabVisible = false;
            // ===== Load dữ liệu DB =====
            SuaChuaData.LoadAll(reoGridControl1);
        }

        // =============================
        // SAVE BUTTON
        // =============================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SuaChuaData.SaveAll(reoGridControl1);
        }
    }
}