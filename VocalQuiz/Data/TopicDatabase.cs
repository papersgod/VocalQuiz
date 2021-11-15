using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using VocalQuiz.Models;

namespace VocalQuiz.Data
{
    public class TopicDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TopicDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Topic>().Wait();
        }

        public Task<List<Topic>> GetTopicsAsync()
        {
            //Get all notes.
            return database.Table<Topic>().ToListAsync();
        }

        public Task<Topic> GetTopicAsync(int id)
        {
            // Get a specific note.
            return database.Table<Topic>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTopicAsync(Topic topic)
        {
            if (topic.Id != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(topic);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(topic);
            }
        }
        
        public Task<int> DeleteTopicAsync(Topic topic)
        {
            // Delete a note.
            return database.DeleteAsync(topic);
        }
    }
}