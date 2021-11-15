using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;
using VocalQuiz.Views;
using Xamarin.Forms;

namespace VocalQuiz.ViewModels
{
    class VocabsViewModel : BaseViewModel
    {
        private Vocabulary _selectedItem1;
        private Vocabulary _selectedItem2;
        public bool deleteFlag = false;

        public ObservableCollection<Vocabulary> Vocabs { get; }
        public Command LoadVocabsCommand { get; }
        public Command AddVocabCommand { get; }
        public Command<Vocabulary> ItemTapped1 { get; }
        public Command<Vocabulary> ItemTapped2 { get; }

        public VocabsViewModel()
        {
            Title = "Thêm từ vựng";
            Vocabs = new ObservableCollection<Vocabulary>();
            LoadVocabsCommand = new Command(async () => await ExecuteLoadVocabsCommand());

            ItemTapped1 = new Command<Vocabulary>(OnVocabSelected);
            ItemTapped2 = new Command<Vocabulary>(OnDeleteSelected);
            AddVocabCommand = new Command(OnAddVocab);
        }

        async Task ExecuteLoadVocabsCommand()
        {
            IsBusy = true;

            try
            {
                Vocabs.Clear();
                var items = await DataStoreVocab.GetItemsAsync();
                foreach (var item in items)
                {
                    Vocabs.Add(item);
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

        public Vocabulary SelectedItem1
        {
            get => _selectedItem1;
            set
            {
                SetProperty(ref _selectedItem1, value);
                OnVocabSelected(value);
            }
        }
        public Vocabulary SelectedItem2
        {
            get => _selectedItem2;
            set
            {
                SetProperty(ref _selectedItem2, value);
                OnDeleteSelected(value);
            }
        }

        private async void OnAddVocab(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewVocabPage));
        }

        async void OnVocabSelected(Vocabulary item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(VocabDetailPage)}?{nameof(VocabDetailViewModel.ItemId)}={item.Id}");
        }

        public async void OnDeleteSelected(Vocabulary item)
        {
            if (item == null)
                return;
            await DataStoreVocab.DeleteItemAsync(item);
            await ExecuteLoadVocabsCommand();
        }
    }
}
