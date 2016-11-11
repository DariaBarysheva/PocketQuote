using Xamarin.Forms;
using PocketQuote.ViewModels;

namespace PocketQuote.Views
{
    public partial class WritersListPage : ContentPage
    {
        //Файл отделенного кода для WritersListPage.xaml. Установка контекста для формы со списком авторов.
        public WritersListPage()
        {
            InitializeComponent();
            BindingContext = new WritersListViewModel() { Navigation = this.Navigation };
        }
    }
}
