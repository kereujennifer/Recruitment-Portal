using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Auth.Models.ViewModels
{
    public class InterviewAndEvaluationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxPoints { get; set; }
        public string Venue { get; set; }
        [DataType(DataType.Date)]
        public DateTime InterViewDate { get; set; }
        public List<Jobs> Jobs { get; set; }
        public List<Evaluation> Evaluations { get; set; }
        [DataType(DataType.Date)]
        public bool isEdit { get; set; }
        public string Status { get; set; }

        public DateTime DateAdded { get; set; }
        public string Question { get; set; }
        public ICollection<QuestionData> Questions { get; set; } = new List<QuestionData>();

            public List<QuestionData> QuestionsToAdd { get; set; }
        public List<QuestionData> QuestionsToEdit { get; set; }
        public List<QuestionData> QuestionsToDelete { get; set; }
    }
}
