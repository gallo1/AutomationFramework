using AutomationFramework.Common;
using OpenQA.Selenium;
using System;

namespace AutomationFramework.PageObjects
{
    public class ChallengeFour : BasePage
    {
        IWebDriver driver;

        public ChallengeFour(IWebDriver driver)
            : base(driver)
        {
            this.driver = driver; 
        }

        By input = By.CssSelector("input.form-control");
        By numbers = By.CssSelector("div#numbers");

        public void verifyPageFourTitle(string Text)
        {
            base.verifyTitle(Text, ChallengeVariable.title);
        }

        public void clickSubmit()
        {
            base.Click(ChallengeVariable.submitButton);
        }

        public void sortNumbers()
        {
            string nums = returnText(numbers, 5);
            string[] splitNums = nums.Split(' ');
            int[] intNums = Array.ConvertAll(splitNums, s => int.Parse(s));
            Array.Sort(intNums);
            var textbox = GetElements(input, 5);
            for (int i = 0; i < intNums.Length; i++)
            {
                textbox[i].SendKeys(intNums[i].ToString());
            }
        }

    }
}
