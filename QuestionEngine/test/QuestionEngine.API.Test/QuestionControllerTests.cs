using FluentAssertions;
using NUnit.Framework;
using QuestionEngine.API.Controllers;
using QuestionEngine.Domain;
using QuestionEngine.Services;

namespace QuestionEngine.API.Test
{
    [TestFixture]
    public class QuestionControllerTests : IGetQuestions
    {
        private int _requestedQuestionId;

        [Test]
        public void should_call_keyword_service_with_text_and_country_id()
        {
            var controller = new QuestionController(this);
            controller.GetQuestion(1);
            _requestedQuestionId.Should().Be(1);
        } 

        public Question GetQuestion(int questionId)
        {
            _requestedQuestionId = questionId;
            return null;
        }
    }
}