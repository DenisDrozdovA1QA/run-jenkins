using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{
    [AllureNUnit]
    [AllureSuite("Cookie")]
    [AllureEpic("Web interface")]
    [AllureFeature("Essential features")]

    public class UnitTest2
    {
        private WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        [AllureStory("Labels")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Den")]
        [AllureLink("Website", "https://dev.example.com/")]
        [AllureIssue("UI-321")]
        [AllureTms("TMS-789")]
        public void DeleteCookie()
        {

            driver.Url = "https://the-internet.herokuapp.com";
            var cookie = new OpenQA.Selenium.Cookie("testkey", "testvalue");
            driver.Manage().Cookies.AddCookie(cookie);
            var cookies = driver.Manage().Cookies.AllCookies;
            Assert.IsTrue(cookies.Contains(cookie), "Cookie wasnt added");
            driver.Manage().Cookies.DeleteCookie(cookie);
            cookies = driver.Manage().Cookies.AllCookies;
            Assert.IsFalse(cookies.Contains(cookie), "Cookie was added");
        }

        [TearDown]
        public void Quit()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenShot = ((ITakesScreenshot) driver).GetScreenshot().AsByteArray;
                AllureLifecycle.Instance.AddAttachment("Failed", "image/png", screenShot);
            }
            driver.Quit();
        }

    }
}
