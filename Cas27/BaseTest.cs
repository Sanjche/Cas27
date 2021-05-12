using System;
using OpenQA.Selenium;

namespace Cas27
{
    abstract class BaseTest
    {

        protected IWebDriver driver;

        protected void GoToURL(string url)
        {
            this.driver.Navigate().GoToUrl(url);
        }

        protected IWebElement MyFindElement(By Selector)
        {
            IWebElement ReturnElement = null;

            try
            {
                ReturnElement = this.driver.FindElement(Selector);
            }
            catch (NoSuchElementException)
            {
                
            }

            return ReturnElement;
        }
    }
}
