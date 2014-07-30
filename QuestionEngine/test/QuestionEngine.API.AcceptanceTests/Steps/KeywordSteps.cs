using FluentAssertions;
using Keywords.API.Controllers;
using Keywords.Domain;
using StructureMap;
using TechTalk.SpecFlow;

namespace Keywords.API.AcceptanceTests.Steps
{
    [Binding]
    public class KeywordSteps
    {
        [Given(@"(.*) has a keyword ID of (.*) and is in (.*) which has an id of (.*) with area (.*) with area id of (.*)")]
        public void GivenHasAKeywordIDOfAndIsInCountry(string text, int keywordId, string country, int countryId, string area, int areaId)
        {
            var keyword = new Keyword(keywordId, text, area, country, areaId, text.Replace(" ", "-"));
            CommonSteps.FakeKeywordRepository.AddFakeKeyword(keyword);
        }

        [When(@"I request the keyword ID for (.*) in country (.*)")]
        public void WhenIRequestTheKeywordIDForInCountry(string text, int countryId)
        {
            var keywordController = ObjectFactory.GetInstance<KeywordController>();
            var keyword = keywordController.GetKeywordByTextAndCountry(text, countryId);
            ScenarioContext.Current["Keyword"] = keyword;
        }

        [When(@"I request the keyword for ID (.*)")]
        public void WhenIRequestTheKeywordForID(int keywordId)
        {
            var keywordController = ObjectFactory.GetInstance<KeywordController>();
            var keyword = keywordController.GetKeywordById(keywordId);
            ScenarioContext.Current["Keyword"] = keyword; 
        }

        [Then(@"keyword ID of (.*) with keyword text of (.*) with country (.*) with area of (.*) and area ID of (.*) is returned")]
        public void ThenTheKeywordIsReturned(int keywordId, string text, string country, string area, int areaId)
        {
            var keyword = ScenarioContext.Current["Keyword"] as Keyword;

            keyword.Id.Should().Be(keywordId);
            keyword.KeywordText.Should().Be(text);
            keyword.AreaName.Should().Be(area);
            keyword.AreaId.Should().Be(areaId);
            keyword.Country.Should().Be(country);
        }
    }
}
