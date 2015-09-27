using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class QuestionAnswer
    {
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }

        public virtual Question Questions { get; set; }
        public virtual Answer Answers { get; set; }
    }
}
