using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver _driver;
        public ProfilePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public By service_tab = By.XPath("//a[text()=\"Services\"]");
        public By language_tab = By.XPath("//a[text()=\"Services\"]");
        public By skill_tab = By.XPath("//a[text()=\"Skills\"]");
        public By message_div = By.XPath("//div[@class=\"ns-box-inner\"]");
        public By message_close_button = By.XPath("//a[@class=\"ns-close\"]");

        public void ClickLanguagesTab()
        {
            Wait.WaitToBeVisible(_driver, language_tab);
            _driver.FindElement(language_tab).Click();
        }
        public void ClickSkillsTab()
        {
            Wait.WaitToBeVisible(_driver, skill_tab);

            _driver.FindElement(skill_tab).Click();
        }
        public void ClickServicesTab()
        {
            Wait.WaitToBeVisible(_driver, service_tab);
            _driver.FindElement(service_tab).Click();

        }

        public string GetMessage()
        {
            string message = _driver.FindElement(message_div).Text;
            Log.Information(message);
            return message;
        }

        public void ClickMessageCloseButton()
        {
            _driver.FindElement(message_close_button).Click();
        }    
    }
    }