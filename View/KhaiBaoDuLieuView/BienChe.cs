using BoDoiApp.Resources;
using BoDoiApp.View.TinhHinhDonVi;
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
            string Bophan = comboBox1.SelectedItem?.ToString();
            NavigationService.Navigate(new ChuYeu(Bophan));
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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new ChuYeu("Tieu Doan"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new TinhHinhVc());

        }

        private void CreateTable()
        {
            
        }
    }
}
