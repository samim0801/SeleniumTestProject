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
    public class LoginUserPage : PageBase
    {
        public LoginUserPage(IWebDriver webDriver) : base(webDriver)
        {
           
        }
        [FindsBy(How = How.Name, Using = "user_login")]
        public IWebElement txtUsername { get; set; }

        [FindsBy(How = How.Name, Using = "user_pass")]
        public IWebElement txtPassword { get; set; }      
        
        [FindsBy(How = How.Name, Using = "loginform")]
        public IWebElement frmLogin { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "ONLINE")]
        public IWebElement lnkBackToStore { get; set; }
        
        public HomePage GobackToStore()
        {
            lnkBackToStore.Click();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            HomePage homePage = new HomePage(base._driver);
            return homePage;

        }
    }
}
