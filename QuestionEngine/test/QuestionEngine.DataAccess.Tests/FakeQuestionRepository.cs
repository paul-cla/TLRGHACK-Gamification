using QuestionEngine.API.Controllers;
using QuestionEngine.Domain;

namespace QuestionEngine.DataAccess.Tests
{
    public class FakeQuestionRepository : IQuestionRepository
    {
        private Question _question;

        public void AddFakeQuestion(Question question)
        {
            _question = question;
        }

        public Question GetQuestion(int questionId)
        {
            return _question;
        }
    }
}