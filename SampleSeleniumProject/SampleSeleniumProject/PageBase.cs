using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
namespace SampleSeleniumProject
{  
    public class PageBase
    {
        public IWebDriver _driver;
        public PageBase(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
       

    }
}
