using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.pageObjects
{
    public class CheckoutPage
    {

        private IWebDriver driver;


        //IList<IWebElement> checkoutcards = driver.FindElements(By.XPath("//h4/a"));
        //driver.FindElement(By.ClassName("btn-success")).Click();

        public CheckoutPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//h4/a")]
        private IList<IWebElement> checkoutCards;

        public IList<IWebElement> getCards()
        {
            
            return checkoutCards;
        }

        [FindsBy(How = How.ClassName, Using = "btn-success")]
        private IWebElement checkoutBtn;

        public DeliveryPage checkout()
        {

            checkoutBtn.Click();
            return new DeliveryPage(driver);
        }

    }
}
