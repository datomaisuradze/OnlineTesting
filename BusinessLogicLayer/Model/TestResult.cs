using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class TestResult
    {
        public int ID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Collected Points")]
        public int CollectedPoints { get; set; }

        [Display(Name = "Test Max Points")]
        public int TestMaxPoints { get; set; }

        public int AnsweredQuestions { get; set; }
    }
}
