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
    

    public class LoginStepDefinition : BasicTests
    {
        HomePage homePage;
        public IWebDriver driver;
        public LoginPage loginPage;
        public LoginStepDefinition(FeatureContext featureContext) : base(featureContext)
        {
        }

        [Given(@"navigates to the login page")]
        public void GivenNavigatesToTheLoginPage()
        {
            string url = GetApplicationConfig("url");
            driver.Navigate().GoToUrl(url);
           // driver = new ChromeDriver();
            homePage = new HomePage();
            homePage.ClickOnSignInLink(driver);

        }

        [When(@"enter valid credentials and click the login button")]
        public void WhenEnterValidCredentialsAndClickTheLoginButton()
        {
             if (!IsUserLoggedIn())
            {
                PerformLogin();
                SetUserLoggedIn(true);
                Console.WriteLine("User logged in successfully.");
            }
            else
            {
                Console.WriteLine("User is already logged in, skipping login.");
            }
            

           /* string email = GetApplictionConfig("username");
            string password = GetApplictionConfig("password");
            loginPage = new LoginPage();
            loginPage.ClickLoginButton(driver, email, password);
           */

        }

        [Then(@"should be redirected to the profile page")]
        public void ThenShouldBeRedirectedToTheProfilePage(string profileUrl)
        {
            string expectedProfileUrl = GetApplicationConfig("profileUrl");
            string currentUrl = driver.Url;

            // Use Assert.AreEqual to compare the two URLs
            Assert.ReferenceEquals(expectedProfileUrl, currentUrl, "The user is not redirected to the profile page.");

            // Verify if the user is logged in
            Assert.Pass(IsUserLoggedIn(), "User login failed.");
        }

        [When(@"enter valid credentials and click the login button")]
        public void WhenEnterValidCredentials()
        {
            
        }

        [Then(@"should be redirected to the profile page")]
        public void ThenShouldBeRedirectedToTheProfilePage()
        {
            throw new PendingStepException();
        }

        [When(@"I enter an invalid email ""([^""]*)""")]
        public void WhenIEnterAnInvalidEmail(string email)
        {
            loginPage.LoginActions(driver, email, "");
        }

        [When(@"I enter an invalid password ""([^""]*)""")]
        public void WhenIEnterAnInvalidPassword(string password)
        {
            loginPage.LoginActions(driver, "", password);
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            driver.FindElement(loginPage.loginButton).Click();
        }

        [Then(@"I should see an error message ""([^""]*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            bool isErrorMessageDisplayed = loginPage.IsLoginFailed(driver);
            Assert.Equals(isErrorMessageDisplayed, "Error message was not displayed.");
            driver.Quit();
        }




    }
}
