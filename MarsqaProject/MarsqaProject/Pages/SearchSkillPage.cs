using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class SearchSkillPage
    {
        private readonly IWebDriver _driver;
        public SearchSkillPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public By seller_name;
        public By service_name;

        public void ClickSellerName(string username)
        {

            seller_name = By.XPath("//a[@class=\"seller-info\"][text()=\"" + username + "\"]");
            Wait.WaitToBeVisible(_driver, seller_name);
            _driver.FindElement(seller_name).Click();
        }

        public void CLickServiceName(string serviceName)
        {

            service_name = By.XPath("//a[@class=\"service-info\"]//p[text()=\"" + serviceName + "\"]");
            Wait.WaitToBeVisible(_driver, service_name);
            _driver.FindElement(service_name).Click();

        }

    }
}

