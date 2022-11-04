using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace WebStore.UITests
{
    public abstract class TestBase : IDisposable
    {
        protected IWebDriver webDriver;

        private readonly Uri _baseUrl = new(@"http://localhost:5000");

        protected TestBase(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    webDriver = new ChromeDriver();
                    break;
                case BrowserType.Yandex:
                    ChromeOptions options = new()
                    {
                        BinaryLocation = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                            , @"Yandex\YandexBrowser\Application\browser.exe"
                        )
                    };
                    webDriver = new ChromeDriver(options);
                    break;
                case BrowserType.Edge:
                    webDriver = new EdgeDriver(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");
                    break;
            }
            webDriver.Navigate().GoToUrl(_baseUrl);
            //webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
        }
        public void Dispose()
        {
            webDriver?.Close();
            webDriver?.Dispose();
        }

        public enum BrowserType : byte
        {
            Chrome = 0,
            Yandex,
            Edge,
        }

    }
}