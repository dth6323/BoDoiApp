using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoDoiApp.Resources
{
    public static class Constants
    {
        public const string CONNECTION_STRING = "Data Source=data2.db;Version=3;";
        // Removed 'const' because Properties.Settings.Default.Username is not a compile-time constant
        public static readonly string CURRENT_USER_ID_VALUE = Properties.Settings.Default.Username;
    }
}
