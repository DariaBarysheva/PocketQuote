using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PocketQuote.Views;

namespace PocketQuote
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> ChooseList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            ChooseList = new ObservableCollection<string>() { "Авторы","Книги","Темы цитат", "Цитаты"};
            this.BindingContext = this;
        }

        public string curSelectedItem;
        public string CurSelectedItem {
            get { return curSelectedItem; }
            set
            {
                if (value != null)
                {
                    curSelectedItem = null;
                    OnPropertyChanged("CurSelectedItem");

                    switch (value)
                    {
                        case "Авторы":
                            Navigation.PushAsync(new WritersListPage());
                            break;
                        case "Книги":
                            Navigation.PushAsync(new BooksListPage(0, ""));
                            break;
                    }
                }
            }
        }

        /*public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectProc(sender, e);
        }
      
        private async void SelectProc(object sender, SelectedItemChangedEventArgs e)
        {

            switch (e.SelectedItem.ToString())
            {
                case "Авторы":
                    await Navigation.PushAsync(new WritersListPage());
                    break;
            }
            CurSelectedItem = null;
        }*/

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
