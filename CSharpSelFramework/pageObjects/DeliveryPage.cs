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
    public class DeliveryPage
    {
        private IWebDriver driver;
        private readonly WebDriverWait wait;
        


        private By country = By.Id("country");
        private By countryText = By.LinkText("India");
        private By checkbox = By.XPath("//label[@for='checkbox2']");
        private By submitBtn = By.XPath("//input[@type='submit']");
        private By alertText = By.ClassName("alert-success");


        public DeliveryPage(IWebDriver driver, int waitSeconds = 10) 
        { 
            this.driver = driver;
            //PageFactory.InitElements(driver,this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));

        }

        public IWebElement GetCountryElement()
        {


            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(country));
        }

        public IWebElement GetCountryText()
        {
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(countryText));

        }





        public void EnterCountry(string countryName)//metoda void wykonuje akcje a Iwebelement zwracaElement na ktorym mozna cos dzialac lub sprawdzenie w tescie
        {
            var element = GetCountryElement();
            element.Clear();
            element.SendKeys(countryName);
        }

        

       

        public void ClickCheckbox()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkbox)).Click();
        }


        public void ClickSubmit()
        {
            driver.FindElement(submitBtn).Click();  
        }

        public string GetAlertMessage()
        {

            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(alertText)).Text;
            
        }

        public void WaitForPageToLoad()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(country));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkbox));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(submitBtn));
        }
    }

    
}
