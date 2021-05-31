using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Cas27.Lib;
using Cas27.PageObjects;

namespace Cas27
{
    class POMTests: BaseTest
    {

        [SetUp]
        public void SetUp()
        {
            Logger.setFileName(@"C:\Kurs\POMTests.log");
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();
            this.wait = new WebDriverWait(this.driver, new TimeSpan(0, 0, 10));
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
        }

        [Test]
        [Category("POM")]
        public void TestHomePage()
        {
            HomePage pomHomePage = new HomePage();
            pomHomePage.Driver = this.driver;

            pomHomePage.GoToPage();

            pomHomePage.linkLogin.Click();

            this.ExplicitWait(5000);
        }

    }
}
