using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Cas27
{
    class SeleniumTests : BaseTest
    {

        [SetUp]
        public void SetUp()
        {
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
        }

        [Test]
        public void TestGoogleSearch()
        {
            MyFunctions.Wait(500);
            this.GoToURL("https://www.google.com/");

            IWebElement SearchField = this.MyFindElement(By.Name("q"));
            SearchField.SendKeys("Selenium automation with C#");

            MyFunctions.Wait(500);

            IWebElement SearchButton = this.MyFindElement(By.Name("btnK"));
            SearchButton.Click();

            MyFunctions.Wait(500);

            IWebElement ChangeToEnglishLink = this.MyFindElement(By.PartialLinkText("to English"));
            ChangeToEnglishLink.Click();

            MyFunctions.Wait(2000);

            IWebElement Body = this.MyFindElement(By.TagName("body"));
            if (Body.Text.Contains("Videos"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Test failed - No videos present.");
            }
        }



    }
}