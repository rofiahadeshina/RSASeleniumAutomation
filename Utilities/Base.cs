using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace C_SeleniumFrameWork.Utilities
{
    class Base
    {
        String browserName;

        public ExtentReports Extent;
        public ExtentTest test;

        //create report file
        [OneTimeSetUp]
        public void SetUp()
        {
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
            Extent.AddSystemInfo("Tester", "Rofiah Adeshina");
            Extent.AddSystemInfo("Host", "LocalHost");

        }

        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void SetUpBrowser()
        {
            
            //read value from terminal at runtime
            browserName = TestContext.Parameters["browserName"];

            if (browserName == null)
            {
                //read value from appconfig
                browserName = ConfigurationManager.AppSettings["browser"];
            }

            test = Extent.CreateTest(TestContext.CurrentContext.Test.Name);

            //invoke chrome browser
            InitBrowser(browserName);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Value.Manage().Window.Maximize(); // maximise browser window
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        public void InitBrowser(String browserName)
        {
            switch(browserName)
            {
                case "FireFox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

            }
        }

        public static JsonReader GetDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorLog = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";


            if (status == TestStatus.Failed)
            {
                test.Fail("Test failed", CaptureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "Test failed with log trace" + errorLog);
            }
            else if (status == TestStatus.Passed)
            {
                
            }
            Extent.Flush();
            
            driver.Value.Quit();
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts =(ITakesScreenshot)driver; //switches driver to screenshot mode
            String screenShotString = ts.GetScreenshot().AsBase64EncodedString; //captures screenshot as base64encoded string

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShotString, screenShotName).Build(); //convert base64string to compatible media entity
        }
    }
}
