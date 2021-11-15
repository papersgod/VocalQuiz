using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VocalQuiz.Models;
using VocalQuiz.ViewModels;
using VocalQuiz.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocalQuiz.Views
{
    public partial class TopicsPage : ContentPage
    {
        TopicsViewModel _viewModel;
        Topic _currentTopic;

        public TopicsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TopicsViewModel();
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

        private Command longPressCommand;

        public ICommand LongPressCommand
        {
            get
            {
                if (longPressCommand == null)
                {
                    longPressCommand = new Command(LongPress);
                }

                return longPressCommand;
            }
        }

        private void LongPress()
        {
           
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                _currentTopic = (Topic)e.CurrentSelection.FirstOrDefault();
            }
        }
    }
}