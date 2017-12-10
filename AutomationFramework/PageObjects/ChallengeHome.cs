using AutomationFramework.Common;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ChallengeHome : BasePage
    {
        public IWebDriver d; 

        public ChallengeHome(IWebDriver driver) : base(driver)
        {
            this.d = driver;
        }

        By submitButton = By.CssSelector("input.btn.btn-lg.btn-primary");
        By title = By.CssSelector("#wrap > div > div.jumbotron > h2");

        public void clickSubmitButton() // clicks the submit button
        {
            base.Click(submitButton);
        }

        public void verifyTitleHomePage(string text)
        {
            base.verifyTitle(text, title);
        }
    }
}
