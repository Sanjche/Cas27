using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Cas27
{
    class SeleniumTests
    {

        IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Close();
        }

        [Test]
        public void TestGoogleSearch()
        {
            MyFunctions.Wait(500);
            Driver.Navigate().GoToUrl("https://www.google.com/");

            IWebElement SearchField = MyFindElement(By.Name("q"));
            SearchField.SendKeys("Selenium automation with C#");

            MyFunctions.Wait(500);

            IWebElement SearchButton = MyFindElement(By.Name("btnK"));
            SearchButton.Click();

            MyFunctions.Wait(500);

            IWebElement ChangeToEnglishLink = MyFindElement(By.PartialLinkText("to English"));
            ChangeToEnglishLink.Click();

            MyFunctions.Wait(2000);

            IWebElement Body = MyFindElement(By.TagName("body"));
            if (Body.Text.Contains("Videos"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Test failed - No videos present.");
            }
        }

        private IWebElement MyFindElement(By Selector)
        {
            IWebElement ReturnElement = null;

            try
            {
                ReturnElement = Driver.FindElement(Selector);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Test failed - Element not found [{0}]", Selector.ToString());
            }

            return ReturnElement;
        }

    }
}