using AutomationFramework.Common;
using OpenQA.Selenium;
using System.Linq;

namespace AutomationFramework.PageObjects
{
    public class ChallengeTwo : BasePage
    {
        IWebDriver driver;

        public ChallengeTwo(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        By strong = By.CssSelector("form#step1 p strong");
        By input = By.CssSelector("form#step1 input.form-control");
        By glyphicon = By.CssSelector("i.glyphicon");
        By ok = By.CssSelector("i.glyphicon.glyphicon-ok");

        public void verifyPageTwoTitle(string Text)
        {
            base.verifyTitle(Text, ChallengeVariable.title);
        }

        public void clickSubmit()
        {
            base.Click(ChallengeVariable.submitButton);
        }

        public void inputText()
        {
            var textbox = GetElements(input, 20);
            int len = textbox.Count();
            string text = returnText(strong, 20);
            var gly = GetElements(glyphicon, 20);
            var match = GetElement(ok, 20).Location;
            for (int i = 0; i < len; i++)
            {
                var find = gly[i].Location;
                if(find == match)
                {
                    textbox[i].SendKeys(text);
                }
            }
        }

    }
}
