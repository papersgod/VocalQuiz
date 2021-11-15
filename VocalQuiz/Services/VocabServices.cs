using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;

namespace VocalQuiz.Services
{
    public class VocabServices : IDataStore<Vocabulary>
    {
        public async Task<bool> AddItemAsync(Vocabulary item)
        {
            await App.VocabDatabase.SaveVocabAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Vocabulary item)
        {
            await App.VocabDatabase.DeleteVocabAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<Vocabulary> GetItemAsync(int id)
        {
            return await App.VocabDatabase.GetVocabAsync(id);
        }

        public async Task<IEnumerable<Vocabulary>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.VocabDatabase.GetVocabsAsync();
        }

        public async Task<bool> UpdateItemAsync(Vocabulary item)
        {
            await App.VocabDatabase.SaveVocabAsync(item);
            return await Task.FromResult(true);
        }
    }
}
