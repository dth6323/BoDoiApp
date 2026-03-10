using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class Dan : UserControl
    {
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
            switch (Section)
            {
                case "Toàn d":
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
                    break;
                case "Hướng chủ yếu":
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
                    break;
                case "Hướng thứ yếu":
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
                    break;
                case "BP PNPS":
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
                    break;
                case "LL còn lại":
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
                    break;
            }
            LoadData(rows);
        }

        private void LoadData(Dictionary<int, int> rows)
        {
            string sql = $"SELECT ItemCode,Tr1_1V, SV_CS, TL_1CS FROM Dan WHERE UserId = '{Constants.CURRENT_USER_ID_VALUE}' AND Note = '{Section}'";
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SQLiteCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    int index = 0;
                    while (reader.Read())
                    {

                        var item = reader["ItemCode"]?.ToString() ?? "0";
                        var trl = reader["Tr1_1V"].ToString() ?? "0";
                        var svcs = reader["SV_CS"].ToString();

                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 0, item);
                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 2, trl);
                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 3, svcs);
                        index++;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn lưu hay không", "Info", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DanData.UpdateHangLoat(reoGridControl1, rows, Section);
            }
            NavigationService.Navigate(new NhuCauDan(Section));

        }
    }
}
