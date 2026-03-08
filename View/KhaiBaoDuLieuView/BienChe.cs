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

            _isLoaded = false; // Explicitly false before adding items

            this.comboBox1.Items.AddRange(new object[] {
        "Hướng Chủ yếu",
        "Hướng Thứ Yếu",
        "Phòng ngự phía sau",
        "LL còn lại"
    });

            _isLoaded = true; // Only true AFTER items are loaded
        }


        private bool _isLoaded = false;

        private void BienChe_Load(object sender, EventArgs e)
        {
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded)
                return;
            if (comboBox1.SelectedItem == null)
                return;

            string Bophan = comboBox1.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(Bophan))
                return;

            try
            {
                NavigationService.Navigate(() => new ChuYeu(Bophan));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Navigation error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new ChuYeu("Tieu Doan"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new TinhHinhVc());

        }

        private void CreateTable()
        {
            
        }
    }
}
