using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using QuestionEngine.API.Controllers;
using QuestionEngine.Domain;

namespace QuestionEngine.DataAccess
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionSet _questionsSet;
        private readonly ObjectCache _cache = MemoryCache.Default;

        public QuestionRepository()
        {
            _questionsSet = new QuestionSet();
        }

        public Question GetQuestion(int questionId)
        {
            return _questionsSet.Questions.FirstOrDefault(x => x.Id == questionId);
        }

        public Question GetQuestionFromRepo(int questionId)
        {
            return _questionsSet.Questions.FirstOrDefault(x => x.Id == questionId);
        }
    }
}
