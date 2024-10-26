using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace MarsqaProject.StepDefinition
{
    [Binding]
    public class SearchSkillStepDefinition : CommonDriver
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private SearchSkillPage _searchPage;
        private LoginPage _loginPage;
        private ProfilePage _profilePage;
        private ServicePage _servicePage;
        public ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        public SearchSkillStepDefinition(IWebDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext) : base(driver, featureContext)
        {
            _driver = driver;
            _homePage = new HomePage(_driver);
            _searchPage = new SearchSkillPage(_driver);
            _loginPage = new LoginPage(_driver);
            _profilePage = new ProfilePage(_driver);
            _servicePage = new ServicePage(_driver);
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"I navigate to the homepage")]
        public void GivenINavigateToTheHomepage()
        {
            Log.Information("Navigating to the home page");
            string url = GetAppConfig("url");
            _driver.Navigate().GoToUrl(url);


        }

        [When(@"I enter a skill ""([^""]*)"" into the search bar")]
        public void WhenIEnterASkillIntoTheSearchBar(string skill)
        {
            _homePage.InputSearchString(skill);
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            _homePage.ClickSearchButton();
        }

        [Then(@"I should see a list of services matching the entered skill")]
        public void ThenIShouldSeeAListOfServicesMatchingTheEnteredSkill()
        {

        }

        [When(@"I click the ""([^""]*)"" infomation and I have logged in the system")]
        public void WhenIClickTheInfomationAndIHaveLoggedInTheSystem(string seller)
        {
            Log.Information("When I click the seller's name and I have logged in the system");
            _searchPage.ClickSellerName(seller);
            if (!IsUserLoggedIn())
            {
                string email = GetAppConfig("username");
                string password = GetAppConfig("password");
                 
            _loginPage.ClickLoginButton(email, password);
            SetUserLoggedIn(true);
            Log.Information("User logged in successfully.");
           } 


        }

        [Then(@"The system should display the seller's details")]
        public void ThenTheSystemShouldDisplayTheSellersDetails()
        {

            _profilePage.ClickServicesTab();

            _profilePage.ClickLanguagesTab();

            _profilePage.ClickSkillsTab();
        }

        [When(@"I click the ""([^""]*)"" infomation")]
        public void WhenIClickTheInfomation(string service)
        {
            _scenarioContext["service"] = service;
            _searchPage.CLickServiceName(service);
        }

        [Then(@"The system should display the details")]
        public void ThenTheSystemShouldDisplayTheDetails()
        {
            string display_skill_name = _servicePage.GetSkillTitle();
            string _service = _scenarioContext["service"].ToString();
            Assert.That(_service.Equals(display_skill_name));
        }

    }
}
