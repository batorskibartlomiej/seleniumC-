using NUnit.Framework.Internal;
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
    internal class WindowHandlers
    {

        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void WindowHandle()
        {
            String email= "mentor@rahulshettyacademy.com";
            string parentWindowId = driver.CurrentWindowHandle;

            driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")).Click();
            

            Assert.AreEqual(2, driver.WindowHandles.Count);
            String childWindowName = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindowName);

             TestContext.Progress.WriteLine(driver.FindElement(By.ClassName("red")).Text);

            string text = driver.FindElement(By.ClassName("red")).Text;
            //Please email us at mentor@rahulshettyacademy.com with below template to receive response
            string[] splittedText = text.Split("at");
            // mentor@rahulshettyacademy.com with below template to receive response
            string[] trimmedString = splittedText[1].Trim().Split(' ');

            Assert.AreEqual(email, trimmedString[0]);

            driver.SwitchTo().Window(parentWindowId);

            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
            Thread.Sleep(5000);


        }


        [TearDown]
        public void Close()
        {
            driver.Dispose();
        }



    }
}
