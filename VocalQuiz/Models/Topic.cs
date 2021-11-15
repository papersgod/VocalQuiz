using SQLite;

namespace VocalQuiz.Models
{
    public class Topic
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = 0;
        public string Text { get; set; }
        public string Description { get; set; }
    }
}