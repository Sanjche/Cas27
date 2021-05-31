using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cas27.Lib;

namespace Cas27.PageObjects
{
    class HomePage: BaseTest
    {

        public HomePage()
        {
            Logger.setFileName(@"C:\Kurs\HomePage.log");
        }

        public IWebDriver Driver
        {
            set
            {
                this.driver = value;
            }
        }

        public IWebElement linkLogin
        {
            get
            {
                return this.MyFindElement(By.LinkText("Login"));
            }
        }

        public void GoToPage()
        {
            this.GoToURL("http://shop.qa.rs");
        }
    }
}
