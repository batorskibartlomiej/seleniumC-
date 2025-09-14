using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();

            driver.Url = "https://rahulshettyacademy.com/AutomationPractice//";



        }

        [Test]  
        public void frame()

        {
            //scroll
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                (By.XPath("//div[@class='inner-box']/h1")));

            //Thread.Sleep(1000);
            TestContext.Progress.WriteLine(driver.FindElement(By.XPath("//div[@class='inner-box']/h1")).Text);

            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.XPath("//h1[1]")).Text);


        }




        [Test]
        public void testAlert()
        {
            String name = "janek";
            driver.FindElement(By.Id("name")).SendKeys(name);
            driver.FindElement(By.Id("confirmbtn")).Click();

            string alertText = driver.SwitchTo().Alert().Text;//to pobeira tekst alertu
            driver.SwitchTo().Alert().Accept();
            //driver.SwitchTo().Alert().Dismiss();
           // driver.SwitchTo().Alert().SendKeys("test"); 

            StringAssert.Contains(name, alertText);

        }

        [Test]
        public void test_AutoSuggestiveDropDowns()
        {

            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.CssSelector(".ui-menu-item div")));

            

            IList<IWebElement> options=  driver.FindElements(By.CssSelector(".ui-menu-item div"));

            //String expectedCountry = "India";

            foreach (IWebElement option in options)

            {
                if (option.Text.Equals("India"))
                {
                    option.Click();
                }
                TestContext.WriteLine(driver.FindElement(By.CssSelector(".ui-menu-item div")).Text);

            }

            Assert.That(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"), Is.EqualTo("India"));

              
        }

        [Test]
        public void test_Action()
        {
            driver.Url = "https://rahulshettyacademy.com/";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.PartialLinkText("More"))).Perform();
            Thread.Sleep(1000);
            a.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();

        }


        [Test]
        public void test_action_dragAndDrop()
        {
            driver.Url = "https://demoqa.com/droppable/";
            Actions a = new Actions(driver);
            a.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();
            Thread.Sleep(1000);

        }




        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }


    }
}
