using PocketQuote.ViewModels;
using Xamarin.Forms;



namespace PocketQuote.Views
{
    //Файл отделенного кода для BookPage.xaml. Устанавливаем контекст для формы с данными об одной книге.
    public partial class BookPage : ContentPage
    {
        public BookViewModel ViewModel { get; private set; }
        public BookPage(BookViewModel bm)
        {
            InitializeComponent();
            ViewModel = bm;
            this.BindingContext = ViewModel;
        }    
    }
}
