
using System.Windows.Forms;

namespace BoDoiApp.View
{
    public interface IFormNavigator
    {
        void NavigateTo(Form nextForm);
        void GoBack();
    }
}
