using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class Dan : UserControl
    {
        private static readonly string BaseDir =
           AppDomain.CurrentDomain.BaseDirectory;

        private static readonly string EXCEL_PATH =
            Path.Combine(BaseDir, "Resources", "Sheet", "DANTEST.xlsx");
        private string Section = "";
        private Dictionary<int, int> rows = new Dictionary<int, int>();
        public Dan(string section)
        {
            Section = section;
            InitializeComponent();
            label4.Text = section;
        }

        private void Dan_Load(object sender, EventArgs e)
        {

            DanData.CreateDatabase();
            reoGridControl1.Load(EXCEL_PATH);

            // Access the worksheet first
            var qstbktWorksheet = reoGridControl1.Worksheets["QSTBKT"];
            reoGridControl1.CurrentWorksheet = qstbktWorksheet;
            VCHCVTKTDATA.LoadTrangKiThuat(reoGridControl1);


            // Switch to Dan worksheet
            var danWorksheet = reoGridControl1.Worksheets["Dan"];

            reoGridControl1.CurrentWorksheet = danWorksheet;
            reoGridControl1.CurrentWorksheet.HideColumns(0, 30);
            reoGridControl1.CurrentWorksheet.HideRows(0, 26);

            var ws = reoGridControl1.CurrentWorksheet;
            var ws1 = reoGridControl1.Worksheets["Dan"];
            reoGridControl1.SheetTabVisible = false;
            ws1.SetSettings(WorksheetSettings.View_ShowHeaders, false);
            ws1.ScaleFactor = 1.0f;
            LockCells(ws1);
            switch (Section)
            {
                case "Toàn d":
                    reoGridControl1.CurrentWorksheet.HideRows(41, 100);
                    rows = new Dictionary<int, int>() {
                         {0,28},
                         {1,30},
                         {2,31},
                         {3,32},
                         {4,33},
                         {5,35},
                         {6,36},
                         {7,37},
                         {8,38},
                         {9,39},
                         {10,40},
                    };

                    LoadData(rows, "Toàn d");
                    LoadData(rows, "Tiểu đoàn");
                    LoadData(rows, "Phối thuộc");
                    ws.HideColumns(47, ws.ColumnCount - 47);
                    ws.HideRows(41, ws.RowCount - 41);

                    break;
                case "Hướng chủ yếu":
                    reoGridControl1.CurrentWorksheet.HideRows(26, 15);
                    reoGridControl1.CurrentWorksheet.HideRows(56, 101);
                    rows = new Dictionary<int, int>() {
                             {0,43},
                             {1,45},
                             {2,46},
                             {3,47},
                             {4,48},
                             {5,50},
                             {6,51},
                             {7,52},
                             {8,53},
                             {9,54},
                             {10,55},
};
                    LoadData(rows, Section);

                    ws.HideColumns(35, ws.ColumnCount - 35);
                    ws.HideRows(56, ws.RowCount - 56);

                    break;
                case "Hướng thứ yếu":

                    reoGridControl1.CurrentWorksheet.HideRows(26, 30);
                    reoGridControl1.CurrentWorksheet.HideRows(71, 101);
                    rows = new Dictionary<int, int>() {
                         {0,58},
                         {1,60},
                         {2,61},
                         {3,62},
                         {4,63},
                         {5,64},
                         {6,66},
                         {7,67},
                         {8,68},
                         {9,69},
                         {10,70 },
                    };
                    LoadData(rows, Section);

                    ws.HideColumns(35, ws.ColumnCount - 35);
                    ws.HideRows(71, ws.RowCount - 71);
                    break;
                case "BP PNPS":
                    reoGridControl1.CurrentWorksheet.HideRows(26, 45);
                    reoGridControl1.CurrentWorksheet.HideRows(86, 101);
                    rows = new Dictionary<int, int>() {
                         {0,73},
                         {1,75},
                         {2,76},
                         {3,77},
                         {4,78},
                         {5,80},
                         {6,81},
                         {7,82},
                         {8,83},
                         {9,84},
                         {10,85 },
};
                    LoadData(rows, Section);
                    ws.HideColumns(35, ws.ColumnCount - 35);
                    ws.HideRows(86, ws.RowCount - 86);
                    break;
                case "LL còn lại":
                    reoGridControl1.CurrentWorksheet.HideRows(0, 86);

                    rows = new Dictionary<int, int>() {
                         {0,88},
                         {1,90},
                         {2,91},
                         {3,92},
                         {4,93},
                         {5,95},
                         {6,96},
                         {7,97},
                         {8,98},
                         {9,99},
                         {10,100},
};
                    LoadData(rows, Section);
                    ws.HideColumns(35, ws.ColumnCount - 35);
                    ws.HideRows(101, ws.RowCount - 101);
                    break;
            }

        }
        private void LockCells(Worksheet ws)
        {
            int[] rows =
            {
        26,27,29,34,
        41,42,44,49,
        56,57,59,64,
        71,72,74,79,
        86,87,89,94
    };

            int[] cols =
            {
        30,31,34,35,36,37,
        40,41,42,43,46
    };

            // lock toàn bộ các hàng
            foreach (var r in rows)
            {
                ws.Ranges[new RangePosition(r, 0, 1, ws.ColumnCount)].IsReadonly = true;
            }

            // lock toàn bộ các cột
            foreach (var c in cols)
            {
                ws.Ranges[new RangePosition(0, c, ws.RowCount, 1)].IsReadonly = true;
            }
        }

        private void LoadData(Dictionary<int, int> rows, string section)
        {
            string sql = $"SELECT ItemCode,Tr1_1V, SV_CS, TL_1CS FROM Dan WHERE UserId = '{Constants.CURRENT_USER_ID_VALUE}' AND Note = '{section}'";

            int[] skipCols = { 31, 34, 37, 40, 43, 46 };

            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    int index = 0;

                    if (section == "Tiểu đoàn")
                    {
                        while (reader.Read())
                        {
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();

                            if (!skipCols.Contains(35))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 36, item);

                            if (!skipCols.Contains(37))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 38, trl);

                            if (!skipCols.Contains(38))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 39, svcs);

                            index++;
                        }
                    }
                    else if (section == "Phối thuộc")
                    {
                        while (reader.Read())
                        {
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();

                            if (!skipCols.Contains(41))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 42, item);

                            if (!skipCols.Contains(43))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 44, trl);

                            if (!skipCols.Contains(44))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 45, svcs);

                            index++;
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            var item = reader["ItemCode"]?.ToString() ?? "0";
                            var trl = reader["Tr1_1V"].ToString() ?? "0";
                            var svcs = reader["SV_CS"].ToString();

                            if (!skipCols.Contains(30))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 30, item);

                            if (!skipCols.Contains(32))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 32, trl);

                            if (!skipCols.Contains(33))
                                reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 33, svcs);

                            index++;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn lưu hay không", "Info", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Section == "Toàn d")
                {
                    DanData.UpdateHangLoat(reoGridControl1, rows, "Toàn d");
                    DanData.UpdateHangLoat(reoGridControl1, rows, "Tiểu đoàn");
                    DanData.UpdateHangLoat(reoGridControl1, rows, "Phối thuộc");
                }
                else
                {
                    DanData.UpdateHangLoat(reoGridControl1, rows, Section);
                }
            }
            reoGridControl1.Dispose();
            NavigationService.Navigate(() => new NhuCauDan(Section));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

    }
}
