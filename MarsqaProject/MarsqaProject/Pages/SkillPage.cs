using MarsqaProject.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class SkillPage : ExtentReport
    {
        private readonly IWebDriver _driver;
        public SkillPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public readonly By active_tab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public readonly By skill_tab = By.XPath("//a[text()=\"Skills\"]");
        public readonly By add_skill_input = By.XPath("//input[@placeholder=\"Add Skill\"]");
        public readonly By skill_Level_dropdown = By.XPath("//select[@name=\"level\"]");
        public By level_option;
        public readonly By skill_andnew_button = By.XPath("//div[@data-tab=\"second\"]//div[@class=\"ui teal button\"]");
        public readonly By add_button = By.XPath("//div[@data-tab=\"second\"]//input[@obj=\"Add\"]");
        public readonly By cancel_button = By.XPath("//div[@data-tab=\"second\"]//input[@obj=\"Cancel\"]");
        public By edit_icon;
        public By delete_icon;
        public readonly By update_button = By.XPath("//div[@data-tab=\"second\"]//input[@obj=\"Update\"]");
        public By last_row_skill = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[1]");
        public By last_row_level = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[2]");
        public void NavigateToSkillsTab()
        {
            Wait.WaitToBeVisible(_driver, skill_tab);
            _driver.FindElement(skill_tab).Click();
        }
        public void ClickAddNewButton()
        {
            Wait.WaitToBeVisible(_driver, skill_andnew_button);
            _driver.FindElement(skill_andnew_button).Click();
        }
        public string GetLastRowSkill()
        {
            Wait.WaitToBeVisible(_driver, last_row_skill);
            return _driver.FindElement(last_row_skill).Text;
        }
        public string GetLastRowLevel()
        {
            Wait.WaitToBeVisible(_driver, last_row_level);
            return _driver.FindElement(last_row_level).Text;
        }

        public void InputSkillDetails(string type, string skill, string level)
        {
            if (level == "" || level == null)
            {
                level = "Choose Skill Level";
            }
            Wait.WaitToBeVisible(_driver, add_skill_input);
            if (type == "new")
            {
                _driver.FindElement(add_skill_input).SendKeys(skill);
                _driver.FindElement(skill_Level_dropdown).Click();
                level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(level_option).Click();
            }
            else if (type == "edit")
            {
                _driver.FindElement(add_skill_input).Clear();
                _driver.FindElement(add_skill_input).SendKeys(skill);
                _driver.FindElement(skill_Level_dropdown).Click();
                level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(level_option).Click();
            }
        }
        public void ClickAddButton()
        {
            _driver.FindElement(add_button).Click();
        }
        public void ClickCancelButton()
        {
            _driver.FindElement(cancel_button).Click();
        }
        public void ClickEditIcon(string skill)
        {
            edit_icon = By.XPath("//td[text()='" + skill + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            Wait.WaitToBeVisible(_driver, edit_icon);
            _driver.FindElement(edit_icon).Click();
        }
        public void ClickDeleteIcon(string skill)
        {

            delete_icon = By.XPath("//td[text()='" + skill + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            Wait.WaitToBeVisible(_driver, delete_icon);
            _driver.FindElement(delete_icon).Click();
        }
        public void ClickUpdateButton()
        {
            Wait.WaitToBeVisible(_driver, update_button);
            _driver.FindElement(update_button).Click();
        }

        public int GetSkillCount()
        {
            _driver.FindElement(skill_tab).Click();
            Wait.WaitToBeVisible(_driver, active_tab);
            IWebElement _active_tab = _driver.FindElement(active_tab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            object obj = Log.Information($"Row count in the active tab: {_count}");
            return _count;
        }


        public void ClearUpAllTheData()
        {
            _driver.FindElement(skill_tab).Click();
            IWebElement _active_tab = _driver.FindElement(active_tab);

            IReadOnlyCollection<IWebElement> remove_icon = _driver.FindElements(By.XPath("//div[@data-tab=\"second\"]//i[@class=\"remove icon\"]"));

            foreach (IWebElement icon in remove_icon)
            {

                icon.Click();
            }
            _driver.Navigate().Refresh();
        }


    }
}
