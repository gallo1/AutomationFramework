using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.SetUp;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using AutomationFramework.Common;
using AutomationFramework.PageObjects;
using Protractor;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.Tests
{
    public class TestAngularJSApp : BaseTest
    {
        private String base_url = "http://www.way2automation.com/angularjs-protractor/banking";
        [Test]
        public void ShouldGreetUsingBinding()
        {
            // Instanciate a classic Selenium's WebDriver
            //var driver = new ChromeDriver();
            // Configure timeouts (important since Protractor uses asynchronous client side scripts)
            //driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));
            NgDriver.Navigate().GoToUrl("http://www.angularjs.org");
            NgDriver.FindElement(NgBy.Model("yourName")).SendKeys("Julie");
            Assert.AreEqual("Hello Julie!", NgDriver.FindElement(NgBy.Binding("yourName")).Text);//}
        }

        [Test]
        public void VerifyAccountDeposit()
        {
            driver.Navigate().GoToUrl(base_url);
            By elem = By.XPath(".//button[contains(text(),'Customer Login')]");
            NgDriver.FindElement(elem).Click();
            //NgDriver.FindElement(By.XPath(".//button[contains(text(),'Customer Login')]")).Click();
            ReadOnlyCollection<NgWebElement> ng_customers = NgDriver.FindElement(NgBy.Model("custId")).FindElements(NgBy.Repeater("cust in Customers"));
            // select customer to log in
            ng_customers.First(cust => Regex.IsMatch(cust.Text, "Harry Potter")).Click();
            By elemBut = By.XPath(".//button[contains(text(),'Login')]");
            NgDriver.FindElement(elemBut).Click();
            //wait until document has fully loaded
            (new BasePage(NgDriver)).WaitUntilDocumentIsReady(TimeSpan.FromSeconds(10));
            //wait for angularjs to load
            (new BasePage(NgDriver)).waitForAngular(NgDriver);
            By Selectelem = By.XPath(".//select[contains(@id,'accountSelect')]");
            //driver.FindElement(By.Id("ps_ck$0"))
            SelectElement SelectAccount = new SelectElement(NgDriver.FindElement(Selectelem));
            //To count elements
            IList<IWebElement> ElementCount = SelectAccount.Options;
            int NumberOfItems = ElementCount.Count;
            Console.WriteLine("Size of SelectAccount Dropdown options: " + NumberOfItems);
            //click selected option
            string SelectedOption = SelectAccount.SelectedOption.Text;
            Console.WriteLine("SelectedOption: " + SelectedOption);
            SelectAccount.SelectedOption.Click();

            NgWebElement ng_account_number_element = NgDriver.FindElement(NgBy.Binding("accountNo"));
            Console.WriteLine("ng_account_number_element: " + ng_account_number_element.Text);
            int account_id = 0;
            string pattern = @"(?<result>\d+)$";
            string input = ng_account_number_element.Text;
            Match match = Regex.Match(input, pattern);
            Console.WriteLine("match: " + match.Value);
            //int.TryParse(ng_account_number_element.Text.FindMatch(@"(?<result>\d+)$"), out account_id);
            int.TryParse(match.Value, out account_id);
            Assert.AreNotEqual(0, account_id);
            int account_amount = -1;
            int.TryParse(Regex.Match(NgDriver.FindElement(NgBy.Binding("amount")).Text, pattern).Value, out account_amount);
            Console.WriteLine("account_amount: " + account_amount);
            Assert.AreNotEqual(-1, account_amount);
            NgDriver.FindElement(By.XPath(".//button[contains(text(),'Deposit')]")).Click();
            // core Selenium
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form[name='myForm']")));
            NgWebElement ng_form_element = new NgWebElement(NgDriver, driver.FindElement(By.CssSelector("form[name='myForm']")));
            NgWebElement ng_deposit_amount_element = ng_form_element.FindElement(NgBy.Model("amount"));
            ng_deposit_amount_element.SendKeys("100");
            //Highlight UI Element
            NgWebElement ng_deposit_button_element = ng_form_element.FindElement(By.XPath(".//button[contains(text(),'Deposit')]"));
            //NgDriver.Highlight(ng_deposit_button_element);
            var jsDriver = (IJavaScriptExecutor)NgDriver;
            var element = ng_deposit_button_element;
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
            ng_deposit_button_element.Click();
            // inspect status message
            var ng_message_element = NgDriver.FindElement(NgBy.Binding("message"));
            StringAssert.Contains("Deposit Successful", ng_message_element.Text);
            //NgDriver.Highlight(ng_message_element);
            jsDriver.ExecuteScript(highlightJavascript, new object[] { ng_message_element });
            // re-read the amount
            int updated_account_amount = -1;
            //int.TryParse(NgDriver.FindElement(NgBy.Binding("amount")).Text.FindMatch(@"(?<result>\d+)$"), out updated_account_amount);
            int.TryParse(Regex.Match(NgDriver.FindElement(NgBy.Binding("amount")).Text, pattern).Value, out updated_account_amount);
            Console.WriteLine("updated_account_amount: " + updated_account_amount);
            Assert.AreEqual(updated_account_amount, account_amount + 100);
           


        }

        [Test]
        public void ShouldListTodos()
        {
            NgDriver.Navigate().GoToUrl("http://www.angularjs.org");
            var elements = NgDriver.FindElements(NgBy.Repeater("todo in todoList.todos"));
            Assert.AreEqual("build an AngularJS app", elements[1].Text.Trim());
            Assert.AreEqual(false, elements[1].Evaluate("todo.done"));
        }

        [Test]
        public void Angular2Test()
        {
            NgDriver.Navigate().GoToUrl("https://material.angular.io/");
            NgDriver.FindElement(By.XPath("//a[@routerlink='guide/getting-started']")).Click();
            Assert.AreEqual("https://material.angular.io/guide/getting-started", NgDriver.Url);
        }

        // <summary>
        // Sample test to demonstrate the use of NgWebDriver with angular page.
        // </summary>
        [Test]
        public void HelloNgDriver()
        {
            NgWebElement ngElement = NgDriver.FindElement(NgBy.Model("q"));
            ngElement.Clear();
            ngElement.SendKeys("Hello NgWebDriver");
        }

        //switching this NgWebDriver to legacy IWebDriver I wrote a simple test with wrapper driver to demonstrate that.
        /// <summary>
        /// Sample test to demonstrate the use of wrapper driver with angular and non-angular hybrid page.
        /// </summary>
        [Test]
        public void HelloNgWrapperDriver()
        {
            NgDriver.Navigate().GoToUrl("https://material.angular.io/");
            IWebElement element = NgDriver.WrappedDriver.FindElement(By.CssSelector("[ng-change='search(q)']"));
            element.Clear();
            element.SendKeys("Hello Wrapper Driver"); 
        }

        [Test]
        public void NonAngularPageShouldBeSupported()
        {
            Assert.DoesNotThrow(() =>
            {
                NgDriver.IgnoreSynchronization = true;
                NgDriver.Navigate().GoToUrl("http://www.google.com");
                NgDriver.IgnoreSynchronization = false;
            });
        }
    }
}
