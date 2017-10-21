using NUnit.Framework;
using Plivo.Http;
using Plivo.Resource.Pricing;

namespace Plivo.test.Resources
{
    [TestFixture]
    public class TestPricing : BaseTestCase
    {
        [Test]
        public void TestPricingGet()
        {
            var id = "abcabcabc";
            var request =
                new PlivoRequest(
                    "GET",
                    "Account/MAXXXXXXXXXXXXXXXXXX/Pricing/" + id + "/",
                    "");
            
            var response = 
                System.IO.File.ReadAllText(
                    SOURCE_DIR + @"test/Mocks/pricingGetResponse.json"
                );
            Setup<Pricing>(
                200,
                response
            );
            Assert.IsEmpty(
                Util.Compare(
                    response,
                    Api.Pricing.Get(id)));
            
            AssertRequest(request);
        }
    }
}
