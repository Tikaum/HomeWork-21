using HomeWork21.Pages;
using HomeWork21.Tests.Tests;
using HomeWork21.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Tests
{
    public class TestUpDownFileDQ : TestBase
    {
        UpDownFileDQPage upDown = new UpDownFileDQPage();

        [Test]
        public void UploadFile()
        {
            upDown.GoToPageUploadDownloadDQ();
            string filePath = "D:\\123qwe.txt";
            string fileName = FileUtils.GetFileName(filePath);
            upDown.UploadFileWithParam(filePath);
            string NameOfUploadFile = upDown.GetNameOfUploadFile();
            Assert.That(NameOfUploadFile, Is.EqualTo(fileName), "The file was not loaded or an invalid name was received.");
        }

        [Test]
        public void DownloadFile()
        {
            upDown.GoToPageUploadDownloadDQ();
            upDown.DownloadFile();
            string downloadPath = "D:\\Downloads";
            string fileName = "sampleFile.jpeg";
            bool successDownload = upDown.WaitDownloadFile(downloadPath, fileName);
            Assert.That(successDownload, "The file was not downloaded or an invalid name was received.");
        }
    }
}
