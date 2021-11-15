using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocalQuiz.Models;

namespace VocalQuiz.Data
{
    public class VocabDatabase
    {
        readonly SQLiteAsyncConnection database;

        public VocabDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Vocabulary>().Wait();
        }
        public Task<List<Vocabulary>> GetVocabsAsync()
        {
            //Get all notes.
            return database.Table<Vocabulary>().ToListAsync();
        }

        public Task<Vocabulary> GetVocabAsync(int id)
        {
            // Get a specific note.
            return database.Table<Vocabulary>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveVocabAsync(Vocabulary vocab)
        {
            if (vocab.Id != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(vocab);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(vocab);
            }
        }

        public Task<int> DeleteVocabAsync(Vocabulary vocab)
        {
            // Delete a note.
            return database.DeleteAsync(vocab);
        }

    }
}
