using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System;
using Spire.Pdf.Exporting.XPS.Schema;
using System.Collections.Generic;

namespace Auth.Models
{
    public class Interview
    {

        public int Id { get; set; }
        public string Venue { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public bool isEdit { get; set; }

        public DateTime DateAdded { get; set; }
        [DataType(DataType.Date)]
        public DateTime InterViewDate { get; set; }
        public string Status { get; set; }
        public List<Jobs> Jobs { get; set; }
        public List<Evaluation> Evaluations { get; set; }
    }
}
