using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class TempClass
    {
        public int ID { get; set; }
        public static int NextQuestionNumber { get; set; }
        public static int TotallyAnsweredQuestions { get; set; }
        public static int GradeForAnswer { get; set; }
        //public static string TestName { get; set; }
        //public static string QuestionDifficultyLevel { get; set; }
        //public static string TempNameForUser { get; set; }
    }
}
