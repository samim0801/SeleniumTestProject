using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validation
{
    public class AssertFor
    {
        private TestFailedException _testFailedEx = null;

        private void AddException(Exception ex)
        {
            if (_testFailedEx == null)
                _testFailedEx = new TestFailedException();
            _testFailedEx.Exceptions.Add(ex);
        }

        public void AreEqual<T>(T expected, T actual, string message, params object[] parameters)
        {
            try { Assert.AreEqual<T>(expected, actual, message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }

        public void AreEqualCollections<T>(T expected, T actual, string message, params object[] parameters)
        {
            try { CollectionAssert.AreEqual((System.Collections.ICollection)expected, (System.Collections.ICollection)actual, message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }

        public void AreEqual<T>(T expected, T actual)
        {
            try { Assert.AreEqual<T>(expected, actual); }
            catch (Exception ex) { AddException(ex); }
        }

        public void IsNotNull<T>(T expected, string message, params object[] parameters)
        {
            try { Assert.IsNotNull(expected, message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }
        public void IsTrue(bool condition, string message, params object[] parameters)
        {
            try { Assert.IsTrue(condition, message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }
        public void IsNull<T>(T expected, string message, params object[] parameters)
        {
            try { Assert.IsNull(expected, message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }

        public void CheckExceptions()
        {
            if (_testFailedEx != null)
                throw _testFailedEx;
        }

        public void Fail(string message, params object[] parameters)
        {
            try { Assert.Fail(message, parameters); }
            catch (Exception ex) { AddException(ex); }
        }

        public string GetAllExceptions()
        {
            string exceptions = string.Empty;
            if (_testFailedEx != null)
            {
                foreach (Exception ex in _testFailedEx.Exceptions)
                {
                    exceptions += string.Concat(ex.Message.ToString(), Environment.NewLine.ToString());
                }
            }
            return exceptions;
        }
    }
}
