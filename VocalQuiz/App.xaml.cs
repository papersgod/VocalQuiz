using System;
using VocalQuiz.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VocalQuiz.Data;
using System.IO;
using VocalQuiz.Services;

namespace VocalQuiz
{
    public partial class App : Application
    {
        static TopicDatabase TopicDB;
        static VocabDatabase VocabDB;

        // Create the database connection as a singleton.
        public static TopicDatabase TopicDatabase
        {
            get
            {
                if (TopicDB == null)
                {
                    TopicDB = new TopicDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Topic.db3"));
                }
                return TopicDB;
            }
        }
        public static VocabDatabase VocabDatabase
        {
            get
            {
                if (VocabDB == null)
                {
                    VocabDB = new VocabDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Vocab.db3"));
                }
                return VocabDB;
            }
        }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<TopicServices>();
            DependencyService.Register<VocabServices>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
