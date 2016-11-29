using System.ComponentModel;
using PocketQuote.Models;

namespace PocketQuote.ViewModels
{
    //Описывает логику работы с данными об одном авторе
    public class WriterViewModel : INotifyPropertyChanged
    {
        //Ссылка на список всех авторов. Передается классом WritersListViewModel при создании объекта текущего класса.
        private WritersListViewModel lvm;
        public WritersListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                lvm = value;
                OnPropertyChanged("ListViewModel");
            }
        }

        //Ссылка на объект модели "Автор" (Writer). 
        public Writer Writer { get; set; }

        //Имя автора - соответствует имени модели "Автор"
        public string Name
        {
            get { return Writer.Name; }
            set
            {
                Writer.Name = value;
                OnPropertyChanged("Name");
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

        public WriterViewModel(/*Writer tempWriter*/)
        {
            /*if (tempWriter == null) //Добавление нового автора
            {
                Writer = new Writer();
            }
            else //Редактирование или удаление автора
            {
                Writer = tempWriter;
            }*/

            Writer = new Writer();
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
