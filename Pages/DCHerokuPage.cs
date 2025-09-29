using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace HomeWork21.Pages
{
    public class DCHerokuPage : BasePage
    {
        private readonly By Checkbox = By.XPath("//form[@id='checkbox-example']/div[@id='checkbox' and text()=' A checkbox']");
        private readonly By ButtonToRemove = By.XPath("//form[@id='checkbox-example']/button[@type='button' and text()='Remove']");
        private readonly By NoteOfMissingCheckbox = By.XPath("//form[@id='checkbox-example']/p[@id='message' and contains(text(), 's gone!')]");
        private readonly By InputField = By.XPath("//form[@id='input-example']/input[@type='text']");
        private readonly By InputFieldDIsabled = By.XPath("//form[@id='input-example']/input[@type='text' and @disabled]");
        private readonly By ButtonOfInputField = By.XPath("//form[@id='input-example']/button[@type='button']");
        private readonly By NoteOfInputEnable = By.XPath("//form[@id='input-example']/p[@id='message' and contains(text(), 's enabled!')]");

        public void GoToPageDynamicControlsHeroku()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_controls");
        }

        public bool IsCheckboxExist()
        {
            bool state = driver.FindElement(Checkbox).Displayed;
            return state;
        }

        public bool IsCheckboxNotExist()
        {
            try
            {
                driver.FindElement(Checkbox);
                return false;
            }
            catch
            {
                return true;
            }
        }

        public void RemoveCheckboxA()
        {
            driver.FindElement(ButtonToRemove).Click();
        }

        public bool IsNoteOfMissingCheckboxExist()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(NoteOfMissingCheckbox));
            bool state = driver.FindElement(NoteOfMissingCheckbox).Displayed;
            return state;
        }

        public bool IsInputFieldDIsabled()
        {
            try
            {
                driver.FindElement(InputFieldDIsabled);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClickOnInputButton()
        {
            driver.FindElement(ButtonOfInputField).Click();
        }

        public bool IsNoteOfInputEnableExist()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(NoteOfInputEnable));
            bool state = driver.FindElement(NoteOfInputEnable).Displayed;
            return state;
        }

        public bool CanIInputTextInField()
        {
            try
            {
                driver.FindElement(InputField).SendKeys("Qwerty123!");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
