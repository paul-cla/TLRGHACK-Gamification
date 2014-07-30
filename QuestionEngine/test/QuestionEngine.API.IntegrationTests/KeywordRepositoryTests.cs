using Keywords.DataAccess;
using NUnit.Framework;

namespace Keywords.API.IntegrationTests
{
    [TestFixture]
    public class KeywordRepositoryTestsForTextAndCountry
    {
        [Test]
        public void it_should_return_keyword_for_known_text()
        {
            var repo = new KeywordRepository(new LateRoomsDatabase());
            const string text = "Manchester";
            const int countryId = 1;
            var keyword = repo.GetKeywordByTextAndCountry(text, countryId);

            Assert.That(keyword.Id, Is.EqualTo(16296355));
            Assert.That(keyword.AreaName, Is.EqualTo("Greater Manchester"));
            Assert.That(keyword.KeywordText, Is.EqualTo("Manchester"));
        }      
        [Test]
        public void it_should_return_default_keyword_for_unknown_text()
        {
            var repo = new KeywordRepository(new LateRoomsDatabase());
            const string text = "No such area";
            const int countryId = 1;
            var keyword = repo.GetKeywordByTextAndCountry(text, countryId);

            Assert.That(keyword.Id, Is.EqualTo(0));
            Assert.That(keyword.AreaName, Is.EqualTo(string.Empty));
            Assert.That(keyword.KeywordText, Is.EqualTo(string.Empty));
        }     
        [Test]
        public void it_should_return_default_keyword_for_text_with_unknown_country()
        {
            var repo = new KeywordRepository(new LateRoomsDatabase());
            const string text = "Manchester";
            const int countryId = 9999;
            var keyword = repo.GetKeywordByTextAndCountry(text, countryId);

            Assert.That(keyword.Id, Is.EqualTo(0));
            Assert.That(keyword.AreaName, Is.EqualTo(string.Empty));
            Assert.That(keyword.KeywordText, Is.EqualTo(string.Empty));
        }

        [Test]
        public void it_should_return_keyword_for_keywordid()
        {
            var repo = new KeywordRepository(new LateRoomsDatabase());
            const int manchesterKeywordId = 16296355;
            var keyword = repo.GetKeywordById(manchesterKeywordId);

            Assert.That(keyword.Id, Is.EqualTo(manchesterKeywordId));
            Assert.That(keyword.KeywordText, Is.EqualTo("Manchester"));
            Assert.That(keyword.AreaName, Is.EqualTo("Greater Manchester"));
        }
    }
}
