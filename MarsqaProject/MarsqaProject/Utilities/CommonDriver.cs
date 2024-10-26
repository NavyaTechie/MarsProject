using MarsqaProject.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;


namespace MarsqaProject.Utilities
{
    public class CommonDriver 
    {

        private IWebDriver _driver;
        private LoginPage _loginPage;
        protected readonly FeatureContext _featureContext;

        public CommonDriver(IWebDriver driver, FeatureContext featureContext)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver);
            _featureContext = featureContext;
        }

        public bool IsUserLoggedIn()
        {
            return _featureContext.ContainsKey("IsLoggedIn") && (bool)_featureContext["IsLoggedIn"];
        }

        public void SetUserLoggedIn(bool isLoggedIn)
        {
            _featureContext["IsLoggedIn"] = isLoggedIn;
        }
        public IWebDriver GetWebDriver()
        {
            if (_driver == null)
            {
                GetNewWebDriver();
            }
            return _driver;
        }

        public void GetNewWebDriver()
        {
            string browserType = GetAppConfig("browserType");
            switch (browserType.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(),VersionResolveStrategy.MatchingBrowser);
                    _driver = new ChromeDriver();
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                default:
                    _driver = new ChromeDriver();
                    break;

            }
            _driver.Manage().Window.Maximize();
        }
        public string GetAppConfig(string key)
        {
            //get AppConfig.json directory

            string configPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory + "../../../").FullName
                  + "\\ConfigFile\\AppConfig.json";

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Config file not found:" + configPath);
            }
            string jsonContent = File.ReadAllText(configPath);
            JObject jsonData = JObject.Parse(jsonContent);
            return jsonData[key]?.ToString() ?? throw new KeyNotFoundException($"Key '{key}' not found in config");
        }


        public void PerformLogin()
        {
            string email = GetAppConfig("username");
            string password = GetAppConfig("password");
            _loginPage.ClickLoginButton(email, password);
        }

    }
}

