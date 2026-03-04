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
    public partial class Dan : UserControl
    {
        public Dan(string section)
        {
            InitializeComponent();
            label4.Text = section;
        }

        private void Dan_Load(object sender, EventArgs e)
        {

        }

    }
}
