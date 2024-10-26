using OpenQA.Selenium;
using MarsqaProject.Utilities;
using MarsqaProject.Pages;
using OpenQA.Selenium.Internal.Logging;
using BoDi;
using Serilog;

namespace MarsqaProject.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver driver;
        private readonly HomePage homePage;

        // Constructor to inject contexts (No WebDriver needed here)
        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            driver = new CommonDriver(driver, _featureContext).GetWebDriver();  // Initialize driver here
            homePage = new HomePage(driver);  // Now pass the initialized driver
        }


        

        [BeforeScenario]
        public void BeforeScenario()
        {
           
            // Initialize WebDriver (only once per feature if needed)
            CommonDriver basicTestObj = new CommonDriver(driver, _featureContext);
            driver = basicTestObj.GetWebDriver(); // WebDriver is initialized here.
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver); // Register WebDriver

           
            
        }
               

        [AfterScenario]
        public void AfterScenario()
        {
           
                driver.Quit();
                
            }
        }

       
    }

