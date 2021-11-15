using VocalQuiz.Models;
using VocalQuiz.ViewModels;
using Xamarin.Forms;

namespace VocalQuiz.Views
{
    public partial class NewTopicPage : ContentPage
    {
        public Topic Item { get; set; }

        public NewTopicPage()
        {
            InitializeComponent();
            BindingContext = new NewTopicViewModel();
        }
    }
}