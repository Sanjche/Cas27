using System;
using OpenQA.Selenium;
using Cas27.Lib;

namespace Cas27
{
    abstract class BaseTest
    {

        protected IWebDriver driver;

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

            Logger.log("INFO", $"Element: <{Selector}> found.");

            return ReturnElement;
        }

        protected void ExplicitWait(int waitTime)
        {
            Logger.log("INFO", $"Sleeping: {waitTime}ms");
            System.Threading.Thread.Sleep(waitTime);
        }
    }
}
