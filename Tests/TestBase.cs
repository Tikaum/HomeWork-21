using HomeWork21.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HomeWork21.Tests.Tests
{
    public class TestBase
    {     
        [SetUp]
        public void Setup()
        {            
        }

        [TearDown]
        public void Teardown()
        {
            DriverManager.Quit();
        }
    }
}
