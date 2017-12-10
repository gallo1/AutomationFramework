using System.ComponentModel;

namespace AutomationFramework.SetUp
{
    public enum EnvironmentType //used with default settings designer to set the default test environment
    {
        [Description("Test")]
        Test = 0, 
        [Description("UAT")] //User Acceptance Testing
        UAT = 1,
    }
}
