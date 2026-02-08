using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.IIIToChucSudung
{
    public partial class _1ToChucSudung : UserControl
    {
        public _1ToChucSudung()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new _2BoTri());
        }

       

        private void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS ToChucSuDungLucLuong (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    TT INTEGER NOT NULL,
    NoiDung TEXT NOT NULL,
    QS_SQ INTEGER DEFAULT 0,
    QS_QNCN INTEGER DEFAULT 0,
    QS_HSQ_BS INTEGER DEFAULT 0,
    QS_Plus INTEGER DEFAULT 0,
    VK_VuKhi INTEGER DEFAULT 0,
    VK_XeMay INTEGER DEFAULT 0,
    VK_TBKhac INTEGER DEFAULT 0,
    HC_KT_QS INTEGER DEFAULT 0,
    HC_KT_TB INTEGER DEFAULT 0,
    TangCuong_QS INTEGER DEFAULT 0,
    TangCuong_TB INTEGER DEFAULT 0
);";
             using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
             {
                 connection.Open();
                 var command = new SQLiteCommand(sql, connection);
                 command.ExecuteNonQuery();
            }
        }

        private void _1ToChucSudung_Load(object sender, EventArgs e)
        {
            CreateTable();
        }
    }
}
