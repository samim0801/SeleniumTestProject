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
    public class ProductPage : PageBase
    {
        public ProductPage(IWebDriver webDriver) : base(webDriver)
        {
            
        }
        [FindsBy(How = How.ClassName, Using = "entry-title")]
        public IWebElement entryTitle { get; set; }

        [FindsBy(How = How.Name, Using = "product_96")]
        public IWebElement frmiPhone4s { get; set; }

        [FindsBy(How = How.ClassName, Using = "go_to_checkout")]
        public IWebElement btnCheckout { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".currentprice.pricedisplay.product_price_96")]
        public IWebElement lblProductPrice { get; set; }
       
        public decimal productCost { get; set; }
        public CheckoutPage AddiPhone4sItemToCart()
        {

            productCost = Convert.ToDecimal(lblProductPrice.Text.Substring(1));
            frmiPhone4s.Submit();
            Thread.Sleep(10);
            btnCheckout.Click();

            Thread.Sleep(10);
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(40));
            CheckoutPage checkoutPage = new CheckoutPage(base._driver);
          
            return checkoutPage;
        }

    }
}
