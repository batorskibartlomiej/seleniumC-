using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.pageObjects
{
    public class LoginPage
    {
        //IWebElement passwordElement = driver.FindElement(By.Id("password"));
        //driver.FindElement(By.xpath("//label[@for='terms']//input[@id='terms']").click();
        //IWebElement signBtnElement = driver.FindElement(By.Id("signInBtn"));

        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }



        //Pageobject factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.XPath, Using = "//label[@for='terms']//input[@id='terms']")]
        private IWebElement checkbox;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "signInBtn")]
        private IWebElement signInBtn;

        public ProductsPage validLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkbox.Click();
            signInBtn.Click();
            return new ProductsPage(driver);
        }

        public IWebElement getUserName()
        {
            return username;
        }

        public IWebElement getCheckbox()
        {
            return checkbox;
        }

        public IWebElement getPassword()
        {
            return password;
        }

        public IWebElement getSignInBtn()
        {
            return signInBtn;
        }

    }


}
