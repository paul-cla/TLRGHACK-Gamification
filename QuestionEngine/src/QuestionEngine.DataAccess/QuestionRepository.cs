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
            _questionsSet.Questions = new List<Question>();
            _questionsSet.Questions.Add(new Question(1, "Test"));
        }

        public Question GetQuestion(int questionId)
        {
            var cacheKey = "GetQuestion" + questionId;
            return _cache.GetOrAdd(cacheKey, () => GetQuestionFromRepo(questionId), new CacheItemPolicy());
        }

        public CheckedQuestion CheckAnswer(int questionId, int answerId)
        {
            var question = _questionsSet.Questions.FirstOrDefault(x => x.Id == questionId && x.);
        }

        public Question GetQuestionFromRepo(int questionId)
        {
            return _questionsSet.Questions.FirstOrDefault(x => x.Id == questionId);
        }
    }
}
