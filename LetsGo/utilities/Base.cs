using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace LetsGo.utilities
{
    public class Base
    {

        public IWebDriver driver;


        public IWebDriver GetDriver()
        {
            return driver;
        }

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }


        [TearDown]
        public void AfterTest()

        {
            driver.Quit();

        }


    }
}
