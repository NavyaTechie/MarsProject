using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class HomePage
    {
        public readonly By signinLink = By.XPath("//a[text()='Sign In']");
        public readonly By searchText = By.XPath("//input[@placeholder=\"What skill would you like to trade?\"]");
        public readonly By searchButton = By.XPath("//button[text()=\"Search\"]");

        public void ClickOnSignInLink(IWebDriver driver)
        {
            driver.FindElement(signinLink).Click();
        }

        public void InputInSearchBox(IWebDriver driver, string search)
        {
            driver.FindElement(searchText).SendKeys(search);
        }
        public void ClickOnSearchButton(IWebDriver driver)
        {
            driver.FindElement(searchButton).Click();
        }
    }
}
