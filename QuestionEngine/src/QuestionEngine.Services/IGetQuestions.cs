using QuestionEngine.Domain;

namespace QuestionEngine.Services
{
    public interface IGetQuestions
    {
        Question GetQuestion(int questionId);
    }
}