using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Runtime;

namespace codingExercise.Models
{
    public class EmailInfo
    {
        public string cost_centre { get; set; }
        public double beforeGST { get; set; }
        public double GST { get; set; }
        public string total { get; set; }
        public string payment_method { get; set; }
        public string vendor { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }
}