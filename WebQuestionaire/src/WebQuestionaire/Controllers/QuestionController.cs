using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebQuestionaire.Models;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebQuestionaire.Controllers
{
    public class QuestionController : Controller
    {
        private List<Question> questions = new List<Question>
        {
            new Question { QuestionId = 1, Text = "The Moon is Round?", Answer = "Yes", OrderId = 1 },
            new Question { QuestionId = 2, Text = "The Moon is Square?", Answer = "No", OrderId = 1 },
            new Question { QuestionId = 3, Text = "The Moon is Big?", Answer = "Yes", OrderId = 1 },
            new Question { QuestionId = 4, Text = "The Moon is Small?", Answer = "No", OrderId = 1 },
            new Question { QuestionId = 5, Text = "Is the Moon a Dylan?", Answer = "No", OrderId = 1 },
            new Question { QuestionId = 6, Text = "Is the Moon not a Dylan?", Answer = "Yes", OrderId = 1 },
            new Question { QuestionId = 7, Text = "Is Dylan as big as the Moon?", Answer = "No", OrderId = 1 }
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            Random r = new Random();
            int rInt = r.Next(1, 10); //random int between 1 and 10

            List<Question> filteredList = questions.Where(x => x.QuestionId == rInt).ToList();

            return View(filteredList);
        }

        // POST:
        [HttpPost]
        public IActionResult Output()
        {
            var wrongQuestions = new List<Question>();
            foreach(var name in Request.Form.Keys.Where(a => a.StartsWith("answer")))
            {
                var answerIndex = name.ToString().Split('_')[1];
                var originalQuestion = questions.FirstOrDefault(q => q.QuestionId == Int32.Parse(answerIndex));
                var postedAnswer = Request.Form.FirstOrDefault(q => q.Key == name);
                if (postedAnswer.Value.ToString().ToLower() != originalQuestion.Answer.ToLower())
                {
                    wrongQuestions.Add(new Question
                    {
                        QuestionId = Int32.Parse(answerIndex),
                        Text = originalQuestion.Text,
                        Answer = originalQuestion.Answer,
                        OrderId = originalQuestion.OrderId
                    });
                    
                }
            }
            if(wrongQuestions.Count > 0)
            {
                return View("Index", wrongQuestions);
            }

            return View("Index", questions);
        }
    }
}
