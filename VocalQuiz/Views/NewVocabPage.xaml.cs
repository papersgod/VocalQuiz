using VocalQuiz.Models;
using VocalQuiz.ViewModels;
using Xamarin.Forms;

namespace VocalQuiz.Views
{
    public partial class NewVocabPage : ContentPage
    {
        public Vocabulary Item { get; set; }
        public NewVocabPage()
        {
            InitializeComponent();
            BindingContext = new NewVocabViewModel();
        }
    }
}