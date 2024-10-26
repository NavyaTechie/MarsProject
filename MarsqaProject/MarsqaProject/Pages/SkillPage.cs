using MarsqaProject.Utilities;
using OpenQA.Selenium;
//using OpenQA.Selenium.DevTools;
using System;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class SkillPage 
    {
        private readonly IWebDriver _driver;
        public SkillPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public readonly By activeTab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public readonly By skillTab = By.XPath("//a[text()=\"Skills\"]");
        public readonly By addSkillText = By.XPath("//input[@placeholder='Add Skill']");
        public readonly By skillLevelDropdown = By.XPath("//select[@name='level']");
        public By level_option;
        public readonly By skillAddnewButton = By.XPath("//div[@class='ui teal button' and text()='Add New']");
        public readonly By addButton = By.XPath("//input[@type='button' and @value='Add']");
        public readonly By cancelButton = By.XPath("//input[@type='button' and @value='Cancel']");
        public By editIcon;
        public By deleteIcon;
        public readonly By updateButton = By.XPath("//input[@type='button' and @value='Update']");
        public By lastRowSkill = By.XPath("(//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[1])[2]");
        public By lastRowLevel = By.XPath("(//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[2])[2]");
        public void NavigateToSkillsTab()
        {
            Wait.WaitToBeVisible(_driver, skillTab);
            _driver.FindElement(skillTab).Click();
        }
        public void ClickAddNewButton()
        {
            Wait.WaitToBeVisible(_driver, skillAddnewButton);
            _driver.FindElement(skillAddnewButton).Click();
        }
        public string GetLastRowSkill()
        {
            Wait.WaitToBeVisible(_driver, lastRowSkill);
            return _driver.FindElement(lastRowSkill).Text;
        }
        public string GetLastRowLevel()
        {
            Wait.WaitToBeVisible(_driver, lastRowLevel);
            return _driver.FindElement(lastRowLevel).Text;
        }

        public void InputSkillDetails(string type, string skill, string level)
        {
            if (level == "" || level == null)
            {
                level = "Choose Skill Level";
            }
            Wait.WaitToBeVisible(_driver, addSkillText);
            if (type == "new")
            {
                _driver.FindElement(addSkillText).SendKeys(skill);
                _driver.FindElement(skillLevelDropdown).Click();
                level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(level_option).Click();
            }
            else if (type == "edit")
            {
                _driver.FindElement(addSkillText).Clear();
                _driver.FindElement(addSkillText).SendKeys(skill);
                _driver.FindElement(skillLevelDropdown).Click();
                level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(level_option).Click();
            }
        }
        public void ClickAddButton()
        {
            _driver.FindElement(addButton).Click();
        }
        public void ClickCancelButton()
        {
            _driver.FindElement(cancelButton).Click();
        }
        public void ClickEditIcon(string skill,string level)
        {
            
            editIcon = By.XPath("//td[text()='" + skill + "']/following-sibling::td[text()='" + level + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            Wait.WaitToBeVisible(_driver, editIcon);
            _driver.FindElement(editIcon).Click();
        }
        public void ClickDeleteIcon(string skill,string level)
        {

            deleteIcon = By.XPath("//td[text()='" + skill + "']/following-sibling::td[text()='" + level + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            Wait.WaitToBeVisible(_driver, deleteIcon);
            _driver.FindElement(deleteIcon).Click();
        }
        public void ClickUpdateButton()
        {
            Wait.WaitToBeVisible(_driver, updateButton);
            _driver.FindElement(updateButton).Click();
        }

        public int GetSkillCount()
        {
            _driver.FindElement(skillTab).Click();
            Wait.WaitToBeVisible(_driver, activeTab);
            IWebElement _active_tab = _driver.FindElement(activeTab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            Log.Information($"Row count in the active tab: {_count}");
            return _count;
        }


        public void ClearUpAllTheData()
        {
            _driver.FindElement(skillTab).Click();
            IWebElement _active_tab = _driver.FindElement(activeTab);

            IReadOnlyCollection<IWebElement> remove_icon = _driver.FindElements(By.XPath("//div[@data-tab=\"second\"]//i[@class=\"remove icon\"]"));

            foreach (IWebElement icon in remove_icon)
            {

                icon.Click();
            }
            _driver.Navigate().Refresh();
        }


    }
}
