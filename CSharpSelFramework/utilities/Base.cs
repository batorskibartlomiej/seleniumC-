
//#pragma warning disable NUnit1032

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

//using ICSharpCode.SharpZipLib.Zip;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V137.Page;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;


namespace CSharpSelFramework.utilities
{
    public class Base
    {

        String browserName;
        public ExtentReports extent;//main report
        public ExtentTest test;//test report
        //report file
        [OneTimeSetUp]
        public void Setup()
        {   
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Bartek");

        }

        //public IWebDriver driver;
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        //[OneTimeSetUp]
        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);//entry do raportow 
            TestContext.Progress.WriteLine("Setup method execution");
            //configuration
            //cmd /c "dotnet test CSharpSelFramework.csproj --filter ""TestCategory=Smoke"" -- TestRunParameters.Parameter(name=""browserName"",value=""Firefox"")"
            //jesli chcemy uruchomic z lini komend musi byc cmd bo powershell zle interpretuje nawiasy
            browserName = TestContext.Parameters["browserName"];
            if(browserName == null)
            { 
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            
            InitBrowser(browserName);


            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //// Wyłączenie menedżera haseł i komunikatów o wycieku
            //var options = new ChromeOptions();
            //options.AddArgument("--guest");
            // driver = new ChromeDriver(options);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    // Wyłączenie menedżera haseł i komunikatów o wycieku
                    var options = new FirefoxOptions();
                    options.AddArgument("--guest");
                    driver.Value = new FirefoxDriver(options);
                    break;
                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    // Wyłączenie menedżera haseł i komunikatów o wycieku
                    var options1 = new ChromeOptions();
                    options1.AddArgument("--guest");
                    driver.Value = new ChromeDriver(options1);
                    break;
                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    // Wyłączenie menedżera haseł i komunikatów o wycieku
                    var options2 = new EdgeOptions();
                    options2.AddArgument("--guest");
                    driver.Value = new EdgeDriver(options2);
                    break;


            }
        }
        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }



       


        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            if (status == TestStatus.Failed)
            {
                string screenshotBase64 = CaptureScreenshot(driver.Value);
                test.Fail("❌ Test failed: " + TestContext.CurrentContext.Result.Message)
                    .AddScreenCaptureFromBase64String(screenshotBase64, "Screenshot on Failure");

                if (stackTrace != null)
                    test.Log(Status.Fail, "StackTrace:\n" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("✅ Test passed successfully");
            }
            else if (status == TestStatus.Skipped)
            {
                test.Skip("⚠️ Test skipped");
            }

            driver.Value.Quit();
            extent.Flush();

            TestContext.Progress.WriteLine("🧹 Browser closed and report updated.");
        }

        private string CaptureScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            OpenQA.Selenium.Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }



    }
}
    
