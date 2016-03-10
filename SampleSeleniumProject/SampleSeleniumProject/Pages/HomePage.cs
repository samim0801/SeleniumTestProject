using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

using System.Threading;
namespace SampleSeleniumProject.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
           
        }
        [FindsBy(How = How.PartialLinkText, Using = "Product")]
        public IWebElement lnkProductCategory { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "iPhones")]
        public IWebElement lnkiPhoneProduct { get; set; }

        [FindsBy(How = How.Name, Using = "product_96")]
        public IWebElement frmiPhone4s { get; set; }

        [FindsBy(How = How.ClassName, Using = "go_to_checkout")]
        public IWebElement btnCheckout { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "My Account")]
        public IWebElement lnkMyAccount { get; set; }

        [FindsBy(How = How.Id, Using = "menu-item-33")]
        public IWebElement lnkProducts { get; set; }
        
        public ProductPage GoToiPhoneProducts()
        {           
            base._driver.Navigate().GoToUrl(Constants.DEMOPROURL);

            Thread.Sleep(5000);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            ProductPage productPage = new ProductPage(base._driver);
          
            return productPage;

        }
        public MyAccountPage GoToMyAccount()
        {
            WebDriverWait wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("My Account")));

            lnkMyAccount.Click();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            MyAccountPage myAccountPage = new MyAccountPage(base._driver);
            return myAccountPage;

        }

    }
}
