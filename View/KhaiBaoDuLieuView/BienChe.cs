using System;
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
    }
}
