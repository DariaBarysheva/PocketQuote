using Xamarin.Forms;
using PocketQuote.ViewModels;
using PocketQuote.Views;

namespace PocketQuote.Views
{
    public partial class BooksListPage : ContentPage
    {
        //Файл отделенного кода для BooksListPage.xaml. Установка контекста для формы со списком книг.
        public BooksListPage()
        {
            InitializeComponent();
            BindingContext = new BooksListViewModel() { Navigation = this.Navigation };
        }
    }
}
