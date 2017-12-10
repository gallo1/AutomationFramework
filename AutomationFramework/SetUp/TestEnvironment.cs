using System;

namespace AutomationFramework.SetUp
{
    public class TestEnvironment
    {
        public string Url { get; private set; }
        public static string URL = Properties.Settings.Default.URL; // used for setting URL to the default url defined in the settings designer
        public static TestEnvironment GetEnvironment() //get environment
        {
            switch (Properties.Settings.Default.Environment)
            {
                case EnvironmentType.Test:
                    //Get the test environment
                    return new TestEnvironment(URL);
                case EnvironmentType.UAT:
                    return new TestEnvironment(" ");
                default:
                    throw new ArgumentException("Invalid environment setting has been used!");
            }
        }

        public TestEnvironment(string URL)
        {
            this.Url = URL;
        }
    }
}
