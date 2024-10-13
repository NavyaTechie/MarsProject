using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace MarsqaProject.StepDefinition
{
    public class LanguagesStepDefinition : BasicTests
    {
        LoginPage loginPage;
        HomePage homePage;
        LanguagePage languagePage;
        ProfilePage profilePage;
        int row_count = 0;

        public LanguagesStepDefinition(FeatureContext featureContext) : base(featureContext)
        {
        }

        [Given(@"navigate to the language tab")]
        public void GivenNavigateToTheLanguageTab()
        {
            languagePage = new LanguagePage();
            languagePage.NavigateToLanguageTab(driver);
        }

        [Given(@"click the Add New Button")]
        public void GivenClickTheAddNewButton()
        {
            languagePage.ClickAddNewButton(driver);
        }

        [When(@"input the  ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string english, string level)
        {
            languagePage.EnterNewLanguage(driver, "new", english, level);
            languagePage.ClickAddButton(driver);
        }

        [Then(@"a ""([^""]*)"" will be display to show the result")]
        public void ThenAWillBeDisplayToShowTheResult(string message)
        {
            Console.WriteLine(message);
            profilePage = new ProfilePage();
            string originalResult = profilePage.GetMessage(driver);
            Assert.That(originalResult.Contains(message));
            profilePage.ClickMessageCloseButton(driver);

           
        }

        [Given(@"click on the Add New Button")]
        public void GivenClickOnTheAddNewButton()
        {
            languagePage.ClickAddNewButton(driver);    
        }

        [When(@"input the  valid information and click the Cancel button")]
        public void WhenInputTheValidInformationAndClickTheCancelButton(Table table)
        {
            
        }

        [Then(@"no new language is created")]
        public void ThenNoNewLanguageIsCreated()
        {
            throw new PendingStepException();
        }

        [Given(@"click the edit Button of a ""([^""]*)""")]
        public void GivenClickTheEditButtonOfA(string malayalam)
        {
            languagePage.ClickEditIconOfALanguage(driver, malayalam);
        }

        [When(@"change the ""([^""]*)"" and ""([^""]*)"" and click Update button")]
        public void WhenChangeTheAndAndClickUpdateButton(string hindi, string basic)
        {
            languagePage.EnterNewLanguage(driver,"edit",hindi,basic);
            languagePage.ClickUpdateButton(driver);
        }

        [When(@"click on the delete Button of a ""([^""]*)""")]
        public void WhenClickOnTheDeleteButtonOfA(string english)
        {
            languagePage.ClickDeleteIconOfALanguage(driver,english);
        }

        [When(@"the user has exactly (.*) languages")]
        public void WhenTheUserHasExactlyLanguages(string p0)
        {
            row_count = languagePage.GetRowCount(driver,"Languages");
        }

        [Then(@"the button should not be visible")]
        public void ThenTheButtonShouldNotBeVisible()
        {
            bool isButtonDisplayed = driver.FindElement(languagePage.addNewButton).Displayed;
            if (row_count == 4)
            {

                Assert.Fail(isButtonDisplayed);
            }
            else
            {
                Assert.Pass(isButtonDisplayed);
            }
        }

        [When(@"the user has fewer than (.*) languages")]
        public void WhenTheUserHasFewerThanLanguages(int p0)
        {
            row_count = languagePage.GetRowCount(driver, "Languages");
        }

        [Then(@"the button should be visible")]
        public void ThenTheButtonShouldBeVisible()
        {
            bool isButtonDisplayed = driver.FindElement(languagePage.addNewButton).Displayed;
            if (row_count == 4)
            {

                Assert.IsTrue(isButtonDisplayed);
            }
            else
            {
                Assert.IsFalse(isButtonDisplayed);
            }
        }
        
        [When(@"input the  ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string english, string fluent)
        {
            driver.FindElement(By.XPath("//input[@name='language']")).SendKeys(english);
            driver.FindElement(By.XPath("//select[@name='level']")).SendKeys(fluent);
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();
        }

        [Then(@"a ""([^""]*)"" will be display to show the result")]
        public void ThenAWillBeDisplayToShowTheResult(string message)
        {
            string actualMessage = driver.FindElement(By.XPath("//div[@class='message']")).Text;
            Assert.AreEqual(message, actualMessage, "The expected message was not displayed.");
        }

        
        [When(@"input the  valid information and click the Cancel button")]
        public void WhenInputTheValidInformationAndClickTheCancelButton(Table table)
        {
            throw new PendingStepException();
        }

        [Then(@"no new language is created")]
        public void ThenNoNewLanguageIsCreated()
        {
            throw new PendingStepException();
        }

        [Given(@"click the edit Button of a ""([^""]*)""")]
        public void GivenClickTheEditButtonOfA(string malayalam)
        {
            throw new PendingStepException();
        }

        [When(@"change the ""([^""]*)"" and ""([^""]*)"" and click Update button")]
        public void WhenChangeTheAndAndClickUpdateButton(string hindi, string basic)
        {
            throw new PendingStepException();
        }

        [When(@"click on the delete Button of a ""([^""]*)""")]
        public void WhenClickOnTheDeleteButtonOfA(string english)
        {
            throw new PendingStepException();
        }

        [When(@"the user has exactly (.*) languages")]
        public void WhenTheUserHasExactlyLanguages(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the button should not be visible")]
        public void ThenTheButtonShouldNotBeVisible()
        {
            throw new PendingStepException();
        }

        [When(@"the user has fewer than (.*) languages")]
        public void WhenTheUserHasFewerThanLanguages(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the button should be visible")]
        public void ThenTheButtonShouldBeVisible()
        {
            throw new PendingStepException();
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public static void WhenInputTheAndAndClickTheAddButton(string malayalam, string native)
        {

        }

        [Then(@"""([^""]*)"" will be displayed")]
        public void ThenWillBeDisplayed(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string p0, string basic)
        {
            throw new PendingStepException();
        }

        [When(@"input the ""([^""]*)"" and leave the level empty")]
        public void WhenInputTheAndLeaveTheLevelEmpty(string english)
        {
            throw new PendingStepException();
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string p0, string fluent)
        {
            throw new PendingStepException();
        }


    }
}
