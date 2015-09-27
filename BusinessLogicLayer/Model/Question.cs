using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class Question
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter question")]
        [Display(Name = "Question")]
        public string TestQuestion { get; set; }

        //public List<string> Answers { get; set; }

        //[Required(ErrorMessage = "Please enter answer1")]
        //public string Answer1 { get; set; }

        //[Required(ErrorMessage = "Please enter answer2")]
        //public string Answer2 { get; set; }

        //[Required(ErrorMessage = "Please enter answer3")]
        //public string Answer3 { get; set; }

        //[Required(ErrorMessage = "Please enter answer4")]
        //public string Answer4 { get; set; }

        [Required(ErrorMessage = "Please enter correct answer")]
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }

        public string SelectedAnswer { get; set; }

        [Required(ErrorMessage = "Please choose category")]
        public string TestName { get; set; }

        [Required(ErrorMessage = "Please choose dificulty level")]
        [Display(Name = "Difficulty Level")]
        public string DifficultyLevel { get; set; }

        [Required(ErrorMessage = "Please enter grade")]
        public int Grade { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public string UserName { get; set; }
    }
}
