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
    public class MyAccountPage : PageBase
    {
        public MyAccountPage(IWebDriver webDriver) : base(webDriver)
        {

        }
        [FindsBy(How = How.PartialLinkText, Using = "Log in")]
        public IWebElement lnkLogin { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Log out")]
        public IWebElement lnkLogout { get; set; }

        [FindsBy(How = How.Name, Using = "log")]
        public IWebElement txtUsername { get; set; }

        [FindsBy(How = How.Name, Using = "pwd")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "loginform")]
        public IWebElement frmLogin { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Your Details")]
        public IWebElement lnkProfile { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_2")]
        public IWebElement txtFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_4")]
        public IWebElement txtAddress { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_5")]
        public IWebElement txtCity { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_6")]
        public IWebElement txtState { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_18")]
        public IWebElement txtPhoneNumber { get; set; }



        [FindsBy(How = How.Name, Using = "submit")]
        public IWebElement btnSaveProfile { get; set; }

        private string strUpdatedName;
        private string strUpdatedAddress;
        private string strUpdatedCity;
        private string strUpdatedState;
        public LoginUserPage GoToLogin()
        {
            WebDriverWait wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Log in")));
            lnkLogin.Click();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            LoginUserPage loginUserPage = new LoginUserPage(base._driver);
            return loginUserPage;
        }

        public void LoginAndViewProfile()
        {
            WebDriverWait wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Log in")));

            txtUsername.SendKeys("sam.samim");
            txtPassword.SendKeys("Test@1234567");
            frmLogin.Submit();

            Thread.Sleep(10);
            wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            element = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Details")));
            lnkProfile.Click();

            wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("wpsc_checkout_form_2")));

        }
        public void UpdateProfile()
        {
            strUpdatedAddress = txtAddress.Text + "1";
            strUpdatedCity = txtCity.GetAttribute("value") + "a";
            
            txtAddress.SendKeys(strUpdatedAddress);

            txtCity.SendKeys(txtCity.Text + "a");

            btnSaveProfile.Click();
        }
        public LoginUserPage Logout()
        {
            WebDriverWait wait = new WebDriverWait(base._driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Log out")));

            lnkLogout.Click();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            LoginUserPage loginUserPage = new LoginUserPage(base._driver);
            return loginUserPage;
        }

        public string GetUpdatedFirstName()
        {
            return strUpdatedName;
        }
        public string GetFirstName()
        {
            return txtFirstName.Text;
        }

        public string GetUpdatedAddress()
        {
            return strUpdatedAddress;
        }
        public string GetAddress()
        {
            return txtAddress.Text;
        }

        public string GetUpdatedCity()
        {
            return strUpdatedCity;
        }
        public string GetCity()
        {
            return txtCity.GetAttribute("value");
        }

        public string GetUpdatedState()
        {
            return strUpdatedState;
        }
        public string GetState()
        {
            return txtState.Text;
        }

    }
}
