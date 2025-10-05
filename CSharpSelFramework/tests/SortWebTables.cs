using CSharpSelFramework.utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium_Learning
{
    [Parallelizable(ParallelScope.Self)]
    public class SortWebTables : Base
    {

        

        [Test]
        public void SortTable()
        {
            ArrayList a = new ArrayList();
            

            SelectElement dropdwon = new SelectElement(driver.Value.FindElement(By.Id("page-menu")));
            dropdwon.SelectByValue("20");

            //step1- get all veggie names into arraylist A
            IList <IWebElement> veggies = driver.Value.FindElements(By.XPath("//td[1]"));

            foreach (IWebElement element in veggies)
            {
                a.Add(element.Text);
            }

            //sort this arraylist
            foreach (String element in a)
            {
                TestContext.WriteLine(element);
            }

            TestContext.WriteLine("after sorting");
            a.Sort();

          



            foreach (String element in a)
            {
                TestContext.WriteLine(element); 
            }
            //step3 - go and click column

            driver.Value.FindElement(By.XPath("//span[text()='Veg/fruit name']")).Click();

            //th[aria - label *= fruit]- przykald jesli chcemy wpisac tylko czesc tekstu
            //th[contains(@aria-label, 'fruit name') 

            Thread.Sleep(4000);



            //step4 - get all veggie names into arraylist B

            ArrayList b = new ArrayList();

            IList<IWebElement> veggiesAfterSorting = driver.Value.FindElements(By.XPath("//td[1]"));

            foreach(IWebElement element in veggiesAfterSorting)
            {
                b.Add(element.Text);
            }

            //compare array A to B = equal

            //Assert.That(a.Equals(b), Is.True);
            Assert.AreEqual(a, b);


        }



    }


}
