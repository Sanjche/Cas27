using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cas27.Lib;

namespace Cas27.PageObjects
{
    class LoginPage: BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement inputUsername
        {
            get
            {
                return this.FindElement(By.Name("username"));
            }
        }

        public IWebElement inputPassword
        {
            get
            {
                return this.FindElement(By.Name("password"));
            }
        }

        public IWebElement buttonLogin
        {
            get
            {
                return this.FindElement(By.Name("login"));
            }
        }

        public IWebElement labelPrijava
        {
            get
            {
                return this.WaitForElementToBeVisible(By.XPath("//h2[text()='Prijava']"));
            }
        }
    }
}
