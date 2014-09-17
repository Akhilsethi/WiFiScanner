using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;


namespace WiFiSpeedDetector.Helpers
{
    class MachineData
    {
        public static GeoIpData GetMachineIP()
        {
            try
            {
                //string externalIP;
                //externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                //externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                //             .Matches(externalIP)[0].ToString();
                //return externalIP;

                //FraudLabsWebServicePortTypeClient o= new FraudLabsWebServicePortTypeClient();

                // FraudLabsInput iFraudLabs = new FraudLabsInput();
                // FraudLabsOutput oFraudLabs = new FraudLabsOutput();
                // iFraudLabs.IP = "49.248.151.106";
                //FraudLabsRequest req = new FraudLabsRequest(iFraudLabs);

                // oFraudLabs = o.FraudLabs(iFraudLabs);

                //return oFraudLabs;

                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();


                //string url = "http://api.ipinfodb.com/v3/ip-city/?key=9e6b8a367e4ad99098861be33fe9d7a66f2f6dc51bd5439f78e4242337980370&ip=" + externalIP;//
                 //"http://freegeoip.net/xml/";

                string url="http://api.ipaddresslabs.com/iplocation/v1.7/locateip?key=demo&ip=" + externalIP + "&format=XML";
                WebClient wc = new WebClient();
                wc.Proxy = null;
                MemoryStream ms = new MemoryStream(wc.DownloadData(url));
               // var s= Encoding.ASCII.GetString(ms.ToArray());
                GeoIpData retval = new GeoIpData();

                //if (!string.IsNullOrEmpty(s))
                //{
                //    var aryData = s.Split(';');

                //    if((aryData != null ) && aryData.Count() > 0)
                //    {
                //        retval.KeyValue.Add("ip_address", aryData[2]);
                //        retval.KeyValue.Add("country_name", aryData[4]);
                //        retval.KeyValue.Add("city", aryData[6]);
                //        retval.KeyValue.Add("latitude", aryData[8]);
                //        retval.KeyValue.Add("longitude", aryData[9]);

                //    }


                //}

                XmlTextReader rdr = new XmlTextReader(url);
                XmlDocument doc = new XmlDocument();
                ms.Position = 0;
                doc.Load(ms);
                ms.Dispose();

                foreach (XmlElement el in doc.ChildNodes[1].ChildNodes)
                {
                    if (el.HasChildNodes && el.ChildNodes.Count > 1)
                    {
                        foreach (XmlElement el1 in el.ChildNodes)
                        {
                            retval.KeyValue.Add(el1.Name, el1.InnerText);
                        }
                    }
                    else
                    {
                        retval.KeyValue.Add(el.Name, el.InnerText);
                    }
                }

           
                return retval;
            }
            catch { return null; }
        }
    }

    class GeoIpData
    {
        public GeoIpData()
        {
            KeyValue = new Dictionary<string, string>();
        }
        public Dictionary<string, string> KeyValue;
    }
}
