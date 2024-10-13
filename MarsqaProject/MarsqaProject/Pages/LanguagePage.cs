using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsqaProject.Pages
{
    public class LanguagePage
    {
        public readonly By languageTab = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[1]/a[1]");

        public readonly By addLanguageTextBox = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[1]/input");
        public readonly By levelDropdown = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[2]/select");

        public readonly By addNewButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div");
        public readonly By addButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[1]");
        public readonly By cancelButton = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[2]");
        public By? languageLevelOption;
        
        public By lastrowLanguage = By.XPath("//tr[last()]//td[1]");
        public By lastrowLevel = By.XPath("//tr[last()]//td[2]");

        public By? editButton;// By.XPath("//td[text()='Mandarin']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
        public By? deleteButton;// By.XPath("//td[text()='Mandarin']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");



        public By updateButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Update\"]");

        public By activeTab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");


        public void NavigateToLanguageTab(IWebDriver driver)
        {


            Wait.WaitToBeVisible(driver, languageTab);
            driver.FindElement(languageTab).Click();
        }
        public void ClickAddNewButton(IWebDriver driver)
        {
            //Wait.WaitToBeVisible(driver, language_addNew_button);
            driver.FindElement(addLanguageTextBox).Click();
        }

        public void EnterNewLanguage(IWebDriver driver, string type, string language, string level)
        {
            if (level == "" || level == null)
            {
                level = "Choose Language Level";
            }
            if (type == "new")
            {
                driver.FindElement(addLanguageTextBox).SendKeys(language);
                driver.FindElement(levelDropdown).Click();
                languageLevelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(languageLevelOption).Click();
            }
            else if (type == "edit")
            {
                driver.FindElement(addLanguageTextBox).Clear();
                driver.FindElement(addLanguageTextBox).SendKeys(language);
                driver.FindElement(levelDropdown).Click();
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
            IReadOnlyCollection<IWebElement> removeIcon = driver.FindElements(By.XPath("//div[@data-tab=\"first\"]//i[@class=\"remove icon\"]"));
            foreach (IWebElement icon in removeIcon)
            {
                icon.Click();
            }
            driver.Navigate().Refresh();
        }

    }
}
