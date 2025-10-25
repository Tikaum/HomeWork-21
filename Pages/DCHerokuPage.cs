using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HomeWork21.Pages
{
    public class DCHerokuPage : BasePage
    {
        private readonly By Checkbox = By.XPath("//form[@id='checkbox-example']/div[@id='checkbox' and text()=' A checkbox']");
        private readonly By ButtonToRemove = By.XPath("//form[@id='checkbox-example']/button[@type='button' and text()='Remove']");
        private readonly By NoteOfMissingCheckbox = By.XPath("//form[@id='checkbox-example']/p[@id='message' and contains(text(), 's gone!')]");
        private readonly By InputField = By.XPath("//form[@id='input-example']/input[@type='text']");        
        private readonly By ButtonOfInputField = By.XPath("//form[@id='input-example']/button[@type='button']");
        private readonly By NoteOfInputEnable = By.XPath("//form[@id='input-example']/p[@id='message' and contains(text(), 's enabled!')]");

        [AllureStep("Переход на страницу динамических контролов")]
        public void GoToPageDynamicControlsHeroku()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_controls");
        }

        [AllureStep("Проверка наличия чекбокса")]
        public bool IsCheckboxExist()
        {
            bool state = driver.FindElement(Checkbox).Displayed;
            return state;
        }

        [AllureStep("Проверка отсутствия чекбокса")]
        public bool IsCheckboxNotExist()
        {
            var Checkboxes = driver.FindElements(Checkbox);
            if (Checkboxes.Count == 0)
            {
                return true;
            }
            else return false;
        }

        [AllureStep("Удаление чекбокса")]
        public void RemoveCheckboxA()
        {
            driver.FindElement(ButtonToRemove).Click();
        }

        [AllureStep("Проверка сообщения о отсутствии чекбокса")]
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

        [AllureStep("Проверка доступности поля для ввода")]
        public bool IsInputFieldEnabled(string testText)
        {
            var Input = driver.FindElement(InputField);
            bool IsFormEnable = Input.Enabled;
            bool IsFormDisplayed = Input.Displayed;
            if (IsFormEnable & IsFormDisplayed)
            {
                Input.SendKeys(testText);
                string text = Input.GetAttribute("value");
                if (text == testText)
                {
                    return true;
                }
                else return false;
            }            
            else return false;
        }

        [AllureStep("Нажатие на кнопку включающую поле ввода")]
        public void ClickOnInputButton()
        {
            driver.FindElement(ButtonOfInputField).Click();
        }

        [AllureStep("Проверка сообщения о доступности поля для ввода")]
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
    }
}
