using OpenQA.Selenium;
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
        //By.Id("country")
        //By.LinkText("India")
        //By.XPath("//label[@for='checkbox2']") driver find by czyli Iweb Element
        //By.XPath("//input[@type='submit']")
        //(By.ClassName("alert-success"))
        By country = By.Id("country");
        By countryText = By.LinkText("India");


        public DeliveryPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        
        }

        public void getCountry1()
        {
            driver.FindElement(country);
        }

        public By getCountry()
        {
            return country;

        }

        public By getCountryText()
        {
            return countryText;

        }

        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement checkbox;
       
        public void clickChekbox()
        {
            checkbox.Click();
            
        }

        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement submitBtn;

        public void clickSubmit()
        {
            submitBtn.Click();

        }

        [FindsBy(How = How.ClassName, Using = "alert-success")]
        private IWebElement allertText;

        public string getAlertText()
        {
            return allertText.Text;

        }















    }

    
}
