using System;
using Xamarin.Forms;
using System.IO;
using PocketQuote.iOS;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace PocketQuote.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            //Определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); //Папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }

            return path;
        }
    }
}