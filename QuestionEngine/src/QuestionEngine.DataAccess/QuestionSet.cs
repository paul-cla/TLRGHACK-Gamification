using System.Collections.Generic;
using QuestionEngine.Domain;

namespace QuestionEngine.DataAccess
{
    public class QuestionSet
    {
        public QuestionSet()
        {
            Questions = QuestionFactory.GenerateQuestions();
        }
        public List<Question> Questions { get; set; }
    }

    public static class QuestionFactory
    {
        public static List<Question> GenerateQuestions()
        {
            var questions = new List<Question>();
            var questionCounter = 0;
            var answerCounter = 0;

            questions.Add(new Question(questionCounter++,
                "You are only in Singapore for one day and you and your friend have a long list of things to see but might not have enough time.  Do you?",
                new List<Answer> {
                    new Answer(answerCounter++, "Suggest they email their manager to say the project won’t be completed on time", false),
                    new Answer(answerCounter++, "Stay and help complete the task together", true),
                    new Answer(answerCounter++, "Leave it to them, they can always catch a later train", false),
                    new Answer(answerCounter++, "Suggest they put the information on a USB stick and complete the work on the journey", false)
                }));

            return questions;
        }
    }
}