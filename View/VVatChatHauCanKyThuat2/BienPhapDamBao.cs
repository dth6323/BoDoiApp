using BoDoiApp.DataLayer;
using BoDoiApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.VVatChatHauCanKyThuat2
{
    public partial class BienPhapDamBao : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();
        public BienPhapDamBao()
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

        private void BienPhapDamBao_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapDamBao");
            if (content == string.Empty)
            {
                return;
            }
            richTextBox1.Text = content;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataLayer.SaveOrUpdate(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "BienPhapDamBao");

        }
    }
}
