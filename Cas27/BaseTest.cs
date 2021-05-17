using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cas27.Lib;

namespace Cas27
{
    abstract class BaseTest
    {

        protected IWebDriver driver;
        protected WebDriverWait wait;

        protected void GoToURL(string url)
        {
            Logger.log("INFO", $"Opening URL: {url}");
            this.driver.Navigate().GoToUrl(url);
        }

        protected IWebElement MyFindElement(By Selector)
        {
            IWebElement ReturnElement = null;
            Logger.log("INFO", $"Looking for element: <{Selector}>");
            try
            {
                ReturnElement = this.driver.FindElement(Selector);
            }
            catch (NoSuchElementException)
            {
                Logger.log("ERROR", $"Can't find element: <{Selector}>");
            }

            if (ReturnElement != null)
            {
                Logger.log("INFO", $"Element: <{Selector}> found.");
            }


            return ReturnElement;
        }

        protected IWebElement WaitForElement(Func<IWebDriver, IWebElement> ExpectedConditions)
        {
            IWebElement ReturnElement = null;
            Logger.log("INFO", $"Waiting for element.");
            try
            {
                ReturnElement = this.wait.Until(ExpectedConditions);

            }
            catch (WebDriverTimeoutException)
            {
                Logger.log("ERROR", $"Can't wait for element.");
            }

            if (ReturnElement != null)
            {
                Logger.log("INFO", $"Element found.");
            }

            return ReturnElement;
        }

        protected void ExplicitWait(int waitTime)
        {
            Logger.log("INFO", $"Sleeping: {waitTime}ms");
            System.Threading.Thread.Sleep(waitTime);
        }
    }
}
