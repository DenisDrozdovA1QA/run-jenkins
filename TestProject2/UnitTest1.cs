using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("TextBox")]
    [AllureEpic("Web interface")]
    [AllureFeature("Essential features")]
    public class Tests
    {
        private WebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        [AllureStory("Eamil")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("John Doe")]
        [AllureLink("Website", "https://dev.example.com/")]
        [AllureIssue("UI-123")]
        [AllureTms("TMS-456")]
        public void EnterEmail()
        {
            var email = "something@example.com";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = "https://the-internet.herokuapp.com/forgot_password";
            var emailLocator = RelativeBy.WithLocator(By.CssSelector("#email")).Above(By.CssSelector("#form_submit"));
            var input = driver.FindElement(emailLocator);
            wait.Until(e => input.Displayed);
            input.SendKeys(email);
        }

        [Test]
        [AllureStory("Eamil")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("John Doe")]
        [AllureLink("Website", "https://dev.example.com/")]
        [AllureIssue("UI-233")]
        [AllureTms("TMS-346")]
        public void EnterEmailFailed()
        {
            var email = "something@example.com";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = "https://the-internet.herokuapp.com/forgot_password";
            var emailLocator = RelativeBy.WithLocator(By.CssSelector("#email")).Above(By.CssSelector("#form_submit"));
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                var input = driver.FindElement(emailLocator);
                wait.Until(e => input.Displayed);
                input.SendKeys(email);
            }, "Input email");

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                Assert.IsTrue(true);
            }, "Assert");

            
        }

        [TearDown]
        public void Quit()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var screenShot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                AllureLifecycle.Instance.AddAttachment("Failed", "image/png", screenShot);
            }
            driver.Quit();
        }
    }
}