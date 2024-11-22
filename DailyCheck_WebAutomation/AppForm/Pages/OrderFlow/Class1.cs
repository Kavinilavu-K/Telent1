using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Locators for LoginPage
        private By userIdField = By.Id("userId");
        private By passwordField = By.Id("password");
        private By submitButton = By.Id("submitButton");

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Method to enter User ID
        public void EnterUserId(string userId)
        {
            _driver.FindElement(userIdField).SendKeys(userId);
        }

        // Method to enter Password
        public void EnterPassword(string password)
        {
            _driver.FindElement(passwordField).SendKeys(password);
        }

        // Method to click Submit Button
        public void ClickSubmit()
        {
            _driver.FindElement(submitButton).Click();
        }

        // Method to perform login
        public void Login(string userId, string password)
        {
            EnterUserId(userId);
            EnterPassword(password);
            ClickSubmit();
        }
    }
}
