using Allure.NUnit.Attributes;
using HomeWork21.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Pages
{
    public class UpDownFileDQPage : BasePage
    {
        private readonly By ButtonUploadFile = By.Id("uploadFile");
        private readonly By PathOfUploadFile = By.Id("uploadedFilePath");
        private readonly By ButtonDownloadFile = By.Id("downloadButton");

        [AllureStep("Переход на страницу с загружаемыми файлами")]
        public void GoToPageUploadDownloadDQ()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/upload-download");
        }

        [AllureStep("Выбор загружаемого файла и его загрузка на сайт")]
        public void UploadFileWithParam(string filePath)
        {
            driver.FindElement(ButtonUploadFile).SendKeys(filePath);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var waitingElement = wait.Until(d => d.FindElement(PathOfUploadFile));
        }

        [AllureStep("Получение имени загруженного на сайт файла")]
        public string GetNameOfUploadFile()
        {
            string FilePath = driver.FindElement(PathOfUploadFile).Text;
            string FileName = FileUtils.GetFileName(FilePath);
            return FileName;
        }

        [AllureStep("Нажатие на кнопку скачивания файла с сайта")]
        public void DownloadFile()
        {
            driver.FindElement(ButtonDownloadFile).Click();
        }

        [AllureStep("Проверка наличия скачаного с сайта файла")]
        public bool WaitDownloadFile(string downloadPath, string fileName, int timeoutSeconds = 10)
        {
            string filePath = Path.Combine(downloadPath, fileName);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d => { return File.Exists(filePath); });
        }
    }
}
