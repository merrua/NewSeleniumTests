﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumTests
{
    public interface IWebDriverFactory
    {
        IWebDriver GetFirefox();
        IWebDriver GetChrome();
        IWebDriver GetEdge();
    }
}
