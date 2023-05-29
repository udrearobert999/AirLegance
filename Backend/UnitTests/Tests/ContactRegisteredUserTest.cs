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
    public class ContactRegisteredUserTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public ContactRegisteredUserTest()
        {
            _driver = new ChromeDriver();
        }
        
        [Fact]
        public void RegisterWorksForNewUser()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");
            //Do the login
            var loginButton1 = _driver.FindElement(By.XPath("//button[text()='Login']"));
            loginButton1.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Name("email")).Count > 0);

            var emailField = _driver.FindElement(By.Name("email"));
            var passwordField = _driver.FindElement(By.Name("password"));

            emailField.SendKeys("vasilescu.alexandru.a3m@student.ucv.ro");
            passwordField.SendKeys("Parola123-");

            var loginButton2 = _driver.FindElement(By.XPath("//button[@type='submit' and text()='Login']"));
            loginButton2.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Id("userCard")).Count > 0);

            //Do the contact
            var contactButton = _driver.FindElement(By.XPath("//button[text()='Contact']"));
            contactButton.Click();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(5)).Until(d =>
                            d.FindElements(By.Id("complaintMessageBox")).Count > 0);

            var messageField = _driver.FindElement(By.Id("complaintMessageBox"));

            messageField.SendKeys("MesajTest");

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
