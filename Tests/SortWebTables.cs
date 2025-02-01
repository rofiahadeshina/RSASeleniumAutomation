using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_SeleniumFrameWork.Utilities;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace C_SeleniumFrameWork.Tests
{
    [Parallelizable(ParallelScope.Self)]
    class SortWebTables:Base
    {

        [Test]
        public void SortTable()
        {
            SelectElement dropDown = new SelectElement(driver.Value.FindElement(By.Id("page-menu")));
            dropDown.SelectByValue("20");

            ArrayList a = new ArrayList();

            IList<IWebElement> veggiElements = driver.Value.FindElements(By.CssSelector("tbody tr td:first-child"));

            foreach (IWebElement veggie in veggiElements)
            {
                a.Add(veggie.Text);
            }

            a.Sort();

            //sort on page
            driver.Value.FindElement(By.CssSelector("th:first-child")).Click();

            ArrayList b = new ArrayList();

            IList<IWebElement> sortedVeggiElements = driver.Value.FindElements(By.CssSelector("tbody tr td:first-child"));

            foreach (IWebElement veg in sortedVeggiElements)
            {
                b.Add(veg.Text);
            }

            Assert.That(b, Is.EqualTo(a));




        }
    }
}
