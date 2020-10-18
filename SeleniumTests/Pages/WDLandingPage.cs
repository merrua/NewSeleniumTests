using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    public class WDLandingPage
    {
        private IWebDriver Driver { get; set; }

        public WDLandingPage(IWebDriver Driver)
        {
            this.Driver = Driver;            
        }

        public IWebElement Grid { get { return Driver.FindElement(By.CssSelector("#grid")); } }
        public List<IWebElement> GridData {  get {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30)); 
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("tr > td:nth-child(4)")));
                return Driver.FindElements(By.CssSelector("tr.k-alt > td:nth-child(4)")).ToList(); } }

        public List<IWebElement> TestData { get { return Driver.FindElements(By.CssSelector("tr > td:nth-child(4)")).ToList(); } }

        public IWebElement CountryTextBox { get { return Driver.FindElement(By.CssSelector("input#txt-multiselect-static-search-CountryFilter")); } }


        public IWebElement HideCountryFilter
        {
            get
            {
                string selector = "div#filterid-CountryFilter.container-title";
                return Driver.FindElement(By.CssSelector(selector));
            }
        }

        public IWebElement ShowCountryFilter
        {
            get
            {
                string selector = "div#filterid-CountryFilter.container-title";
                return Driver.FindElement(By.CssSelector(selector));
            }
        }

        public IWebElement CountrySelectAllCheckbox { get {
                return Driver.FindElement(By.CssSelector("#multiselect-static-content-CountryFilter"));
            } }

        public IWebElement CheckboxBelgium { get { 
                return Driver.FindElement(By.CssSelector("#Belgium-cb-label-CountryFilter")); 
            } }

        public IWebElement GetCheckboxForCountry(string country)
        {
            string CountrySelector = "#"+ country + "-cb-label-CountryFilter";
            Console.WriteLine(CountrySelector);
            return Driver.FindElement(By.CssSelector(CountrySelector));
        }

        public IWebElement CountryButtonUpdate { get { return Driver.FindElement(By.CssSelector("#btn-update")); } }
        public IWebElement CountryButtonClose { get { return Driver.FindElement(By.CssSelector("#btn-close")); } }

        public IWebElement FirstPageButton
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a.k-link:nth-child(1)")));
                return Driver.FindElement(By.CssSelector("a.k-link:nth-child(1)"));
            }
        }

        public IWebElement PreviousPageButton
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a.k-link:nth-child(2)")));
                return Driver.FindElement(By.CssSelector("a.k-link:nth-child(2)"));
            }
        }

        public IWebElement CurrentPageButton
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".k-current-page")));
                return Driver.FindElement(By.CssSelector(".k-current-page"));
            }
        }

        public IWebElement NextPageButton
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a.k-link:nth-child(4)")));
                return Driver.FindElement(By.CssSelector("a.k-link:nth-child(4)"));
            }
        }

        public IWebElement LastPageButton
        {
            get
            {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a.k-link:nth-child(5)")));
                return Driver.FindElement(By.CssSelector("a.k-link:nth-child(5)"));
            }
        }

        public IWebElement GetCompanyLink(string CompanyName)
        {
            string CompanySelector = "a[aria-label*='"+ CompanyName + "']";
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(CompanySelector)));
            return Driver.FindElement(By.CssSelector(CompanySelector));
        }

        public List<IWebElement> GetGridCompanyList()
        {
            string CompanySelector = "tr > td:nth-child(1)";
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
            wait.Until(
                      SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(CompanySelector)));
            return Driver.FindElements(By.CssSelector(CompanySelector)).ToList();
        }

        public List<IWebElement> GetGridMeetingDateList()
        {
            string MeetingDateSelector = "tr > td:nth-child(2)";
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(MeetingDateSelector)));
            return Driver.FindElements(By.CssSelector(MeetingDateSelector)).ToList();
        }

        public List<IWebElement> GetGridMeetingTypeList()
        {
            string MeetingDateSelector = "tr > td:nth-child(3)";
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
            wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(MeetingDateSelector)));
            return Driver.FindElements(By.CssSelector(MeetingDateSelector)).ToList();
        }

        public List<IWebElement> GetGridCountryList()
        {
            string CountrySelector = "tr > td:nth-child(4)";
            return Driver.FindElements(By.CssSelector(CountrySelector)).ToList();
        }

        public IWebElement CompanySearchBox
        {
            get
            {
                string CompanySearchBoxSelector = "#kendo-Search-for-company";
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(CompanySearchBoxSelector)));
                return Driver.FindElement(By.CssSelector(CompanySearchBoxSelector));
            }
        }
    }
}
