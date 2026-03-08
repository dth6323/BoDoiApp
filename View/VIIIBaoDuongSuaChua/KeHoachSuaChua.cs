using BoDoiApp.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class KeHoachSuaChua : UserControl
    {
        private static readonly string BaseDir =
            AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "Book2.xlsx");

        private ReoGridControl reoGridControl1;
        public KeHoachSuaChua()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void KeHoachSuaChua_Load(object sender, EventArgs e)
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
            // ===== BOTTOM PANEL =====
            TableLayoutPanel bottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };

            // Chia 4 cột đều nhau
            for (int i = 0; i < 4; i++)
            {
                bottom.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25));
            }

            root.Controls.Add(bottom, 0, 2);


            // ===== NÚT TRỞ VỀ =====
            Button btnBack = new Button
            {
                Text = "Trở về",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            btnBack.Click += (s, ev) =>
            {
                NavigationService.Back();
            };

            bottom.Controls.Add(btnBack, 0, 0);


            // ===== NÚT TRANG CHỦ =====
            Button btnHome = new Button
            {
                Text = "Trang chủ",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            btnHome.Click += (s, ev) =>
            {
                NavigationService.Navigate(() => new Form1());
            };

            bottom.Controls.Add(btnHome, 1, 0);


            // ===== NÚT LƯU =====
            Button btnSave = new Button
            {
                Text = "Lưu",
                Anchor = AnchorStyles.None,
                AutoSize = true
            };

            btnSave.Click += BtnSave_Click;

            bottom.Controls.Add(btnSave, 2, 0);


            // ===== PANEL PHẢI (CHO NÚT TIẾP) =====


            // ===== NÚT TIẾP =====
            Button btnTiep = new Button();
            btnTiep.Text = "Tiếp";
            btnTiep.Anchor =  AnchorStyles.None;
            btnTiep.AutoSize=true;

            btnTiep.Click += (s, e2) =>
            {
                NavigationService.Navigate(() => new _3CanDoiVaYdinhBaoDam());
            };

            bottom.Controls.Add(btnTiep);
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
                reoGridControl1.Worksheets["KeHoachSuaChua"];

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
            ws.HideColumns(17, ws.ColumnCount - 17);

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
