using BoDoiApp.form;
using BoDoiApp.View.KhaiBaoDuLieuView;
using BoDoiApp.View.VIIBaoDamQuanY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            NavigationService.Init(this);
            NavigationService.Navigate(new ThuYeu());
        }

        public void ShowView(UserControl view)
        {
            panel1.Controls.Clear();
            view.Dock = DockStyle.Fill;
            panel1.Controls.Add(view);
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
