using Day08;
using Xunit;

namespace Day08Tests
{
    public class LicenceTests
    {
        [Fact]
        public void FromStringLoadsLicenceCorrectly()
        {
            const int expected = 138;
            var subject = Licence.FromString("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2");
            var actual = subject.MetadataSum;
            Assert.Equal(expected, actual);
        }
    }
}
