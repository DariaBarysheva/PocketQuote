using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PocketQuote
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "QuotesDB.db";
        public static WriterRepository database;
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
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PocketQuote.MainPage());
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
