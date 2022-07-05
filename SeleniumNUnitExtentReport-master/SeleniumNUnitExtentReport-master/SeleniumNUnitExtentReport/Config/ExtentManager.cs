﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SeleniumNUnitExtentReport.Config
{
    [TestFixture]
    public class ExtentManager
    {
        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public ExtentTest _test;
        public IWebDriver _driver;

        public ExtentManager()
        {

        }

        //public static ExtentReports GetInstance()
        //{
        //    if(extent == null)
        //    {

        //        string reportPath = @"D:\Consent_Automation_Testing\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport\Reports\Report.html";
        //        //var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        //        //var actualPath = path.Substring(0, path.LastIndexOf("bin"));
        //        //var projectPath = new Uri(actualPath).LocalPath;
        //        //Directory.CreateDirectory(projectPath.ToString() + "Reports");
        //        //var reportPath = projectPath + "Reports\\ExtentReport.html";
        //        var htmlReporter = new ExtentHtmlReporter(reportPath);

        //        extent = new ExtentReports();
        //        extent.AttachReporter(htmlReporter);

        //        extent.AddSystemInfo("Host Name", "LocalHost");
        //        extent.AddSystemInfo("Environment", "QA");
        //        extent.AddSystemInfo("Dheeraj Kumar", "TestUser");

        //        string extentConfigPath = @"D:\Consent_Automation_Testing\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport\extent-config.xml";
        //        htmlReporter.LoadConfig(extentConfigPath);
        //    }
        //    return extent;
            
        //}

        
        public static ExtentReports Onetime()
        {
            if (extent == null)
            {
                var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var actualPath = path.Substring(0, path.LastIndexOf("bin"));
                var projectPath = new Uri(actualPath).LocalPath;
                Directory.CreateDirectory(projectPath.ToString() + "Reports");
                var reportPath = projectPath + "Reports\\ExtentReport.html";
                var htmlReporter = new ExtentHtmlReporter(reportPath);

                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);

                extent.AddSystemInfo("Host Name", "LocalHost");
                extent.AddSystemInfo("Environment", "QA");
                extent.AddSystemInfo("Dheeraj Kumar", "TestUser");
            }
            return extent;
        }

        [TearDown]
        protected void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    DateTime time = DateTime.Now;
                    String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
                    String screenShotPath = Capture(_driver, fileName);
                    _test.Log(Status.Fail, "Fail");
                    _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

            //_extent.Flush();
            _driver.Quit();

            extent.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService("webdriver.chrome.driver", @"D:\Consent_Automation_Testing\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport-master\SeleniumNUnitExtentReport\bin\Debug\chromedriver.exe");
            _driver = new ChromeDriver(service);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            _driver.Manage().Window.Maximize();
            _test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
        }
        public IWebDriver GetDriver()
        {
            return _driver;
        }
        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }

    }
}
