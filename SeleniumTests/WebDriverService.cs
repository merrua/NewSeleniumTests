using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public class WebDriverService
    {
        private readonly IWebDriverFactory webDriverFactory;

        public WebDriverService(IWebDriverFactory webDriverFactory)
        {
            this.webDriverFactory = webDriverFactory;
        }

        public IWebDriver GetBrowser(BrowserType browserType)
        {
            IWebDriver driver = null;

            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver = this.webDriverFactory.GetChrome();
                    break;
                case BrowserType.Firefox:
                    driver = this.webDriverFactory.GetFirefox();
                    break;
                case BrowserType.Edge:
                    driver = this.webDriverFactory.GetEdge();
                    break;
                default:
                    driver = this.webDriverFactory.GetChrome();
                    break;
            }

            return driver;            
        }
    }
}
