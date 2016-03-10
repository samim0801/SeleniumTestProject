using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Validation;
using Steps;
namespace APITestProject
{
    [TestClass]
    public class ApiTest
    {
        public TestContext TestContext
        {
            get;
            set;
        }
        TestSteps testSteps = new TestSteps();
                
        [Description("Verify station name")]
        [TestMethod]
        public void TC_Excercise_2_001_VerifyStationNameInResponse()
        {
            try
            {
                AssertFor assertFor = new AssertFor();
                TestContext.WriteLine("Test execution started");

                Dictionary<string, string> dictParameters = new Dictionary<string, string>();
                dictParameters.Add("location", "Austin");
                dictParameters.Add("ev_network", "ChargePoint Network");
                ServiceCall objServiceCall = new ServiceCall();
                string serviceURL = objServiceCall.BuildUri(Constants.NEARESTSTATION, "json", dictParameters);
                
                HttpWebResponse httpResponse = objServiceCall.CallService(Constants.MethodName.GET, serviceURL, Constants.ContentType.APPLICATIONJSON, null, null, null);
                testSteps.AddSteps(assertFor, "Call API");
                StreamReader loResponseStream = new StreamReader(httpResponse.GetResponseStream(), true);
                string responseBody = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                httpResponse.Close();

                assertFor.IsTrue(responseBody.Contains("HYATT AUSTIN"), "HYATT AUSTIN is not present in API Response");

                testSteps.AddSteps(assertFor, "Verify stationname exists in API response");

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

        [Description("Verify station address")]
        [TestMethod]
        public void TC_Excercise_2_002_VerifyStationAddressInResponse()
        {
            try
            {
                AssertFor assertFor = new AssertFor();
                TestContext.WriteLine("Test execution started");
                
                ServiceCall objServiceCall = new ServiceCall();
                string serviceURL = objServiceCall.BuildUri(Constants.STATIONID, "json", null);

                HttpWebResponse httpResponse = objServiceCall.CallService(Constants.MethodName.GET, serviceURL, Constants.ContentType.APPLICATIONJSON, null, null, null);
                testSteps.AddSteps(assertFor, "Call API");
                StreamReader loResponseStream = new StreamReader(httpResponse.GetResponseStream(), true);
                string responseBody = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                httpResponse.Close();

                assertFor.IsTrue(responseBody.Contains("208 Barton Springs"), "Street address is not valid for station");
                assertFor.IsTrue(responseBody.Contains("Austin"), "City is not valid for station");
                assertFor.IsTrue(responseBody.Contains("78704"), "Zipcode is not valid for station");
                assertFor.IsTrue(responseBody.Contains("TX"), "State is not valid for station");

                testSteps.AddSteps(assertFor, "Verify station address is valid in API response");

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
    }
}
