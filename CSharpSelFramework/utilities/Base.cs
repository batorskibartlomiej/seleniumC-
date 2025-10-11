
//#pragma warning disable NUnit1032

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSelFramework.utilities
{
    public class Base
    {

        String browserName;
        //public IWebDriver driver;
        public static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        //[OneTimeSetUp]
        [SetUp]
        public void StartBrowser()
        {
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



        //[OneTimeTearDown]
        [TearDown]
        public void AfterTest()
        {

            
                        driver.Value.Dispose();   // <-- zwalnia zasoby po zamknięciu
                 
                


        }

        //[OneTimeTearDown]
        //public void DisposeThreadLocal()
        //{
        //    if (driver != null)
        //    {
        //        driver.Value.Dispose(); // zwalnia sam ThreadLocal
        //    }
        //}
    }
}
    
