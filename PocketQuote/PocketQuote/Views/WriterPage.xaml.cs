using PocketQuote.ViewModels;
using Xamarin.Forms;

namespace PocketQuote.Views
{
    //Файл отделенного кода для WriterPage.xaml. Устанавливаем контекст для формы с данными об одном авторе.
    public partial class WriterPage : ContentPage
    {
        public WriterViewModel ViewModel { get; private set; }
        public WriterPage(WriterViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }

        #region Использовалось ранее
        /*
        //Обработка нажатия кнопки "Добавить"
        private void SaveWriter(object sender, EventArgs e)
        {
            var writer = (Writer)BindingContext;
            if (!String.IsNullOrEmpty(writer.Name))
            {
                App.Database.SaveItem(writer);
            }
            this.Navigation.PopAsync();
        }

        //Обработка нажатия кнопки "Удалить"
        private void DeleteWriter(object sender, EventArgs e)
        {
            var writer = (Writer)BindingContext;
            App.Database.DeleteItem(writer.Id);
            this.Navigation.PopAsync();
        }

        //Обработка нажатия кнопки "Отмена"
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
        */
        #endregion
    }
}
