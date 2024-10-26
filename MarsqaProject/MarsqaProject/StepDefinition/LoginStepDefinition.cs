using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MarsqaProject.StepDefinition
{
    [Binding]
    

    public class LoginStepDefinition : CommonDriver
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;


        public LoginStepDefinition(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver);
            _homePage = new HomePage(_driver);
        }

        [Given(@"navigates to the login page")]
        public void GivenNavigatesToTheLoginPage()
        {
            string url = GetAppConfig("url");
            _driver.Navigate().GoToUrl(url);
            _homePage.ClickSignInLink();
        }

        [When(@"enter valid credentials and click the login button")]
        public void WhenEnterValidCredentialsAndClickTheLoginButton()
        {
            
            PerformLogin();
        }

        [Then(@"should be redirected to the profile page")]
        public void ThenShouldBeRedirectedToTheProfilePage()
        {

            string profileUrl = GetAppConfig("profileUrl");
            string currentUrl = _driver.Url;
            
        }

    }
}
