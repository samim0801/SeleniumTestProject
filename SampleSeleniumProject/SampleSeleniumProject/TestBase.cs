using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;

namespace SampleSeleniumProject
{
    public class TestBase
    {
        private IWebDriver driver;    

       public IWebDriver getDriver()
        {
            return driver;
        }

        private void setDriver(string browserType, string appURL)
        {
            switch (browserType)
            {
                case "chrome":
                    driver = initChromeDriver(appURL);
                    break;
                case "ie":
                    driver = initIEDriver(appURL);
                    break;
                default:                   
                    driver = initIEDriver(appURL);
                    break;                    
            }
        }

        private static IWebDriver initChromeDriver(string appURL)
        {           
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(appURL);
            return driver;
        }

        private static IWebDriver initIEDriver(string appURL)
        {            
            IWebDriver driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(appURL);
            return driver;
        }

   
        public void initializeTestBase(string browserType, string appURL)
        {
            try
            {
                setDriver(browserType, appURL);

            }
            catch (Exception e)
            {
               //do something
            }
        }


        public void TestBaseCleanUp()
        {
            driver.Quit();
        }
    }
}

