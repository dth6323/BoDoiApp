using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class Dan : UserControl
    {
        private string Section = "dan";
        private Dictionary<int,int> rows = new Dictionary<int,int>();
        public Dan(string section)
        {
            Section = section;
            InitializeComponent();
            label4.Text = section;
        }

        private void Dan_Load(object sender, EventArgs e)
        {
            
            DanData.CreateDatabase();
            Dictionary<int, int> rows = new Dictionary<int, int>()
            {
                {0,1},
                {1,2},
                {2,3},
                {3,4},
                {4,5},
                {5,7},
                {6,8},
                {7,9},
                {8,10},
                {9,12},
                {10,14},
            };
            LoadData(rows);
        }

        private void LoadData(Dictionary<int,int> rows)
        {
            string sql = $"SELECT ItemCode,Tr1_1V, SV_CS, TL_1CS FROM Dan WHERE UserId = '{Constants.CURRENT_USER_ID_VALUE}' AND Note = '{Section}'";
            using(var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                connection.Open();
                using(var command = new SQLiteCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    int index = 0;
                    while (reader.Read())
                    {

                        var item = reader["ItemCode"]?.ToString() ?? "0";
                        var trl = reader["Tr1_1V"].ToString() ?? "0";
                        var svcs = reader["SV_CS"].ToString();

                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index],0, item);
                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 2, trl);
                        reoGridControl1.CurrentWorksheet.SetCellData(rows[index], 3, svcs);
                        index++;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Bạn có muốn lưu hay không","Info",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DanData.UpdateHangLoat(reoGridControl1, rows, Section);
            }

        }
    }
}
