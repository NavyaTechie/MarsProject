using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace MarsqaProject.StepDefinition
{
    [Binding]
    public class SearchSkillStepDefinition : BasicTests
    {
        HomePage homePage;
        SearchSkillPage searchSkillPage;
        LoginPage loginPage;
        ProfilePage profilePage;
        ServicePage servicePage;
        private ScenarioContext scenarioContext;

        public SearchSkillStepDefinition(FeatureContext featureContext) : base(featureContext)
        {

        }

        [Given(@"I navigate to the homepage")]
        public void GivenINavigateToTheHomepage()
        {
            Log.Information("Navigating to the home page");
            string url = GetApplictionConfig("url");
            driver.Navigate().GoToUrl(url);
            homePage = new HomePage();
        }

        [When(@"I enter a skill ""([^""]*)"" into the search bar")]
        public void WhenIEnterASkillIntoTheSearchBar(string skill)
        {
            homePage.InputInSearchBox(driver, skill);
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            homePage.ClickOnSearchButton(driver);
        }

        [Then(@"I should see a list of services matching the entered skill")]
        public void ThenIShouldSeeAListOfServicesMatchingTheEnteredSkill()
        {
            searchSkillPage = new SearchSkillPage();
        }

        [When(@"I click the ""([^""]*)"" infomation and I have logged in the system")]
        public void WhenIClickTheInfomationAndIHaveLoggedInTheSystem(string seller)
        {
            Log.Information("When I click the seller's name and I have logged in the system");
            searchSkillPage.ClickSellerName(driver, seller);
            if (!IsUserLoggedIn())
            {
                string email = GetApplictionConfig("username");
                string password = GetApplictionConfig("password");
                loginPage = new LoginPage();
                loginPage.LoginActions(driver, email, password);
                SetUserLoggedIn(true);
                Console.WriteLine("User logged in successfully.");
            }
        }

        [Then(@"The system should display the seller's details")]
        public void ThenTheSystemShouldDisplayTheSellersDetails()
        {
            profilePage = new ProfilePage();
            profilePage.ClickServicesTab(driver);
            Thread.Sleep(2000);
            profilePage.ClickLanguagesTab(driver);
            Thread.Sleep(2000);
            profilePage.ClickSkillsTab(driver);
        }

        [When(@"I click the ""([^""]*)"" infomation")]
        public void WhenIClickTheInfomation(string p0)
        {
            scenarioContext["service"] = service;
            searchPage.CLickServiceName(driver, service);
        }
    

        [Then(@"The system should display the details")]
        public void ThenTheSystemShouldDisplayTheDetails()
        {
            servicePage = new ServicePage();
            string display_skill_name = servicePage.GetSkillTitle(driver);
            string _service = scenarioContext["service"].ToString();
            Assert.IsTrue(_service.Equals(display_skill_name));
        }

    }
}
