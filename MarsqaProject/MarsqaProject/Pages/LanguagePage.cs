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
        private readonly IWebDriver _driver;
        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public readonly By language_tab = By.XPath("//a[text()=\"Languages\"]");

        public readonly By languange_question_text = By.XPath("//div[contains(text(), 'How many languages do you speak?')]");

        public readonly By add_language_input = By.XPath("//input[@placeholder=\"Add Language\"]");
        public readonly By language_level_dropdown = By.XPath("//select[@name=\"level\"]");

        public readonly By language_addNew_button = By.XPath("//div[@data-tab=\"first\"]//div[@class=\"ui teal button \"]");
        public readonly By add_button = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Add\"]");
        public readonly By cancel_button = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Cancel\"]");
        public By language_level_option;
        public By lastrow_Language = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[1]");
        public By lastrow_level = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[2]");

        public By edit_button;// By.XPath("//td[text()='Mandarin']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
        public By delete_button;// By.XPath("//td[text()='Mandarin']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");



        public By update_button = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Update\"]");

        public By active_tab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public By message_close_button = By.XPath("//a[@class=\"ns-close\"]");


        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }
        public void ClickMessageCloseButton()
        {
            Wait.WaitToBeVisible(_driver, message_close_button);
            try
            {
                IWebElement ele = _driver.FindElement(message_close_button);
                if (ele != null)
                {
                    ele.Click();
                }
            }
            catch (StaleElementReferenceException)
            {
                // Handle the case where the element is stale and re-fetch
                Log.Information("Stale element, refreshing collection...");
            }


        }

        public void NavigateToLanguageTab()
        {
            Wait.WaitToBeClickable(_driver, language_tab);
            _driver.FindElement(language_tab).Click();
        }
        public void ClickAddNewButton()
        {
            Wait.WaitToBeClickable(_driver, language_addNew_button);
            _driver.FindElement(language_addNew_button).Click();
        }

        public bool AddBewButtonIsVisible()
        {
            if (language_addNew_button != null)
            {
                try
                {
                    IWebElement addNewButton = _driver.FindElement(language_addNew_button);
                    return true;
                }
                catch (NoSuchElementException ex)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        public void InputNewLanguageDetails(string type, string language, string level)
        {
            if (level == "" || level == null)
            {
                level = "Choose Language Level";
            }
            if (type == "new")
            {
                _driver.FindElement(add_language_input).SendKeys(language);
                _driver.FindElement(language_level_dropdown).Click();
                language_level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(language_level_option).Click();
            }
            else if (type == "edit")
            {
                _driver.FindElement(add_language_input).Clear();
                _driver.FindElement(add_language_input).SendKeys(language);
                _driver.FindElement(language_level_dropdown).Click();
                language_level_option = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                _driver.FindElement(language_level_option).Click();
            }

        }

        public void ClickAddButton()
        {
            _driver.FindElement(add_button).Click();
        }

        public string getLastRowLanguage()
        {
            Wait.WaitToBeVisible(_driver, lastrow_Language);
            return _driver.FindElement(lastrow_Language).Text;
        }

        public string getLastRowLevel()
        {
            Wait.WaitToBeVisible(_driver, lastrow_level);
            return _driver.FindElement(lastrow_level).Text;
        }
        public void ClickCancelButton()
        {
            _driver.FindElement(cancel_button).Click();
        }
        public int GetLanguageCount()
        {
            _driver.FindElement(language_tab).Click();
            Wait.WaitToBeVisible(_driver, active_tab);
            IWebElement _active_tab = _driver.FindElement(active_tab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            return _count;
        }


        public void ClickEditIconOfALanguage(string language)
        {
            edit_button = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            _driver.FindElement(edit_button).Click();
        }

        public void ClickDeleteIconOfALanguage(string language)
        {
            delete_button = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            _driver.FindElement(delete_button).Click();
        }

        public void ClickUpdateButton()
        {
            _driver.FindElement(update_button).Click();
        }

        public void ClearUpAllTheData()
        {
            try
            {
                IReadOnlyCollection<IWebElement> removeIcons = _driver.FindElements(By.XPath("//div[@data-tab='first']//i[@class='remove icon']"));
                while (removeIcons.Count > 0)
                {
                    foreach (IWebElement icon in removeIcons.ToList())
                    {
                        try
                        {
                            // Click on the remove icon
                            Wait.WaitToBeClickable(_driver, icon);
                            icon.Click();


                        }
                        catch (StaleElementReferenceException)
                        {
                            // Handle the case where the element is stale and re-fetch
                            Log.Information("Stale element, refreshing collection...");
                        }
                    }

                    // Re-fetch the list of remove icons, as they may have changed after each click
                    removeIcons = _driver.FindElements(By.XPath("//div[@data-tab='first']//i[@class='remove icon']"));
                }

                // Refresh the page after clearing all the elements
                _driver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                Log.Information($"An error occurred while clearing the data: {ex.Message}");
            }
        }



    }
}
}
