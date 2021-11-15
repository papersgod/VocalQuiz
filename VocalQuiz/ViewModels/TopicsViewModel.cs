using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using VocalQuiz.Models;
using VocalQuiz.Views;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    public class TopicsViewModel : BaseViewModel
    {
        private Topic _selectedItem1;
        private Topic _selectedItem2;
        public bool deleteFlag = false;

        public ObservableCollection<Topic> Topics { get; }
        public Command LoadTopicsCommand { get; }
        public Command AddTopicCommand { get; }
        public Command<Topic> ItemTapped1 { get; }
        public Command<Topic> ItemTapped2 { get; }
        public TopicsViewModel()
        {
            Title = "Thêm chủ đề";
            Topics = new ObservableCollection<Topic>();
            LoadTopicsCommand = new Command(async () => await ExecuteLoadTopicsCommand());

            ItemTapped1 = new Command<Topic>(OnTopicSelected);
            ItemTapped2 = new Command<Topic>(OnDeleteSelected);
            AddTopicCommand = new Command(OnAddTopic);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem1 = null;
            SelectedItem2 = null;
        }

        public Topic SelectedItem1
        {
            get => _selectedItem1;
            set
            {
                SetProperty(ref _selectedItem1, value);
                OnTopicSelected(value);
            }
        }

        public Topic SelectedItem2
        {
            get => _selectedItem2;
            set
            {
                SetProperty(ref _selectedItem2, value);
                OnDeleteSelected(value);
            }
        }

        private async void OnAddTopic(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTopicPage));
        }

        async void OnTopicSelected(Topic item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TopicDetailPage)}?{nameof(TopicDetailViewModel.ItemId)}={item.Id}");
        }

        public async void OnDeleteSelected(Topic item)
        {
            if (item == null)
                return;
            await DataStoreTopic.DeleteItemAsync(item);
            await ExecuteLoadTopicsCommand();
        }
    }
}