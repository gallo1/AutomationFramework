using AutomationFramework.SetUp;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Protractor;

namespace AutomationFramework.Common
{
    public class BasePage
    {
        protected IWebDriver d;
        protected NgWebDriver NgDriver { get; private set; }
        public static int waitsec = Properties.Settings.Default.WaitTime; //gets default wait time
        NoSuchElementException ex = new NoSuchElementException();
        private static string Date = DateTime.Now.ToString("dd-MM-yyy+HH-mm-ss");

        public BasePage(IWebDriver driver) //base page call for driver setup, also sets the default page time out to 60 seconds
        {
            TestReport.Test().Log(Status.Info, "intialising " + this);
            this.d = driver;
            d.SwitchTo().DefaultContent();
            d.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            
        }


        /// <summary>
        /// Method used to get an element from a web page and return it
        /// </summary>
        /// <param name="elem">how the element will be found</param>
        /// <param name="timeoutInSeconds">how long to search for the element</param>
        public IWebElement GetElement(By elem, int timeoutInSeconds)
        {
            TestReport.Test().Log(Status.Info, "Finding element: " + elem);

            if(waitForElementExist(elem,timeoutInSeconds) == true)
            {
                IWebElement e = d.FindElement(elem);
                TestReport.Test().Log(Status.Info, "Element found!");
                return e;
            }
            else
            {
                string message = "Could not find element! \r\n";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("GetElementError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }


        //wait until document has fully loaded. JS and JQuery is fully loaded
        //You must wait for Javascript and jQuery to complete load. 
        //Run Javascript to verify if jQuery.active is 0 and document.readyState is whole, 
        //which implies the JS and jQuery fill is complete.
        public void WaitUntilDocumentIsReady(TimeSpan timeout)
        {
            IJavaScriptExecutor javaScriptExecutor = d as IJavaScriptExecutor;
            WebDriverWait wait = new WebDriverWait(d, timeout);
            // Check if document is ready
            Func<IWebDriver, bool> readyCondition = d => (Boolean)javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete' && jQuery.active == 0)");
            wait.Until(readyCondition);

        }

        //Check whether JQuery is being used:
        public bool IsJqueryBeingUsed(IJavaScriptExecutor jsExecutor)
        {
            var isTheSiteUsingJQuery = jsExecutor.ExecuteScript("return window.jQuery != undefined");
            return (bool)isTheSiteUsingJQuery;
        }
        //wait for angular js to load
        public void waitForAngular(NgWebDriver NgDriver)
        {
            NgDriver.WaitForAngular();
        }

        /// <summary>
        /// Overide of Getelement, uses default waittime instead of user defined
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public IWebElement GetElement(By elem)
        {
            TestReport.Test().Log(Status.Info, "Finding element: " + elem);

            if(waitForElementExist(elem,waitsec) == true)
            {
                IWebElement e = d.FindElement(elem);
                TestReport.Test().Log(Status.Info, "Element found!");
                return e;
            }
            else
            {
                string message = "Could not find element! \r\n";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("GetElementError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }

        /// <summary>
        /// Wait for elements to be visable and returns them to a list
        /// </summary>
        /// <param name="elem">used to help find the elements</param>
        /// <param name="timeoutInSeconds">how long to wait for the elements to be visible</param>
        /// <returns></returns>
        public IList<IWebElement> GetElements(By elem, int timeoutInSeconds)
        {
            TestReport.Test().Log(Status.Info, "Attempting to find group of elements using: " + elem);

            if(waitForElementExist(elem,timeoutInSeconds) == true)
            {
                TestReport.Test().Log(Status.Info, "Element Found!");
                IList<IWebElement> e = d.FindElements(elem).ToList();
                return e;
            }
            else
            {
                string message = "Could not find elements to add to list! \r\n";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("GetElementsError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }

        }
        /// <summary>
        /// Override of GetElements, uses default wait time instead of user defined
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public IList<IWebElement> GetElements(By elem)
        {
            TestReport.Test().Log(Status.Info, "Attempting to find group of elements using: " + elem);

            if(waitForElementExist(elem, waitsec) == true)
            {
                TestReport.Test().Log(Status.Info, "Element Found!");
                IList<IWebElement> e = d.FindElements(elem).ToList();
                return e;
            }
            else
            {
                string message = "Could not find elements to add to list! \r\n";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("GetElementsError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }

        /// <summary>
        /// Waits for element to be visiable then clicks element. 
        /// </summary>
        /// <param name="elem"></param>
        public void Click(By elem)
        {
            TestReport.Test().Log(Status.Info, "Attempting to click element: " + elem);

            if(waitForElementVisible(elem,waitsec) == true)
            {
                d.FindElement(elem).Click();
                TestReport.Test().Log(Status.Info, "Element succesfully clicked!");
            }
            else
            {
                string message = "Could not find an element to click";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("GetElementError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }

        /// <summary>
        /// User defined wait time to wait for element to be visiable then return the text that it contains
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public string returnText(By elem, int timeoutInSeconds)
        {
            TestReport.Test().Log(Status.Info, "Attempting to return text from element: " + elem);
           if(waitForElementVisible(elem, timeoutInSeconds) == true)
            {
                var e = d.FindElement(elem).Text;
                TestReport.Test().Log(Status.Info, "Text found: " + e);
                return e;
            }
           else
            {
                string message = "Could not find string to return";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("ReturnTextError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }

        /// <summary>
        /// Override for return text that uses default wait time
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public string returnText(By elem)
        {
            TestReport.Test().Log(Status.Info, "Attempting to return text from element: " + elem);

            if(waitForElementVisible(elem, waitsec) == true)
            {
                var e = d.FindElement(elem).Text;
                TestReport.Test().Log(Status.Info, "Text found: " + e);
                return e;
            }
            else
            {
                string message = "Could not find string to return";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("ReturnTextElementError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new NoSuchElementException(message + ex);
            }
        }

        /// <summary>
        /// waits for element to be visiable and then sends text
        /// Method is used for sending text to textboxes
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="text"></param>
        public void sendText(By elem, string text)
        {
            TestReport.Test().Log(Status.Info, "Attempting to find element: " + elem + "to send text " + text + "to");
            if (waitForElementVisible(elem, waitsec))
            {
                d.FindElement(elem).SendKeys(text);
                TestReport.Test().Log(Status.Info, "Found element sending text!");
            }
            else
            {
                string message = "Unable to send text as there is no such element!";
                TestReport.Test().Fail(message);
                ScreenShot.takeScreenshot("sendTextElementError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new Exception(message + ex);
            }
        }

        /// <summary>
        /// Method to verify that the automation script is on the right page
        /// </summary>
        /// <param name="text"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool verifyTitle(string text, By title)
        {
            string match = returnText(title, 20);
            if (text == match)
            {
                return true;
            }
            else
            {
                TestReport.Test().Fail("Unable to find text!");
                ScreenShot.takeScreenshot("VerifyTitleError" + ScenarioContext.Current.ScenarioInfo + Date, d);
                throw new Exception("Unable to find text!");
            }
        }

        /// <summary>
        /// Used in conjunction with get elements
        /// Waits for element to load
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        private bool waitForElementExist(By e, int t)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(t));
                wait.Until(ExpectedConditions.ElementExists(e));
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// Used with methods that interact with page objects
        /// waits for the page objects to be visiable. 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        private bool waitForElementVisible(By e, int t)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(t));
                var f = wait.Until(ExpectedConditions.ElementIsVisible(e));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
