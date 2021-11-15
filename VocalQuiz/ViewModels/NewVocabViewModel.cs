using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Data;
using VocalQuiz.Models;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    public class NewVocabViewModel : BaseViewModel
    {
        private string inKorean;
        private string inVietnamese;
        private Topic topic;
        public Topic Topic
        {
            get => topic;
            set => SetProperty(ref topic, value);
        }

        public ObservableCollection<Topic> Topics { get; }
        public NewVocabViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Topics = new ObservableCollection<Topic>();
            _ = ExecuteLoadTopicsCommand();
        }
        async Task ExecuteLoadTopicsCommand()
        {
            IsBusy = true;

            try
            {
                Topics.Clear();
                var items = await DataStoreTopic.GetItemsAsync();
                foreach (var item in items)
                {
                    Topics.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(inKorean)
                && !String.IsNullOrWhiteSpace(inVietnamese);
        }

        public string InKorean
        {
            get => inKorean;
            set => SetProperty(ref inKorean, value);
        }

        public string InVietnamese
        {
            get => inVietnamese;
            set => SetProperty(ref inVietnamese, value);
        }
      
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Vocabulary newItem = new Vocabulary()
            {
                InKorean = InKorean,
                InVietNamese = InVietnamese,
                TopicId = Topic.Id
            };

            await DataStoreVocab.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
