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
    public class WDMeetingDetailPage
    {
        private IWebDriver Driver { get; set; }

        public WDMeetingDetailPage(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public string MeetingDetailCompanyName { get 
        {
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("h2#detail-issuer-name")));
                return Driver.FindElement(By.CssSelector("h2#detail-issuer-name")).Text; } 
        }

        public IWebElement CompanySearchBox { get
        {
            WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 30));
                wait.Until(
                    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("#kendo-Search-for-company")));
                return Driver.FindElement(By.CssSelector("#kendo-Search-for-company")); }
        }
    }
}
