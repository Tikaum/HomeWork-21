using HomeWork21.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Pages
{
    public class BasePage
    {
        protected IWebDriver driver = DriverManager.Driver;
    }
}
