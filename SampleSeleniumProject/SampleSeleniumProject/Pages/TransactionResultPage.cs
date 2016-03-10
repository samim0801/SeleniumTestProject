using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


namespace SampleSeleniumProject.Pages
{
    public class TransactionResultPage : PageBase
    {
        public TransactionResultPage(IWebDriver webDriver) : base(webDriver)
        {

        }
        [FindsBy(How = How.ClassName, Using = "wpsc-transaction-results-wrap")]
        public IWebElement divResult { get; set; }

        public string getTransactionResult()
        {
            return divResult.Text;
        }
    }
}
