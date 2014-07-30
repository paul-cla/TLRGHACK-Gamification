using NUnit.Framework;
using QuestionEngine.DataAccess;

namespace QuestionEngine.API.IntegrationTests
{
    [TestFixture]
    public class QuestionRepositoryTests
    {
        [Test]
        public void it_should_return_keyword_for_known_text()
        {
            var repo = new QuestionRepository();
            var question = repo.GetQuestion(1);

            Assert.That(question.Id, Is.EqualTo(1));
            Assert.That(question.Text, Is.EqualTo("Question 1"));
            //Assert.That(question.Answers.ToArray()[0], Is.EqualTo("Answer 1"));
            //Assert.That(question.Answers.ToArray()[1], Is.EqualTo("Answer 2"));
            //Assert.That(question.Answers.ToArray()[2], Is.EqualTo("Answer 3"));
        }
    }
}
