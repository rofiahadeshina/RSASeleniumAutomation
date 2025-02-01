using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace C_SeleniumFrameWork.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        //driver.FindElement(By.Name("password"))

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        //driver.FindElements(By.CssSelector("input[type = 'radio']"))
        [FindsBy (How = How.CssSelector, Using = "input[type = 'radio']")]
        private IList<IWebElement> radioButton;

        //IWebElement dropdown = driver.FindElement(By.CssSelector("Select.form-control"));
        [FindsBy(How = How.CssSelector, Using = "Select.form-control")]
        private IWebElement dropDownElement;

        //driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        private IWebElement checkBoxElement;

        //driver.FindElement(By.XPath("//input[@id='signInBtn']")).Click();
        [FindsBy(How = How.XPath, Using = "//input[@id='signInBtn']")]
        private IWebElement signInButton;

        public IWebElement getUsername()
        {
            return username;
        }

        public IWebElement getPassword()
        {
            return password;
        }

        public IList<IWebElement> getRadio()
        {
            return radioButton;
        }

        public IWebElement getDropDown()
        {
            return dropDownElement;
        }

        public IWebElement getCheckbox()
        {
            return checkBoxElement;
        }

        public IWebElement getSignInButton()
        {
            return signInButton;
        }
    }
}
