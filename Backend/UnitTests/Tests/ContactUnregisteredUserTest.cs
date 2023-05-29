using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace UnitTests.Tests
{
    public class ContactUnregisteredUserTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly HttpClient _client;

        public ContactUnregisteredUserTest()
        {
            _driver = new ChromeDriver();
            _client = new HttpClient();
        }

        [Fact]

        public void SubmitWorksForUnregistredUser()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");

            var loginButton = _driver.FindElement(By.XPath("//button[text()='Contact']"));
            loginButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Name("firstName")).Count > 0);

            var firstnameField = _driver.FindElement(By.Name("firstName"));
            var lastnameField = _driver.FindElement(By.Name("lastName"));
            var usernameField = _driver.FindElement(By.Name("email"));
            
            var messageField = _driver.FindElement(By.XPath("/html/body/div/div[2]/div/div/div/form/div/div[4]/div/div/textarea[1]"));


            //Enter user details 

            firstnameField.SendKeys("test_first_name");
            lastnameField.SendKeys("test_last_name");
            usernameField.SendKeys("test-email@gmail.com");
            messageField.SendKeys("test_message");

            var submitButton = _driver.FindElement(By.XPath("//button[@type='submit' and text()='Submit']"));
            submitButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                             d.FindElements(By.Id("snackbar")).Count > 0);
            var sentMessage = _driver.FindElement(By.Id("snackbar"));
            Assert.NotNull(sentMessage);
        }
        public void Dispose()
        {
            _driver.Quit();
        }

    }
}
