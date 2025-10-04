using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Pages
{
    public class DropDQPage : BasePage
    {
        private readonly By SmallRectangle = By.XPath("//div[@id='draggable' and text()='Drag me']");
        private readonly By BigRectangle = By.XPath("//div[@id='droppable']/p[contains(text(), 'Drop')]");

        public void GoToPageDroppableDQ()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/droppable");
        }

        public void DragAndDrop()
        {
            var SR = driver.FindElement(SmallRectangle);
            var BR = driver.FindElement(BigRectangle);
            var actions = new Actions(driver);
            actions.DragAndDrop(SR, BR).Perform();
        }

        public string GetTextOnBR()
        {
            string VT = driver.FindElement(BigRectangle).Text;
            return VT;
        }
    }
}
