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

namespace MarsqaProject.Utilities
{
    public class BasicTests:CommonDriver 
    {

        public static IWebDriver driver;
        public readonly FeatureContext _featureContext;
        LoginPage loginPage = new LoginPage();
        LanguagePage languagePage = new LanguagePage();
        SkillPage skillPage = new SkillPage();
        private static object configPath;

        public BasicTests(FeatureContext featureContext)
        {

            _featureContext = featureContext;
        }

        public static IWebDriver GetWebDriver(IWebDriver? driver)
        {
            if (driver == null)
            {
                GetNewWebDriver();
            }
            return driver;
        }

        public static void GetNewWebDriver()
        {
            string browserType = GetApplicationConfig("browserType");
            switch (browserType.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;

            }
            driver.Manage().Window.Maximize();
        }
        public static string GetApplicationConfig(string key)
        {
            try
            {
                // Construct the path to the config file
                string configPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory + "../../../").FullName, "ConfigFile", "specflow.json");

                // Read the contents of the file
                string jsonContent = File.ReadAllText(configPath);

                // Parse the JSON
                JObject jsonData = JObject.Parse(jsonContent);

                // Check if the key exists
                if (jsonData[key] != null)
                {
                    return jsonData[key].ToString();
                }
                else
                {
                    throw new KeyNotFoundException($"Key '{key}' not found in the config file.");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The configuration file was not found at {configPath}.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Failed to parse the configuration file.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the configuration.", ex);
            }
        }

        public bool IsUserLoggedIn()
        {
            return _featureContext.ContainsKey("IsLoggedIn") && (bool)_featureContext["IsLoggedIn"];

        }

        public void SetUserLoggedIn(bool isLoggedIn)
        {
            _featureContext["IsLoggedIn"] = isLoggedIn;
        }

        public static void PerformLogin()
        {
            string email = GetApplicationConfig("username");
            string password = GetApplicationConfig("password");
            LoginPage loginPage = new LoginPage();
            loginPage.LoginActions(driver, email, password);
        }
    }
}

