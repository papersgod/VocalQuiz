using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;

namespace VocalQuiz.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        public ObservableCollection<Vocabulary> Vocabs { get; }
        public ObservableCollection<Topic> Topics { get; }
        private int itemId;
        private string inKorean;
        private string inVietnamese;
        private Topic topic;
        private string answer = "";
        private bool result;

        public bool Result
        {
            get { return result; }
            set { result = value; }
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
        public Topic Topic
        {
            get => topic;
            set => SetProperty(ref topic, value);
        }
        //public int ItemId
        //{
        //    get
        //    {
        //        return itemId;
        //    }
        //    set
        //    {
        //        itemId = value;
        //        RandomQuiz(value);
        //    }
        //}
        public bool _flag;
        public async void RandomQuiz()
        {
            try
            {
                answer = string.Empty;
                var item = await DataStoreVocab.GetItemsAsync();
                var data = item.Where(i => i.TopicId == Topic.Id).ToList();
                if (data.Count <= 1)
                {
                    return;
                }
                Random rand = new Random();
                var ranValue = data.ElementAt(rand.Next(data.Count()));
                //ItemId = ranValue.Id;
                var randomBool = rand.Next(2);
                if (randomBool == 1)
                {
                    InKorean = ranValue.InKorean;
                    InVietnamese = string.Empty;
                    answer = ranValue.InVietNamese;
                    _flag = true;
                }
                else
                {
                    InVietnamese = ranValue.InVietNamese;
                    InKorean = string.Empty;
                    answer = ranValue.InKorean;
                    _flag = false;
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public QuizViewModel()
        {
            Title = "Quizz";
            //Vocabs = new ObservableCollection<Vocabulary>();
            Topics = new ObservableCollection<Topic>();
            //_ = ExecuteLoadVocabsCommand();
            _ = ExecuteLoadTopicsCommand();
        }

        public bool CheckResult()
        {
            if (_flag)
            {
                if (InVietnamese == answer)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (InKorean == answer)
                {
                    return true;
                }
                return false;
            }
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
    }
}
