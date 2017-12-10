using AutomationFramework.Common;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ChallengeFinish : BasePage
    {
        IWebDriver driver;

        public ChallengeFinish(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver; 
        }

        By title = By.CssSelector("h1");

        public void verifyPageFinsihTitle(string Text)
        {
            verifyTitle(Text, title);
        }
    }
}
