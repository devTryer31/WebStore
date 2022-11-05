using OpenQA.Selenium;

namespace WebStore.UITests.Routing
{
    public abstract class RoutingTestsBase : TestBase
    {
        public RoutingTestsBase(BrowserType browser) : base(browser)
        { }

        [Fact]
        public void NonExistedEndpointTest()
        {
            //arr/act
            webDriver.Navigate().GoToUrl(webDriver.Url + "/room");

            //assert
            var imgSrc = webDriver.FindElement(By.XPath("/html/body/div[1]/div[2]/img"))
                .GetAttribute("src");

            Assert.Equal(@"http://localhost:5000/images/404/404.png", imgSrc);
        }
    }
}
