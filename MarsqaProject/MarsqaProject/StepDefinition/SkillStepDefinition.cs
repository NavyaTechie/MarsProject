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
    [Binding]
    public class SkillStepDefinition : CommonDriver
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage loginPage;
        private readonly HomePage homePage;
        private readonly SkillPage skillsPage;
        private readonly ProfilePage profilePage;
        int row_count = 0;
        public ScenarioContext _scenarioContext;

        public SkillStepDefinition(IWebDriver driver, ScenarioContext scenarioContext, FeatureContext featureContext) : base(driver, featureContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            loginPage = new LoginPage(_driver);
            homePage = new HomePage(_driver);
            skillsPage = new SkillPage(_driver);
            profilePage = new ProfilePage(_driver);

        }
        [Given(@"The data is clean up  and  Navigate to the skill tab")]
        public void GivenTheDataIsCleanUpAndNavigateToTheSkillTab()
        {
            string url = GetAppConfig("url");
            _driver.Navigate().GoToUrl(url);
            homePage.ClickSignInLink();
            PerformLogin();

            Thread.Sleep(4000);
            
            skillsPage.NavigateToSkillsTab();
           skillsPage.ClearUpAllTheData();
            row_count = 0;
           skillsPage.NavigateToSkillsTab();

        }

        [Given(@"click the AddNew Button")]
        public void GivenClickTheAddNewButton()
        {
            skillsPage.ClickAddNewButton();

        }

        [When(@"Input the skill name ""([^""]*)"" and level ""([^""]*)""")]
        public void WhenInputTheSkillNameAndLevel(string skill, string level)
        {
            Log.Information("the skill is " + skill + " the level is " + level);
            skillsPage.InputSkillDetails("new", skill, level);
            _scenarioContext["skill"] = skill;
            _scenarioContext["level"] = level;

        }

        [When(@"Click the Add button")]
        public void WhenClickTheAddButton()
        {
            skillsPage.ClickAddButton();
        }

        [Then(@"a skill is created")]
        public void ThenASkillIsCreated()
        {
            int actual_count = skillsPage.GetSkillCount();

            string skill = skillsPage.GetLastRowSkill();
            string level = skillsPage.GetLastRowLevel();
            Assert.IsTrue(actual_count == 1);
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(level.Equals(_scenarioContext["level"].ToString()));


        }

        [When(@"click the cancel button")]
        public void WhenClickTheCancelButton()
        {
            skillsPage.ClickCancelButton();
        }

        [Then(@"no skill is created")]
        public void ThenNoSkillIsCreated()
        {
            int count = skillsPage.GetSkillCount();
            Assert.IsTrue(count == 0);
        }

        
        [Given(@"add a skill succeed")]
        public void GivenAddASkillSucceed(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                skillsPage.ClickAddNewButton();
                skillsPage.InputSkillDetails("new", row[0], row[1]);
                skillsPage.ClickAddButton();
                // _profilePage.ClickMessageCloseButton();
                _scenarioContext["skill"] = row[0];
                _scenarioContext["level"] = row[1];
                row_count++;
            }
            Log.Information("row count is" + skillsPage.GetSkillCount());
            Assert.IsTrue(skillsPage.GetSkillCount().Equals(table.Rows.Count));


        }

        [When(@"add another skill")]
        public void WhenAddAnotherSkill(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                skillsPage.ClickAddNewButton();
                skillsPage.InputSkillDetails("new", row[0], row[1]);
                skillsPage.ClickAddButton();
                
            }
        }

        [Then(@"no more skill is created")]
        public void ThenNoMoreSkillIsCreated()
        {
            int current_count = skillsPage.GetSkillCount();
            Assert.AreEqual(row_count, current_count);

        }

        [Then(@"skill is created")]
        public void ThenSkillIsCreated()
        {
            Assert.IsTrue(skillsPage.GetSkillCount() == 1);

        }

        
        [When(@"click the delete icon")]
        public void WhenClickTheDeleteIcon()
        {
            skillsPage.ClickDeleteIcon(_scenarioContext["skill"].ToString(), _scenarioContext["level"].ToString());
        }

        
        [When(@"click the edit icon")]
        public void WhenClickTheEditIcon()
        {
            skillsPage.ClickEditIcon(_scenarioContext["skill"].ToString(), _scenarioContext["level"].ToString());
        }

        [When(@"update the skill with below data")]
        public void WhenUpdateTheSkillWithBelowData(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                 skillsPage.InputSkillDetails("edit", row[0], row[1]);
                _scenarioContext["skill_update"] = row[0];
                _scenarioContext["level_update"] = row[1];
            }
        }

        [When(@"click update button")]
        public void WhenClickUpdateButton()
        {
            skillsPage.ClickUpdateButton();
        }

        [Then(@"the skill is updated")]
        public void ThenTheSkillIsUpdated()
        {
            Thread.Sleep(1000);
            string skill_page = skillsPage.GetLastRowSkill();
            string level_page = skillsPage.GetLastRowLevel();

            Log.Information("the skill is " + skill_page + " the level is " + level_page);
            Assert.IsTrue(skill_page.Equals(_scenarioContext["skill_update"].ToString()));
            Assert.IsTrue(level_page.Equals(_scenarioContext["level_update"].ToString()));
        }

        [Then(@"the skill is not updated")]
        public void ThenTheSkillIsNotUpdated()
        {
            string skill = skillsPage.GetLastRowSkill();
            string level = skillsPage.GetLastRowLevel();
            Log.Information("the skill is " + skill + " the level is " + level);
            Assert.IsTrue(skillsPage.GetSkillCount() == 1);
            Assert.IsTrue(skill.Equals(_scenarioContext["skill"].ToString()));
            Assert.IsTrue(level.Equals(_scenarioContext["level"].ToString()));
        }

        [When(@"I click the skill tab")]
        public void WhenIClickTheSkillTab()
        {
            profilePage.ClickSkillsTab();
        }

        [Then(@"I should see the skill list")]
        public void ThenIShouldSeeTheSkillList()
        {
            Assert.IsTrue(skillsPage.GetSkillCount() > 0);
        }
                             

        [Then(@"the skill will be deleted")]
        public void ThenTheSkillWillBeDeleted()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(skillsPage.GetSkillCount() == 0);
        }



    }
}
