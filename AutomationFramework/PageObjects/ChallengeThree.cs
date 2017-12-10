using AutomationFramework.Common;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ChallengeThree : BasePage
    {
        IWebDriver driver;
        public ChallengeThree(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver; 
        }

        By strong = By.CssSelector("strong#value");
        By input = By.CssSelector("input.form-control");

        public void verifyPageThreeTitle(string Text)
        {
            base.verifyTitle(Text, ChallengeVariable.title);
        }

        public void clickSubmit()
        {
            base.Click(ChallengeVariable.submitButton);
        }

        public void InputText()
        {
            string text = returnText(strong, 20);
            sendText(input, text);
        }

    }
}
