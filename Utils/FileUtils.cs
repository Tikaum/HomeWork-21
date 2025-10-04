using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork21.Utils
{
    public class FileUtils
    {
        public static string GetFileName(string filePath)
        {
            string fileName = filePath.Split('\\').LastOrDefault() ?? "";
            return fileName;
        }
    }
}
