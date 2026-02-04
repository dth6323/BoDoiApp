using BoDoiApp.Resources;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BienChe : UserControl
    {

        public BienChe()
        {
            InitializeComponent();
        }

        private void BienChe_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Form1());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Bophan = comboBox1.SelectedItem?.ToString();
            NavigationService.Navigate( new ChuYeu(Bophan));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new ThuYeu());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void CreateTable()
        {
            using (var connection = new SQLiteConnection(Constants.CONNECTION_STRING))
            {
                string sql = "CREATE TABLE IF NOT EXISTS ToChucBienChe (\r\n    ID INTEGER PRIMARY KEY AUTOINCREMENT,\r\n    UserId TEXT NOT NULL,\r\n    vcId INTEGER NOT NULL,\r\n    QuyDinhDuTru REAL,\r\n    PhaiCo0400N REAL,\r\n    PhaiCSCD REAL,\r\n    TieuThuGDCB REAL,\r\n    TieuThuGDCD REAL\r\n);";
            }
            ;
        }
    }
}
