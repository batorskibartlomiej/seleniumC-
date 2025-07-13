using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Learning
{
    internal class AlertsActionsAutoSuggestive
    {


        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();

            driver.Url = "https://rahulshettyacademy.com/AutomationPractice//";



        }

        [Test]
        public void testAlert()
        {
            String name = "janek";
            driver.FindElement(By.Id("name")).SendKeys(name);
            driver.FindElement(By.Id("confirmbtn")).Click();

            string alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            //driver.SwitchTo().Alert().Dismiss();
           // driver.SwitchTo().Alert().SendKeys("test"); 

            StringAssert.Contains(name, alertText);


        }




        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }


    }
}
