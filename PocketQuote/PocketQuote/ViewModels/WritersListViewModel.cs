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
    //Класс, описывающий логику работу с таблицей "Автор" (Writers) в БД
    #region Использовалось ранее
    /*public class WriterRepository
    {
        SQLiteConnection database;

        //Конструктор - устанавливаем соединение и создаем таблицу "Автор"
        public WriterRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Writer>();
        }

        //Получаем текущий список авторов
        public IEnumerable<Writer> GetItems()
        {
            return (from i in database.Table<Writer>() select i).ToList();

        }

        //Получаем автора по уникальному идентификатору
        public Writer GetItem(int id)
        {
            return database.Get<Writer>(id);
        }

        //Удаляем автора по уникальному идентификатору
        public int DeleteItem(int id)
        {
            return database.Delete<Writer>(id);
        }

        //Обновляем информацию об авторе, если передан его id; в противном случае - создаем нового.
        //В обоих случаях возвращаем id обновленного/добавленного автора
        public int SaveItem(Writer item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }*/
    #endregion

    public class WritersListViewModel : INotifyPropertyChanged
    {
        //Соединение с БД
        private SQLiteConnection databaseConnection;

        //Список авторов
        public ObservableCollection<WriterViewModel> Writers { get; set; }

        //Выбранный автор
        private WriterViewModel selectedWriter;
        public WriterViewModel SelectedWriter
        {
            get { return selectedWriter; }
            set
            {
                if (value != null) //нужна проверка - чтобы не выйти на ошибку, когда список пуст
                {
                    //Writer tempWriter = new Writer() { Id = value.Id, Name = value.Name };
                    WriterViewModel tempWriter = new WriterViewModel() { Writer = new Writer() { Id = value.Writer.Id, Name = value.Writer.Name }, ListViewModel = this };
                    selectedWriter = null; //чтобы элемент в исходном списке можно было сразу снова выбрать без перехода на другой элемент
                    OnPropertyChanged("SelectedWriter");

                    //открываем страницу с данными об одном авторе
                    Navigation.PushAsync(new WriterPage(tempWriter /*new WriterViewModel(tempWriter) { ListViewModel = this })*/));
                }
            }
        }

        //Команды для взаимодействия с представлением View
        public ICommand CreateWriterCommand { protected set; get; }
        public ICommand DeleteWriterCommand { protected set; get; }
        public ICommand SaveWriterCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        //Объект навигации, необходимый для команды BackCommand (передается при исходном создании 
        //объекта WritersListViewModel из WritersListPage)
        public INavigation Navigation { get; set; }

        public WritersListViewModel()
        {
            //Устанавливаем соединение с БД - в звисимости от платформы путь будет отличаться, поэтому используем DependencyService
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(App.DATABASE_NAME);
            databaseConnection = new SQLiteConnection(databasePath);
            databaseConnection.CreateTable<Writer>();

            //Загружаем содержимое списка авторов на форме
            UpdateWriters();

            CreateWriterCommand = new Command(CreateWriter);
            DeleteWriterCommand = new Command(DeleteWriter);
            SaveWriterCommand = new Command(SaveWriter);
            BackCommand = new Command(Back);
        }

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //Обновление списка авторов на форме - грузим таблицу с БД, упорядочиваем авторов по имени
        private void UpdateWriters()
        {
            Writers = new ObservableCollection<WriterViewModel>();
            foreach (Writer w in databaseConnection.Table<Writer>().OrderBy(el => el.Name))
            {
                WriterViewModel temp = new WriterViewModel() { Writer = w, ListViewModel = this};
                Writers.Add(temp);
            }
        }         
       
        //Добавление автора после нажатия кнопки "Добавить" на форме WritersListPage
        private void CreateWriter()
        {
            Navigation.PushAsync(new WriterPage(new WriterViewModel() { ListViewModel = this }));
        }

        //Сохранение изменений в информации об авторе - после выбора автора в списке на форме WritersListPage, 
        //внесения изменений и нажатия "Сохранить" на форме WritersPage
        private void SaveWriter(object writerObject)
        {
            WriterViewModel writer = writerObject as WriterViewModel;
            if (writer != null && writer.IsValid)
            {
                if (writer.Writer.Id != 0)
                {
                    if (databaseConnection.Update(writer.Writer) == 1) //Если обновили в БД - обновляем элемент в списке
                    {
                        WriterViewModel updatedWriter = Writers.FirstOrDefault(w => w.Writer.Id == writer.Writer.Id);
                                                        /*(from w in Writers
                                                        where w.Writer.Id == writer.Writer.Id
                                                        select w).First();*/
                        if (updatedWriter != null)
                        {
                            updatedWriter.Name = writer.Name;
                            SortWriters();
                        }
                    }
                }
                else
                {
                    if (databaseConnection.Insert(writer.Writer) == 1) //Если добавили в БД - добавляем элемент в список
                    {
                        Writers.Add(writer);
                        SortWriters();
                    }
                }
            }
            //UpdateWriters(); //Обновляем список на форме - пока так
            Back(); //Возврат на исходную страницу           
        }

        //Удаление автора - после выбора автора в списке на форме WritersListPage, 
        //и нажатия "Удалить" на форме WritersPage
        private void DeleteWriter(object writerObject)
        {
            WriterViewModel writer = writerObject as WriterViewModel;
            if (writer != null)
            {
                    if (databaseConnection.Delete<Writer>(writer.Writer.Id) == 1) //Если удалили в БД - удаляем в списке
                    {
                        WriterViewModel deletedWriter = Writers.FirstOrDefault(w => w.Writer.Id == writer.Writer.Id); /*(from w in Writers
                                                     where w.Writer.Id == writer.Writer.Id
                                                     select w).First();*/
                        if (deletedWriter != null)
                        {
                            Writers.Remove(deletedWriter);
                        }
                    }
             }
             //UpdateWriters(); //Обновляем список на форме - пока так
             Back(); //Возврат на исходную страницу                      
        }

        //сортировка списка авторов по ФИО (после добавления и изменения отдельных записей)
        private void SortWriters()
        {
            Writers = new ObservableCollection<WriterViewModel>(Writers.OrderBy(w => w.Name));
            OnPropertyChanged("Writers");
        }

        //Возврат на предыдущую страницу
        private void Back()
        {
            Navigation.PopAsync();
        }
    }
}
