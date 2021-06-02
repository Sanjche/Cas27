using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cas27.Lib;

namespace Cas27.PageObjects
{
    class HomePage: BasePage
    {

        public HomePage(IWebDriver driver) : base(driver) { }

        public IWebElement labelQAShop
        {
            get
            {
                return this.FindElement(By.XPath("//h1[text()='Quality Assurance (QA) Shop']"));
            }
        }

        public IWebElement linkLogin
        {
            get
            {
                return this.FindElement(By.LinkText("Login"));
            }
        }

        public IWebElement linkRegister
        {
            get
            {
                return this.FindElement(By.LinkText("Register"));
            }
        }

        public IWebElement linkLogout
        {
            get
            {
                return this.FindElement(By.PartialLinkText("Logout"));
            }
        }

        public IWebElement linkViewCart
        {
            get
            {
                return this.FindElement(By.LinkText("View shopping cart"));
            }
        }

        public IWebElement alertSuccess
        {
            get
            {
                return this.FindElement(
                    By.XPath("//div[contains(@class, 'success') and contains(., 'Uspeh')]")
                );
            }
        }

        public bool IsCartEmpty()
        {
            this.linkViewCart.Click();
            this.ExplicitWait(100);
            IWebElement alert = this.FindElement(
                By.XPath("//div[contains(@class, 'warning') and contains(text(), 'Your cart is empty.')]")
            );

            return alert != null;
        }

        public void GoToPage()
        {
            this.GoToURL("http://shop.qa.rs");
        }

        public bool IsUserLoggedIn()
        {
            if (this.linkLogout != null)
            {
                return this.linkLogout.Displayed;
            } else
            {
                return false;
            }
        }

        public bool IsAlertSuccessVisible()
        {
            return this.alertSuccess.Displayed;
        }
    }
}
