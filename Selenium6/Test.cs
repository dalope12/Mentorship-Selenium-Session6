using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Selenium6
{
    public class Tests
    {
        IWebDriver webdriver;

        [SetUp]
        public void Setup()
        {
            webdriver = new ChromeDriver("C:\\Drivers\\mentor\\");
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webdriver.Manage().Window.Maximize();
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Daniel Lopez")]
        public void LoginTest()
        {
            string username = "email@company.io";
            string password = "password";

            // Locators
            By locatorButtonSignIn = By.XPath("//a[text()='Sign in']");
            By locatorInputEmail = By.Name("identifier");
            By locatorInputPassword = By.Name("password");

            // URL
            webdriver.Url = "https://www.google.com";

            // Login
            webdriver.FindElement(locatorButtonSignIn).Click();
            webdriver.FindElement(locatorInputEmail).SendKeys(username);
            webdriver.FindElement(locatorInputEmail).SendKeys(Keys.Enter);
            webdriver.FindElement(locatorInputPassword).SendKeys(password);
            webdriver.FindElement(locatorInputPassword).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Daniel Lopez")]
        public void WindowsTest()
        {
            // Locators
            By locatorButton1 = By.Id("windowButton");
            By locatorSearchInput = By.Name("q");
            By locatorGoogleSearchButton = By.ClassName("gNO89b");

            // URL
            webdriver.Url = "https://demoqa.com/browser-windows";

            // Store the parent window of the driver
            String parentWindowHandle = webdriver.CurrentWindowHandle;
            Console.WriteLine("Parent window's handle -> " + parentWindowHandle);

            // Multiple click to open multiple window
            for (var i = 0; i < 3; i++)
            {
                webdriver.FindElement(locatorButton1).Click();
                Thread.Sleep(3000);
            }

            // Store all the opened window into the list 
            // Print each and every items of the list

            IList<string> lstWindow = new List<string>(webdriver.WindowHandles);

            foreach (var handle in lstWindow)
            {
                Console.WriteLine(handle);
            }

            int count = 0;
            foreach (var handle in lstWindow)
            {
                Console.WriteLine($"Switching to window {count}- > " + handle);
                Console.WriteLine("Navigating to google.com");

                //Switch to the desired window first and then execute commands using driver
                webdriver.SwitchTo().Window(handle);
                webdriver.Navigate().GoToUrl("https://google.com");
                webdriver.FindElement(locatorSearchInput).SendKeys($"Query {count}");
                webdriver.FindElements(locatorGoogleSearchButton)[1].Click();
                Thread.Sleep(3000);
                count++;
            }
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Daniel Lopez")]
        public void TabsTest()
        {
            // Locators
            By locatorButton1 = By.Id("tabButton");
            By locatorSearchInput = By.Name("q");
            By locatorGoogleSearchButton = By.ClassName("gNO89b");

            // URL
            webdriver.Url = "https://demoqa.com/browser-windows";

            // Store the parent window of the driver
            String parentWindowHandle = webdriver.CurrentWindowHandle;
            Console.WriteLine("Parent window's handle -> " + parentWindowHandle);

            // Multiple click to open multiple window
            for (var i = 0; i < 3; i++)
            {
                webdriver.FindElement(locatorButton1).Click();
                Thread.Sleep(3000);
            }

            string newTabHandle = webdriver.WindowHandles[2];

            // Store all the opened window into the list 
            // Print each and every items of the list

            IList<string> lstWindow = new List<string>(webdriver.WindowHandles);

            foreach (var handle in lstWindow)
            {
                Console.WriteLine(handle);
            }

            Console.WriteLine($"Switching to tab -- > " + newTabHandle);
            Console.WriteLine("Navigating to google.com");

            //Switch to the desired window first and then execute commands using driver
            webdriver.SwitchTo().Window(newTabHandle);
            webdriver.Navigate().GoToUrl("https://google.com");
            webdriver.FindElement(locatorSearchInput).SendKeys($"Query 1");
            webdriver.FindElements(locatorGoogleSearchButton)[1].Click();
            Thread.Sleep(3000);
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the driver instance.
            webdriver.Close();
            webdriver.Quit();
        }

    }
}