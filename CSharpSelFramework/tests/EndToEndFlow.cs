
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
                    product.FindElement(By.CssSelector(".card-footer button")).Click();

                }
                TestContext.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);

            }

            driver.FindElement(By.PartialLinkText("Checkout")).Click();
            Thread.Sleep(3000);

            IList<IWebElement> checkoutcards = driver.FindElements(By.XPath("//h4/a"));
            for (int i = 0; i < checkoutcards.Count; i++)
            {
                actualProducts[i] = checkoutcards[i].Text;

            }
            Assert.AreEqual(expectedProducts, actualProducts);

            driver.FindElement(By.ClassName("btn-success")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("country")));
            driver.FindElement(By.Id("country")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();

            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            string textAfterPurchase = driver.FindElement(By.ClassName("alert-success")).Text;
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
