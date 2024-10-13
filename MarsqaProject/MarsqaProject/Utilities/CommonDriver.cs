using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver { get; set; } 
        public static void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        public static void Quit()
        {
            driver.Quit();
        }

    }
}

