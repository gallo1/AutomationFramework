using System.ComponentModel;

namespace AutomationFramework.SetUp
{
    public enum BrowserType //used with settings designer to set default web drivers
    {
        [Description("Chrome")]
        Chrome = 0, 
        [Description("FireFox")]
        FireFox = 1,
        [Description("InternetExplorer")]
        InternetExplorer = 2,
        [Description("PhantomJS")]
        PhantomJS = 3,
    }
}
