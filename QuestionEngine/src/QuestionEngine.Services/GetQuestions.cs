using QuestionEngine.API.Controllers;
using QuestionEngine.Domain;

namespace QuestionEngine.Services
{
    public class GetQuestions : IGetQuestions
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestions(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Question GetQuestion(int questionId)
        {
            return _questionRepository.GetQuestion(questionId);
        }
    }
}
