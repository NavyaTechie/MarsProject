using MarsqaProject.Pages;
using MarsqaProject.Utilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Modules.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.StepDefinition
{
    public class SkillStepDefinition : BasicTests
    {
        LoginPage loginPage;
        HomePage homePage;
        SkillPage skillPage;
        ProfilePage profilePage;
        int row_count = 0;

        public SkillStepDefinition(FeatureContext featureContext) : base(featureContext)
        {
        }

        [Given(@"navigate to the skills tab")]
        public void GivenNavigateToTheSkillsTab()
        {
            skillPage = new SkillPage();
            skillPage.NavigateToSkillsTab(driver);
        }

        [Given(@"click the AddNew Button")]
        public void GivenClickTheAddNewButton()
        {
            skillPage.ClickAddNewButton(driver);
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the add button")]
        public void WhenInputTheAndAndClickTheAddButton(string skill, string level)
        {
            skillPage.InputSkillDetails(driver, "new", skill, level);
            skillPage.ClickAddButton(driver);
        }

        [When(@"input the ""([^""]*)"" and ""([^""]*)"" and click the cancel button")]
        public void WhenInputTheAndAndClickTheCancelButton(string skill, string level)
        {
            row_count = skillPage.GetSkillCount(driver);
            skillPage.InputSkillDetails(driver, "new", skill, level);
            skillPage.ClickCancelButton(driver);

        }

        [Then(@"no more skill is created")]
        public void ThenNoMoreSkillIsCreated()
        {
            int current_count = skillPage.GetSkillCount(driver);
            Assert.Equals(row_count, current_count);
        }

        [Given(@"click the edit button of a ""([^""]*)""")]
        public void GivenClickTheEditButtonOfA(string skill)
        {
            skillPage.ClickEditIcon(driver, skill);
        }

        [When(@"change the ""([^""]*)""""([^""]*)"" and click Update button")]
        public void WhenChangeTheAndClickUpdateButton(string skill, string level)
        {
            skillPage.InputSkillDetails(driver, "edit", skill, level);
            skillPage.ClickUpdateButton(driver);
        }

        [When(@"click the delete button of a ""([^""]*)""")]
        public void WhenClickTheDeleteButtonOfA(string skill)
        {
            skillPage.ClickDeleteIcon(driver, skill);
        }
       
        [Then(@"a ""([^""]*)"" will be displayed to show the result")]
        public void ThenAWillBeDisplayedToShowTheResult(string message)
        {
            profilePage = new ProfilePage();
            string actural_result = profilePage.GetMessage(driver);
            profilePage.ClickMessageCloseButton(driver);
            Assert.That(actural_result.Contains(message));
        }

    }
}
