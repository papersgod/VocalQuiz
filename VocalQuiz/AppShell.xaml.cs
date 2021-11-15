using System;
using System.Collections.Generic;
using VocalQuiz.ViewModels;
using VocalQuiz.Views;
using Xamarin.Forms;

namespace VocalQuiz
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TopicDetailPage), typeof(TopicDetailPage));
            Routing.RegisterRoute(nameof(NewTopicPage), typeof(NewTopicPage));
            Routing.RegisterRoute(nameof(VocabDetailPage), typeof(VocabDetailPage));
            Routing.RegisterRoute(nameof(NewVocabPage), typeof(NewVocabPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
