using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;
namespace Steps
{
    public class TestSteps
    {
        private List<TestSteps> lstSteps = new List<TestSteps>();


        public TestSteps()
        {
            this.StepNo = 0;
            this.Comments = string.Empty;
            this.ExceptionDetails = string.Empty;
        }
        private TestSteps(int stepNo, string comments, string exceptionDetails, string stepResult)
        {
            this.StepNo = stepNo;
            this.Comments = comments;
            this.ExceptionDetails = exceptionDetails;
            this.StepResult = stepResult;
        }
        public int StepNo { get; set; }
        public string Comments { get; set; }
        public string ExceptionDetails { get; set; }
        public string StepResult { get; set; }

        public void AddSteps(int stepNo, AssertFor assertfor, string comments = "")
        {
            string exeptionDetails = assertfor.GetAllExceptions();
            //See if the last step contains same exception then do not add the exception details
            if (lstSteps.Where(a => a.ExceptionDetails.Equals(exeptionDetails)).Count() > 0)
                exeptionDetails = string.Empty;
            string result = string.IsNullOrEmpty(exeptionDetails) ? "PASS" : "FAIL";
            this.StepNo = stepNo;
            lstSteps.Add(new TestSteps(this.StepNo, comments, exeptionDetails, result));
        }

        public void AddSteps(AssertFor assertfor, string comments = "")
        {
            string exeptionDetails = assertfor.GetAllExceptions();
            //See if the last step contains same exception then do not add the exception details
            if (lstSteps.Where(a => a.ExceptionDetails.Equals(exeptionDetails)).Count() > 0)
                exeptionDetails = string.Empty;
            string result = string.IsNullOrEmpty(exeptionDetails) ? "PASS" : "FAIL";
            this.StepNo++;
            lstSteps.Add(new TestSteps(this.StepNo, comments, exeptionDetails, result));
        }

        public void AddSteps(List<int> stepNos, AssertFor assertfor, string comments = "")
        {
            foreach (int stepNo in stepNos)
            {
                AddSteps(stepNo, assertfor, comments);
            }
        }

        public StringBuilder RetrieveAllExecutionInformation()
        {
            StringBuilder info = new StringBuilder();
            foreach (TestSteps testStep in lstSteps)
            {
                string details = string.IsNullOrEmpty(testStep.ExceptionDetails) ? "" : string.Concat(" Exception Details :", testStep.ExceptionDetails);
                info.Append(string.Concat("Test Step :", testStep.StepNo, " Test Comments :", testStep.Comments, " Test Step Result :", testStep.StepResult, details));
                info.Append(Environment.NewLine);
            }
            return info;
        }

        public TestSteps RetrieveLastStep()
        {
            if (lstSteps.Count > 0)
                return lstSteps.Last();
            return new TestSteps();
        }

        public TestSteps RetrieveLastStepWithException()
        {
            return lstSteps.Where(a => a.ExceptionDetails != string.Empty).First();
        }
    }
}

