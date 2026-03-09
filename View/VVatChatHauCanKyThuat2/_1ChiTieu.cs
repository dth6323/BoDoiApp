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

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class _1ChiTieu : UserControl
    {
        public _1ChiTieu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.Back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new _2PhanCap());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(()=>new NhuCauDan("Toàn d"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new NhuCauDan("Hướng chủ yếu"));

        }

        private void button5_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new NhuCauDan("Hướng thứ yếu"));

        }

        private void button6_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new NhuCauDan("BP PNPS"));

        }

        private void button7_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(() => new NhuCauDan("LL còn lại"));

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
