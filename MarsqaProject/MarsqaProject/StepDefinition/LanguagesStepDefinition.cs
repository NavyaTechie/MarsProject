using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using System;
using Serilog;
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
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;
        private readonly LanguagePage _languagePage;
        private readonly ProfilePage _profilePage;
        int row_count = 0;
        public ScenarioContext _scenarioContext;


        public LanguagesStepDefinition(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext) : base(driver, featureContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;

            _loginPage = new LoginPage(_driver);
            _homePage = new HomePage(_driver);
            _profilePage = new ProfilePage(_driver);
            _languagePage = new LanguagePage(_driver);
        }



        [Given(@"User login the system")]
        public void GivenUserLoginTheSystem()
        {
            string url = GetAppConfig("url");
            _driver.Navigate().GoToUrl(url);
            _homePage.ClickSignInLink();
            PerformLogin();


        }

        [When(@"The data is clean up")]
        public void WhenTheDataIsCleanUp()
        {
            Thread.Sleep(2000);
            _languagePage.NavigateToLanguageTab();
            _languagePage.ClearUpAllTheData();
            _scenarioContext["language_count"] = 0;
            row_count = 0;

        }

        [When(@"Navigate to the language tab")]
        public void WhenNavigateToTheLanguageTab()
        {
            _languagePage.NavigateToLanguageTab();
        }

        [Given(@"Add a language succeed")]
        public void GivenAddALanguageSucceed(Table table)
        {
            List<string> languages = new List<string>();
            List<string> levels = new List<string>();

            foreach (TableRow row in table.Rows)
            {
                _languagePage.ClickAddNewButton();
                _languagePage.InputNewLanguageDetails("new", row[0], row[1]);
                _languagePage.ClickAddButton();
                _languagePage.ClickMessageCloseButton();
                // Store the languages and levels in lists
                languages.Add(row[0]);
                levels.Add(row[1]);
            }

            int language_count_page = _languagePage.GetLanguageCount();
            Log.Information("Row count is: " + language_count_page);

            // Store lists in ScenarioContext
            _scenarioContext.Set(languages, "languages");
            _scenarioContext.Set(levels, "levels");
            // Store the last added language and level
            if (languages.Count > 0 && levels.Count > 0)
            {
                _scenarioContext.Set(languages[^1], "language"); // Last element of the list
                _scenarioContext.Set(levels[^1], "level");
            }
            _scenarioContext["language_count"] = table.Rows.Count;
            // Assert that the number of added languages matches the number of rows in the table
            Assert.AreEqual(table.Rows.Count, language_count_page, "The count of languages on the page does not match the expected count.");
        }


        [When(@"I click the language tab")]
        public void WhenIClickTheLanguageTab()
        {
            _languagePage.RefreshPage();
        }


        [Then(@"I should see the language list with correct information")]
        public void ThenIShouldSeeTheLanguageListWithCorrectInformation()
        {
            ValidateLanguageCount();
        }


        [Then(@"The AndNew button is invisible")]
        public void ThenTheAndNewButtonIsInvisible()
        {
            Assert.IsTrue(_scenarioContext["language_count"].Equals(4));
            Assert.IsTrue(!_languagePage.AddBewButtonIsVisible());
        }

        [Then(@"The AndNew button is visible")]
        public void ThenTheAndNewButtonIsVisible()
        {
            Assert.Less((int)_scenarioContext["language_count"], 4);
            Assert.IsTrue(_languagePage.AddBewButtonIsVisible());
        }


        [Then(@"New language is created")]
        public void ThenNewLanguageIsCreated()
        {

            string language = _languagePage.getLastRowLanguage();
            string level = _languagePage.getLastRowLevel();


            ValidateLanguageCount();
            ValidateLanguageAndLevel(language, level);
        }
        private void ValidateLanguageCount()
        {

            int expectedLanguageCount = _scenarioContext.Get<int>("language_count");
            int actualLanguageCount = _languagePage.GetLanguageCount();

            Assert.AreEqual(expectedLanguageCount, actualLanguageCount, $"Expected language count: {expectedLanguageCount}, but got: {actualLanguageCount}");
        }

        private void ValidateLanguageAndLevel(string language, string level)
        {
            string expectedLanguage = _scenarioContext.Get<string>("language");
            string expectedLevel = _scenarioContext.Get<string>("level");

            Assert.AreEqual(expectedLanguage, language, $"Expected language: '{expectedLanguage}', but got: '{language}'");
            Assert.AreEqual(expectedLevel, level, $"Expected level: '{expectedLevel}', but got: '{level}'");
        }

        [Then(@"No more language is created")]
        public void ThenNoMoreLanguageIsCreated()
        {

            Assert.IsTrue(_languagePage.GetLanguageCount().Equals(_scenarioContext["language_count"]));
        }

        [When(@"Add another language")]
        public void WhenAddAnotherLanguage(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _languagePage.ClickAddNewButton();
                _languagePage.InputNewLanguageDetails("new", row[0], row[1]);
                _languagePage.ClickAddButton();
                _profilePage.ClickMessageCloseButton();
                _scenarioContext["language_new"] = row[0];
                _scenarioContext["level_new"] = row[1];

            }
        }





        [When(@"Input the language name ""(.*)"" and level ""(.*)""")]
        public void WhenInputTheLanguageAndLevel(string language, string level)
        {
            Log.Information("the language is " + language + " the level is " + level);
            _languagePage.InputNewLanguageDetails("new", language, level);
            _scenarioContext["language"] = language;
            _scenarioContext["level"] = level;
        }


        [Then(@"The language will be delete")]
        public void ThenTheLanguageWillBeDelete()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(_languagePage.GetLanguageCount().Equals(0));
        }


        [When(@"Update the language")]
        public void WhenUpdateTheLanguage(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _languagePage.InputNewLanguageDetails("edit", row[0], row[1]);
                _profilePage.ClickMessageCloseButton();
                _scenarioContext["language_update"] = row[0];
                _scenarioContext["level_update"] = row[1];
            }
        }


        [Then(@"The language is updated")]
        public void ThenTheLanguageIsUpdated()
        {
            Thread.Sleep(1000);
            string language_page = _languagePage.getLastRowLanguage();
            string level_page = _languagePage.getLastRowLevel();

            Log.Information("the language is " + language_page + " the level is " + level_page);
            Assert.IsTrue(language_page.Equals(_scenarioContext["language_update"].ToString()));
            Assert.IsTrue(level_page.Equals(_scenarioContext["level_update"].ToString()));
        }



        [Then(@"The language is not updated")]
        public void ThenTheLanguageIsNotUpdated()
        {
            Thread.Sleep(1000);
            string language_page = _languagePage.getLastRowLanguage();
            string level_page = _languagePage.getLastRowLevel();

            Log.Information("the language is " + language_page + " the level is " + level_page);
            Assert.IsTrue(language_page.Equals(_scenarioContext["language"].ToString()));
            Assert.IsTrue(level_page.Equals(_scenarioContext["level"].ToString()));
        }

        [Given(@"Click the language AddNew Button")]
        public void GivenClickTheLanguageAddNewButton()
        {
            _languagePage.ClickAddNewButton();
        }

        [When(@"Click the language Add button")]
        public void WhenClickTheLanguageAddButton()
        {
            _languagePage.ClickAddButton();
            row_count++;
            _scenarioContext["language_count"] = row_count;

        }

        [When(@"Click the language cancel button")]
        public void WhenClickTheLanguageCancelButton()
        {
            _languagePage.ClickCancelButton();
        }

        [When(@"Click the language delete icon")]
        public void WhenClickTheLanguageDeleteIcon()
        {
            _languagePage.ClickDeleteIconOfALanguage(_scenarioContext["language"].ToString());
        }


        [When(@"Click the language edit icon")]
        public void WhenClickTheLanguageEditIcon()
        {
            _languagePage.ClickEditIconOfALanguage(_scenarioContext["language"].ToString());
        }


        [When(@"Click language update button")]
        public void WhenClickLanguageUpdateButton()
        {
            _languagePage.ClickUpdateButton();
        }
    }
}