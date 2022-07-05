using AventStack.ExtentReports;
using NUnit.Framework;
using SeleniumNUnitExtentReport.Config;
using SeleniumNUnitExtentReport.PageMethods;

namespace SeleniumNUnitExtentReport.TestCases
{
    [TestFixture]
    public class LoginTest : ExtentManager
    {
        ExtentReports rep = ExtentManager.Onetime();
        LoginPage loginPage;
      
        [Test]
        [Category("Login")]
        public void test_validLogin()
        {
            loginPage = new LoginPage(GetDriver());
            _driver.Url = "http://datapp32/DAConsentEngineAPI/DAConsentEngine/Registration";           
        }

        

    }
}
