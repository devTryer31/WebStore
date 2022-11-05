using OpenQA.Selenium;

namespace WebStore.UITests.AdminPannel
{
    public abstract class ProductListTestsBase : TestBase
    {
        private readonly string _name;
        private readonly string _img;

        protected ProductListTestsBase(BrowserType browser) : base(browser)
        {
            AdminLogIn();
            Random gen = new();
            _name = $"New-case-name-{gen.Next()}";
            _img = $"New-case-ImgUrl-{gen.Next()}";
        }

        private void AdminLogIn()
        {
            webDriver.FindElement(By.XPath(@"//*[@id=""header""]/div[2]/div/div/div[2]/div/ul/li[4]/a")).Click();
            webDriver.FindElement(By.Id("LoginViewModel_Name")).SendKeys("Admin");
            webDriver.FindElement(By.Id("LoginViewModel_Password")).SendKeys("Admin_pass_321");
            webDriver.FindElement(By.XPath(@"//*[@id=""login_form""]/button")).Click();
            webDriver.FindElement(By.XPath(@"//*[@id=""header""]/div[2]/div/div/div[2]/div/ul/li[2]/a")).Click();
            webDriver.FindElement(By.XPath("//*[@id=\"main-menu\"]/li[2]/a")).Click();
        }

        protected void EnterTheNewItem()
        {
            //Кликаем на ссылку +new
            webDriver.FindElement(By.XPath(@"//*[@id=""page-inner""]/div/table/tbody/tr[1]/td/a")).Click();
            webDriver.FindElement(By.Id("Name")).SendKeys(_name);
            webDriver.FindElement(By.Id("ImgUrl")).SendKeys(_img);

            webDriver.FindElement(By.XPath(@"//*[@id=""page-inner""]/div/div[1]/div/form/div[5]/input")).Click();
        }

        [Fact]
        public void TitleCheck()
        {
            Assert.Equal("View - Admin", webDriver.Title);
        }

        [Fact]
        public void CanAddNewElementTest()
        {
            //arr/act
            EnterTheNewItem();

            //res
            //Что мы вернулись обратно в список
            Assert.Equal(@"http://localhost:5000/Admin/Products", webDriver.Url);

            Assert.Contains(//Проверка наличия имени записи.
                webDriver.FindElements(By.XPath(@"//*[@id=""page-inner""]/div/table/tbody/tr/td[2]")),
                td => td.Text == _name
            );
            Assert.Contains(//Проверка наличия картинки записи.
                webDriver.FindElements(By.XPath(@"//*[@id=""page-inner""]/div/table/tbody/tr/td[4]/img")),
                td => td.GetAttribute("alt") == _name && td.GetAttribute("src") == $@"http://localhost:5000/images/shop/{_img}"
            );
        }
    }
}
