using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebStore.UITests
{
    public abstract class LoginOrRegisterTestBase : TestBase
    {
        protected readonly IWebElement _loginNameInput;
        protected readonly IWebElement _registerNameInput;

        protected LoginOrRegisterTestBase(BrowserType browser) : base(browser)
        {
            webDriver.FindElement(By.XPath(@"//*[@id=""header""]/div[2]/div/div/div[2]/div/ul/li[4]/a")).Click();
            _loginNameInput = webDriver.FindElement(By.Id("LoginViewModel_Name"));
            _registerNameInput = webDriver.FindElement(By.Id("RegisterViewModel_Name"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void MouseHoveringInputsDisablingTest(bool hoverOnLoginForm)
        {
            //arrange
            var nameInput = hoverOnLoginForm ? _loginNameInput : _registerNameInput;
            //act
            new Actions(webDriver)
                .MoveToElement(nameInput)
                .Perform();
            //assert
            var secondFormInputs = webDriver.FindElements(By.ClassName(hoverOnLoginForm ? "_r_inp" : "_l_inp"));
            bool allDisable = secondFormInputs.All(i => !string.IsNullOrEmpty(i.GetAttribute("disabled")));
            Assert.True(allDisable);
        }

        [Theory]
        [InlineData("dasdasd 142", false)]
        [InlineData("some@email.com", true)]
        public void NameValidationTest(string name, bool shouldBeSuccess)
        {
            _registerNameInput.SendKeys(name);
            new Actions(webDriver)
                .SendKeys(Keys.Tab)
                .Perform();

            IWebElement nameErrPlate = null;

            var findErrPlate = () =>
                    nameErrPlate = webDriver.FindElement(By.Id("register_name_err"))
                        .FindElement(By.CssSelector("p"));

            if (shouldBeSuccess)
                Assert.Throws<NoSuchElementException>(findErrPlate);
            else
                findErrPlate.Invoke();

            if (!shouldBeSuccess)
                Assert.Equal("Name should be e-mail or consist of digit and letters.", nameErrPlate.Text);
            else
                Assert.Null(nameErrPlate);
        }

    }
}
