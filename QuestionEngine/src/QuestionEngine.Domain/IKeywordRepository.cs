namespace Keywords.Domain
{
    public interface IKeywordRepository
    {
        Keyword GetKeywordByTextAndCountry(string text, int countryId);
        Keyword GetKeywordById(int keywordId);
        Region GetKeywordByText(string text, int countryId);
        Region GetRegionById(int regionId);
        void Heartbeat();
    }
}