using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsqaProject.Utilities;

namespace MarsqaProject.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public readonly By signIn = By.XPath("//a[text()='Sign In']");
        public readonly By searchInputBar = By.XPath("//input[@placeholder='What skill would you like to trade?']");
        public readonly By searchButton = By.XPath("//button[text()='Search']");

        public void ClickSignInLink()
        {
            Wait.WaitToBeClickable(_driver, signIn);
            _driver.FindElement(signIn).Click();
        }

        public void InputSearchString(string search)
        {
            Wait.WaitToBeClickable(_driver, searchInputBar);
            _driver.FindElement(searchInputBar).SendKeys(search);
        }
        public void ClickSearchButton()
        {
            Wait.WaitToBeClickable(_driver, searchButton);
            _driver.FindElement(searchButton).Click();
        }

    }
}



