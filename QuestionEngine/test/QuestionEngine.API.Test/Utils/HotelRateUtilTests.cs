using FluentAssertions;
using HotelDetails.API.Utils;
using NUnit.Framework;

namespace HotelDetails.API.Test.Utils
{
    [TestFixture]
    public class HotelRateUtilTests
    {
        [Test]
        public void null_rateplancode_should_be_sanitised_as_null()
        {
            HotelRateUtil.SanitisedRatePlanCode(null).Should().Be(null);
        }

        [Test]
        public void empty_rateplancode_should_be_sanitised_as_empty()
        {
            HotelRateUtil.SanitisedRatePlanCode(string.Empty).Should().Be(string.Empty);
        }

        [TestCase("SOMETHING-LIBERATE-RATEPLANCODE-1", "SOMETHING-LIBERATE-RATEPLANCODE-1")]
        [TestCase("LIBERATERATEPLANCODE-1", "LIBERATERATEPLANCODE-1")]
        [TestCase("LIBERATE-RATEPLANCODE1", "LIBERATE-RATEPLANCODE1")]
        [TestCase("RATEPLANCODE-1", "RATEPLANCODE-1")]
        [TestCase("LIBERATE-RATEPLANCODE-1A", "LIBERATE-RATEPLANCODE-1A")]
        [TestCase("ALIBERATE-RATEPLANCODE-1", "ALIBERATE-RATEPLANCODE-1")]
        [TestCase("LIBERATE-RATEPLANCODE-20140101-1A", "LIBERATE-RATEPLANCODE-20140101-1A")]
        [TestCase("ALIBERATE-RATEPLANCODE-20140101-1", "ALIBERATE-RATEPLANCODE-20140101-1")]
        [TestCase("liberateLIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-1", "liberateLIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-1")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-1-A", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-1-A")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140507-1A", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140507-1A")]
        public void rateplancode_which_is_not_prefixed_with_LIBERATE_and_suffixed_with_number_1_should_not_be_altered_when_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }

        [TestCase("LIBeratE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-A", "LIBeratE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-A")]
        [TestCase("LIBeratE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-1A", "LIBeratE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-1A")]
        public void rateplancode_which_is_prefixed_with_LIBERATE_and_not_suffixed_with_number_should_not_be_altered_when_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }

        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-2014031-2", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-2014031-2")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140-3", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140-3")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-209-4", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-209-4")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20 4031-5", "LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20 4031-5")]
        public void rateplancode_which_is_prefixed_with_LIBERATE_and_suffixed_with_number_and_with_unexpected_format_date_should_not_be_altered_when_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }

        [TestCase("LIBERATE-abc-1", "abc")]
        [TestCase("LIBERATE-abc  a-1", "abc  a")]
        [TestCase("liberate-RPC-ABC-1", "RPC-ABC")]
        [TestCase("liberate-RPC-ABD-1", "RPC-ABD")]
        [TestCase("LIBeratE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-1", "CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}")]
        public void rateplancode_which_is_prefixed_with_LIBERATE_and_suffixed_with_hyphen_1_is_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }

        [Test]
        [TestCase("LIBERATE-abc-2", "LIBERATE-abc-2")]
        [TestCase("liberate-RPC-ABC-3", "liberate-RPC-ABC-3")]
        [TestCase("liberate-RPC-ABC-4", "liberate-RPC-ABC-4")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140307-2", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20150307-3", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("LIBERATE-DBL{hyphen}E10{hyphen}UB-20140507-4", "DBL{hyphen}E10{hyphen}UB")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140807-5", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("liberate-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140607-6", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140323-7", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("LIBERATE-DBL{hyphen}E10{hyphen}PG-20140326-8", "DBL{hyphen}E10{hyphen}PG")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}-20140326-9", "CG{hyphen}TODOS SUPERI~{hyphen}BB{hyphen}E10{hyphen}")]
        [TestCase("LIBERATE-FAM{hyphen}E10{hyphen}PG-20140326-10", "FAM{hyphen}E10{hyphen}PG")]
        [TestCase("liBERate-TPL{hyphen}E10{hyphen}UB-20140326-110", "TPL{hyphen}E10{hyphen}UB")]
        public void rateplancode_which_is_prefixed_with_LIBERATE_and_suffixed_with_number_greater_than_1_is_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }

        [Test]
        [TestCase("LIBERATE-abc-20140101-1", "abc")]
        [TestCase("liberate-RPC-ABC-19991212-12", "RPC-ABC")]
        [TestCase("liberate-RPC-ABC-20200102-22", "RPC-ABC")]
        [TestCase("LIBERATE-CG{hyphen}TODOS~{hyphen}RO{hyphen}E10-1", "CG{hyphen}TODOS~{hyphen}RO{hyphen}E10")]
        [TestCase("LIBERATE-CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}-20140227-2", "CG{hyphen}TODOS SUPERI~{hyphen}RO{hyphen}E10{hyphen}")]
        public void rateplancode_which_is_prefixed_with_LIBERATE_and_suffixed_with_hyphen_yyyymmdd_hyphen_number_is_sanitised(string ratePlan, string sanitisedRatePlan)
        {
            HotelRateUtil.SanitisedRatePlanCode(ratePlan).Should().Be(sanitisedRatePlan);
        }
    }
}
