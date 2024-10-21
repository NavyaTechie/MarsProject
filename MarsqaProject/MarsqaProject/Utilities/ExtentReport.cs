using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Utilities
{
    public class ExtentReport
    {/*
        public static ExtentReport _extentReport;

        public static string dir = AppDomain.CurrentDomain.BaseDirectory;

        public static string testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestReport");
        public static ExtentTest test;
        public static void InitializeReport()
        {
            Console.WriteLine("Current Dir:" + dir);
            var htmlReporter = new ExtentSparkReporter(testResultPath + "\\extent-report.html");
            htmlReporter.Config.ReportName = "Mars Test Report";
            htmlReporter.Config.DocumentTitle = "Mars Test Report";
            htmlReporter.Config.Theme = Theme.Standard;


            _extentReport = new ExtentReport();
            _extentReport.AttachReporter(htmlReporter);
            _extentReport.AddSystemInfo("Application", "MVP-Mars");
            _extentReport.AddSystemInfo("Browser", "Chrome");
            _extentReport.AddSystemInfo("OS", "Windows");
        }

        public static void ExtentReportTearDown()
        {
            _extentReport.Flush();

        }
        public static void CreateTest(string testName)
        {
            test = _extentReport.CreateTest(testName);
        }

        public static string AddScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;

            string filePath = dir.Replace("bin\\Debug\\net6.0", "TestReport");

            ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takesScreenshot.GetScreenshot();
            string screenshotLocation = Path.Combine(filePath, scenarioContext.ScenarioInfo.Title + ".png");
            screenshot.SaveAsFile(screenshotLocation);
            return screenshotLocation;
        } */
    }
        

}


