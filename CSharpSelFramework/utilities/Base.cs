

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

        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        //[OneTimeSetUp]
        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Setup method execution");
            //configuration
            string browsername = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browsername);


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

            {
                try
                {
                    if (driver.Value != null)
                    {
                        driver.Value.Quit();      // <-- zamyka przeglądarkę
                        driver.Value.Dispose();   // <-- zwalnia zasoby po zamknięciu
                        driver.Value = null;
                    }
                }
                catch (Exception e)
                {
                    TestContext.WriteLine("Błąd przy zamykaniu przeglądarki: " + e.Message);
                }

            }


        }
    }
    } 
