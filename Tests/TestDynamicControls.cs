using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Tests
{
    public class TestDynamicControls : TestBase
    {
        DCHerokuPage dCHeroku = new DCHerokuPage();

        [Test]
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
            bool stateOfInputDIsable = dCHeroku.IsInputFieldDIsabled();
            bool stateOfInputText = dCHeroku.CanIInputTextInField();
            Assert.That(stateOfInputDIsable, Is.True, "The field is available for input (from тote)");
            Assert.That(stateOfInputText, Is.False, "The field is available for input (from text input)");
            dCHeroku.ClickOnInputButton();
            bool stateOfNoteFromInput = dCHeroku.IsNoteOfInputEnableExist();
            stateOfInputText = dCHeroku.CanIInputTextInField();
            Assert.That(stateOfNoteFromInput, Is.True, "There is no message about the field's availability, or it contains incorrect text.");
            Assert.That(stateOfInputText, Is.True, "The field is not available for input (from text input)");
        }
    }
}
