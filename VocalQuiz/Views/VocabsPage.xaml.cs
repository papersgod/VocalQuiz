using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;
using VocalQuiz.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocalQuiz.Views
{
    public partial class VocabsPage : ContentPage
    {
        VocabsViewModel _viewModel;
        Vocabulary _currentTopic;

        public VocabsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new VocabsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void OnDisplayAlertQuestionButtonClicked(object sender, EventArgs e)
        {
            if (_currentTopic == null)
            {
                return;
            }
            bool response = await DisplayAlert("Delete?", "Would you like to delete this record?", "Yes", "No");
            _viewModel.OnDeleteSelected(_currentTopic);
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                _currentTopic = (Vocabulary)e.CurrentSelection.FirstOrDefault();
            }
        }
    }
}