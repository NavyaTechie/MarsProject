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
        public readonly By signInButtonLocator = By.XPath("//A[@class='item'][text()='Sign In']");

        public readonly By emailTextBox = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input");

        public readonly By passwordTextBox = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input");

        public readonly By loginButton = By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button");

        public readonly By errorMessageLocator = By.XPath("//div[@class='ui negative message']");
        public void LoginActions(IWebDriver driver, string email, string password)
        {
            Wait.WaitToBeVisible(driver, emailTextBox);
            Wait.WaitToBeVisible(driver, passwordTextBox);
            driver.FindElement(emailTextBox).SendKeys(email);
            driver.FindElement(passwordTextBox).SendKeys(password);
            driver.FindElement(loginButton).Click();
        }

        public bool IsLoginFailed(IWebDriver driver)
        {
            // Wait for the error message to be visible
            try
            {
                Wait.WaitToBeVisible(driver, errorMessageLocator);
                // Check if error message is displayed
                return driver.FindElement(errorMessageLocator).Displayed;
            }
            catch (NoSuchElementException)
            {
                // If no error message, login did not fail
                return false;
            }
        }









    }
}
