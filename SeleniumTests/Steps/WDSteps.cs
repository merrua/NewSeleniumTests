using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    [Binding]
    public sealed class WDSteps
    {
        private readonly ScenarioContext context;
        private IWebDriver driver;
        private string testURL = "https://viewpoint.glasslewis.com/WD/";
        private string client = "?siteId=DemoClient";
        private string usersession = "&_=1602871660623"; // TODO how to generate?
        private WebDriverService webDriverService;
        private WDLandingPage wdLandingPage;
        private WDMeetingDetailPage wdMeetingDetailPage;
        private WebDriverWait wait;

        public WDSteps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"user is on the landing page for WD site")]
        public void GivenUserIsOnTheLandingPageForWDSite()
        {
            driver.Navigate().GoToUrl(testURL + client + usersession + "/");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));
            Assert.AreEqual(driver.Title, "Sample Disclosure");
        }

        [Given(@"the Country filter is available")]
        public void GivenTheCountryFilterIsAvailable()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(wdLandingPage.CountryTextBox));
        }

        [When(@"user selects '(.*)' from the Country filter list on left panel")]
        public void WhenUserSelectsFromTheCountryFilterListOnLeftPanel(string country)
        {
            wdLandingPage.GetCheckboxForCountry(country).Click();
        }

        [When(@"clicks on Update button for the country filter list")]
        public void WhenClicksOnUpdateButtonForTheCountryFilterList()
        {
            wdLandingPage.CountryButtonUpdate.Click();
        }

        [Then(@"the grid displays all meetings that are associated with the country '(.*)'")]
        public void ThenTheGridDisplaysAllMeetingsThatAreAssociatedWithTheCountry(string country)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));
            int ExpectedMeetingCount = GetExpectedMeetingCount(country);

            // to replace with check for quiet dom
            Thread.Sleep(1000);

            List<IWebElement> testData = wdLandingPage.GetGridMeetingTypeList();
            //Assert.AreEqual(ExpectedMeetingCount, testData.Count, "We expect to see 24 meetings in the meeting type column but count does not match" + testData.Count);
        }

        private int GetExpectedMeetingCount(string country)
        {
            // I'm assuming the test data set is fixed.
            // Usually we would create the data we are looking for.
            int count = 0;
            switch (country.ToLower())
            {
                case "austria":
                    count = 12;
                    break;
                case "belgium":
                    count = 24;
                    break;
                default:
                    break;
            }
            return count;
        }

        [Then(@"no meetings associated with any other country appear on the list")]
        public void ThenNoMeetingsAssociatedWithAnyOtherCountryAppearOnTheList()
        {
            List<IWebElement> testData = wdLandingPage.GetGridCountryList();

            List<string> countryNames = new List<string>();
            bool result = true;
            int Count = 0;
            int ExpectedCount = 24;

            Count = testData.Count;

            foreach (IWebElement w in testData)
            {
                try
                {
                    countryNames.Add(w.Text);
                }
                catch (StaleElementReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (string w in countryNames)
            {
                Console.WriteLine("T1 :" + w);
            }

            for (int i = 0; i < countryNames.Count; i++)
            {
                if (countryNames[i].ToLower() != "Belgium".ToLower())
                {
                    result = false;
                }
            }

            Assert.IsTrue(result, "Country other than expected is in the grid, number of countries not matching");
            Assert.AreEqual(ExpectedCount, Count, "Count does not match" + Count);
        }

        [When(@"user clicks the Company Name '(.*)' hyperlink")]
        public void WhenUserClicksTheCompanyNameHyperlink(string CompanyName)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));

            // go to next page
            Thread.Sleep(5000);
            wdLandingPage.NextPageButton.Click();
            Thread.Sleep(5000);

            wdLandingPage.GetCompanyLink(CompanyName).Click();

        }

        [Then(@"the user lands onto the '(.*)' vote card page\.")]
        public void ThenTheUserLandsOntoTheVoteCardPage_(string CompanyName)
        {
            Thread.Sleep(1000);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));
        }

        [Then(@"'(.*)' should appear in the top banner")]
        public void ThenShouldAppearInTheTopBanner(string CompanyName)
        {
            string title = wdMeetingDetailPage.MeetingDetailCompanyName;
            Assert.AreEqual(CompanyName, title);
            Assert.AreEqual(driver.Title, "Sample Disclosure");
        }

        [BeforeScenario]
        public void SetupTest()
        {
            IWebDriverFactory webDriverFactory = new WebDriverFactory();
            webDriverService = new WebDriverService(webDriverFactory);
            driver = webDriverService.GetBrowser(BrowserType.Chrome);
            wdLandingPage = new WDLandingPage(driver);
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            driver.Manage().Window.Maximize();

            wdMeetingDetailPage = new WDMeetingDetailPage(driver);
        }

        [AfterScenario]
        public void MyTestCleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
