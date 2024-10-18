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
        private readonly IWebDriver _driver;
        public ServicePage(IWebDriver driver)
        {
            _driver = driver;
        }


        private IWebElement service_name => _driver.FindElement(By.XPath("//span[@class = \"skill-title\"]"));
        public string GetSkillTitle()
        {

            return service_name.Text;
        }
        
    }
}
