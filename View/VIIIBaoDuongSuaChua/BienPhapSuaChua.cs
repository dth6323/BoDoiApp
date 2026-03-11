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

namespace BoDoiApp.View.VIIIBaoDuongSuaChua
{
    public partial class BienPhapSuaChua : UserControl
    {
        private RichTextBoxData dataLayer = new RichTextBoxData();

        public BienPhapSuaChua()
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
            dataLayer.SaveOrUpdate(Constants.CURRENT_USER_ID_VALUE, richTextBox1.Text, "BienPhapSuaChua");

        }
        private void BienPhapSuaChua_Load(object sender, EventArgs e)
        {
            var content = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BienPhapSuaChua");

            if (content != string.Empty)
            {
                richTextBox1.Text = content;
                return;
            }

            // Nếu chưa có BienPhapSuaChua thì load 2 key cũ
            var yDinh = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BaoDuongSuaChua_3_YDinh");
            var canDoi = dataLayer.LoadDataFromDatabase(Constants.CURRENT_USER_ID_VALUE, "BaoDuongSuaChua_3_CanDoi");

            var merged = $"{yDinh}\n\n{canDoi}".Trim();

            if (!string.IsNullOrEmpty(merged))
            {
                richTextBox1.Text = merged;
            }
        }
    }
}
