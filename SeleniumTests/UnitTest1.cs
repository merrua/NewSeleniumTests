using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        private string testURL = "https://viewpoint.glasslewis.com/WD/";
        private string client = "?siteId=DemoClient";
        private string usersession = "&_=1602871660623"; // TODO how to generate?
        private WebDriverService webDriverService;
        private WDLandingPage wdLandingPage;
        private WDMeetingDetailPage wdMeetingDetailPage;
        private WebDriverWait wait;
        private IJavaScriptExecutor js;

        [Test]
        public void Test_Country_Filter_Belgium_Checkbox()
        {
            int count = 0;
            bool result = true;

            driver.Navigate().GoToUrl(testURL + client + usersession + "/");

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));

            wdLandingPage.CheckboxBelgium.Click();
            wdLandingPage.CountryButtonUpdate.Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));

            // to replace with check for quiet dom
            Thread.Sleep(1000);

            List<IWebElement> testData = wdLandingPage.GetGridCountryList();
            List<string> countryNames = new List<string>();
            count = testData.Count;

            foreach (IWebElement w in testData)
            {
                try
                {
                    countryNames.Add(w.Text);
                }
                catch(StaleElementReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach(string w in countryNames)
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
            Assert.AreEqual(12, count, "Count does not match" + count);
            Assert.AreEqual("Sample Disclosure", driver.Title, "Page title does not match" + driver.Title);
        }

        [Test]
        public void Test_Company_Name()
        {
            //bool found = false;
            List<IWebElement> testData = new List<IWebElement>();
            List<string> companyNames = new List<string>();

            driver.Navigate().GoToUrl(testURL + client + usersession + "/");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));
            Assert.AreEqual(driver.Title, "Sample Disclosure");

            Thread.Sleep(5000);

            // go to next page
            wdLandingPage.NextPageButton.Click();

            Thread.Sleep(5000);

            wdLandingPage.GetCompanyLink("Activision Blizzard Inc").Click();

            Thread.Sleep(1000);

            // go through each page and check for the company we want

            //while (found == false && wdLandingPage.LastPageButton.Enabled == true)
            //{
            //    testData = wdLandingPage.getGridCompanyList();

            //    foreach (IWebElement w in testData)
            //    {
            //        try
            //        {
            //            companyNames.Add(w.Text);
            //            Console.WriteLine(w.Text);
            //        }
            //        catch (StaleElementReferenceException ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //        }
            //    }

            //    for (int i = 0; i < companyNames.Count; i++)
            //    {
            //        if (companyNames[i].ToLower() == "Activision Blizzard Inc".ToLower())
            //        {
            //            found = true;
            //        }
            //    }
            //    if(!found)
            //      wdLandingPage.NextPageButton.Click();

            //    Thread.Sleep(1000);
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(wdLandingPage.CurrentPageButton));
            //}

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("grid")));
            string title = wdMeetingDetailPage.MeetingDetailCompanyName;
            Assert.AreEqual("Activision Blizzard Inc", title);
            Assert.AreEqual(driver.Title, "Sample Disclosure");
        }


        [SetUp]
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

        [TearDown]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
    }
}
