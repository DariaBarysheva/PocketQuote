using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using PocketQuote.Views;
using PocketQuote.Models;


namespace PocketQuote.ViewModels
{
    //Класс, описывающий логику работу с таблицей "Книга" (Books) в БД

    public class BooksListViewModel : INotifyPropertyChanged
    {
        //Соединение с БД
        private SQLiteConnection databaseConnection;

        //Список авторов
        public ObservableCollection<BookViewModel> Books { get; set; }

        //Выбранная книга
        private BookViewModel selectedBook;
        public BookViewModel SelectedBook
        {
            get { return selectedBook; }
            set
            {
                if (value != null) //нужна проверка - чтобы не выйти на ошибку, когда список пуст
                {
                    BookViewModel tempBook = new BookViewModel() { Book = new Book() { Id = value.Book.Id, Name = value.Book.Name, Writer_id = value.Book.Writer_id, Writer_name = value.Book.Writer_name }, ListViewModel = this };
                    selectedBook = null; //чтобы элемент в исходном списке можно было сразу снова выбрать без перехода на другой элемент
                    OnPropertyChanged("SelectedBook");

                    //открываем страницу с данными об одном авторе
                    Navigation.PushAsync(new BookPage(tempBook));
                }
            }
        }

        //Команды для взаимодействия с представлением View
        public ICommand CreateBookCommand { protected set; get; }
        public ICommand DeleteBookCommand { protected set; get; }
        public ICommand SaveBookCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        //Объект навигации, необходимый для команды BackCommand (передается при исходном создании 
        //объекта BooksListViewModel из BooksListPage)
        public INavigation Navigation { get; set; }

        public BooksListViewModel()
        {
            //Устанавливаем соединение с БД - в звисимости от платформы путь будет отличаться, поэтому используем DependencyService
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(App.DATABASE_NAME);
            databaseConnection = new SQLiteConnection(databasePath);
            databaseConnection.CreateTable<Writer>();
            databaseConnection.CreateTable<Book>();

            //Загружаем содержимое списка книг на форме
            UpdateBooks();

            CreateBookCommand = new Command(CreateBook);
            DeleteBookCommand = new Command(DeleteBook);
            SaveBookCommand = new Command(SaveBook);
            BackCommand = new Command(Back);
        }

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //Обновление списка книг на форме - грузим таблицу с БД, упорядочиваем книг по имени автора и названию
        private void UpdateBooks()
        {
            Books = new ObservableCollection<BookViewModel>();

            /* Сейчас JOIN в LINQ для SqlLite не поддерживается
             * var result = (from b in databaseConnection.Table<Book>()
                          join w in databaseConnection.Table<Writer>() on b.Writer_id equals w.Id
                          select new { Id = b.Id, Name = b.Name, Writer_id = b.Writer_id, Writer_name = w.Name}).OrderBy(el => el.Writer_name).ThenBy(el => el.Name);
                          */
            foreach (Book b in databaseConnection.Query<Book>("SELECT Books.Id, Books.Name, Books.Writer_id, Writers.Name AS 'Writer_name' FROM Books INNER JOIN Writers ON Writers.id = Books.Writer_id"))
            {
                BookViewModel temp = new BookViewModel() { Book = b, ListViewModel = this };
                Books.Add(temp);
            }
        }

        //Добавление книги после нажатия кнопки "Добавить" на форме BooksListPage
        private void CreateBook()
        {
            Navigation.PushAsync(new BookPage(new BookViewModel() { ListViewModel = this }));
        }

        //Сохранение изменений в информации о книге - после выбора книги в списке на форме BooksListPage, 
        //внесения изменений и нажатия "Сохранить" на форме BookPage
        private void SaveBook(object bookObject)
        {
            BookViewModel book = bookObject as BookViewModel;
            if (book != null && book.IsValid)
            {
                if (book.Book.Id != 0)
                {
                    if (databaseConnection.Update(book.Book) == 1) //Если обновили в БД - обновляем элемент в списке
                    {
                        BookViewModel updatedBook = Books.FirstOrDefault(b => b.Book.Id == book.Book.Id);                        
                        if (updatedBook != null)
                        {
                            updatedBook.Name = book.Name;
                            SortBooks();
                        }
                    }
                }
                else
                {
                    if (databaseConnection.Insert(book.Book) == 1) //Если добавили в БД - добавляем элемент в список
                    {
                        Books.Add(book);
                        SortBooks();
                    }
                }
            }
            Back(); //Возврат на исходную страницу           
        }

        //Удаление книги - после выбора книги в списке на форме BooksListPage, 
        //и нажатия "Удалить" на форме BookPage
        private void DeleteBook(object bookObject)
        {
            BookViewModel book = bookObject as BookViewModel;
            if (book != null)
            {
                if (databaseConnection.Delete<Book>(book.Book.Id) == 1) //Если удалили в БД - удаляем в списке
                {
                    BookViewModel deletedBook = Books.FirstOrDefault(b => b.Book.Id == book.Book.Id);
                    if (deletedBook != null)
                    {
                        Books.Remove(deletedBook);
                    }
                }
            }
            Back(); //Возврат на исходную страницу            
        }

        //сортировка списка книг по ФИО автора и названию книги (после добавления и изменения отдельных записей)
        private void SortBooks()
        {
            Books = new ObservableCollection<BookViewModel>(Books.OrderBy(b => b.Writer_name).ThenBy(b => b.Name));
            OnPropertyChanged("Books");
        }

        //Возврат на предыдущую страницу
        private void Back()
        {
            Navigation.PopAsync();
        }
    }
}

