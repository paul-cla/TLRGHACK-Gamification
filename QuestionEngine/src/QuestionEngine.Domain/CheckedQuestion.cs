namespace QuestionEngine.API.Controllers
{
    public class CheckedQuestion
    {
        private readonly int _questionId;
        private readonly bool _correct;

        public CheckedQuestion(int questionId, bool correct)
        {
            _questionId = questionId;
            _correct = correct;
            
        }
    }
}