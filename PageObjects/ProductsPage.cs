using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace C_SeleniumFrameWork.PageObjects
{
    class ProductsPage
    {
        private By addToCart = By.CssSelector(".card-footer .btn");
        IWebDriver driver;

        private By cardTitle = By.CssSelector(" h4.card-title a");
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> appCard;

        //driver.FindElement(By.PartialLinkText("Checkout")).Click();
        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkout;

        //driver.FindElement(By.CssSelector(".btn-success")).Click();
        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement submit;
        public IList<IWebElement> AppCard()
        {
            return appCard;
        }

        public IWebElement CheckOut()
        {
            return checkout;
        }

        public IWebElement Submit()
        {
            return submit;
        }
        public void waitForCheckBoxDisplay()
        {
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(8));

            wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public By AddProductToCart()
        {
            return addToCart;
        }
    }

}
