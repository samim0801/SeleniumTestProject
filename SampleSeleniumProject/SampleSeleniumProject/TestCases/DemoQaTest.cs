using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleSeleniumProject.Pages;
using Validation;
using Steps;
namespace SampleSeleniumProject
{
    [TestClass]
    public class DemoQaTest : TestBase
    {
        public DemoQaTest()
        {
            base.initializeTestBase(Constants.BROWSER, Constants.DEMOURL);
        }
        public TestContext TestContext
        {
            get;
            set;
        }
        TestSteps testSteps = new TestSteps();

        [Description("Verify order submission is successful")]
        [TestMethod]
        public void TC_Excercise_1_001_VerifyOrderSubmission()
        {
            try
            {
                AssertFor assertFor = new AssertFor();
                TestContext.WriteLine("Test execution started");
                HomePage homePage = new HomePage(base.getDriver());
                ProductPage productPage = homePage.GoToiPhoneProducts();
                testSteps.AddSteps(assertFor, "Open Products Page");

                CheckoutPage checkoutPage = productPage.AddiPhone4sItemToCart();
                testSteps.AddSteps(assertFor, "Add Item to the cart");

                TransactionResultPage transactionResultPage = checkoutPage.DoCheckout();
                testSteps.AddSteps(assertFor, "Do Check out");

                decimal TotalPrice = productPage.productCost + checkoutPage.shippingCost;
                assertFor.IsTrue(transactionResultPage.getTransactionResult().Contains("Thank you, your purchase is pending"), "Your order is not successful");
                assertFor.IsTrue(transactionResultPage.getTransactionResult().Contains(TotalPrice.ToString()), "Total Price Doesn't match");
                testSteps.AddSteps(assertFor, "Verify Total Cost of the Product and order placement's success message");
                
                TestContext.WriteLine("Test execution successfully");
            }            
            catch (Exception ex)
            {
                TestContext.WriteLine("Test execution failed. Exception : {0}", ex.Message);
                throw;
            }
            finally
            {
                TestContext.WriteLine(testSteps.RetrieveAllExecutionInformation().ToString());
            }
        }

        [Description("Verify account details are saved")]
        [TestMethod]
        public void TC_Excercise_1_002_VerifyAccountDetails()
        {
            try
            {
                AssertFor assertFor = new AssertFor();
                TestContext.WriteLine("Test execution started");
                HomePage homePage = new HomePage(base.getDriver());
                MyAccountPage myAccountPage = homePage.GoToMyAccount();
                testSteps.AddSteps(assertFor, "Open MyAccount Page");                

                myAccountPage.LoginAndViewProfile();
                testSteps.AddSteps(assertFor, "Login and Goto Profile");

                myAccountPage.UpdateProfile();
                testSteps.AddSteps(assertFor, "Update Profile");

                LoginUserPage loginUserPage = myAccountPage.Logout();
                testSteps.AddSteps(assertFor, "Log Out");

                string strUpdatedCity = myAccountPage.GetUpdatedCity();

                homePage = loginUserPage.GobackToStore();
                testSteps.AddSteps(assertFor, "Go back to Home Page");

                myAccountPage = homePage.GoToMyAccount();
                testSteps.AddSteps(assertFor, "Open MyAccount Page after logout");

                myAccountPage.LoginAndViewProfile();
                testSteps.AddSteps(assertFor, "Login again and Goto Profile");

                string strCity = myAccountPage.GetCity();     
                               
                assertFor.AreEqual(strUpdatedCity, strCity, "Account details are not been updated");              
                testSteps.AddSteps(assertFor, "Verify account details are updated successfully or not");

                loginUserPage = myAccountPage.Logout();
                homePage = loginUserPage.GobackToStore();

                assertFor.CheckExceptions();
                TestContext.WriteLine("Test execution successfully");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Test execution failed. Exception : {0}", ex.Message);
                throw;
            }
            finally
            {
                TestContext.WriteLine(testSteps.RetrieveAllExecutionInformation().ToString());
            }
        }

        [Description("Verify validation for empty cart")]
        [TestMethod]
        public void TC_Excercise_1_003_VerifyCartEmptyMessage()
        {
            try
            {
                AssertFor assertFor = new AssertFor();
                TestContext.WriteLine("Test execution started");
                HomePage homePage = new HomePage(base.getDriver());
                ProductPage productPage = homePage.GoToiPhoneProducts();
                testSteps.AddSteps(assertFor, "Open Products Page");

                CheckoutPage checkoutPage = productPage.AddiPhone4sItemToCart();
                testSteps.AddSteps(assertFor, "Add Item to the cart");               

                string strMessage = checkoutPage.RemoveAllItemsandCheckOut();
                testSteps.AddSteps(assertFor, "Remove all items and Check out");
              
                assertFor.IsTrue(strMessage.Contains("Oops, there is nothing in your cart."), "Cart empty message is not shown");
              
                testSteps.AddSteps(assertFor, "Verify cart empty message");

                assertFor.CheckExceptions();
                TestContext.WriteLine("Test execution successfully");
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Test execution failed. Exception : {0}", ex.Message);
                throw;
            }
            finally
            {
                TestContext.WriteLine(testSteps.RetrieveAllExecutionInformation().ToString());
            }
        }
        [TestCleanup]
        public void SampleTestCleanup()
        {
           base.TestBaseCleanUp();
        }
    }
}
