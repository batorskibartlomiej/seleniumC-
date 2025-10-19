

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing.Text;
using System.Threading;

namespace LetsGo.pageObjects
{
    public class LoginPage
    {


        private IWebDriver driver;
        private WebDriverWait wait;


        private By username = By.Id("username");
        private By checkbox = By.XPath("//label[@for='terms']//input[@id='terms']");
        private By password = By.Id("password");
        private By signInBtn = By.Id("signInBtn");

        public LoginPage(IWebDriver driver, int waitSecond = 10) 
        {

            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSecond));
   
        }


        public IWebElement GetUserName()
        {
            return driver.FindElement(username);
        }

        public IWebElement GetPassword()
        {
            return driver.FindElement(password);
        }

        public void ClickCheckbox()
        {
            //driver.FindElement(checkbox).Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkbox)).Click();
        }

        public void ClickSignInBtn()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(signInBtn)).Click();
        }


        public void ValidLogin(string user, string pass)
        {
            GetUserName().SendKeys(user);
            GetPassword().SendKeys(pass);
            ClickCheckbox();
            ClickSignInBtn();
            Thread.Sleep(10000);
            //return new ProductsPage(driver);
        }






    }
}
