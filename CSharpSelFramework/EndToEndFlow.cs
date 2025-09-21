
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using CSharpSelFramework.utilities;

namespace CSharpSelFramework
{
    public class EndToEndFlow : Base
    {

        [Test]
        public void EndToEndFlowTest()
        {

            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];

            IWebElement usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys("rahulshettyacademy");

            IWebElement passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys("learning");

            IWebElement signBtnElement = driver.FindElement(By.Id("signInBtn"));
            signBtnElement.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible
                (By.PartialLinkText("Checkout")));

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in products)
            {

                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
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

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("country")));
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
