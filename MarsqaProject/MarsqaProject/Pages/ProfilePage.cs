using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MarsqaProject.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver _driver;
        public ProfilePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public By serviceTab = By.XPath("//a[text()=\"Services\"]");
        public By languageTab = By.XPath("//a[text()=\"Services\"]");
        public By skillTab = By.XPath("//a[text()=\"Skills\"]");
        public By messageDiv = By.XPath("//div[@class=\"ns-box-inner\"]");
        public By messageCloseButton = By.XPath("//a[@class=\"ns-close\"]");

        public void ClickLanguagesTab()
        {
            Wait.WaitToBeVisible(_driver, languageTab);
            _driver.FindElement(languageTab).Click();
        }
        public void ClickSkillsTab()
        {
            Wait.WaitToBeVisible(_driver, skillTab);

            _driver.FindElement(skillTab).Click();
        }
        public void ClickServicesTab()
        {
            Wait.WaitToBeVisible(_driver, serviceTab);
            _driver.FindElement(serviceTab).Click();

        }

        public string GetMessage()
        {
            string message = _driver.FindElement(messageDiv).Text;
            Log.Information(message);
            return message;
        }

        public void ClickMessageCloseButton()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            IList<IWebElement> elements = _driver.FindElements(By.XPath("//a[@class='ns-close']"));
            Console.WriteLine(elements.Count);
            if (elements.Count>0)
            {
                _driver.FindElement(By.XPath("//a[@class='ns-close']")).Click();
            }
        }    
    }
    }