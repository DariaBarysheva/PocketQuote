using System.ComponentModel;
using PocketQuote.Models;

namespace PocketQuote.ViewModels
{
    //Описывает логику работы с данными об одной книге
    public class BookViewModel : INotifyPropertyChanged
    {
        //Ссылка на список всех книг. Передается классом BooksListViewModel при создании объекта текущего класса.
        private BooksListViewModel lvm;
        public BooksListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                lvm = value;
                OnPropertyChanged("ListViewModel");
            }
        }

        //Ссылка на объект модели "Книга" (Book). 
        public Book Book { get; set; }

        //Название книги - соответствует имени модели "Книга"
        public string Name
        {
            get { return Book.Name; }
            set
            {
                Book.Name = value;
                OnPropertyChanged("Name");
            }
        }

        //Идентификатор автора
        public int Writer_id
        {
            get { return Book.Writer_id; }
            set
            {
                Book.Writer_id = value;
                OnPropertyChanged("Writer_id");
            }
        }

        //ФИО автора
        public string Writer_name
        {
            get { return Book.Writer_name; }
            set
            {
                Book.Writer_name = value;
                OnPropertyChanged("Writer_name");
            }
        }

        //Проверка того, что имя введено
        public bool IsValid
        {
            get
            {
                return (!(Name == null || string.IsNullOrEmpty(Name.Trim())));
            }
        }

        public BookViewModel()
        {
             Book = new Book();
        }

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

