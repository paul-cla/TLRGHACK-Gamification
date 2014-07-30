namespace QuestionEngine.Domain
{
    public interface IQuestionRepository
    {
        Question GetQuestion(int questionId);
    }
}