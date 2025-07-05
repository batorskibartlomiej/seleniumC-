using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Learning
{
    internal class Locators
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
        public void LocatorsIdentification()
        {

           driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            
            

            driver.FindElement(By.Name("password")).SendKeys("123456");
            //tagname[attrinute='value']-css
            // #id
            //.classname
            //.text-info span:nth-child(1) input
            //XPATH
            // //tagname[@attrinute='value']- xpath

            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();
            //driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click;
            //signBtnElement.Click();
            //8

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(By.XPath("//input[@id='signInBtn']"), "Sign In"));
            String errorMsg = driver.FindElement(By.XPath("//div[@class='alert alert-danger col-md-12']")).Text;
            
            TestContext.Progress.WriteLine(errorMsg);
            //Assert.That(errorMsg.Equals("Incorrect username/password."), Is.True);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttr = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/documents-request";
            Assert.AreEqual(expectedUrl, hrefAttr);
           


        }
        

        [TearDown]
        public void CloseBrowser()
        {
            TestContext.Progress.WriteLine("Tear dwon method");
            driver.Dispose();
        }

    }
}
