using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PayPlusModels
{
    public class RequestReport
    {
        [Display(Name ="Pay +")]
        public int PayPadId { get; set; }

        [Display(Name = "Trámite")]
        public int TransactId { get; set; }

        [Display(Name = "Estado")]
        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string  DateStartString { get; set; }
        public string DateFinishString { get; set; }
    }
}