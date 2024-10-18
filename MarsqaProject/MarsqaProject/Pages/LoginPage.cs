using MarsqaProject.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement email_input => _driver.FindElement(By.XPath("//input[@name=\"email\"]"));
        private IWebElement password_input => _driver.FindElement(By.XPath("//input[@name=\"password\"]"));
        private IWebElement login_button => _driver.FindElement(By.XPath("//button[text()=\"Login\"]"));

        public void ClickLoginButton(string email, string password)
        {

            email_input.SendKeys(email);
            password_input.SendKeys(password);
            login_button.Click();
        }
    }
}









    

