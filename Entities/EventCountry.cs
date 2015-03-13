using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSProofofConcept.Entities
{
    public class EventCountry
    {
        public int EventID { get; set; }
        public string ISOCountryCode { get; set; }
        public int SortOrder { get; set; }
        public string IsDefault { get; set; }
    }

    public class Message
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public int UserID { get; set; }
        public bool IsSent { get; set; }
    }
}