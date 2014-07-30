using QuestionEngine.API.DependencyResolution;
using QuestionEngine.DataAccess.Tests;
using QuestionEngine.Domain;
using StructureMap;
using TechTalk.SpecFlow;

namespace QuestionEngine.API.AcceptanceTests.Steps
{
    [Binding]
    public class CommonSteps
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            FakeKeywordRepository = new FakeQuestionRepository();

            IoC.Initialize();
            ObjectFactory.EjectAllInstancesOf<IQuestionRepository>();
            ObjectFactory.Configure(
                x => x.For<IQuestionRepository>().Use(FakeKeywordRepository));

        }
        public static FakeQuestionRepository FakeKeywordRepository
        {
            get { return ScenarioContext.Current.Get<FakeQuestionRepository>(); }
            set { ScenarioContext.Current.Set(value); }
        }
    }
}