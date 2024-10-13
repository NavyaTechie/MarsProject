using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class ServicePage
    {
        public readonly By service_name = By.XPath("//span[@class = \"skill-title\"]");

        public string GetSkillTitle(IWebDriver driver)
        {
            Wait.WaitToBeVisible(driver, service_name);
            return driver.FindElement(service_name).Text;
        }
    }
}
