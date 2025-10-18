using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CSharpSelFramework.utilities;
using CSharpSelFramework.pageObjects;
namespace Selenium_Learning
{
    [Parallelizable(ParallelScope.Self)] // run all test files inm project parallel
    //[Parallelizable(ParallelScope.Children)]//run all test methods in one calss parallel
    public class EndToEndFlow : Base
    {
        [Test, TestCaseSource("AddTestDataConfig"), Category("Regression")]
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCaseSource("AddTestDataConfig")]
        // run all test files inm project parallel
        [Parallelizable(ParallelScope.All)]//run all data sets of Test method in parallel
                                           //dotnet test pathto.csproj (alltest) from temrinal
                                           //dotnet test CsharpSelFramework.csproj --filter TestCategory=Smoke //jesli dana kategoria
                                           //cmd /c "dotnet test CSharpSelFramework.csproj --filter ""TestCategory=Smoke"" -- TestRunParameters.Parameter(name=""browserName"",value=""Firefox"")"
                                           //jesli chcemy uruchomic z lini komend musi byc cmd bo powershell zle interpretuje nawiasy
        public void EndToEndFlowTest(String username, String password, String[] expectedProducts)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productPage = loginPage.ValidLogin(username, password);
            productPage.WaitForCheckPageDisplay();
            IList<IWebElement> products = productPage.GetCards();

            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productPage.GetCardTitle()).Text))
                {
                    product.FindElement(productPage.AddToCartButton()).Click();
                }
                TestContext.WriteLine(product.FindElement(productPage.GetCardTitle()).Text);
            }

            CheckoutPage checkoutPage = productPage.Checkout();
            IList<IWebElement> checkoutCards = checkoutPage.GetCards();
            List<string> actualProducts = new List<string>();

            foreach (var card in checkoutCards)
            {
                actualProducts.Add(card.Text);
            }
            CollectionAssert.AreEquivalent(expectedProducts, actualProducts);
            DeliveryPage deliveryPage = checkoutPage.Checkout();

            deliveryPage.EnterCountry("ind");
            deliveryPage.GetCountryText().Click();
            deliveryPage.ClickCheckbox();
            deliveryPage.ClickSubmit();
            string textAfterPurchase = deliveryPage.GetAlertMessage();
            string expectedText = " Thank you! Your order will be delivered in next few weeks :-).\r\n        ";
            StringAssert.Contains("Succes", textAfterPurchase);
        }

        

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
}
