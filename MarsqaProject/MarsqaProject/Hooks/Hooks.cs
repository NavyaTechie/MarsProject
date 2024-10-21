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
            driver = new BasicTests(driver, _featureContext).GetWebDriver(); // Initialize driver here
            homePage = new HomePage(driver); // Now pass the initialized driver
        }


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            object value = NUnit.Framework.Internal.Logger.InitializeLogger();
           // Log.Information("Starting test run");
          //  ExtentReport.InitializeReport();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
           // OpenQA.Selenium.Internal.Logging.Log.Information("Starting Scenario: {scenarioTitle}", _scenarioContext.ScenarioInfo.Title);
           // ExtentReport.CreateTest(_scenarioContext.ScenarioInfo.Title);
            //ExtentReport.test.Log(Status.Info, "Starting Scenario: " + _scenarioContext.ScenarioInfo.Title);

            // Initialize WebDriver (only once per feature if needed)
            BasicTests basicTestObj = new BasicTests(driver, _featureContext);
            driver = basicTestObj.GetWebDriver(); // WebDriver is initialized here.
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver); // Register WebDriver

           
            // Handle login only once per feature (if @NoBeforeFeature is used)
            /* bool skipBeforeFeature = _featureContext.FeatureInfo.Tags.Contains("NoBeforeFeature");
             if (skipBeforeFeature)
             {
                 Log.Information("Skipping BeforeFeature hook for this feature");
                 return;
             }
             // Log in only once per feature
             if (!baseTestInstance.IsUserLoggedIn())
             {
                 string url = baseTestInstance.GetApplictionConfig("url");
                 _driver.Navigate().GoToUrl(url);
                 _homePage.ClickSignInLink();
                 baseTestInstance.PerformLogin();
                 baseTestInstance.SetUserLoggedIn(true);
             }*/
        }

        [AfterStep]
        public void AfterStep()
        { /*
            if (_scenarioContext.TestError == null)
            {
                ExtentReport.test.Log(Status.Pass, _scenarioContext.StepContext.StepInfo.Text);
            }
            else
            {
                string screenshotPath = ExtentReport.AddScreenshot(driver, _scenarioContext);
                ExtentReport.test.Log(Status.Fail, _scenarioContext.StepContext.StepInfo.Text + " - Error: " + _scenarioContext.TestError.Message);
                ExtentReport.test.AddScreenCaptureFromPath(screenshotPath);
            }  */
        }

        [AfterScenario]
        public void AfterScenario()
        {
           /* if (_scenarioContext.TestError != null)
            {
                ExtentReport.test.Log(Status.Fail, "Scenario Failed");
            }
            else
            {
                ExtentReport.test.Log(Status.Pass, "Scenario Passed");
            } */

            // Quit and clean up WebDriver
           // if (driver != null)
            //{
                driver.Quit();
                //driver.Dispose();
                //driver = null; // Optional cleanup
            }
        }

       /* [AfterTestRun]
        public static void AfterTestRun()
        {
            Log.Information("Test run completed");
            NUnit.Framework.Internal.Logger.CloseAndFlushLogger();
            ExtentReport.ExtentReportTearDown();
        } */
    }

