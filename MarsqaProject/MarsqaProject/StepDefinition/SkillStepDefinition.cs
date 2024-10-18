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

namespace MarsqaProject.StepDefinition
{
    public class SkillStepDefinition : BasicTests
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;
        private readonly SkillPage _skillsPage;
        private readonly ProfilePage _profilePage;
        int row_count = 0;
        public ScenarioContext _scenarioContext;

        public SkillStepDefinition(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext) : base(driver, featureContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPage(_driver);
            _homePage = new HomePage(_driver);
            _skillsPage = new SkillPage(_driver);
            _profilePage = new ProfilePage(_driver);

        }
        [Given(@"navigate to the skills tab")]
        public void GivenNavigateToTheSkillsTab()
        {

            _skillsPage.NavigateToSkillsTab();

        }

        [Given(@"click the AddNew Button")]
        public void GivenClickTheAddNewButton()
        {
            _skillsPage.ClickAddNewButton();
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string skill, string level)
        {
            _skillsPage.InputSkillDetails("new", skill, level);
            _skillsPage.ClickAddButton();
        }



        [Given(@"click the edit button of a ""([^""]*)""")]
        public void GivenClickTheEditButtonOfA(string skill)
        {
            _skillsPage.ClickEditIcon(skill);
        }

        [When(@"change the ""([^""]*)""""([^""]*)"" and click Update button")]
        public void WhenChangeTheAndClickUpdateButton(string skill, string level)
        {
            _skillsPage.InputSkillDetails("edit", skill, level);
            _skillsPage.ClickUpdateButton();
        }

        [When(@"click the delete button of a ""([^""]*)""")]
        public void WhenClickTheDeleteButtonOfA(string skill)
        {
            _skillsPage.ClickDeleteIcon(skill);
        }

        [Then(@"a ""([^""]*)"" will be displayed to show the result")]
        public void ThenAWillBeDisplayedToShowTheResult(string message)
        {

            string actural_result = _profilePage.GetMessage();
            _profilePage.ClickMessageCloseButton();
            Assert.That(actural_result.Contains(message));

        }


        [Given(@"The data is clean up  and  Navigate to the skill tab")]
        public void GivenTheDataIsCleanUpAndNavigateToTheSkillTab()
        {
            string url = GetAppConfig("url");
            _driver.Navigate().GoToUrl(url);
            _homePage.ClickSignInLink();
            PerformLogin();

            Thread.Sleep(2000);
            // _profilePage.ClickSkillsTab();
            _skillsPage.NavigateToSkillsTab();
            _skillsPage.ClearUpAllTheData();
            row_count = 0;
            _skillsPage.NavigateToSkillsTab();
        }

        [When(@"Input the skill name ""(.*)"" and level ""(.*)""")]
        public void WhenInputTheSkillAndLevel(string skill, string level)
        {
            Log.Information("the skill is " + skill + " the level is " + level);
            _skillsPage.InputSkillDetails("new", skill, level);
            _scenarioContext["skill"] = skill;
            _scenarioContext["level"] = level;
        }


        [When(@"Click the Add button")]
        public void WhenClickTheAddButton()
        {
            _skillsPage.ClickAddButton();
        }

        [Then(@"a skill is created")]
        public void ThenASkillIsCreated()
        {

            int actual_count = _skillsPage.GetSkillCount();

            string skill = _skillsPage.GetLastRowSkill();
            string level = _skillsPage.GetLastRowLevel();
            Assert.IsTrue(actual_count == 1);
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(level.Equals(_scenarioContext["level"].ToString()));

        }

        [When(@"click the cancel button")]
        public void WhenClickTheCancelButton()
        {
            _skillsPage.ClickCancelButton();
        }

        [Then(@"no skill is created")]
        public void ThenNoSkillIsCreated()
        {
            int actual_count = _skillsPage.GetSkillCount();
            Assert.IsTrue(actual_count == 0);
        }


        [Given(@"add a skill succeed")]
        public void GivenAddASkillSucceed(Table table)
        {


            foreach (TableRow row in table.Rows)
            {
                _skillsPage.ClickAddNewButton();
                _skillsPage.InputSkillDetails("new", row[0], row[1]);
                _skillsPage.ClickAddButton();
                // _profilePage.ClickMessageCloseButton();
                _scenarioContext["skill"] = row[0];
                _scenarioContext["level"] = row[1];
                row_count++;
            }
            Log.Information("row count is" + _skillsPage.GetSkillCount());
            Assert.IsTrue(_skillsPage.GetSkillCount().Equals(table.Rows.Count));

        }


        [When(@"add another skill")]
        public void WhenAddAnotherSkill(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _skillsPage.ClickAddNewButton();
                _skillsPage.InputSkillDetails("new", row[0], row[1]);
                _skillsPage.ClickAddButton();
                //_profilePage.ClickMessageCloseButton();
            }
        }


        [Then(@"no more skill is created")]
        public void ThenNoMoreSkillIsCreated()
        {
            int current_count = _skillsPage.GetSkillCount();
            Assert.AreEqual(row_count, current_count);
        }

        [Then(@"skill is created")]
        public void ThenSkillIsCreated()
        {
            Assert.IsTrue(_skillsPage.GetSkillCount() == 1);
        }


        [When(@"click the delete icon")]
        public void WhenClickTheDeleteIcon()
        {
            _skillsPage.ClickDeleteIcon(_scenarioContext["skill"].ToString());
        }

        [Then(@"the skill will be delete")]
        public void ThenTheSkillWillBeDelete()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(_skillsPage.GetSkillCount() == 0);
        }

        [When(@"click the edit icon")]
        public void WhenClickTheEditIcon()
        {
            _skillsPage.ClickEditIcon(_scenarioContext["skill"].ToString());
        }

        [When(@"update the skill with below data")]
        public void WhenUpdateTheSkillWithBelowData(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _skillsPage.InputSkillDetails("edit", row[0], row[1]);
                _scenarioContext["skill_update"] = row[0];
                _scenarioContext["level_update"] = row[1];
            }


        }

        [When(@"click update button")]
        public void WhenClickUpdateButton()
        {
            _skillsPage.ClickUpdateButton();
        }

        [Then(@"the skill is updated")]
        public void ThenTheSkillIsUpdated()
        {
            Thread.Sleep(1000);
            string skill_page = _skillsPage.GetLastRowSkill();
            string level_page = _skillsPage.GetLastRowLevel();

            Log.Information("the skill is " + skill_page + " the level is " + level_page);
            Assert.IsTrue(skill_page.Equals(_scenarioContext["skill_update"].ToString()));
            Assert.IsTrue(level_page.Equals(_scenarioContext["level_update"].ToString()));
        }

        [Then(@"the skill is not updated")]
        public void ThenTheSkillIsNotUpdated()
        {
            string skill = _skillsPage.GetLastRowSkill();
            string level = _skillsPage.GetLastRowLevel();
            Log.Information("the skill is " + skill + " the level is " + level);
            Assert.IsTrue(_skillsPage.GetSkillCount() == 1);
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(level.Equals(_scenarioContext["level"].ToString()));

        }

        [When(@"I click the skill tab")]
        public void WhenIClickTheSkillTab()
        {
            _profilePage.ClickSkillsTab();
        }

        [Then(@"I should see the skill list")]
        public void ThenIShouldSeeTheSkillList()
        {
            Assert.IsTrue(_skillsPage.GetSkillCount() > 0);
        }

    }
}
