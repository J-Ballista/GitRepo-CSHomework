using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebQuestionaire.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public int OrderId { get; set; }
    }
}
