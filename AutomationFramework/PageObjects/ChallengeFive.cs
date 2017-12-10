using AutomationFramework.Common;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ChallengeFive : BasePage
    {
        IWebDriver driver;

        public ChallengeFive(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver;
        }

        public void verifyPageFiveTitle(string Text)
        {
            base.verifyTitle(Text, ChallengeVariable.title);
        }

        public void clickSubmit()
        {
            base.Click(ChallengeVariable.submitButton);
        }
    }
}
