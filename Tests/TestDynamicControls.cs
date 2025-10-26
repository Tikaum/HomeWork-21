using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using HomeWork21.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace HomeWork21.Tests
{
    public class TestDynamicControls : TestBase
    {
        DCHerokuPage dCHeroku = new DCHerokuPage();

        [Test]
        [Category("extended")]
        [Category("AllTests")]
        [AllureTag("extended")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureOwner("TimKay")]
        [AllureSuite("Activating/deactivating a checkbox and input field")]
        public void RemoveCheckboxAndEnableInput()
        {            
            dCHeroku.GoToPageDynamicControlsHeroku();
            bool stateOfCheckbox = dCHeroku.IsCheckboxExist();
            Assert.That(stateOfCheckbox, Is.True, "The checkbox does not exist");
            dCHeroku.RemoveCheckboxA();
            bool stateOfNote = dCHeroku.IsNoteOfMissingCheckboxExist();
            Assert.That(stateOfNote, Is.True, "There is no message about the checkbox disappearing, or it contains incorrect text.");
            stateOfCheckbox = dCHeroku.IsCheckboxNotExist();
            Assert.That(stateOfCheckbox, Is.True, "The checkbox is displayed");
            string testText = "123Qwerty!";
            bool stateOfInput = dCHeroku.IsInputFieldEnabled(testText);            
            Assert.That(stateOfInput, Is.False, "The field is available for input");           
            dCHeroku.ClickOnInputButton();
            bool stateOfNoteFromInput = dCHeroku.IsNoteOfInputEnableExist();
            stateOfInput = dCHeroku.IsInputFieldEnabled(testText);
            Assert.That(stateOfNoteFromInput, Is.True, "There is no message about the field's availability, or it contains incorrect text.");
            Assert.That(stateOfInput, Is.True, "The field is not available for input");
            DriverManager.Quit();
        }
    }
}
