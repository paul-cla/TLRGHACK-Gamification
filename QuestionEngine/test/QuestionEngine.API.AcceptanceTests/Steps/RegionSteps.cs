using FluentAssertions;
using Keywords.API.Controllers;
using Keywords.Domain;
using StructureMap;
using TechTalk.SpecFlow;

namespace Keywords.API.AcceptanceTests.Steps
{
    [Binding]
    public class RegionSteps
    {

        [Given(@"(.*) has a region ID of (.*)")]
        public void GivenTestCityHasARegionIDOf(string text, int regionId)
        {
            var region = new Region(regionId, text, text.Replace(" ", "-"));
            CommonSteps.FakeKeywordRepository.AddFakeRegion(region);
        }

        [When(@"I request the region ID for (.*) in country (.*)")]
        public void WhenIRequestTheRegionIDForTestCity(string text, int countryId)
        {
            var regionController = ObjectFactory.GetInstance<RegionController>();
            var region = regionController.GetRegionByText(text, countryId);
            ScenarioContext.Current["Region"] = region;
        }

        [Then(@"region ID of (.*) with region text of (.*) is returned")]
        public void ThenRegionIDOfWithRegionTextOfTestCityIsReturned(int regionId, string text)
        {
            var region = ScenarioContext.Current["Region"] as Region;
            region.Id.Should().Be(regionId);
            region.RegionText.Should().Be(text);
        }

        [When(@"I request the region for ID (.*)")]
        public void WhenIRequestTheRegionForID(int regionId)
        {
            var regionController = ObjectFactory.GetInstance<RegionController>();
            var region = regionController.GetRegionById(regionId);
            ScenarioContext.Current["Region"] = region;
        }
    }
}
