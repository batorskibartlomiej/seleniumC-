
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using CSharpSelFramework.utilities;
using CSharpSelFramework.pageObjects;

namespace CSharpSelFramework.tests
{
    public class EndToEndFlow : Base
    {

        [Test]
        public void EndToEndFlowTest()
        {

            string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            LoginPage loginPage =  new LoginPage(getDriver());
            
            ProductsPage productPage = loginPage.validLogin("rahulshettyacademy", "learning");
            productPage.waitForCheckPageDisplay();


            

          

            IList<IWebElement> products = productPage.getCards();

            foreach (IWebElement product in products)
            {

                if (expectedProducts.Contains(product.FindElement(productPage.getCardTitle()).Text))
                {
                    product.FindElement(productPage.addToCartButton()).Click();

                }
                TestContext.WriteLine(product.FindElement(productPage.getCardTitle()).Text);

            }

            CheckoutPage checkoutPage = productPage.chekout();
           
            Thread.Sleep(3000);

            IList<IWebElement> checkoutcards = checkoutPage.getCards();
            for (int i = 0; i < checkoutcards.Count; i++)
            {
                actualProducts[i] = checkoutcards[i].Text;

            }
            Assert.AreEqual(expectedProducts, actualProducts);

            DeliveryPage deliveryPage = checkoutPage.checkout();

           

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("country")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(deliveryPage.getCountry()));
            driver.FindElement(deliveryPage.getCountry()).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(deliveryPage.getCountryText()));
            driver.FindElement(deliveryPage.getCountryText()).Click();

            deliveryPage.clickChekbox();
            deliveryPage.clickSubmit();

            //string textAfterPurchase = driver.FindElement(By.ClassName("alert-success")).Text;
            string textAfterPurchase = deliveryPage.getAlertText();
            string expectedText = " Thank you! Your order will be delivered in next few weeks :-).\r\n        ";

            StringAssert.Contains("Succes", textAfterPurchase);

            //IWebElement checkoutElement = driver.FindElement(By.XPath("//div[@id='navbarResponsive']//a[contains(.,'Checkout(0)')]"));
            ////div[@id="navbarResponsive"]//a[contains(.,"Checkout ( 0 )")]

        }

        //[TearDown]
        //public void StopBrowser()
        //{
        //    driver.Dispose();

        //}
    }

}
