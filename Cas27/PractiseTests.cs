using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Cas27.Lib;
using System.Collections.ObjectModel;

namespace Cas27
{
    class PractiseTesting : BaseTest
    {
        [SetUp]

        public void SetUp()
        {
            Logger.log("INFO:", "Starting test");
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
        public void DisplayingPage()
        {
            Logger.log("INFO", "DisplayingPageTest");
            Logger.log("INFO", "Starting test");

            this.GoToURL("http://shop.qa.rs/");

            Logger.endTest();

        }

        [Test]

        public void Register()


        {


            Logger.log("INFO", "Registration test");
            Logger.log("INFO", "Sarting test");

            this.GoToURL("http://shop.qa.rs/");

            IWebElement registracija = this.MyFindElement(By.LinkText("Register"));
            registracija?.Click();

            Assert.AreEqual(this.driver.Url, "http://shop.qa.rs/register");

            this.PopulateInput(By.Name("ime"), "TIme");
            this.PopulateInput(By.Name("prezime"), "Tprezime");
            this.PopulateInput(By.Name("email"), "test@lozinka.com");
            this.PopulateInput(By.Name("korisnicko"), "sss");
            this.PopulateInput(By.Name("lozinka"), "aaa");
            this.PopulateInput(By.Name("lozinkaOpet"), "aaa");

            IWebElement register = this.WaitForElement(EC.ElementToBeClickable(By.Name("register")));
            register?.Click();

            IWebElement alertDiv = this.WaitForElement(EC.ElementIsVisible(By.XPath("//div[contains(@class,'alert-succes')]")));
            Assert.IsTrue(alertDiv.Displayed);

            Logger.endTest();


        }

        [Test]

        public void login()
        {
            Logger.log("INFO", "StartingLoginTest");

            this.GoToURL("http://shop.qa.rs/");

            IWebElement login = this.MyFindElement(By.LinkText("Login"));
            login.Click();

            IWebElement prijava = this.WaitForElement(EC.ElementIsVisible(By.XPath("//form/h2[contains(text(),'Prijava')]")));

            Assert.IsTrue(prijava.Displayed);

            IWebElement username = this.MyFindElement(By.Name("username"));
            this.PopulateInput(By.Name("username"), "aaa");

            IWebElement passvord = this.MyFindElement(By.Name("password"));
            this.PopulateInput(By.Name("password"), "aaa");

            IWebElement loginbtn = this.MyFindElement(By.Name("login"));
            loginbtn.Click();

            IWebElement greetingmsg = this.WaitForElement(EC.ElementIsVisible(By.XPath("//div/h2[contains(text(),'Welcome back')]")));

            Logger.log("INFO", "Welcome message is displyed");

            ExplicitWait(100);

            Assert.IsTrue(greetingmsg.Displayed);

            Logger.endTest();

        }


        [Test]
        [Category("shop.qa.rs")]
        public void TestLogin()
        {
            Logger.beginTest("TestLogin");
            Logger.log("INFO", "Starting test.");

            Assert.IsTrue(Login("sss", "sss"));

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

            //domaći za 32 cas

            IWebElement chartN = this.WaitForElement(EC.ElementIsVisible(By.XPath("//h2[contains(text(), 'You have successfully placed')]")));
           

            int chartNuberStart = chartN.Text.IndexOf("#");
            int chartNumberEnd = chartN.Text.IndexOf(")", chartNuberStart);
            int chartNumberLenght = chartNumberEnd - chartNuberStart;
            string chartNumber = chartN.Text.Substring(chartNuberStart, chartNumberLenght);



            IWebElement chartP = this.MyFindElement(By.XPath("//h3[contains(.,'Your credit card has been charged')]"));



           
            
            string chartPri = chartP.Text.Substring(chartP.Text.IndexOf("$"));

            string chartPrice = chartPri.Replace("$", ""); 

            Logger.log("INFO", $"{chartNumber}");
            Logger.log("INFO", $"{chartPrice}");

            IWebElement orderHistory = this.MyFindElement(By.LinkText("Order history"));
            orderHistory.Click();

            IWebElement orderN = this.WaitForElement(EC.ElementIsVisible(By.XPath("//tr/td[contains(text(), '#')]")));
            string orderNumber = orderN.Text.Substring(orderN.Text.IndexOf("#"));

            

            Logger.log("INFO", $"{orderNumber}");

            Assert.AreEqual(chartNumber, orderNumber);

            string orderNum = $"{orderNumber}";



            IWebElement orderP = this.WaitForElement(EC.ElementIsVisible(By.XPath($"//tr[contains(.,'{orderNum}')]/td[@class= 'total']")));

            string orderPri = orderP.Text.Substring(0);

            string orderPrice = orderPri.Replace(".00", "");



            Logger.log("INFO",  $" cena je = {orderPrice}");



            Assert.AreEqual(chartPrice, orderPrice);


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
