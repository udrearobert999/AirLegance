using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests
{
    public class RegisterTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public RegisterTest()
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void RegisterWorksForNewUser()
        {
            Random randomNumber = new Random();
            string email = "testemail" + randomNumber.Next(100, 10000000) + "@gmail.com";

            _driver.Navigate().GoToUrl("http://localhost:3000/");

            //Do the register
            var registerButton1 = _driver.FindElement(By.XPath("//button[text()='Register']"));
            registerButton1.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Name("email")).Count > 0);

            var firstnameField = _driver.FindElement(By.Name("firstName"));
            var lastnameField = _driver.FindElement(By.Name("lastName"));
            var emailFieldRegister = _driver.FindElement(By.Name("email"));
            var passwordFieldRegister = _driver.FindElement(By.Name("password"));
 
            firstnameField.SendKeys("testFirst");
            lastnameField.SendKeys("testLast");
            emailFieldRegister.SendKeys(email);
            passwordFieldRegister.SendKeys("TestPassword123-");

            var registerButton2 = _driver.FindElement(By.XPath("//button[@type='submit' and text()='Register']"));
            registerButton2.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.XPath("//button[text()='Login']")).Count > 0);
            //int waitDurationInSeconds = 10;
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitDurationInSeconds));
            //Thread.Sleep(TimeSpan.FromSeconds(waitDurationInSeconds));

            //Do the login
            var loginButton1 = _driver.FindElement(By.XPath("//button[text()='Login']"));
            loginButton1.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Name("email")).Count > 0);

            var emailFieldLogin = _driver.FindElement(By.Name("email"));
            var passwordFieldLogin = _driver.FindElement(By.Name("password"));

            emailFieldLogin.SendKeys(email);
            passwordFieldLogin.SendKeys("TestPassword123-");

            var loginButton2 = _driver.FindElement(By.XPath("//button[@type='submit' and text()='Login']"));
            loginButton2.Click();

            //Verify
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
