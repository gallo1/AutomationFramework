using AutomationFramework.Common;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ChallengeOne : BasePage
    {
        IWebDriver d;
        public ChallengeOne(IWebDriver driver) : base(driver)
        {
            this.d = driver;
        }

        By strong = By.CssSelector("#step1 > p:nth-child(3) > strong");
        By textbox = By.CssSelector("input.form-control");
        By glyph = By.CssSelector("span.input-group-addon");
        By gl = By.CssSelector("span");
        By ok = By.CssSelector("span i.glyphicon.glyphicon-ok");

        public void verifyPageOneTitle(string Text)
        {
            verifyTitle(Text, ChallengeVariable.title);
        }

        public void clickSubmit()
        {
            Click(ChallengeVariable.submitButton);
        }

        public void fillCorrectTextBox()
        {
            var textboxes = GetElements(textbox, 20);
            var glyphs = GetElements(gl, 20);
            string text = returnText(strong, 20);
            int len = glyphs.Count;
            var match = GetElement(ok, 5).Location;
            for (int i = 0; i < len; i++)
            {
                var find = glyphs[i].FindElement(By.CssSelector("i.glyphicon")).Location;
                if(match == find){
                    textboxes[i].SendKeys(text);
                }
            }
        }
    }
}
