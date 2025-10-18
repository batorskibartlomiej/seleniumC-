using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.PageObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace CSharpSelFramework.pageObjects
{
    public class LoginPage
    {
        

        private IWebDriver driver;
        private WebDriverWait wait;


        private By username = By.Id("username");
        private By checkbox = By.XPath("//label[@for='terms']//input[@id='terms']");
        private By password = By.Id("password");
        private By signInBtn = By.Id("signInBtn");




        public LoginPage(IWebDriver driver, int waitSecond = 10 )
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


        public ProductsPage ValidLogin(string user, string pass)
        {
            GetUserName().SendKeys(user);
            GetPassword().SendKeys(pass);
            ClickCheckbox();
            ClickSignInBtn();
            return new ProductsPage(driver);
        }


    }


}
