using System.ComponentModel;
using VocalQuiz.ViewModels;
using Xamarin.Forms;

namespace VocalQuiz.Views
{
    public partial class TopicDetailPage : ContentPage
    {
        public TopicDetailPage()
        {
            InitializeComponent();
            BindingContext = new TopicDetailViewModel();
        }
    }
}