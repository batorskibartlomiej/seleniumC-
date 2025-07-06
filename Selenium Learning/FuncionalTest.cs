using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Learning
{
    internal class FuncionalTest
    {
        IWebDriver driver;



        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Setup method execution");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //implicit wait
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";



        }

        [Test]
        public void dropdown()
        {
            IWebElement dropdown = driver.FindElement(By.CssSelector("select.form-control"));

            SelectElement s = new SelectElement(dropdown);
            s.SelectByText("Teacher");
            s.SelectByValue("consult");
            Thread.Sleep(1000);

        }

        [Test]
        public void radioButton()
        {

            IList <IWebElement> rdos=driver.FindElements(By.CssSelector("input[type='radio']"));
            foreach (IWebElement radioButton in rdos)
            {
               if ( radioButton.GetAttribute("value").Equals("user"))
                { 
                    radioButton.Click(); 
                }

            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(By.Id("okayBtn")));

            driver.FindElement(By.Id("okayBtn")).Click();

            bool result = driver.FindElement(By.Id("usertype")).Selected;
            //input[type='radio']

            Assert.That(result, Is.False);//z powodu bledu na stronie

        }


        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Tear dwon method");
            driver.Dispose();
        }
    }
}
