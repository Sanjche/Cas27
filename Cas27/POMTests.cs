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

            Assert.IsTrue(pomHomePage.labelQAShop.Displayed);
        }

        [Test]
        [Category("POM")]
        public void TestLogin()
        {
            HomePage pomHomePage = new HomePage(this.driver);
            LoginPage pomLoginPage = new LoginPage(this.driver);

            pomHomePage.GoToPage();
            pomHomePage.linkLogin.Click();

            Assert.IsTrue(pomLoginPage.labelPrijava.Displayed);
            this.ExplicitWait(100);

            pomLoginPage.inputUsername.SendKeys("aaa");
            pomLoginPage.inputPassword.SendKeys("aaa");
            pomLoginPage.buttonLogin.Click();

            Assert.IsTrue(pomHomePage.IsUserLoggedIn());
        }

        [Test]
        [Category("POM")]
        public void TestRegistration()
        {
            HomePage pomHomePage = new HomePage(this.driver);
            RegisterPage pomRegisterPage = new RegisterPage(this.driver);

            pomHomePage.GoToPage();
            pomHomePage.linkRegister.Click();

            this.ExplicitWait(100);

            pomRegisterPage.EnterFirstName("Test Ime");
            pomRegisterPage.inputLastName.SendKeys("Test Prezime");
            pomRegisterPage.inputEmail.SendKeys("Test@Email.local");
            pomRegisterPage.inputUsername.SendKeys("TestKorisnickoIme");
            pomRegisterPage.inputPassword.SendKeys("TestLozinka");
            pomRegisterPage.inputPasswordRepeat.SendKeys("TestLozinka");
            pomRegisterPage.buttonRegister.Click();

            Assert.IsTrue(pomHomePage.IsAlertSuccessVisible());
        }

        [Test]
        [Category("POM")]
        public void TestIsCartEmpty()
        {
            HomePage pomHomePage = new HomePage(this.driver);
            LoginPage pomLoginPage = new LoginPage(this.driver);

            pomHomePage.GoToPage();
            pomHomePage.linkLogin.Click();

            Assert.IsTrue(pomLoginPage.labelPrijava.Displayed);
            this.ExplicitWait(100);

            pomLoginPage.inputUsername.SendKeys("aaa");
            pomLoginPage.inputPassword.SendKeys("aaa");
            pomLoginPage.buttonLogin.Click();

            Assert.IsTrue(pomHomePage.IsUserLoggedIn());

            Assert.IsTrue(pomHomePage.IsCartEmpty());
        }

    }
}
