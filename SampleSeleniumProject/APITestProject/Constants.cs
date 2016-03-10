using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace APITestProject
{
    public static class Constants
    {
        public class ContentType
        {
            public const string APPLICATIONJSON = "application/json";
            public const string APPLICATIONXML = "application/xml";
            public const string application_x_www_form_urlencoded = "application/x-www-form-urlencoded";
            public const string TEXTXML = "text/xml";
        }
        public class MethodName
        {
            public const string POST = "Post";
            public const string PUT = "Put";
            public const string GET = "Get";
            public const string DELETE = "Delete";
            public const string SEARCH = "Search";
            public const string POSTDELETE = "PostDelete";
        }
        public const string NEARESTSTATION = "nearest";
        public const string STATIONID = "62029";
    }
}
