using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Cas27.Lib;

namespace Cas27
{
    class Tests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Logger.setFileName(@"C:\Kurs\Tests.log");
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
        [Category("shop.qa.rs")]
        public void TestIfLoginPageIsDisplayed()
        {
            Logger.beginTest("TestIfLoginPageIsDisplayed");
            Logger.log("INFO", "Starting test.");

            this.GoToURL("http://shop.qa.rs");

            IWebElement loginLink = this.MyFindElement(By.LinkText("Login"));
            loginLink?.Click();

            bool formExists = this.ElementExists(By.ClassName("form-signin"));
            bool h2Exists = this.ElementExists(By.XPath("//form/h2[contains(text(), 'Prijava')]"));

            Logger.endTest();
            Assert.IsTrue(formExists);
            Assert.IsTrue(h2Exists);    
            
            /* ekvivalent Assert.IsTrue
            
            if (formExists == true)
            {
                Assert.Pass();
            } else
            {
                Assert.Fail();
            }
            */
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestLogin()
        {
            Logger.beginTest("TestLogin");
            Logger.log("INFO", "Starting test.");

            Assert.IsTrue(Login("aaa", "aaa"));

            Logger.endTest();
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestAddToCart()
        {
            Logger.beginTest("TestAddToCart");
            Logger.log("INFO", "Starting test.");

            Assert.IsTrue(Login("aaa", "aaa"));

            IWebElement dropdown = this.MyFindElement(
                By.XPath(
                    "//div//h3[contains(text(), 'starter')]//ancestor::div[contains(@class, 'panel')]//select"
                )
            );

            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue("7");

            IWebElement orderButton = this.MyFindElement(
                By.XPath(
                    "//div//h3[contains(text(), 'starter')]//ancestor::div[contains(@class, 'panel')]//input[@type='submit']"
                )
            );

            orderButton.Click();

            // domaci ispod ove linije ----------

            Logger.endTest();
        }

        public bool Login(string Username, string Password)
        {
            this.GoToURL("http://shop.qa.rs/login");

            bool formExists = this.ElementExists(By.ClassName("form-signin"));
            Assert.IsTrue(formExists);

            IWebElement inputUsername = this.WaitForElement(EC.ElementIsVisible(By.Name("username")));
            inputUsername.SendKeys(Username);

            IWebElement inputPassword = this.MyFindElement(By.Name("password"));
            inputPassword.SendKeys(Password);

            IWebElement buttonLogin = this.MyFindElement(By.Name("login"));
            buttonLogin.Click();

            return this.ElementExists(By.XPath("//h2[contains(text(), 'Welcome back,')]"));
        }
    }
}
