using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class ProfilePage
    {
        
        
        public readonly By languageTab = By.XPath("//a[text()=\"Languages\"]");
       
        public readonly By addLanguageTextBox = By.XPath("//input[@placeholder=\"Add Language\"]");
        public readonly By languageLevelDropdown = By.XPath("//select[@name=\"level\"]");
        
        public readonly By languageAddNewButton = By.XPath("//div[@data-tab=\"first\"]//div[@class=\"ui teal button \"]");
        public readonly By addButton =By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Add\"]");
        public readonly By cancelButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Cancel\"]");
        public By lastrowLanguage = By.XPath("//tr[last()]//td[1]");
        public By lastrowLevel = By.XPath("//tr[last()]//td[2]");

        public By? editButton;
        public By? deleteButton;
        public By? languageLevelOption;

        public By messageDiv =By.XPath("//div[@class=\"ns-box-inner\"]");

        public By updateButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Update\"]");

        public By activeTab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public By message_close_button = By.XPath("//a[@class=\"ns-close\"]");

        public void NavigateToLanguageTab(IWebDriver driver)
        {


            Wait.WaitToBeVisible(driver, languageTab);
            driver.FindElement(languageTab).Click();
        }
        public void ClickAddNewButton(IWebDriver driver)
        {
            Wait.WaitToBeVisible(driver, languageAddNewButton);
            driver.FindElement(languageAddNewButton).Click();
        }

        public void InputNewLanguageDetails(IWebDriver driver, string type, string language, string level)
        {
            if (type == "new")
            {
                driver.FindElement(addLanguageTextBox).SendKeys(language);
                driver.FindElement(languageLevelDropdown).Click();
                languageLevelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(languageLevelOption).Click();
            }
            else if (type == "edit")
            {
                driver.FindElement(addLanguageTextBox).Clear();
                driver.FindElement(addLanguageTextBox).SendKeys(language);
                driver.FindElement(languageLevelDropdown).Click();
                languageLevelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(languageLevelOption).Click();
            }

        }

        public void ClickAddButton(IWebDriver driver)
        {
            driver.FindElement(addButton).Click();
        }

        public string getLastRowLanguage(IWebDriver driver)
        {
            return driver.FindElement(lastrowLanguage).Text;
        }

        public string getLastRowLevel(IWebDriver driver)
        {
            return driver.FindElement(lastrowLevel).Text;
        }
        public void ClickCancelButton(IWebDriver driver)
        {
            driver.FindElement(cancelButton).Click();
        }
        public int GetRowCount(IWebDriver driver, string type)
        {
            driver.FindElement(languageTab).Click();
            IWebElement _active_tab = driver.FindElement(activeTab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            Console.WriteLine($"Row count in the active tab: {_count}");
            return _count;
        }

        public void ClickEditIconOfALanguage(IWebDriver driver, string language)
        {
            editButton = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            driver.FindElement(editButton).Click();
        }

        public void ClickDeleteIconOfALanguage(IWebDriver driver, string language)
        {
            deleteButton = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            driver.FindElement(deleteButton).Click();
        }

        public void ClickUpdateButton(IWebDriver driver)
        {
            driver.FindElement(updateButton).Click();
        }

        public void ClearUpAllTheData(IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> remove_icon = driver.FindElements(By.XPath("//div[@data-tab=\"first\"]//i[@class=\"remove icon\"]"));
            foreach (IWebElement icon in remove_icon)
            {
                icon.Click();
            }
            driver.Navigate().Refresh();

        }
        public string GetMessage(IWebDriver driver)
        {
            Wait.WaitToBeVisible(driver, messageDiv);
            string message = driver.FindElement(messageDiv).Text;
            Console.WriteLine(message);
            return message;
        }
        public void ClickMessageCloseButton(IWebDriver driver)
        {
            driver.FindElement(message_close_button).Click();
        }
    }
}

