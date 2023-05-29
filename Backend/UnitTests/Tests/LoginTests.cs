using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UnitTests.Tests
{
    public class LoginTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly HttpClient _client;

        public LoginTests()
        {
            _driver = new ChromeDriver();
            _client = new HttpClient();
        }

        [Fact]
        public void LoginWorksForRegisteredUser()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");

            var loginButton = _driver.FindElement(By.XPath("//button[text()='Login']"));
            loginButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Name("email")).Count > 0);

            var usernameField = _driver.FindElement(By.Name("email"));
            var passwordField = _driver.FindElement(By.Name("password"));

            // Enter user details for login
            usernameField.SendKeys("test-email@gmail.com");
            passwordField.SendKeys("TestPassword123-");

            var submitButton = _driver.FindElement(By.XPath("//button[@type='submit' and text()='Login']"));
            submitButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Id("userCard")).Count > 0);
            var welcomeMessage = _driver.FindElement(By.Id("userCard"));
            Assert.NotNull(welcomeMessage);
        }
        public void Dispose()
        {
           _driver.Quit();
        }
    }
}