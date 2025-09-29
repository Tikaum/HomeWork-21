using HomeWork21.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Tests.Tests
{
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        public void Teardown()
        {
            DriverManager.Quit();
        }

    }
}
