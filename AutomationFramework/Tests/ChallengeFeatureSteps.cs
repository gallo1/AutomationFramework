using AutomationFramework.Common;
using AutomationFramework.PageObjects;
using AutomationFramework.SetUp;
using System;
using TechTalk.SpecFlow;

namespace AutomationFramework
{
    [Binding]
    public class ChallengeFeatureSteps : BaseTest
    {
        [Given(@"I am on the Challenge Home Page and click the '(.*)'")]
        public void GivenIAmOnTheChallengeHomePageAndClickThe(string submit)
        {
            switch (submit)
            {
                case "Start the Challenge Button":
                    ChallengeHome home = new ChallengeHome(driver);
                    home.verifyTitleHomePage("Scripting Challenge");
                    home.clickSubmitButton();
                    TestReport.Test().Pass("Start Challenge Successfull");
                    break;
                default:
                    TestReport.Test().Fail("Failure! Challenge unable to start");
                    ScreenShot.takeScreenshot("FeatureFileError_" + ScenarioContext.Current.ScenarioInfo.Title, driver);
                    throw new ArgumentException("Error invalid argument!");
            }
        }



        [Then(@"I should see the '(.*)' page title")]
        [Then(@"I should be on the '(.*)' Page")]
        public void ThenIShouldBeOnThePage(string challengeTitle)
        {
            switch (challengeTitle)
            {
                case "Challenge 1":
                    ChallengeOne one = new ChallengeOne(driver);
                    one.verifyPageOneTitle(challengeTitle);
                    break;
                case "Challenge 2":
                    ChallengeTwo two = new ChallengeTwo(driver);
                    two.verifyPageTwoTitle(challengeTitle);
                    break;
                case "Challenge 3":
                    ChallengeThree three = new ChallengeThree(driver);
                    three.verifyPageThreeTitle(challengeTitle);
                    break;
                case "Challenge 4":
                    ChallengeFour four = new ChallengeFour(driver);
                    four.verifyPageFourTitle(challengeTitle);
                    break;
                case "Challenge 5":
                    ChallengeFive five = new ChallengeFive(driver);
                    five.verifyPageFiveTitle(challengeTitle);
                    break;
                case "Complete!":
                    ChallengeFinish finish = new ChallengeFinish(driver);
                    finish.verifyPageFinsihTitle(challengeTitle);
                    break;
                default:
                    TestReport.Test().Fail("Failue! Unanle to verify title.");
                    ScreenShot.takeScreenshot("FeatureFileError_" + ScenarioContext.Current.ScenarioInfo.Title, driver);
                    throw new ArgumentException("Invalid page title entered");
            }
        }

        [Given(@"I am now on the '(.*)' page I should complete the challenge")]
        public void GivenIAmNowOnThePageIShouldCompleteTheChallenge(string challengeTitle)
        {
            switch (challengeTitle)
            {
                case "Challenge 1":
                    ChallengeOne one = new ChallengeOne(driver);
                    one.fillCorrectTextBox();
                    one.clickSubmit();
                    TestReport.Test().Pass("Challenge 1 successfull!");
                    break;
                case "Challenge 2":
                    ChallengeTwo two = new ChallengeTwo(driver);
                    two.inputText();
                    two.clickSubmit();
                    TestReport.Test().Pass("Challenge 2 successfull!");
                    break;
                case "Challenge 3":
                    ChallengeThree three = new ChallengeThree(driver);
                    three.InputText();
                    three.clickSubmit();
                    TestReport.Test().Pass("Challenge 3 successfull!");
                    break;
                case "Challenge 4":
                    ChallengeFour four = new ChallengeFour(driver);
                    four.sortNumbers();
                    four.clickSubmit();
                    TestReport.Test().Pass("Challenge 4 successfull!");
                    break;
                case "Challenge 5":
                    ChallengeFive five = new ChallengeFive(driver);
                    five.clickSubmit();
                    TestReport.Test().Pass("Challenge 5 successfull!");
                    break;
                case "Complete!":
                    ChallengeFinish finish = new ChallengeFinish(driver);
                    finish.verifyPageFinsihTitle(challengeTitle);
                    TestReport.Test().Pass("Challenge Complete successfull!");
                    break;
                default:
                    TestReport.Test().Fail("Failure! The attemped Challenge has failed.");
                    ScreenShot.takeScreenshot("FeatureFileError_" + ScenarioContext.Current.ScenarioInfo.Title, driver);
                    throw new ArgumentException("Invalid page title entered");
            }
        }

    }
}
