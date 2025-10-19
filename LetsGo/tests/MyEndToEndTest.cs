using LetsGo.pageObjects;
using LetsGo.utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGo.tests
{
    public class MyEndToEndTest: Base
    {


        [Test]
        public void WrongPassword()
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.ValidLogin("test", "password");
            string alertText = loginPage.GetAlertMsg();
            Console.WriteLine(alertText);

            Assert.AreEqual("Incorrect username/password.", alertText);

        }


    }
}
