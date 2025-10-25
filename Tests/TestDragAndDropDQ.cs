using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using NUnit.Framework;
using Allure.NUnit;


namespace HomeWork21.Tests
{
    [AllureNUnit]
    public class TestDragAndDropDQ : TestBase
    {
        DropDQPage dropDQ = new DropDQPage();

        [Test]
        [AllureTag("smoke")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("TimKay")]
        [AllureSuite("Drag and drop small rectangle on big rectangle")]
        public void DragAndDropRectangle()
        {
            dropDQ.GoToPageDroppableDQ();
            dropDQ.DragAndDrop();
            string ValidationText = dropDQ.GetTextOnBR();
            Assert.That(ValidationText, Is.EqualTo("Dropped!"), "The text on the rectangle is incorrect");
        }
    }
}
