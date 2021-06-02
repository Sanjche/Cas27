using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cas27.Lib;

namespace Cas27.PageObjects
{
    class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver) { }

        public IWebElement inputFirstName
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("ime"));
            }
        }

        public IWebElement inputLastName
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("prezime"));
            }
        }

        public IWebElement inputEmail
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("email"));
            }
        }

        public IWebElement inputUsername
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("korisnicko"));
            }
        }

        public IWebElement inputPassword
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("lozinka"));
            }
        }

        public IWebElement inputPasswordRepeat
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("lozinkaOpet"));
            }
        }

        public IWebElement buttonRegister
        {
            get
            {
                return this.WaitForElementToBeVisible(By.Name("register"));
            }
        }

        public void EnterFirstName(string FirstName)
        {
            this.inputFirstName.SendKeys(FirstName);
        }

    }
    
}
