using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Spacetime.Core.Shared;

namespace Spacetime.Core.Tests
{
    public class UrlBuilderTests
    {
        [Fact]
        public void GetUrl_ShouldAddQueryParams()
        {
            var sut = new UrlBuilder();
            var expected = "http://localhost:80?foo=bar&foo2=bar2";
            var request = new SpacetimeRequest
            {
                URL = "http://localhost:80",
                QueryParams = new List<QueryParamDto> { 
                    new QueryParamDto { Name = "foo", Value = "bar" },
                    new QueryParamDto { Name = "foo2", Value = "bar2" }
                }
            };

            var actual = sut.GetUrl(request);

            actual.Should().Be(expected);
        }
    }
}