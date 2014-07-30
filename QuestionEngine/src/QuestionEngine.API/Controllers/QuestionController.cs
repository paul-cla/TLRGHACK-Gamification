using System.Web.Http;
using QuestionEngine.Domain;
using QuestionEngine.Services;
using WebAPI.OutputCache;

namespace QuestionEngine.API.Controllers
{
    public class QuestionController : ApiController
    {
        private readonly IGetQuestions _questionService;

        public QuestionController(IGetQuestions questionService)
        {
            _questionService = questionService;
        }

        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public Question GetQuestion(int questionId)
        {
            return _questionService.GetQuestion(questionId);
        }
        
        [CacheOutput(ClientTimeSpan = 86400, ServerTimeSpan = 86400)]
        [HttpGet]
        public CheckedQuestion CheckAnswer(int questionId, int answerId)
        {
            return _questionService.CheckAnswer(questionId, answerId);
        }
    }
}
