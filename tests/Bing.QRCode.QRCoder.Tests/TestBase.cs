using Xunit.Abstractions;

namespace Bing.QRCode.QRCoder.Tests
{
    public class TestBase
    {
        protected ITestOutputHelper Output;

        public TestBase(ITestOutputHelper output)
        {
            Output = output;
        }
    }
}
