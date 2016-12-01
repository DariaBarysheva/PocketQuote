using System;
using PocketQuote.ViewModels;
using Xamarin.Forms;
using PocketQuote.Models;


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
            foreach (Writer w in ViewModel.WritersList)
            {
                wPicker.Items.Add(w.Name);
            }
            if(ViewModel.Writer_name != null)
            {
                wPicker.SelectedIndex = wPicker.Items.IndexOf(ViewModel.Writer_name);
            }
        }

        public void wPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewModel.Writer_id = ViewModel.WritersList[wPicker.SelectedIndex].Id;
            ViewModel.Writer_name = ViewModel.WritersList[wPicker.SelectedIndex].Name;
        }
    }
}
