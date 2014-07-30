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
            var questionCounter = 1;
            var answerCounter = 1;

            questions.Add(new Question(questionCounter++,
                "A colleague needs to finish an urgent project before they leave",
                new List<Answer> {
                    new Answer(answerCounter++, "Suggest they email their manager to say the project won’t be completed on time", false),
                    new Answer(answerCounter++, "Stay and help complete the task together", true),
                    new Answer(answerCounter++, "Leave it to them, they can always catch a later train", false),
                    new Answer(answerCounter++, "Suggest they put the information on a USB stick and complete the work on the journey", false)
                },
                new List<Media>
                    {
                        new Media("image", "Peninsula.jpg"),
                       new Media("video", "https://www.youtube.com/watch?v=AlQvFI-ezoc") 
                    }));

            questions.Add(new Question(questionCounter++,
                "The train to Euston has been cancelled with no notification of the next train",
                new List<Answer> {
                    new Answer(answerCounter++, "Ask a guard for their advice on the quickest route", true),
                    new Answer(answerCounter++, "Hang around and see if the board gets updated with new times", false),
                    new Answer(answerCounter++, "Rearrange your flights for the next day", false),
                    new Answer(answerCounter++, "There’s a train on the next platform – jump on it and take it from there", false)
                            },
                new List<Media>
                    {
                        new Media("image", "Manchester.jpg"),
                       new Media("video", "https://www.youtube.com/watch?v=AU-PA8Kqov4") 
                    }));

            return questions;
        }
    }
}