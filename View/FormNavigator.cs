using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View
{
    public class FormNavigator : IFormNavigator
    {
        private readonly Stack<Form> _history = new Stack<Form>();

        public void NavigateTo(Form nextForm)
        {
            _history.Push(nextForm);
            if (_history.Count > 0)
            {
                Console.WriteLine("HISTORY COUNT: " + _history.Count);
                var current = _history.Peek();
                current.Hide();
            }


            nextForm.FormClosed += (s, e) => GoBack();
            nextForm.Show();
        }

        public void GoBack()
        {
            if (_history.Count <= 1)
                return;

            var current = _history.Pop();
            current.FormClosed -= (s, e) => GoBack();
            current.Dispose();

            var previous = _history.Peek();
            previous.Show();
        }
    }
}