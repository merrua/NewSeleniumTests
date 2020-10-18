using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Microsoft.Edge.SeleniumTools;

namespace SeleniumTests
{
    public class WebDriverFactory : IWebDriverFactory
    {
        string path = @"C:\Code\Webdriver";

        public IWebDriver GetFirefox()
        {
            FirefoxOptions options = new FirefoxOptions();
            //options.AddArgument("headless");
            return new FirefoxDriver();
        }

        public IWebDriver GetChrome()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            return new ChromeDriver(options);
        }

        public IWebDriver GetEdge()
        {
            // edge chrome 75 or later. check edge://settings/help

            EdgeOptions options = new EdgeOptions();
            options.UseChromium = true;
            options.AddArgument("headless");
            return new EdgeDriver(options);
        }
    }
}
