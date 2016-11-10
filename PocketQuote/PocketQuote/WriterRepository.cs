using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace PocketQuote
{
    //Класс, описывающий логику работу с таблицей "Автор" (Writers) в БД
    public class WriterRepository
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
    }
}
