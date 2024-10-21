using MarsqaProject.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MarsqaProject.Pages
{
    public class LanguagePage
    {
        private readonly IWebDriver driver;
        public LanguagePage(IWebDriver Driver)
        {
            Driver = driver;
        }

        public readonly By languageTab = By.XPath("//a[text()=\"Languages\"]");

       // public readonly By languange_question_text = By.XPath("//div[contains(text(), 'How many languages do you speak?')]");

        public readonly By addLanguageInput = By.XPath("//input[@placeholder=\"Add Language\"]");
        public readonly By languageLevelDropdown = By.XPath("//select[@name=\"level\"]");

        public readonly By languageAddNewButton = By.XPath("//div[@data-tab=\"first\"]//div[@class=\"ui teal button \"]");
        public readonly By addButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Add\"]");
        public readonly By cancelButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Cancel\"]");
        public By languageLevelOption;
        public By lastrowLanguage = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[1]");
        public By lastrowLevel = By.XPath("//table[@class='ui fixed table']/tbody[last()]/tr[last()]/td[2]");

        public By editButton;
        public By deleteButton;



        public By updateButton = By.XPath("//div[@data-tab=\"first\"]//input[@value=\"Update\"]");

        public By activeTab = By.XPath("//div[contains(@class, 'ui bottom attached tab segment') and contains(@class, 'active')]");
        public By msgCloseButton = By.XPath("//a[@class=\"ns-close\"]");


        public void RefreshPage()
        {
            driver.Navigate().Refresh();
        }
        /* public void ClickMessageCloseButton()
         {
             Wait.WaitToBeVisible(driver, msgCloseButton);
             try
             {
                 IWebElement webElement = driver.FindElement(msgCloseButton);
                 if (webElement != null)
                 {
                     webElement.Click();
                 }
             }
             catch (StaleElementReferenceException)
             {
                 // Handle the case where the element is stale and re-fetch
                 Log.Information("Stale element, refreshing collection...");
             }


         } */
        public void ClickMessageCloseButton()
        {
            // Wait until the message close button is visible
            Wait.WaitToBeVisible(driver, msgCloseButton);

            // Retry mechanism for stale element reference
            const int maxRetries = 3;
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    // Attempt to find and click the close button
                    driver.FindElement(msgCloseButton).Click();
                    Log.Information("Message close button clicked successfully.");
                    break; // Break out of loop if successful
                }
                catch (StaleElementReferenceException)
                {
                    retryCount++;
                    Log.Information("Stale element reference exception encountered. Retry attempt: {RetryCount}", retryCount);

                    if (retryCount >= maxRetries)
                    {
                        Log.Error("Failed to click the message close button after {MaxRetries} attempts due to stale element.", maxRetries);
                        throw; // Re-throw the exception after max retries
                    }
                }
                catch (NoSuchElementException ex)
                {
                    Log.Error("Message close button not found: {ExceptionMessage}", ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Log.Error("An error occurred while attempting to click the message close button: {ExceptionMessage}", ex.Message);
                    throw;
                }
            }
        }


        public void NavigateToLanguageTab()
        {
            Wait.WaitToBeClickable(driver, languageTab);
            driver.FindElement(languageTab).Click();
        }
        public void ClickAddNewButton()
        {
            Wait.WaitToBeClickable(driver, languageAddNewButton);
            driver.FindElement(languageAddNewButton).Click();
        }

        public bool AddBewButtonIsVisible()
        {
            if (languageAddNewButton != null)
            {
                try
                {
                    IWebElement addNewButton = driver.FindElement(languageAddNewButton);
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
                driver.FindElement(addLanguageInput).SendKeys(language);
                driver.FindElement(languageLevelDropdown).Click();
                languageLevelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(languageLevelOption).Click();
            }
            else if (type == "edit")
            {
                driver.FindElement(addLanguageInput).Clear();
                driver.FindElement(addLanguageInput).SendKeys(language);
                driver.FindElement(languageLevelDropdown).Click();
                languageLevelOption = By.XPath("//select[@name=\"level\"]//option[text()=\"" + level + "\"]");
                driver.FindElement(languageLevelOption).Click();
            }

        }

        public void ClickAddButton()
        {
            driver.FindElement(addButton).Click();
        }

        public string getLastRowLanguage()
        {
            Wait.WaitToBeVisible(driver, lastrowLanguage);
            return driver.FindElement(lastrowLanguage).Text;
        }

        public string getLastRowLevel()
        {
            Wait.WaitToBeVisible(driver, lastrowLevel);
            return driver.FindElement(lastrowLevel).Text;
        }
        public void ClickCancelButton()
        {
            driver.FindElement(cancelButton).Click();
        }
        public int GetLanguageCount()
        {
            driver.FindElement(languageTab).Click();
            Wait.WaitToBeVisible(driver, activeTab);
            IWebElement _active_tab = driver.FindElement(activeTab);
            By row = By.XPath(".//table[contains(@class, 'ui fixed table')]//tbody//tr");
            IReadOnlyCollection<IWebElement> rows = _active_tab.FindElements(row);
            int _count = rows.Count;
            return _count;
        }


        public void ClickEditIconOfALanguage(string language)
        {
            editButton = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='outline write icon']");
            driver.FindElement(editButton).Click();
        }

        public void ClickDeleteIconOfALanguage(string language)
        {
            deleteButton = By.XPath("//td[text()='" + language + "']/following-sibling::td[@class='right aligned']//i[@class='remove icon']");
            driver.FindElement(deleteButton).Click();
        }

        public void ClickUpdateButton()
        {
            driver.FindElement(updateButton).Click();
        }

        public void ClearUpAllTheData()
        {
            try
            {
                IReadOnlyCollection<IWebElement> removeIcons = driver.FindElements(By.XPath("//div[@data-tab='first']//i[@class='remove icon']"));
                while (removeIcons.Count > 0)
                {
                    foreach (IWebElement icon in removeIcons.ToList())
                    {
                        try
                        {
                            // Click on the remove icon
                            Wait.WaitToBeClickable(driver, icon);
                            icon.Click();


                        }
                        catch (StaleElementReferenceException)
                        {
                            // Handle the case where the element is stale and re-fetch
                            Log.Information("Stale element, refreshing collection...");
                        }
                    }

                    // Re-fetch the list of remove icons, as they may have changed after each click
                    removeIcons = driver.FindElements(By.XPath("//div[@data-tab='first']//i[@class='remove icon']"));
                }

                // Refresh the page after clearing all the elements
                driver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                Log.Information($"An error occurred while clearing the data: {ex.Message}");
            }
        }
        
    }
}

