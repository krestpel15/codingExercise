using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using codingExercise.Models;

namespace codingExercise.Controllers
{
    public class CalculatorController : ApiController
    {
        public static IEnumerable<EmailInfo> GetAllEmailInfo()
        {   
            //create an EmailInfo object
            EmailInfo emailInfo = new EmailInfo();

            //initialize 
            string cost_centre = null;
            string total = "";
            string payment_method = null;
            string vendor = null;
            string description = null;
            string date = null;
            double amount = 0.0;
            double beforeGST = 0.0;
            double GST = 0.0;

            //create nex xml document
                XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //load the xml file
                xmlDoc.Load("D:\\Project folder\\serkoTask.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message); ;
            }

            //parse through xml files
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/message/expense");
            XmlNodeList nodeList2 = xmlDoc.DocumentElement.SelectNodes("/message/booking");

            foreach (XmlElement node in nodeList)
            {
                cost_centre = node.SelectSingleNode("cost_centre").InnerText;
                total = node.SelectSingleNode("total").InnerText;
                payment_method = node.SelectSingleNode("payment_method").InnerText;
            }

            foreach (XmlElement node in nodeList2)
            {
                vendor = node.SelectSingleNode("vendor").InnerText;
                description = node.SelectSingleNode("description").InnerText;
                date = node.SelectSingleNode("date").InnerText;
            }

            //try-catch for invalid total
            try
            {
                amount = Double.Parse(total);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }

            GST = Math.Round(((amount * 3) / 23), 2); //calculate gst, calculation was lifted from kiwitax
            beforeGST = Math.Round((amount - GST), 2); //calculate before gst amount
            

            //return unknown as cost centre value if cost_centre is empty
            if (cost_centre != "")
            {
                emailInfo.cost_centre = cost_centre;
            } else
            {
                emailInfo.cost_centre = "Unknown";
            }

            emailInfo.beforeGST = beforeGST;
            emailInfo.GST = GST;

            //return invalid as total if total is empty
            if (total != "")
            {
                emailInfo.total = total;
            } else
            {
                emailInfo.total = "Invalid";
            }

            emailInfo.payment_method = payment_method;
            emailInfo.vendor = vendor;
            emailInfo.description = description;
            emailInfo.date = date;
            
            
            yield return emailInfo;   
        }

        // GET api/calculator
        [HttpGet]
        public IEnumerable<EmailInfo> GetDetails()
        {
            return GetAllEmailInfo();
        }
    }
}

