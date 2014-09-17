using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace WifiSpeeddetectorWebService
{


    /// Summary description for Service1   
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int UploadGlobalFile(byte[] oData, string sfile)
        {
            var iID = 0;
            StreamReader reader = null;
            FileStream fs = null;
            SqlConnection oConnection = null;
            try
            {

                var sGlobalIP = string.Empty;
                var sIsp = string.Empty;
                var sCity = string.Empty;
                var sCountry = string.Empty;
                var sLatitude = string.Empty;
                var sLongitude = string.Empty;

                oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());

                oConnection.Open();

                string sDirPath = HttpContext.Current.Server.MapPath(@"~\Uploadedfiles\");
                if (!Directory.Exists(sDirPath))
                {
                    Directory.CreateDirectory(sDirPath);
                }

                fs = new FileStream(sDirPath + sfile, FileMode.Create);

                fs.Write(oData, 0, oData.Length);
                fs.Flush();
                fs.Close();
                fs.Dispose();

                if (File.Exists(sDirPath + "/" + sfile))
                {
                    reader = new StreamReader(File.OpenRead(sDirPath + sfile));
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values != null && values.Count() > 0)
                        {
                            sGlobalIP = values[0];
                            sIsp = values[1];
                            sCity = values[2];
                            sCountry = values[3];
                            sLatitude = values[4];
                            sLongitude = values[5];



                            var sSql = "INSERT INTO tblGlobal(IPAddress, ISP,City,Country,Latitude,Longitude) VALUES('" + sGlobalIP + "','" + sIsp + "','" + sCity + "','" + sCountry + "','" + sLatitude + "','" + sLongitude + "');";
                            sSql = sSql + "SELECT @@Identity;";
                            SqlCommand cmd = new SqlCommand(sSql, oConnection);

                            if (oConnection.State != System.Data.ConnectionState.Open)
                            {
                                oConnection.Open();
                            }

                            iID = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                }

                DeleteFile(sDirPath + "/" + sfile);


                return iID;

            }
            catch (Exception ex)
            {
                return iID;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (oConnection.State == System.Data.ConnectionState.Open)
                {
                    oConnection.Close();
                }
            }

        }

        [WebMethod]
        public void UploadFiles(byte[] oData, string sfile, int iGlobalID, int iOption)
        {
            SqlConnection oConnection = null;
            try
            {

                var iID = 0;
                oConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
                var sSql = string.Empty;
                SqlCommand cmd = null;
                oConnection.Open();

                string sDirPath = HttpContext.Current.Server.MapPath(@"~\Uploadedfiles\");
                if (!Directory.Exists(sDirPath))
                {
                    Directory.CreateDirectory(sDirPath);
                }
                FileStream fs = new FileStream(sDirPath + sfile, FileMode.Create);

                fs.Write(oData, 0, oData.Length);
                fs.Flush();
                fs.Close();
                fs.Dispose();

                if (File.Exists(sDirPath + "/" + sfile))
                {
                    var reader = new StreamReader(File.OpenRead(sDirPath + sfile));
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values != null && values.Count() > 0)
                        {

                            switch (iOption)
                            {
                                case 0:
                                    var sUploadSpeed = values[1];
                                    var sDownloadSpeed = values[2];
                                    var sIpAddress = values[3];
                                    var sRoundTripTime = values[4];
                                    var sTTL = values[5];
                                    var sByteSent = values[6];
                                    var sMACAddressE = values[7];
                                    sSql = "INSERT INTO tblExternalPing(GlobalID,UploadSpeed,DownloadSpeed,IPAddress,RoundTripTime,TTL,ByteSent,MACAddress) VALUES('" + iGlobalID + "','" + sUploadSpeed + "','" + sDownloadSpeed + "','" + sIpAddress + "','" + sRoundTripTime + "','" + sTTL + "','" + sByteSent + "','" + sMACAddressE + "');";
                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;
                                case 1:
                                    var sIPAddress = values[0];
                                    var sMACAddress = values[1];
                                    var sVendor = values[2];
                                    var sPing = values[3];
                                    var rt = values[4];
                                    var ttl = values[5];
                                    var bytesent = values[6];
                                    var sPortScan = values[7];

                                    sSql = "INSERT INTO tblLocalNetwork(GlobalID, IPAddress,MACAddress,Vendor,RoundTripTime,TTL,ByteSent,PortScan) VALUES('" + iGlobalID + "','" + sIPAddress + "','" + sMACAddress + "','" + sVendor + "','" + rt + "','" + ttl + "','" + bytesent + "','" + sPortScan + "');";

                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;
                                case 2://Trace routr
                                    sIpAddress = values[0];
                                    var sTraceRoute = values[1];

                                    sSql = "INSERT INTO tblPingTraceRoute(GlobalID,TraceRouteIP) VALUES('" + iGlobalID + "','" + sTraceRoute + "');";

                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;
                                case 3:
                                    var Make = values[0];
                                    var Model = values[1];
                                    var SecurityType = values[2];
                                    var SSIDName = values[3];
                                    sMACAddress = values[4];
                                    var NetworkType = values[5];
                                    var SignalStrength = values[6];
                                    var frequency = values[7];
                                    var Channel = values[8];
                                    var windowSize = values[9];
                                    var IPAddress = values[10];

                                    sSql = "INSERT INTO tblWiFiDeviceDetailsRange(GlobalID, Make,Model,SecurityType,SSIDName,MACAddress,NetworkType,SignalStrength,Frequency,Channel,WindowSize,IPAddress) VALUES('" + iGlobalID + "','" + Make + "','" + Model + "','" + SecurityType + "','" + SSIDName + "','" + sMACAddress + "','" + NetworkType + "','" + SignalStrength + "','" + frequency + "','" + Channel + "','" + windowSize + "','" + IPAddress + "');";
                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;

                                case 4:// tbl_wificonnect
                                    Make = values[0];
                                    Model = values[1];
                                    SecurityType = values[2];
                                    SSIDName = values[3];
                                    sMACAddress = values[4];
                                    NetworkType = values[5];
                                    SignalStrength = values[6];
                                    frequency = values[7];
                                    Channel = values[8];
                                    IPAddress = values[9];

                                    sSql = "INSERT INTO tblWiFiConnectDetails(GlobalID, Make,Model,SecurityType,SSIDName,MACAddress,NetworkType,SignalStrength,Frequency,Channel,IPAddress) VALUES('" + iGlobalID + "','" + Make + "','" + Model + "','" + SecurityType + "','" + SSIDName + "','" + sMACAddress + "','" + NetworkType + "','" + SignalStrength + "','" + frequency + "','" + Channel + "','" + IPAddress + "');";
                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;

                                case 5://tblPortScan
                                    sIPAddress = values[0];
                                    var sPort = values[1];
                                    var sStatus = values[2];

                                    sSql = "INSERT INTO tblPortScan(GlobalID,IPAddress, Port,Status) VALUES('" + iGlobalID + "','" + sIPAddress + "','" + sPort + "','" + sStatus + "');";
                                    cmd = new SqlCommand(sSql, oConnection);

                                    cmd.ExecuteNonQuery();


                                    break;
                            }
                        }
                    }
                }

                DeleteFile(sDirPath + "/" + sfile);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (oConnection != null && oConnection.State == System.Data.ConnectionState.Open)
                {
                    oConnection.Close();
                }
            }
        }
        [WebMethod]
        public int UploadTextFile(byte[] fileData)
        {
            string  sDirPath = HttpContext.Current.Server.MapPath(@"~\Uploadedfiles\");

            // instance a memory stream and pass the byte array to its constructor

            MemoryStream ms = new MemoryStream(fileData);

            // instance a filestream pointing to the  // storage folder, use the original file name            // to name the resulting file

            FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploadedfiles/") + "Test.txt", FileMode.Create);

            // write the memory stream containing the original           // file as a byte array to the filestream
            ms.WriteTo(fs);
            ms.Close();
            fs.Close();
            fs.Dispose();

            return 0;
        }
        [WebMethod]
        public byte[] DownloadTextFile()
        {


            string sDirPath = HttpContext.Current.Server.MapPath(@"~\Uploadedfiles\");

            if (!Directory.Exists(sDirPath))
            {
                return null;
            }

            string filePath = sDirPath + "Test.txt";

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] FileData = new byte[fs.Length - 1];

            fs.Read(FileData, 0, System.Convert.ToInt32(fs.Length) - 1);
            fs.Close();
            return FileData;

        }
        private void DeleteFile(string sFilePath)
        {
            try
            {
                if (File.Exists(sFilePath))
                {
                    File.Delete(sFilePath);
                }
            }
            catch (Exception ex)
            {
            }
        }
        [WebMethod()]
        public byte[] DownloadFile()
        {
            byte[] b1 = null;
            string sDirPath = HttpContext.Current.Server.MapPath(@"~/Uploadedfiles/Test.txt");
            string FName = string.Empty;
            FName = sDirPath;
            System.IO.FileStream fs1 = null;
            fs1 = System.IO.File.Open(FName, FileMode.Open, FileAccess.Read);
            b1 = new byte[fs1.Length];
            fs1.Read(b1, 0, (int)fs1.Length);
            fs1.Close();
            return b1;
        }
    }
}
