using Keywords.API.DependencyResolution;
using Keywords.DataAccess;
using Keywords.DataAccess.Tests;
using Keywords.Domain;
using StructureMap;
using TechTalk.SpecFlow;

namespace Keywords.API.AcceptanceTests.Steps
{
    [Binding]
    public class CommonSteps
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            FakeKeywordRepository = new FakeKeywordRepository();

            IoC.Initialize();
            ObjectFactory.EjectAllInstancesOf<IKeywordRepository>();
            ObjectFactory.Configure(
                x => x.For<IKeywordRepository>().Use(FakeKeywordRepository));

        }
        public static FakeKeywordRepository FakeKeywordRepository
        {
            get { return ScenarioContext.Current.Get<FakeKeywordRepository>(); }
            set { ScenarioContext.Current.Set(value); }
        }
    }
}