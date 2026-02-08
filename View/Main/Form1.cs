using BoDoiApp.View;
using BoDoiApp.View.KhaiBaoDuLieuView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp
{
    public partial class Form1 : UserControl
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           NavigationService.Navigate(new KhaiBaoDuLieu());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CenterButton();
        }

        //private void CenterButton()
        //{
        //    // Assuming the FlowLayoutPanel is named 'flowLayoutPanel1'
        //    if (flowLayoutPanel1 != null)
        //    {
        //        flowLayoutPanel1.Left = (this.ClientSize.Width - flowLayoutPanel1.Width) / 2;
        //        flowLayoutPanel1.Top = (this.ClientSize.Height - flowLayoutPanel1.Height) / 2;
        //    }
        //}

        private void Form1_Resize(object sender, EventArgs e)
        {
            //CenterButton();
        }

        private void btn_dkkhbdhckt_Click(object sender, EventArgs e)
        {

        }
    }
}
