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
            HomePage pomHomePage = new HomePage(this.driver);
            pomHomePage.GoToPage();

            Assert.IsTrue(pomHomePage.IsPageDisplayed());
        }

        [Test]
        [Category("POM")]
        public void TestLogin()
        {
            HomePage pomHomePage = new HomePage(this.driver);

            pomHomePage.GoToPage();

            LoginPage pomLoginPage = pomHomePage.ClickOnLogin();

            Assert.IsTrue(pomLoginPage.IsPageDisplayed());

            pomLoginPage.EnterUsername("aaa");
            pomLoginPage.EnterPassword("aaa");
            pomHomePage = pomLoginPage.ClickOnLoginButton();

            Assert.IsTrue(pomHomePage.IsUserLoggedIn());
        }

        [Test]
        [Category("POM")]
        public void TestRegistration()
        {
            HomePage pomHomePage = new HomePage(this.driver);

            pomHomePage.GoToPage();
            RegisterPage pomRegisterPage = pomHomePage.ClickOnRegister();

            pomRegisterPage.EnterFirstName("Test Ime");
            pomRegisterPage.EnterLastName("Test Prezime");
            pomRegisterPage.EnterEmail("Test@Email.local");
            pomRegisterPage.EnterUsername("TestKorisnickoIme");
            pomRegisterPage.EnterPassword("TestLozinka");
            pomRegisterPage.EnterPasswordAgain("TestLozinka");
            pomHomePage = pomRegisterPage.ClickOnRegisterButton();

            Assert.IsTrue(pomHomePage.IsAlertSuccessVisible());
        }

        [Test]
        [Category("POM")]
        public void TestIsCartEmpty()
        {
            HomePage pomHomePage = new HomePage(this.driver);

            pomHomePage.GoToPage();

            LoginPage pomLoginPage = pomHomePage.ClickOnLogin();

            Assert.IsTrue(pomLoginPage.IsPageDisplayed());

            pomLoginPage.EnterUsername("aaa");
            pomLoginPage.EnterPassword("aaa");

            pomHomePage = pomLoginPage.ClickOnLoginButton();

            Assert.IsTrue(pomHomePage.IsUserLoggedIn());
            Assert.IsTrue(pomHomePage.IsCartEmpty());
        }

    }
}
