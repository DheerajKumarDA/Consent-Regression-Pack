using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumNUnitExtentReport.PageMethods
{
    public class Verify_PartyConsent_NewID
    {
        public IWebDriver driver;

        public Verify_PartyConsent_NewID(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetNewId(int year, int month, int day)
        {
            driver.Url = "https://chris927.github.io/generate-sa-idnumbers/";
            Thread.Sleep(2000);
            SelectElement selectYear = new SelectElement(driver.FindElement(By.Id("year")));
            selectYear.SelectByIndex(year);
            SelectElement selectMonth = new SelectElement(driver.FindElement(By.Id("month")));
            selectYear.SelectByIndex(month);
            SelectElement selectDay = new SelectElement(driver.FindElement(By.Id("day")));
            selectYear.SelectByIndex(day);

            driver.FindElement(By.XPath("//body/div[1]/div[1]/div[1]/form[1]/input[1]")).Click();
            Thread.Sleep(2000);

            string id = driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/p[1]")).Text;
            return id;

        }

        public void EnterId_ConsentUi(string id)
        {
            IWebElement body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Control + 't');
            driver.Url = "http://datapp32/DAConsentEngineUI/FRG";
        }

    }
}

