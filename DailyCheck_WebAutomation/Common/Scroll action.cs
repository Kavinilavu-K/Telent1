using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCheck_WebAutomation.Common
{
    public class Scroll_action
    {

        private readonly IWebDriver _driver;

        public Scroll_action(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ScrollDown()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollBy(0, 250);"); // Scroll down by 250px

            {
            }

        }
    }
}