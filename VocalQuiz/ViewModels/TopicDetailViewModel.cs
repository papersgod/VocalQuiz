using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VocalQuiz.Models;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TopicDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private string description;
        public int Id { get; set; }
        private Topic topic;
        public Topic Topic
        {
            get => topic;
            set => SetProperty(ref topic, value);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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

        public TopicDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStoreTopic.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
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
            Topic newItem = new Topic()
            {
                Id = Id,
                Text = Text,
                Description = Description
            };

            await DataStoreTopic.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
