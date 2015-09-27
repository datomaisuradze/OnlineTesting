using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnswerID { get; set; }
        public string QuestionAnswer { get; set; }


        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
