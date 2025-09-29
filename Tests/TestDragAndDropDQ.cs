using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Tests
{
    public class TestDragAndDropDQ : TestBase
    {
        DropDQPage dropDQ = new DropDQPage();

        [Test]
        public void DragAndDropRectangle()
        {
            dropDQ.GoToPageDroppableDQ();
            dropDQ.DragAndDrop();
            string ValidationText = dropDQ.GetTextOnBR();
            Assert.That(ValidationText, Is.EqualTo("Dropped!"), "The text on the rectangle is incorrect");
        }
    }
}
