using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using HomeWork21.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HomeWork21.Tests
{
    [AllureNUnit]
    public class TestUpDownFileDQ : TestBase
    {
        UpDownFileDQPage upDown = new UpDownFileDQPage();

        [Test]
        [Category("smoke")]
        [Category("AllTests")]
        [AllureTag("smoke")]
        [AllureSeverity(SeverityLevel.trivial)]
        [AllureOwner("TimKay")]
        [AllureSuite("Uploading file on site")]
        public void UploadFile()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless=new", "--no-sandbox", "--disable-dev-shm-usage", "--disable-gpu", "--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            upDown.GoToPageUploadDownloadDQ();
            string filePath = "D:\\123qwe.txt";
            string fileName = FileUtils.GetFileName(filePath);
            upDown.UploadFileWithParam(filePath);
            string NameOfUploadFile = upDown.GetNameOfUploadFile();
            Assert.That(NameOfUploadFile, Is.EqualTo(fileName), "The file was not loaded or an invalid name was received.");
            DriverManager.Quit();
        }

        [Test]
        [Category("regression")]
        [Category("AllTests")]
        [AllureTag("regression")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureOwner("TimKay")]
        [AllureSuite("Downloading file from site")]
        public void DownloadFile()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless=new", "--no-sandbox", "--disable-dev-shm-usage", "--disable-gpu", "--window-size=1920,1080");
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            upDown.GoToPageUploadDownloadDQ();
            upDown.DownloadFile();
            string downloadPath = "D:\\Downloads";
            string fileName = "sampleFile.jpeg";
            bool successDownload = upDown.WaitDownloadFile(downloadPath, fileName);
            Assert.That(successDownload, "The file was not downloaded or an invalid name was received.");
            DriverManager.Quit();
        }
    }
}
