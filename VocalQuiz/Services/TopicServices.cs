using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;

namespace VocalQuiz.Services
{
    public class TopicServices : IDataStore<Topic>
    {

        public async Task<bool> AddItemAsync(Topic item)
        {
            await App.TopicDatabase.SaveTopicAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Topic topic)
        {
            await App.TopicDatabase.DeleteTopicAsync(topic);
            return await Task.FromResult(true);
        }

        public async Task<Topic> GetItemAsync(int id)
        {
            return await App.TopicDatabase.GetTopicAsync(id);
        }

        public async Task<IEnumerable<Topic>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.TopicDatabase.GetTopicsAsync();
        }

        public async Task<bool> UpdateItemAsync(Topic item)
        {
            await App.TopicDatabase.SaveTopicAsync(item);
            return await Task.FromResult(true);
        }
    }
}
