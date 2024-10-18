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
        public readonly By signin_link = By.XPath("//a[text()='Sign In']");
        public readonly By search_input = By.XPath("//input[@placeholder=\"What skill would you like to trade?\"]");
        public readonly By search_button = By.XPath("//button[text()=\"Search\"]");

        public void ClickSignInLink()
        {
            Wait.WaitToBeClickable(_driver, signin_link);
            _driver.FindElement(signin_link).Click();
        }

        public void InputSearchString(string search)
        {
            Wait.WaitToBeClickable(_driver, search_input);
            _driver.FindElement(search_input).SendKeys(search);
        }
        public void ClickSearchButton()
        {
            Wait.WaitToBeClickable(_driver, search_button);
            _driver.FindElement(search_button).Click();
        }

    }
}



