using System;
using Xamarin.Forms;
 
namespace PocketQuote
{
    public partial class WriterPage : ContentPage
    {
        public WriterPage()
        {
            InitializeComponent();
        }

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
    }
}
