using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class SkillPage : CommonDriver
    {
        public readonly By activeTab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public readonly By skillTab = By.XPath("//a[text()=\"Skills\"]");
        public readonly By addSkillTextBox = By.XPath("//input[@placeholder=\"Add Skill\"]");
        public readonly By skillLevelDropdown = By.XPath("//select[@name=\"level\"]");
        public By levelOption;
        public readonly By skillAddNewButton = By.XPath("//div[@data-tab=\"second\"]//div[@class=\"ui teal button\"]");
        public readonly By addButton = By.XPath("//div[@data-tab=\"second\"]//input[@value=\"Add\"]");
        public readonly By cancelButton = By.XPath("//div[@data-tab=\"second\"]//input[@value=\"Cancel\"]");
        public By editOption;
        public By deleteOption;
        private By skillAndNewButton;
        public readonly By updateButton = By.XPath("//div[@data-tab=\"second\"]//input[@value=\"Update\"]");



        public void NavigateToSkillsTab(IWebDriver driver)
        {


            Wait.WaitToBeVisible(driver, skillTab);
            driver.FindElement(skillTab).Click();
        }
        public void ClickAddNewButton(IWebDriver driver)
        {
            Wait.WaitToBeVisible(driver, skillAndNewButton);
            driver.FindElement(skillAndNewButton).Click();
        }


        public void InputSkillDetails(IWebDriver driver, string type, string skill, string level)
        {
            if (level == "" || level == null)
            {
                level = "Choose Skill Level";
            }
            Wait.WaitToBeVisible(driver, addSkillTextBox);
            if (type == "new")
            {
                driver.FindElement(addSkillTextBox).SendKeys(skill);
                driver.FindElement(skillLevelDropdown).Click();
                levelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(levelOption).Click();
            }
            else if (type == "edit")
            {
                driver.FindElement(addSkillTextBox).Clear();
                driver.FindElement(addSkillTextBox).SendKeys(skill);
                driver.FindElement(skillLevelDropdown).Click();
                levelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(levelOption).Click();
            }
        }
        public void ClickAddButton(IWebDriver driver)
        {
            driver.FindElement(addButton).Click();
        }
        public void ClickCancelButton(IWebDriver driver)
        {
            driver.FindElement(cancelButton).Click();
        }
        public void ClickEditIcon(IWebDriver driver, string skill)
        {
            editOption = By.XPath("//td[text()='" + skill + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            Wait.WaitToBeClickable(driver, editOption);
            driver.FindElement(editOption).Click();
        }
        public void ClickDeleteIcon(IWebDriver driver, string skill)
        {

            deleteOption = By.XPath("//td[text()='" + skill + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            Wait.WaitToBeClickable(driver, deleteOption);
            driver.FindElement(deleteOption).Click();
        }
        public void ClickUpdateButton(IWebDriver driver)
        {
            driver.FindElement(updateButton).Click();
        }

        public int GetSkillCount(IWebDriver driver)
        {
            driver.FindElement(skillTab).Click();
            IWebElement _active_tab = driver.FindElement(activeTab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            Console.WriteLine($"Row count in the active tab: {_count}");
            return _count;
        }


        public void ClearUpAllTheData(IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> remove_icon = driver.FindElements(By.XPath("//div[@data-tab=\"second\"]//i[@class=\"remove icon\"]"));
            foreach (IWebElement icon in remove_icon)
            {
                icon.Click();
            }
            driver.Navigate().Refresh();
        }

    }
}
