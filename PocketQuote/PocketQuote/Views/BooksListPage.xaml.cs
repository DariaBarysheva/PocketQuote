using Xamarin.Forms;
using PocketQuote.ViewModels;
using PocketQuote.Views;

namespace PocketQuote.Views
{
    public partial class BooksListPage : ContentPage
    {
        //Файл отделенного кода для BooksListPage.xaml. Установка контекста для формы со списком книг.
        //Входной параметр: идентификатор автора, если нужны книги конкретного автора
        public BooksListPage(int writerId, string writerName)
        {
            InitializeComponent();
            BindingContext = new BooksListViewModel(writerId, writerName) { Navigation = this.Navigation };
        }
    }
}
