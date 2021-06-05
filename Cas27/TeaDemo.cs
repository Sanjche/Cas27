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
    class TeaDemo : BaseTest
    {
        [SetUp]

        public void SetUp()

        {
            Logger.log("INFO", "Starting Test");
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

        public void PageIsVisibleTest()

        {
            Logger.log("INFO", "Page is wisible Test starting");
            this.GoToURL("http://www.practiceselenium.com/welcome.html");

            IWebElement greetingsmg = this.MyFindElement(By.XPath("//h1[contains(text(),'passionate about')]"));

            Assert.IsTrue(greetingsmg.Displayed);
        }

        [Test]

        public void OurPassion()

        {
            Logger.log("INFO", "OurPassion Test starting");

            this.GoToURL("http://www.practiceselenium.com/welcome.html");
            ExplicitWait(300);
            IWebElement OurPassion = this.MyFindElement(By.XPath("//div/ul/li[contains(.,'Our Passion')]"));
            ExplicitWait(100);
            OurPassion.Click();
            ExplicitWait(100);
            IWebElement experts = this.WaitForElement(EC.ElementIsVisible(By.XPath("//h2[contains(text(),'The Experts')]")));

            Assert.IsTrue(experts.Displayed);


        }

        [Test]
        public void TalkTea()
        {
            Logger.log("INFO", "TalkTea Test starting");
            this.GoToURL("http://www.practiceselenium.com/welcome.html");
            ExplicitWait(300);
            IWebElement talkTea = this.MyFindElement(By.XPath("//div/ul/li[contains(.,'Talk Tea')]"));
            talkTea.Click();
            IWebElement talkTeamsg = this.WaitForElement(EC.ElementIsVisible(By.XPath("//h1[contains(.,'Talk Tea')]")));

            Assert.IsTrue(talkTeamsg.Displayed);

            this.PopulateInput(By.Name("name"), "Mlata");
            this.PopulateInput(By.Name("email"), "mlata@mail.com");
            this.PopulateInput(By.Name("subject"), "Pera");
            this.PopulateInput(By.Name("message"), "neka tamo poruka");

            IWebElement submitbtn = this.MyFindElement(By.XPath("//input[(@class='form-submit')]"));
            submitbtn.Click();


        }

        [Test]

        public void CheckOut()
        {
            Logger.log("INFO", "TalkTea Test starting");
            this.GoToURL("http://www.practiceselenium.com/welcome.html");
            ExplicitWait(300);
            IWebElement checkOut = this.MyFindElement(By.XPath("//div/ul/li[contains(.,'Check Out')]"));
            checkOut.Click();

            

            IWebElement dropdown = this.MyFindElement(By.XPath("//div/select"));

            

            SelectElement select = new SelectElement(dropdown);
            select.SelectByIndex(1);

            IWebElement cardNumber = this.MyFindElement(By.XPath("//div/input[@class='span4']"));
            cardNumber.SendKeys("265897456");

            IWebElement cardHolder =this.MyFindElement(By.XPath("//div/input[@class='span5']"));
            cardHolder.SendKeys("banka");

            IWebElement verification = this.MyFindElement(By.XPath("//div/input[@class='span1']"));
            verification.SendKeys("369");

            IWebElement placeOrder = this.MyFindElement(By.XPath("//div/button"));
            placeOrder.Click();






        }
        









    }
        
}

    