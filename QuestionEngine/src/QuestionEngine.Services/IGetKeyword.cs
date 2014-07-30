using Keywords.Domain;

namespace Keywords.Services
{
    public interface IGetKeyword
    {
        Keyword GetKeywordByTextAndCountry(string text, int countryId);
        Keyword GetKeywordById(int keywordId);
        Region GetRegionByText(string text, int countryId);
        Region GetRegionById(int regionId);
        IStatus Status();
    }
}