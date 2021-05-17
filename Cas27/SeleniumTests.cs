using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Cas27.Lib;
using System.Diagnostics;

namespace Cas27
{
    class SeleniumTests : BaseTest
    {

        [SetUp]
        public void SetUp()
        {
            Logger.setFileName(@"C:\Kurs\SeleniumTests.log");
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
        public void TestGoogleSearch()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Logger.beginTest("TestGoogleSearch");
            Logger.log("INFO", "Starting test.");

            this.GoToURL("https://www.google.com/");

            IWebElement SearchField = this.MyFindElement(By.Name("q"));
            SearchField.SendKeys("Selenium automation with C#");

            IWebElement SearchButton = this.WaitForElement(
                EC.ElementToBeClickable(By.XPath("//input[@name='btnK']"))
            );
            
            SearchButton?.Click();
            /*
             * ? menja ovaj if:
             * 
             * if (SearchButton != null)
             * {
             *     SearchButton.Click();
             * }
            */

            IWebElement ChangeToEnglishLink = this.MyFindElement(By.XPath("//a[contains(., 'to English')]"));
            ChangeToEnglishLink?.Click();

            IWebElement Body = this.MyFindElement(By.TagName("body"));
            bool containsVideos = Body.Text.Contains("Videos");

            stopwatch.Stop();
            Logger.log("INFO", $"Test ran for: {stopwatch.ElapsedMilliseconds / 1000} seconds");
            if (containsVideos)
            {
                Logger.log("ASSERT", "Test passed.");
                Logger.endTest();
                Assert.Pass();
            }
            else
            {
                Logger.log("ASSERT", "Test failed - No videos present.");
                Logger.endTest();
                Assert.Fail("Test failed - No videos present.");
            }
        }



    }
}