using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<QuestionData> Questions { get; set; } = new List<QuestionData>();
        public  string Status { get; set; }
        public bool isEdit { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateAdded { get; set; }


    }
}