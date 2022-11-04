using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebStore.UITests.TestBase;

namespace WebStore.UITests.ChromeBrowser
{
    public class LoginAndRegisterChromeTests : LoginOrRegisterTestBase
    {
        public LoginAndRegisterChromeTests() : base(BrowserType.Chrome)
        { }
    }
}
