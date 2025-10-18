using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private WebDriverWait wait;


        By checkoutCards = By.XPath("//h4/a");
        By checkoutBtn = By.ClassName("btn-success");

        

        public CheckoutPage(IWebDriver driver, int waitSecond = 10) 
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSecond)); 
        }

        public IList<IWebElement> GetCards()
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
        .PresenceOfAllElementsLocatedBy(checkoutCards));
        }

        public IWebElement GetCheckoutBtn()
        {
            return driver.FindElement(checkoutBtn);
        }

        public DeliveryPage Checkout()
        {
            GetCheckoutBtn().Click();
            var deliveryPage = new DeliveryPage(driver);
            deliveryPage.WaitForPageToLoad(); 
            return deliveryPage;
        }



       

    }
}
