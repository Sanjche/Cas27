using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Cas27.Lib;
using System.Collections.ObjectModel;

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
        public void TestAddToCartAndCheckout()
        {
            string PackageName = "pro";
            string PackageQuantity = "5";

            Logger.beginTest("TestAddToCartAndCheckout");
            Logger.log("INFO", "Starting test.");

            Assert.IsTrue(Login("aaa", "aaa"));

            IWebElement dropdown = this.MyFindElement(
                By.XPath(
                    $"//div//h3[contains(text(), '{PackageName}')]//ancestor::div[contains(@class, 'panel')]//select"
                )
            );

            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue(PackageQuantity);

            IWebElement orderButton = this.MyFindElement(
                By.XPath(
                    $"//div//h3[contains(text(), '{PackageName}')]//ancestor::div[contains(@class, 'panel')]//input[@type='submit']"
                )
            );

            orderButton.Click();

            // domaci ispod ove linije ----------

            this.WaitForElement(
                EC.ElementIsVisible(
                    By.XPath("//h1[contains(., 'Order #')]")
                )
            );

            Assert.AreEqual(this.driver.Url, "http://shop.qa.rs/order");

            ReadOnlyCollection<IWebElement> redovi = this.driver.FindElements(
                By.XPath(
                    $"//tr//td[contains(text(), '{PackageName.ToUpper()}')]//parent::tr//td[contains(text(), '{PackageQuantity}')]//parent::tr"
                )
            );

            Assert.GreaterOrEqual(redovi.Count, 1);

            IWebElement totalColumn = this.MyFindElement(By.XPath("//td[contains(., 'Total:')]"));

            string cartTotal = totalColumn.Text.Substring(7);
            //string cartTotal = totalColumn.Text.Replace("Total: ", "");

            Logger.log("INFO", $"Cart price is: {cartTotal}");

            //

            IWebElement checkoutButton = this.MyFindElement(By.Name("checkout"));
            checkoutButton?.Click();

            IWebElement checkoutSuccess = this.WaitForElement(
                EC.ElementIsVisible(
                    By.XPath("//h2[contains(., 'Order #')]")
                )
            );

            Assert.IsTrue(checkoutSuccess.Displayed);

            IWebElement h3charged = this.MyFindElement(By.XPath("//h3[contains(., 'Your credit card has been charged')]"));
            
            string cardCharged = h3charged.Text.Substring(h3charged.Text.IndexOf("$"));
            Logger.log("INFO", $"Card charged: {cardCharged}");

            Assert.AreEqual(cartTotal, cardCharged);

            Logger.endTest();
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestRegister()
        {
            Logger.beginTest("TestRegister");
            Logger.log("INFO", "Starting test.");

            this.GoToURL("http://shop.qa.rs/");

            IWebElement registerLink = this.WaitForElement(EC.ElementToBeClickable(By.LinkText("Register")));
            registerLink?.Click();

            Assert.AreEqual(this.driver.Url, "http://shop.qa.rs/register");

            this.PopulateInput(By.Name("ime"), "TestIme");

            this.PopulateInput(By.Name("prezime"), "TestPrezime");

            this.PopulateInput(By.Name("email"), "test@email.local");

            this.PopulateInput(By.Name("korisnicko"), "testime");

            this.PopulateInput(By.Name("lozinka"), "nekajakasifra");

            this.PopulateInput(By.Name("lozinkaOpet"), "nekajakasifra");

            IWebElement registerButton = this.MyFindElement(By.Name("register"));
            registerButton?.Click();

            IWebElement alertDiv = this.WaitForElement(
                EC.ElementIsVisible(
                    By.XPath("//div[contains(@class, 'alert-success')]")
                )
            );

            Assert.IsTrue(alertDiv.Displayed);

            Logger.endTest();
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestLogout()
        {
            Logger.beginTest("TestLogout");
            Logger.log("INFO", "Starting test.");

            Assert.IsTrue(Login("aaa", "aaa"));

            IWebElement logout = this.MyFindElement(By.XPath("//a[contains(., 'Logout')]"));

            Assert.IsTrue(logout.Displayed);
            logout.Click();

            IWebElement login = this.WaitForElement(
                EC.ElementIsVisible(
                    By.LinkText("Login")
                )
            );

            Assert.IsTrue(login.Displayed);

            Logger.endTest();
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestSignin()
        {
            Logger.beginTest("TestSignin");
            Logger.log("INFO", "Starting test.");

            this.GoToURL("http://shop.qa.rs/");

            IWebElement signin = this.MyFindElement(By.LinkText("Sign in"));
            signin.Click();

            this.WaitForElement(
                EC.ElementIsVisible(
                    By.XPath("//h2[contains(text(),'Prijava')]")
                )
            );

            Logger.endTest();
        }


        [Test]
        [Category("playground")]
        public void TestStringPlayground()
        {
            Logger.beginTest("TestStringPlayground");
            Logger.log("INFO", "Starting test.");

            this.GoToURL("http://shop.qa.rs/");

            IWebElement text = this.MyFindElement(By.XPath("//h3[contains(., 'BUGS')]"));

            int stringStart = 6;
            int length = 0;
            length = (text.Text.IndexOf(" ", stringStart) - stringStart);
            string your = text.Text.Substring(stringStart, length);

            Logger.log("INFO", $"Value of your: {your}");

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
