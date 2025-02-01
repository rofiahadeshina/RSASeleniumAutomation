using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_SeleniumFrameWork.PageObjects;
using C_SeleniumFrameWork.Utilities;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace C_SeleniumFrameWork.Tests
{
    [Parallelizable(ParallelScope.Self)]
    class E2ETest : Base
    {

        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("Regression")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String username, String password, String[] expectedProducts)
        {
            LoginPage loginPage = new LoginPage(driver.Value);
            //username
            loginPage.getUsername().SendKeys(username);

            //password
            loginPage.getPassword().SendKeys(password);

            //click on admin radio button
            IList<IWebElement> rdos = loginPage.getRadio();

            foreach (IWebElement radioButton in rdos)
            {
                if (radioButton.GetAttribute("value").Equals("admin"))
                {
                    radioButton.Click();
                }
            }

            //dropdown web element - pick role from dropdown
            IWebElement dropdown = loginPage.getDropDown();

            SelectElement s = new SelectElement(dropdown);
            s.SelectByValue("consult");

            loginPage.getCheckbox().Click();

            loginPage.getSignInButton().Click();


            //Products Page
            ProductsPage productPage = new ProductsPage(driver.Value);

            productPage.waitForCheckBoxDisplay();

            

            IList<IWebElement> pageProducts = productPage.AppCard();

            foreach (IWebElement product in pageProducts)
            {
                if (expectedProducts.Contains(product.FindElement(productPage.GetCardTitle()).Text))
                {
                    product.FindElement(productPage.AddProductToCart()).Click();
                }
            }

            Thread.Sleep(3000);
            productPage.CheckOut().Click();

            productPage.Submit().Click();

        }

        [Test, Category("Smoke")]
        public void LocatorsIdentification()
        {
            //username
            driver.Value.FindElement(By.Id("username")).SendKeys("RofiahAdeshina");
            driver.Value.FindElement(By.Id("username")).Clear();
            driver.Value.FindElement(By.Id("username")).SendKeys("Rofiah");

            //password
            driver.Value.FindElement(By.Name("password")).SendKeys("Olaitan212.");

            //using css selector => tagname[attribute = 'value']

            IWebElement rsaHomePageLink =
                driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttribute = rsaHomePageLink.GetAttribute("href");
            String expectedValue = "https://rahulshettyacademy.com/documents-request";
            Assert.That(hrefAttribute, Is.EqualTo(expectedValue));

            //driver.FindElement(By.Id("terms")).Click();
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //using xpath => //tagname[@attribute = 'value']
            driver.Value.FindElement(By.XPath("//input[@id = 'signInBtn']")).Click();


            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(
                driver.Value.FindElement(By.XPath("//input[@id = 'signInBtn']")), "Sign In"));

            String errorMessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
           yield return new TestCaseData(GetDataParser().ExtractData("username"), GetDataParser().ExtractData("password"), GetDataParser().ExtractDataArray("products"));

           yield return new TestCaseData(GetDataParser().ExtractData("username_wrong"), GetDataParser().ExtractData("password_wrong"), GetDataParser().ExtractDataArray("products"));
        }
    }
}














//[TestCase("rahulshetty", "learning")]
//[TestCase("rahulshettyacademy", "learning")]

//driver.FindElement(By.CssSelector(".validate")).SendKeys("ind");

//WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));

//wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".suggestions ul li a")));

//IList<IWebElement> countries = driver.FindElements(By.CssSelector(".suggestions ul li a"));

//foreach (IWebElement country in countries)
//{

//    if (country.Text.Equals("India"))
//    {
//        country.Click();
//    }
//}