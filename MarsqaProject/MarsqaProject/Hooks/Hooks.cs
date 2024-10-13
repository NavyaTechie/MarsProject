
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using MarsqaProject.Utilities;
using MarsqaProject.Pages;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Internal.Logging;

namespace MarsqaProject.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        public readonly FeatureContext _featureContext;

        public Hooks(FeatureContext featureContext) : base(featureContext)
        { 
        
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.InitializeLogger();
            Log.Information("Starting test run");

            ExtentReport.InitializeReport();
        }

        [BeforeScenario("@LoginRequired")]
        public void BeforeScenarioWithLogin()
        {


        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {


        }
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Log.Information("Starting Scenario: {scenarioTitle}", scenarioContext.ScenarioInfo.Title);
            ExtentReport.CreateTest(scenarioContext.ScenarioInfo.Title);
            ExtentReport.test.Log(Status.Info, "Starting Scenario:" + scenarioContext.ScenarioInfo.Title);
        }
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError == null)
            {
                ExtentReport.test.Log(Status.Pass, scenarioContext.StepContext.StepInfo.Text);
            }
            else
            {
                string screenshotPath = ExtentReport.AddScreenshot(driver, scenarioContext);
                ExtentReport.test.Log(Status.Fail, scenarioContext.StepContext.StepInfo.Text + " - Error: " + scenarioContext.TestError.Message);
                ExtentReport.test.AddScreenCaptureFromPath(screenshotPath);
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                ExtentReport.test.Log(Status.Fail, "Scenario Failed");
            }
            else
            {
                ExtentReport.test.Log(Status.Pass, "Scenario Passed");
            }
        }


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Log.Information("Starting Feature: {featureTitle}", featureContext.FeatureInfo.Title);

            //Initialize WebDriver only once for the entire feature
            GetWebDriver();
            string url = GetApplictionConfig("url");
            driver.Navigate().GoToUrl(url);
            HomePage homePage = new HomePage();
            homePage.ClickSignInLink(driver);


            // Check if the feature has the @NoBeforeFeature tag
            bool skipBeforeFeature = featureContext.FeatureInfo.Tags.Contains("NoBeforeFeature");
            if (skipBeforeFeature)
            {

                Console.WriteLine(" Skipping BeforeFeature hook fo this feature");
                return;
            }

            // Log in only once for the entire feature
            BaseTest baseTestInstance = new BaseTest(featureContext);
            if (!baseTestInstance.IsUserLoggedIn())
            {
                PerformLogin();
                baseTestInstance.SetUserLoggedIn(true);
            }


        }
        [AfterFeature]
        public static void AfterFeature()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            Log.Information("Test run completed");
            Logger.CloseAndFlushLogger();
            ExtentReport.ExtentReportTearDown();
        }
    }
}
