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
    public class CheckoutPage : PageBase
    {
        public CheckoutPage(IWebDriver webDriver) : base(webDriver)
        {

        }
        [FindsBy(How = How.ClassName, Using = "step2")]
        public IWebElement btnContinue { get; set; }

        [FindsBy(How = How.Id, Using = "current_country")]
        public IWebElement lstCountry { get; set; }

        [FindsBy(How = How.Name, Using = "wpsc_submit_zipcode")]
        public IWebElement btnCalculate { get; set; }


        [FindsBy(How = How.ClassName, Using = "wpsc_checkout_forms")]
        public IWebElement frmCheckout { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_9")]
        public IWebElement txtEmail { get; set; }


        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_2")]
        public IWebElement txtFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_3")]
        public IWebElement txtLastName { get; set; }


        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_4")]
        public IWebElement txtAddress { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_5")]
        public IWebElement txtCity { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_6")]
        public IWebElement txtState { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_7")]
        public IWebElement lstBillingCountry { get; set; }

        [FindsBy(How = How.Id, Using = "wpsc_checkout_form_18")]
        public IWebElement txtPhone { get; set; }

        [FindsBy(How = How.Id, Using = "shippingSameBilling")]
        public IWebElement chkShippingDetails { get; set; }

        
        [FindsBy(How = How.CssSelector, Using = "tr.total_price.total_shipping > td.wpsc_totals > span.pricedisplay.checkout-shipping >span.pricedisplay")]
        public IWebElement lblShippingPrice { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".adjustform.remove")]
        public IList<IWebElement> frmRemoveAllItems { get; set; }

        [FindsBy(How = How.ClassName, Using = "entry-content")]
        public IWebElement divMessage { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Home")]
        public IWebElement lnkHome { get; set; }
        
        public decimal shippingCost { get; set; }      

        public TransactionResultPage DoCheckout()
        {
            btnContinue.Click();
          
            SelectElement selector = new SelectElement(lstCountry);
            selector.SelectByText("India");

            btnCalculate.Click();

            txtEmail.SendKeys("test@test.com");
            txtFirstName.SendKeys("Test");
            txtLastName.SendKeys("Test");
            txtAddress.SendKeys("Test Address");
            txtCity.SendKeys("Test");
            selector = new SelectElement(lstBillingCountry);
            selector.SelectByText("India");

            txtState.SendKeys("Test");
            txtPhone.SendKeys("123-456-7890");

            shippingCost = Convert.ToDecimal(lblShippingPrice.Text.Substring(1)); 

            chkShippingDetails.Click();
            frmCheckout.Submit();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            TransactionResultPage transactionResultPage = new TransactionResultPage(base._driver);

            return transactionResultPage;
        }
        public string RemoveAllItemsandCheckOut()
        {   
            foreach (IWebElement frm in frmRemoveAllItems)
            {
                frm.Submit();              
            }        

            return divMessage.Text;
        }
        public void GoToHome()
        {
            lnkHome.Click();
        }
    }
}
