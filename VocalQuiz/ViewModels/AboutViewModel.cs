using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://translate.google.com/?hl=vi&sl=vi&tl=ko&op=translate"));
        }

        public ICommand OpenWebCommand { get; }
    }
}