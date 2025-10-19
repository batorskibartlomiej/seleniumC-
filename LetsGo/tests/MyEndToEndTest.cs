using LetsGo.pageObjects;
using LetsGo.utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsGo.tests
{
    public class MyEndToEndTest: Base
    {


        [Test]
        public void MyEndToEndTestFlow()
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.ValidLogin("test", "password");

        }


    }
}
