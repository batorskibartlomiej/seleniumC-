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
    public class E2ETest
    {

        IWebDriver driver;



        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("Setup method execution");
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        [Test]
        public void E2Etest()
        {

            String[] expectedProducts = { "iphone X", "Blackberry" };
            IWebElement usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys("rahulshettyacademy");

            IWebElement passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys("learning");

            IWebElement signBtnElement = driver.FindElement(By.Id("signInBtn"));
            signBtnElement.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                (By.PartialLinkText("Checkout")));

            IList <IWebElement> products=  driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in products)
            {
                
             if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();

                }
                TestContext.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                

            }
            
            
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            Thread.Sleep(3000);


            //IWebElement checkoutElement = driver.FindElement(By.XPath("//div[@id='navbarResponsive']//a[contains(.,'Checkout(0)')]"));
            ////div[@id="navbarResponsive"]//a[contains(.,"Checkout ( 0 )")]


        }




        [TearDown]
        public void StopBrowser()
        {
            driver.Dispose();

        }
    }


}
