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
        public By seller_name;
        public By service_name;
        public readonly By search_skill_input = By.XPath("");
        public readonly By search_user_input = By.XPath("");

        public void ClickSellerName(IWebDriver driver, string username)
        {

            seller_name = By.XPath("//a[@class=\"seller-info\"][text()=\"" + username + "\"]");
            Wait.WaitToBeVisible(driver, seller_name);
            driver.FindElement(seller_name).Click();
        }

        public void CLickServiceName(IWebDriver driver, string serviceName)
        {

            service_name = By.XPath("//a[@class=\"service-info\"]//p[text()=\"" + serviceName + "\"]");
            Wait.WaitToBeVisible(driver, service_name);
            driver.FindElement(service_name).Click();
        }
    }
}
