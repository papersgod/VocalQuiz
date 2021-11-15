using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocalQuiz.Views
{
    public partial class QuizPage : ContentPage
    {
        QuizViewModel _viewModel;
        public QuizPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new QuizViewModel();
            //_viewModel.Topic.Id = 1;
            btnCheck.Text = "Start";
            btnNext.IsVisible = false;
            mainStack.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (btnCheck.Text == "Start")
            {
                _viewModel.RandomQuiz();
                btnCheck.Text = "Check";
                mainStack.IsVisible = true;
                btnNext.IsVisible = true;
            }
            else
            {
                if (_viewModel.CheckResult())
                {
                    await DisplayAlert("Notification", "Chính xác!", "OK");
                }

                else
                {
                    await DisplayAlert("Notification", "Sai rồi", "OK");
                }
            } 
        }
        private void Next_Clicked(object sender, EventArgs e)
        {
            _viewModel.RandomQuiz();
            btnCheck.Text = "Check";
        }

      
    }
}