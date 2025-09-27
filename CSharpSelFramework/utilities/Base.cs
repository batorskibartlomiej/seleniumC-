

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSelFramework.utilities
{
    public class Base
    {

        public IWebDriver driver;


        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Setup method execution");
            //configuration
            string browsername = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browsername);


            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //// Wyłączenie menedżera haseł i komunikatów o wycieku
            //var options = new ChromeOptions();
            //options.AddArgument("--guest");
            // driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        public IWebDriver getDriver()
        {
            return driver;  
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
                    driver = new FirefoxDriver(options);
                    break;
                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    // Wyłączenie menedżera haseł i komunikatów o wycieku
                    var options1 = new ChromeOptions();
                    options1.AddArgument("--guest");
                    driver = new ChromeDriver(options1);
                    break;
                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    // Wyłączenie menedżera haseł i komunikatów o wycieku
                    var options2 = new EdgeOptions();
                    options2.AddArgument("--guest");
                    driver = new EdgeDriver(options2);
                    break;


            }
        }



            [TearDown]
            public void AfterTest()
            {
                driver.Dispose();
                driver.Quit();

            }


        }
    } 
