using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class KhaiBaoDuLieu : UserControl
    {
        public KhaiBaoDuLieu()
        {
            InitializeComponent();
        }

        private void KhaiBaoDuLieu_Load(object sender, EventArgs e)
        {

        }

        private void btn_tttb_Click(object sender, EventArgs e)
        {
            FormMana.ThongTinTapBai.Show();
            this.Hide();
        }

        private void btn_qstgcd_Click(object sender, EventArgs e)
        {
            FormMana.QuanSoChienDau.Show();
            this.Hide();
        }

        private void btn_vchc_Click(object sender, EventArgs e)
        {
            FormMana.VatChatHienCo.Show();
            this.Hide();
        }

        private void btn_qddtttbsvc_Click(object sender, EventArgs e)
        {
            FormMana.QuyDinhDuTruTieuThuBoSung.Show();
            this.Hide();
        }
    }
}
