using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VocalQuiz.Models
{
    public class Vocabulary
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = 0;
        public int TopicId { get; set; } = 0;
        public string InKorean { get; set; }
        public string InVietNamese { get; set; }
    }
}
