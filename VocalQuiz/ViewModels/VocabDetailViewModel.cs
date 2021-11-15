using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class VocabDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Topic> Topics { get; }
        private int itemId;
        private string inKorean;
        private string inVietnamese;
        private Topic topic;

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
        public Topic Topic
        {
            get => topic;
            set => SetProperty(ref topic, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                if (itemId != value)
                {
                    itemId = value;
                    LoadItemId(value);
                }      
            }
        }
        public Command LoadTopicsCommand { get; }

        public VocabDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Topics = new ObservableCollection<Topic>();
            _= ExecuteLoadTopicsCommand();
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
        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStoreVocab.GetItemAsync(itemId);
                ItemId = item.Id;
                InKorean = item.InKorean;
                InVietnamese = item.InVietNamese;
                Topic = await DataStoreTopic.GetItemAsync(item.TopicId);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
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
                Id = ItemId,
                InKorean = InKorean,
                InVietNamese = InVietnamese,
                TopicId = Topic.Id,
            };

            await DataStoreVocab.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
