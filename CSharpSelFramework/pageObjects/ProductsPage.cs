using AngleSharp.Dom;
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
    public class ProductsPage
    {
        private IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");


        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
        //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                //(By.PartialLinkText("Checkout")));

            //IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));
        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;  
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How= How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;


        public IList<IWebElement> getCards()
        {
            return cards;
        }

        public By getCardTitle()//by-> locator not I web element
        {
            return cardTitle;
        }

        public void waitForCheckPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
            (By.PartialLinkText("Checkout")));
        }




    }
}
