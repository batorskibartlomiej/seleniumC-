using CSharpSelFramework.utilities;
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
    [Parallelizable(ParallelScope.Self)]
    internal class WindowHandlers: Base
    {

        

        [Test]
        public void WindowHandle()
        {
            String email= "mentor@rahulshettyacademy.com";
            string parentWindowId = driver.Value.CurrentWindowHandle;

            driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material")).Click();
            

            Assert.AreEqual(2, driver.Value.WindowHandles.Count);
            String childWindowName = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(childWindowName);

             TestContext.Progress.WriteLine(driver.Value.FindElement(By.ClassName("red")).Text);

            string text = driver.Value.FindElement(By.ClassName("red")).Text;
            //Please email us at mentor@rahulshettyacademy.com with below template to receive response
            string[] splittedText = text.Split("at");
            // mentor@rahulshettyacademy.com with below template to receive response
            string[] trimmedString = splittedText[1].Trim().Split(' ');

            Assert.AreEqual(email, trimmedString[0]);

            driver.Value.SwitchTo().Window(parentWindowId);

            driver.Value.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
            Thread.Sleep(5000);


        }


       



    }
}
