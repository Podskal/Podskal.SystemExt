using System;
using System.Linq;
using Podskal.SystemExt.LinqExt;
using System.Collections.Generic;
using Xunit;

namespace Podskal.SystemExtTests.LinqExt
{
    public class EnumerableExtTests
    {
        [Fact]
        public void GivenNullFirstCollection_WhenZipStrict_ThenArgumentException()
        {
            // Given
            IEnumerable<Int32> first = null;
            var second = new[] { 10 };
            Func<Int32, Int32, String> zipFunc = (f, s) => $"{f} - {s}";
            // When Then
            Assert.Throws<ArgumentNullException>(
                () => 
                    first.ZipStrict(second, zipFunc).ToList()); 
        }
    }
}
