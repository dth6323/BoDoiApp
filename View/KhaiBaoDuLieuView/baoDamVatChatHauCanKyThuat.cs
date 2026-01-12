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
    public partial class baoDamVatChatHauCanKyThuat : Form
    {
        public baoDamVatChatHauCanKyThuat()
        {
            InitializeComponent();
        }

        private void baoDamVatChatHauCanKyThuat_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("Đạn", "Tấn");
            dataGridView1.Rows.Add("Quân nhu", "Tấn");
            dataGridView1.Rows.Add("Quân y", "Tấn");
            dataGridView1.Rows.Add("Doanh trại", "Tấn");
            dataGridView1.Rows.Add("VTKT", "Tấn");
        }
    }
}
