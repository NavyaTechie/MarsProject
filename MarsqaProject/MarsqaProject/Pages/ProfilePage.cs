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
        private readonly IWebDriver driver;
        public ProfilePage(IWebDriver Driver)
        {
            Driver = driver;
        }

        public By serviceTab = By.XPath("//a[text()=\"Services\"]");
        public By languageTab = By.XPath("//a[text()=\"Services\"]");
        public By skillTab = By.XPath("//a[text()=\"Skills\"]");
        public By messageDiv = By.XPath("//div[@class=\"ns-box-inner\"]");
        public By messageCloseButton = By.XPath("//a[@class=\"ns-close\"]");

        public void ClickLanguagesTab()
        {
            Wait.WaitToBeVisible(driver, languageTab);
            driver.FindElement(languageTab).Click();
        }
        public void ClickSkillsTab()
        {
            Wait.WaitToBeVisible(driver, skillTab);

            driver.FindElement(skillTab).Click();
        }
        public void ClickServicesTab()
        {
            Wait.WaitToBeVisible(driver, serviceTab);
            driver.FindElement(serviceTab).Click();

        }

        public string GetMessage()
        {
            string message = driver.FindElement(messageDiv).Text;
            Log.Information(message);
            return message;
        }

        public void ClickMessageCloseButton()
        {
            driver.FindElement(messageCloseButton).Click();
        }    
    }
    }