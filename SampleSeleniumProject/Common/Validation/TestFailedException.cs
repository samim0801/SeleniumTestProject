using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Validation
{
    class TestFailedException : Exception
    {
        public List<Exception> Exceptions { get; set; }
        public TestFailedException()
            : base()
        {
            InitializeException();
        }
               
        protected void InitializeException()
        {
            Exceptions = new List<Exception>();
        }

        public override string Message
        {
            get
            {
                return ToString();
            }
        }

       
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }


        public override string ToString()
        {
            string message = string.Format("Test failed. {0}", Environment.NewLine);
            if (Exceptions.Count != 0)
            {
                StringBuilder error = new StringBuilder(message);
                foreach (var exception in Exceptions)
                {
                    error.AppendFormat("Exception Message: {1}{0}Callstack:{2}{0}",
                        Environment.NewLine,
                        exception.Message,
                        exception.ToString());
                }
                return error.ToString();
            }
            return message;
        }
    }
}
