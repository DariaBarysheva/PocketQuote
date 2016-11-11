using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PocketQuote
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        #region Использовалось ранее
        /*
        //Первоначальная загрузка списка авторов при открытии страницы
        protected override void OnAppearing()
        {
            writersList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }

        //Обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Writer selectedWriter = (Writer)e.SelectedItem;
            WriterPage writerPage = new WriterPage();
            writerPage.BindingContext = selectedWriter;
            await Navigation.PushAsync(writerPage);
        }

        //Обработка нажатия кнопки добавления
        private async void CreateWriter(object sender, EventArgs e)
        {
            Writer newWriter = new Writer();
            WriterPage writerPage = new WriterPage();
            writerPage.BindingContext = newWriter;
            await Navigation.PushAsync(writerPage);
        }
        */
        #endregion
    }
}
