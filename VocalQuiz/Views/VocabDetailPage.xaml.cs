using VocalQuiz.ViewModels;
using Xamarin.Forms;

namespace VocalQuiz.Views
{
    public partial class VocabDetailPage : ContentPage
    {
        public VocabDetailPage()
        {
            InitializeComponent();
            BindingContext = new VocabDetailViewModel();
        }
    }
}