using AutomationFramework.SetUp;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions.Internal;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using log4net;
using log4net.Config;
using Protractor;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace AutomationFramework.Common
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected TestEnvironment environment; //intialises test environment
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected NgWebDriver NgDriver { get; private set; }
        protected OpenQA.Selenium.Interactions.Actions actions;
        private int wait_seconds = 3;
        private int highlight_timeout = 100;
        private IAlert alert;
        private string alert_text;
        private Regex theReg;
        private MatchCollection theMatches;
        private Match theMatch;
        private Capture theCapture;
        [BeforeScenario]
        [SetUp]
        public void Intialize()
        {

            TestReport.StartTest(TestContext.CurrentContext.Test.Name);

            var option = new InternetExplorerOptions()
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };
            // Instanciate a classic Selenium's WebDriver
            switch (Properties.Settings.Default.Browser)
            {
                case BrowserType.Chrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserType.FireFox:
                    driver = new FirefoxDriver();
                    break;
                case BrowserType.InternetExplorer:
                    driver = new InternetExplorerDriver(option);
                    break;
                case BrowserType.PhantomJS:
                    driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
                    break;
                default:
                    throw new Exception("Browser type invalid");
            }
            driver.Navigate().GoToUrl(TestEnvironment.GetEnvironment().Url); //navigates to test url
            driver.Manage().Window.Maximize(); //maximises browser window
            // Configure timeouts (important since Protractor uses asynchronous client side scripts)
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(10));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(wait_seconds));
            NgDriver = new NgWebDriver(driver);
            actions = new OpenQA.Selenium.Interactions.Actions(driver);
            //TestManager.driver = driver;
            
        }

        [AfterScenario]
        [TearDown]
        public void TestCleanUp() //Closes webdriver and manually kills the process
        {
            TestReport.Instance.Flush();
            if (driver == null)
            {
                return;
            }
            else
            {
                driver.Quit(); 
                switch (Properties.Settings.Default.Browser)
                {
                    case BrowserType.Chrome:
                        KillProcess("chrome.exe");
                        KillProcess("chromedriver.exe");
                        break;
                    case BrowserType.FireFox:
                        KillProcess("firefox.exe");
                        break;
                    case BrowserType.InternetExplorer:
                        KillProcess("iexplorer.exe");
                        break;
                    default:
                        new ArgumentException("Invalid browser type!");
                        break;
                }
                driver = null;
            }
        }
        /// <summary>
        /// Method to kill processes after automation run has finished to ensure that the process has fully ended
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName) 
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }
    }
}
