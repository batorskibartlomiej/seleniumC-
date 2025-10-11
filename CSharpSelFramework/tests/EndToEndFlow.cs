
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

            //string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            LoginPage loginPage =  new LoginPage(getDriver());

            ProductsPage productPage = loginPage.validLogin(username, password);
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
           
            

            IList<IWebElement> checkoutcards = checkoutPage.getCards();
            for (int i = 0; i < checkoutcards.Count; i++)
            {
                actualProducts[i] = checkoutcards[i].Text;

            }
            Assert.AreEqual(expectedProducts, actualProducts);

            DeliveryPage deliveryPage = checkoutPage.checkout();

           

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
           
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(deliveryPage.getCountry()));
            driver.Value.FindElement(deliveryPage.getCountry()).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(deliveryPage.getCountryText()));
            driver.Value.FindElement(deliveryPage.getCountryText()).Click();

            deliveryPage.clickChekbox();
            deliveryPage.clickSubmit();

            
            string textAfterPurchase = deliveryPage.getAlertText();
            string expectedText = " Thank you! Your order will be delivered in next few weeks :-).\r\n        ";

            StringAssert.Contains("Succes", textAfterPurchase);

            

        }


        [Test, Category("Smoke")]
        public void LocatorsIdentification()
        {

            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");



            driver.Value.FindElement(By.Name("password")).SendKeys("123456");
            //tagname[attrinute='value']-css
            // #id
            //.classname
            //.text-info span:nth-child(1) input
            //XPATH
            // //tagname[@attrinute='value'][2]- xpath
            //th[aria - label *= fruit]- przykald jesli chcemy wpisac tylko czesc tekstu
            //th[contains(@aria-label, 'fruit name') - kiedy podajemy niepelny napis
            //span[text()='Veg/fruit name'] - a tu pelny

            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
            driver.Value.FindElement(By.XPath("//input[@id='signInBtn']")).Click();
            //driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click;
            //signBtnElement.Click();
            //8

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(By.XPath("//input[@id='signInBtn']"), "Sign In"));
            String errorMsg = driver.Value.FindElement(By.XPath("//div[@class='alert alert-danger col-md-12']")).Text;

            TestContext.Progress.WriteLine(errorMsg);
            //Assert.That(errorMsg.Equals("Incorrect username/password."), Is.True);

            



        }







        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {

            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
            
        }

        
    }

}
