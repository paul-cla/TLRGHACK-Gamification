using System.Runtime.Caching;
using FluentAssertions;
using NUnit.Framework;

namespace QuestionEngine.DataAccess.Tests
{
    [TestFixture]
    public class ObjectCacheExtensionsTest
    {
        [Test]
        public void should_add_item_to_cache_if_not_found()
        {
            var cache = new MemoryCache("TestCache");

            cache.GetOrAdd("key", () => "value", new CacheItemPolicy());

            cache.Get("key").Should().Be("value");
        }

        [Test]
        public void should_return_existing_item_if_already_in_cache()
        {
            var cache = new MemoryCache("TestCache");
            cache.Add("key", "value", new CacheItemPolicy());

            var returnedValue = cache.GetOrAdd("key", () => "fake value that should not be returned", new CacheItemPolicy());

            returnedValue.Should().Be("value");
        }
    }
}
