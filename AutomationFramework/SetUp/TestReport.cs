using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;


namespace AutomationFramework.SetUp
{
    public class TestReport
    {
        private static string Destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string Path = Destination + @"\Reports";
        private static string Date = DateTime.Now.ToString("dd-MM-yyy+HH-mm-ss");
        private static readonly ExtentReports _instance = new ExtentReports();
        private static ExtentTest _test;


        static TestReport() //intialises report and sets output to html file. Also checks to see if report file exists
        {
            string file = "\\report" + Date + ".html";
            if (!(Directory.Exists(Path)))
            {
                Directory.CreateDirectory((Path));
            }

            var htmlreport = new ExtentHtmlReporter(Path + file);
            Instance.AttachReporter(htmlreport);
        }

        public static void StartTest(string name) //set the name of the test
        {
            _test = Instance.CreateTest(name);
            
        }

        public static ExtentTest Test() // retrieves current test
        {
            return _test;
        }


        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }

    }
}
