
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace CSharpSelFramework.pageObjects
{
    public class ProductsPage
    {
        private IWebDriver driver;
        private readonly WebDriverWait wait;

        By cardTitle = By.CssSelector(".card-title a");
        By addToCart = By.CssSelector(".card-footer button");
        By cards = By.TagName("app-card");
        By checkout = By.PartialLinkText("Checkout");



        public ProductsPage(IWebDriver driver, int waitSecond= 10)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSecond));

        }

        public IList<IWebElement> GetCards()
        {
            return driver.FindElements(cards);
        }


        public By AddToCartButton()
        {
            return addToCart;
        }


        public IWebElement CheckoutButton()
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(checkout));
        }




        public By GetCardTitle()
        {
            return cardTitle;
        }




        public CheckoutPage Checkout()
        {
            CheckoutButton().Click();
            return new CheckoutPage(driver);
        }




        public void WaitForCheckPageDisplay()
        {
            
            wait.Until(ExpectedConditions.ElementIsVisible
            (checkout));
        }




    }
}
