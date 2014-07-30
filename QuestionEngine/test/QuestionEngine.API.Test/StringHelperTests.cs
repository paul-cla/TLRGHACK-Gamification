using Keywords.API.Support;
using NUnit.Framework;

namespace Keywords.API.Test
{
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void should_return_decoded_string()
        {
            const string input = "Test~~AMP~~Test";
            var output = StringHelper.DecodeAmpersands(input);
            Assert.That(output, Is.EqualTo("Test&Test"));
        }
    }
}
