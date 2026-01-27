using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View
{
    public static class NavigationService
    {
        private static MainForm _mainForm;
        private static Stack<UserControl> _history = new Stack<UserControl>();

        public static void Init(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public static void Navigate(UserControl view)
        {
            if (_mainForm == null)
                throw new InvalidOperationException("NavigationService not initialized");

            if (_history.Count > 0)
                _history.Peek().Visible = false;

            _history.Push(view);
            _mainForm.ShowView(view);
        }

        public static void Back()
        {
            if (_history.Count <= 1)
                return;

            _history.Pop();
            _mainForm.ShowView(_history.Peek());
        }
    }
}
