using Hidden_Drit.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hidden_Drit
{
    public partial class App : Application
    {
        public static string DbPath = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string pDBPath)
        {
            InitializeComponent();

            DbPath = pDBPath;

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            DB_Seed();
        }

        private void DB_Seed()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<TrackLevel>();
                if(conn.Table<TrackLevel>().Count() == 0)
                {
                    TrackLevel Easy = new TrackLevel() { Name= "Easy", Description = "Easy" };
                    TrackLevel Moderate = new TrackLevel() { Name = "Moderate", Description = "Moderate" };
                    TrackLevel Difficult = new TrackLevel() { Name = "Difficult", Description = "Difficult" };

                    conn.Insert(Easy);
                    conn.Insert(Moderate);
                    conn.Insert(Difficult);
                }

                conn.CreateTable<TrackType>();
                if(conn.Table<TrackType>().Count() == 0)
                {
                    TrackType Enduro = new TrackType() { Name = "Enduro", Description = "Enduro" };
                    TrackType Motocross = new TrackType() { Name = "Motocross", Description = "Motocross" };
                    TrackType Outrides = new TrackType() { Name = "Outrides", Description = "Outrides" };
                    TrackType MountainBike = new TrackType() { Name = "Mountain Bike", Description = "Mountain Bike" };
                    TrackType Other = new TrackType() { Name = "Other", Description = "Other" };

                    conn.Insert(Enduro);
                    conn.Insert(Motocross);
                    conn.Insert(Outrides);
                    conn.Insert(MountainBike);
                    conn.Insert(Other);
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
