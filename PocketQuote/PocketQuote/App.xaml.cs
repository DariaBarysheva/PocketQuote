using PocketQuote.Views;
using Xamarin.Forms;

namespace PocketQuote
{
    public partial class App : Application
    {
        //База данных SQLite, с которой будет соединяться приложение. Создана в DB Browser for SQLite.
        //Добавлена в папку Assets в проекте PocketQuote.Droid. При первом заходе в приложение копируется на смартфон, при последующих - читается в нем.
        public const string DATABASE_NAME = "QuotesDB.db"; 
        /*public static WriterRepository database;
        public static WriterRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new WriterRepository(DATABASE_NAME);
                }
                return database;
            }
        }*/

        public App()
        {
            InitializeComponent();
            /*MainPage = new NavigationPage(new PocketQuote.MainPage());*/
            MainPage = new NavigationPage(new WritersListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
