using OpenQA.Selenium;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace AutomationFramework.SetUp
{
    public class ScreenShot
    {
        private static string Destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string Path = Destination + @"\Reports\ScreenShots";

        public static void takeScreenshot(string fileName, IWebDriver driver)
        {
            if (!(Directory.Exists(Path)))
            {
                Directory.CreateDirectory((Path));
            }

            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            screenshot.SaveAsFile(Path + @"\" + fileName + ".jpeg", ImageFormat.Gif);
        }
    }
}
