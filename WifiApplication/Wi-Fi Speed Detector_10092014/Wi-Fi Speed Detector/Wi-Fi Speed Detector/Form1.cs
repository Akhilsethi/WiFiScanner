using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiFiSpeedDetector.Helpers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using System.Data.Odbc;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using NativeWifi;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Diagnostics;
using System.Threading;
using System.DirectoryServices;
using System.Collections.ObjectModel;
using System.Management.Instrumentation;
using Wi_Fi_Speed_Detector;
using log4net;
using System.Configuration;
using System.Data.SqlClient;
using log4net.Appender;





namespace WiFiSpeedDetector
{

    public static class ThreadExtension
    {
        public static void WaitAll(this IEnumerable<Thread> threads)
        {
            if (threads != null)
            {
                foreach (Thread thread in threads)
                { thread.Join(); }
            }
        }
    }

    public partial class Form1 : Form
    {
        ArrayList lstSpeed = new ArrayList();
        string IPAddressGlobal = string.Empty;
        string CountryName = string.Empty;
        string city = string.Empty;
        string isp = string.Empty;
        string latitude = string.Empty;
        string longitude = string.Empty;
        string localipaddress = string.Empty;
        string localmacaddress = string.Empty;
        string localvendor = string.Empty;
        string localping = "1";
        int iGlobalID = 0;

        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1SoapClient obj = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1SoapClient();

        List<string> networkComputers = GetNetworkComputerNames();
        StringBuilder sbIP = new StringBuilder();
        StringBuilder sbport = null;

        public Form1()
        {
            log.Info("====================================================================");
            log.Info("Application started");
            log.Info("====================================================================");
            InitializeComponent();
            textBox1.Text = LocalIPAddress();
            progressBar1.Visible = false;
            log.Info("Deleting older files...");
            string[] lstfiles = Directory.GetFiles("../", "*.csv");
            if (lstfiles != null && lstfiles.Count() > 0)
            {
                for (var iCount = 0; iCount < lstfiles.Length; iCount++)
                {
                    File.Delete(lstfiles[iCount]);
                }
            }
            log.Info("Files deleted successfully...");
        }

        public string ExecuteCommandSync(object command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                return result.Trim();
            }
            catch (Exception objException)
            {
                // Log the exception
                return string.Empty;
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            string[] csvFiles = Directory.GetFiles("../../", "*.csv");

            if (csvFiles != null && csvFiles.Count() > 0)
            {
                for (var i = 0; i < csvFiles.Length; i++)
                {
                    File.Delete(csvFiles[i]);
                }
            }
        }

        private void OnFormActivated(object sender, EventArgs e)
        {

        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private static List<string> GetNetworkComputerNames()
        {
            List<string> networkComputerNames = new List<string>();
            const int MAX_PREFERRED_LENGTH = -1;
            int SV_TYPE_WORKSTATION = 1;
            int SV_TYPE_SERVER = 2;
            IntPtr buffer = IntPtr.Zero;
            IntPtr tmpBuffer = IntPtr.Zero;
            int entriesRead = 0;
            int totalEntries = 0;
            int resHandle = 0;
            int sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));
            try
            {
                int ret = NetServerEnum(null, 100, ref buffer,
                    MAX_PREFERRED_LENGTH,
                    out entriesRead,
                    out totalEntries, SV_TYPE_WORKSTATION |
                    SV_TYPE_SERVER, null, out
                    resHandle);
                if (ret == 0)
                {
                    for (int i = 0; i < totalEntries; i++)
                    {
                        tmpBuffer = new IntPtr((int)buffer +
                                   (i * sizeofINFO));
                        _SERVER_INFO_100 svrInfo = (_SERVER_INFO_100)
                        Marshal.PtrToStructure(tmpBuffer,
                                typeof(_SERVER_INFO_100));

                        networkComputerNames.Add(svrInfo.sv100_name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                NetApiBufferFree(buffer);
            }

            return networkComputerNames;
        }

        //declare the Netapi32 : NetServerEnum method import
        [System.Runtime.InteropServices.DllImport("Netapi32", CharSet = CharSet.Auto, SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]

        // The NetServerEnum API function lists all servers of the specified type that are visible in a domain.
        public static extern int NetServerEnum(
            string ServerNane, int dwLevel, ref IntPtr pBuf, int dwPrefMaxLen, out int dwEntriesRead, out int dwTotalEntries, int dwServerType,
            string domain, out int dwResumeHandle);

        //declare the Netapi32 : NetApiBufferFree method import
        [DllImport("Netapi32", SetLastError = true), SuppressUnmanagedCodeSecurityAttribute]

        // Netapi32.dll : The NetApiBufferFree function frees the memory that the NetApiBufferAllocate function allocates.         
        public static extern int NetApiBufferFree(IntPtr pBuf);

        //create a _SERVER_INFO_100 STRUCTURE
        [StructLayout(LayoutKind.Sequential)]
        public struct _SERVER_INFO_100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string sv100_name;
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {


            log.Info("Fetching data started...");
            try
            {
                progressBar1.Visible = true;

                tabControlWIFI.Visible = true;

                progressBar1.Increment(10);
                log.Info("Chekcking for internet connection.");
                if (!CommonClass.CheckInternet())
                {
                    txtMessageLocalNet.Text = "Your device is not connected to the internet.";
                    log.Info("Machine is not connected to the device...");
                }
                else
                {
                    log.Info("Checking for gloabl data...");
                    WriteLableString("Checking for Global data...");
                    txtMessageLocalNet.Text = "Your device is connected to the internet.";
                    GeoIpData oData = MachineData.GetMachineIP();
                    log.Info("Fetching global data completed");
                    if (oData != null)
                    {
                        if (oData.KeyValue.Count > 0)
                        {

                            log.Info("====================Global data===================");
                            foreach (var oTemp in oData.KeyValue)
                            {
                                switch (oTemp.Key.ToLower())
                                {
                                    case "ip_address":
                                        txtMessageLocalNet.AppendText(Environment.NewLine + "Current IP: " + oTemp.Value);
                                        IPAddressGlobal = oTemp.Value;
                                        log.Info("Global IP : " + IPAddressGlobal);
                                        break;
                                    case "country_name":
                                        txtMessageLocalNet.AppendText(Environment.NewLine + "Country name: " + oTemp.Value);
                                        CountryName = oTemp.Value;
                                        log.Info("Country : " + CountryName);
                                        break;
                                    case "city":
                                        if (!string.IsNullOrEmpty(oTemp.Value))
                                        {
                                            txtMessageLocalNet.AppendText(Environment.NewLine + "City: " + oTemp.Value);
                                            city = oTemp.Value;
                                            log.Info("City : " + city);
                                        }
                                        else
                                        {
                                            txtMessageLocalNet.AppendText(Environment.NewLine + "Failed to get the city name");
                                            city = oTemp.Value;
                                        }
                                        break;
                                    case "isp":
                                        txtMessageLocalNet.AppendText(Environment.NewLine + "ISP: " + oTemp.Value);
                                        isp = oTemp.Value;
                                        log.Info("ISP : " + isp);
                                        break;
                                    case "latitude":
                                        txtMessageLocalNet.AppendText(Environment.NewLine + "Latitude: " + oTemp.Value);

                                        latitude = oTemp.Value;
                                        log.Info("Lattitude : " + latitude);
                                        break;
                                    case "longitude":
                                        txtMessageLocalNet.AppendText(Environment.NewLine + "Longitude: " + oTemp.Value);
                                        longitude = oTemp.Value;
                                        log.Info("Longitude : " + longitude);
                                        break;
                                }
                            }
                        }
                    }

                    else
                    {
                        txtMessageLocalNet.AppendText(Environment.NewLine + "Failed to get the geolocation Data");
                        log.Info("Failed to get the global data");
                    }
                    progressBar1.Increment(10);
                    WriteLableString("Checking for type of connections..");
                    //Update machine data to csv file...
                    string[][] output1 = new string[][]{
                    new string[]{IPAddressGlobal,isp,city,CountryName,latitude,longitude} 
                     };
                    CommonClass.CreatingCsvFiles(output1, 0);
                    byte[] aryData = CommonClass.GetByteData("global.csv");
                    iGlobalID = obj.UploadGlobalFile(aryData, "Data" + Guid.NewGuid() + ".csv");

                    log.Info("Global data uploaded successfully.");

                    XmlDocument xmlDoc = new XmlDocument();

                    var lstAvailableNetworks = CommonClass.CheckAvailableNetworks();
                    if (lstAvailableNetworks != null && lstAvailableNetworks.Count > 0)
                    {
                        foreach (var oNetwork in lstAvailableNetworks)
                        {
                            if (oNetwork.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                            {
                                switch (oNetwork.NetworkInterfaceType)
                                {
                                    case NetworkInterfaceType.Wireless80211:
                                        WlanClient client = new WlanClient();
                                        progressBar1.Increment(10);
                                        WriteLableString("Fetching WIFI Data..");
                                        Wifi();
                                        foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                                        {
                                            foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                                            {

                                            }
                                            break;
                                        }
                                        break;

                                    case NetworkInterfaceType.Ethernet:

                                        break;
                                }
                            }
                        }
                    }

                    else
                    {
                        WriteMessage("Unable to find available networks.");
                    }

                    progressBar1.Increment(10);

                    log.Info("Checking total internet speed");

                    log.Info("=======================================");

                    #region External website(to check total internet speed)
                    Ping pingSender = new Ping();
                    PingOptions options = new PingOptions();
                    options.DontFragment = true;

                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    log.Info("External Ping IP " + CommonData.IPToPing);
                    ExternalMessage("Pinging to " + CommonData.IPToPing);

                    PingReply reply = pingSender.Send(CommonData.IPToPing, timeout, buffer, options);

                    ExternalMessage("Reply from " + CommonData.IPToPing);
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            ExternalMessage(string.Format("Address: {0}", reply.Address.ToString()));
                            log.Info(string.Format("Address: {0}", reply.Address.ToString()));
                            ExternalMessage(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                            log.Info(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                            ExternalMessage(string.Format("Time to live: {0}", reply.Options.Ttl));
                            log.Info(string.Format("Time to live: {0}", reply.Options.Ttl));
                            ExternalMessage(string.Format("Buffer size: {0}", reply.Buffer.Length));
                            log.Info(string.Format("Buffer size: {0}", reply.Buffer.Length));
                            break;
                        case IPStatus.TimedOut:
                            ExternalMessage("Ping request timedout.");
                            log.Info("Ping request timedout");
                            break;
                        case IPStatus.Unknown:
                            ExternalMessage("Unexpected result.");
                            log.Info("Unexpected result");
                            break;
                        case IPStatus.NoResources:
                            ExternalMessage("Ping request failed due to the insufficient resources");
                            log.Info("Ping request failed due to the insufficient resources");
                            break;
                    }
                    progressBar1.Increment(10);
                    WriteLableString("fetching Trace Route Data..");
                    log.Info("Trace route process started");
                    TraceRoute();
                    log.Info("Trace route process end");
                    log.Info("=======================================");

                    progressBar1.Increment(10);

                    CheckUploadSpeed();
                    progressBar1.Increment(10);

                    CheckDownloadSpeed();

                    progressBar1.Increment(10);
                    WriteLableString("Checking Local Devices...");


                    log.Info("External ping process completed");
                    log.Info("======================================");
                    log.Info("Looking for local network devices");
                    LocalNetwork();
                    log.Info("Finished with local devices");
                    progressBar1.Increment(10);
                    #endregion

                    UploadLogFile();
                    MessageBox.Show("Processing is Complete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process failed with error " + ex.Message);
                log.Error("Error", ex);
            }
            finally
            {
                progressBar1.Visible = false;
                WriteLableString(string.Empty);
            }
        }

        string PingS = string.Empty;
        string UploadTm = string.Empty;
        string DownloadTm = string.Empty;
        string tracrouteIPaddress = string.Empty;

        private void CheckUploadSpeed()
        {
            log.Info("Checking upload speed");
            DateTime dtS = DateTime.Now;
            double starttime = Environment.TickCount;
            string pathSource = Application.StartupPath.ToString() + @"\Test.txt";
            FileStream stream = File.OpenRead(pathSource);
            byte[] fileBytes = new byte[stream.Length];
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            stream.Dispose();
            obj.UploadTextFile(fileBytes, "Test.txt", 1);
            double endtime = Environment.TickCount;
            double secs = Math.Floor(endtime - starttime) / 1000;
            double secs2 = Math.Round(secs, 0);
            double kbsec = Math.Round((fileBytes.Length * 0.00098) / secs);
            double mbsec = kbsec / 100;
            UploadTm = Convert.ToString(mbsec) + " mb/sec";
            ExternalMessage(Environment.NewLine);
            ExternalMessage("Upload Time" + UploadTm);
            DateTime dtE = DateTime.Now;
            TimeSpan dtC = dtE.Subtract(dtS);
            log.Info("Upload speed : " + UploadTm);
            log.Info("Time Consuming for file upload: " + dtC.Minutes + " Minutes");


        }
        private void CheckDownloadSpeed()
        {
            log.Info("Checking download speed");
            DateTime dtS = DateTime.Now;
            double starttime = Environment.TickCount;
            System.IO.FileStream fs1 = null;
            byte[] b1 = null;
            b1 = obj.DownloadFile();
            string sPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            
            fs1 = new FileStream(sPath + "\\" + "Test.txt", FileMode.Create);
            fs1.Write(b1, 0, b1.Length);
            fs1.Close();
            fs1.Dispose();
            fs1 = null;

            double endtime = Environment.TickCount;
            double secs = Math.Floor(endtime - starttime) / 1000;
            double secs2 = Math.Round(secs, 0);
            double kbsec = Math.Round((b1.Length * 0.00098) / secs);
            double mbsec = kbsec / 100;
            DownloadTm = Convert.ToString(mbsec) + " mb/sec";
            ExternalMessage("Download Time" + DownloadTm);
            log.Info("Download Time : " + DownloadTm);
            ExternalMessage(Environment.NewLine);
            DateTime dtE = DateTime.Now;
            TimeSpan dtC = dtE.Subtract(dtS);
            log.Info("Time Consuming for file Download: " + dtC.Minutes + " Minutes");
            //Add External Ping
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            var roundTrip = 0.00;
            string buffersize = string.Empty;
            string Ttl = string.Empty;
            string ipaddressexternal = CommonData.IPToPing;

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 5000;
            PingReply reply = pingSender.Send(ipaddressexternal, timeout, buffer, options);
            log.Info("Pinging to external ip");
            switch (reply.Status)
            {
                case IPStatus.Success:
                    LocalnetMessage(string.Format("Address: {0}", reply.Address.ToString()));
                    log.Info(string.Format("Address: {0}", reply.Address.ToString()));
                    LocalnetMessage(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                    log.Info(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                    roundTrip += reply.RoundtripTime;
                    LocalnetMessage(string.Format("Time to live: {0}", reply.Options.Ttl));
                    log.Info(string.Format("Time to live: {0}", reply.Options.Ttl));
                    Ttl = reply.Options.Ttl.ToString();
                    LocalnetMessage(string.Format("Buffer size: {0}", reply.Buffer.Length));
                    buffersize = reply.Buffer.Length.ToString();
                    log.Info(string.Format("Buffer size: {0}", reply.Buffer.Length));
                    break;
                case IPStatus.TimedOut:
                    LocalnetMessage("Ping request timedout.");
                    log.Info("Ping request timedout");
                    break;
                case IPStatus.Unknown:
                    LocalnetMessage("Unexpected result.");
                    log.Info("Unexpected result");
                    break;
                case IPStatus.NoResources:
                    LocalnetMessage("Ping request failed due to the insufficient resources");
                    log.Info("Ping request failed due to the insufficient resources");
                    break;
                default:
                    break;
            }
            string exmacAddress = string.Empty;
            if (reply.Status == IPStatus.Success)
                PingS = "1";
            else
                PingS = "0";
            string[][] output1 = new string[][]{
            new string[]{PingS,UploadTm,DownloadTm,CommonData.IPToPing,roundTrip.ToString(),Ttl, buffersize,exmacAddress}};
            CommonClass.CreatingCsvFiles(output1, 1);
            log.Info("Uploading external data file");
            byte[] aryData = CommonClass.GetByteData("externalping.csv");
            obj.UploadFiles(aryData, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 0);
            log.Info("Successfully uploaded external data file");

            if(File.Exists(sPath + "\\Test.txt"))
            {
                File.Delete(sPath + "\\Test.txt");
            }

        }
        private void TraceRoute()
        {

            var iCount = 1;

            ExternalMessage("Tracing root for " + CommonData.IPToPing);
            log.Info("Tracing root for : " + CommonData.IPToPing);
            IEnumerable<IPAddress> tracert = CommonClass.GetTraceRoute(CommonData.IPToPing);
            string traceroute = string.Empty;
            if (tracert != null)
            {
                if (tracert.Count() > 0)
                {
                    foreach (var hop in tracert)
                    {
                        if (hop != null)
                        {
                            ExternalMessage(string.Format("{0} \t {1}", iCount, hop.ToString()));
                            traceroute = hop.ToString();
                            log.Info(traceroute);
                            string[][] output2 = new string[][]{
                                new string[]{CommonData.IPToPing,traceroute} };
                            CommonClass.CreatingCsvFiles(output2, 4);
                        }
                        else
                        {
                            log.Info("Request timedout");
                            ExternalMessage(string.Format("{0} \t {1}", iCount, "Request Timed Out."));
                            string[][] output1 = new string[][]{new string[]{CommonData.IPToPing,"Request Timed Out."} 
                            };
                        }
                        iCount++;
                    }
                }
            }
            byte[] aryData = CommonClass.GetByteData("ping_tracerout.csv");

            log.Info("Uploading trace route file.");

            if (aryData != null && aryData.Length > 0)
            {
                obj.UploadFiles(aryData, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 2);
                obj.UploadGlobalFile(aryData, "Data" + Guid.NewGuid() + ".csv");
            }

            log.Info("Successfully uploaded trace route file.");
        }

        private void WriteLableString(string sMessage)
        {
            Application.DoEvents();
            labelMessages.Text = sMessage;
        }

        private void WriteMessage(string sMessage)
        {
            txtMessageLocalNet.AppendText(Environment.NewLine + sMessage);
        }
        private void ExternalMessage(string sMessage)
        {
            ExternalMessageBox.AppendText(Environment.NewLine + sMessage);
        }
        private void WifiRangeMessage(string sMessage)
        {
            WifiTextBox.AppendText(Environment.NewLine + sMessage);
        }
        private void LocalnetMessage(string sMessage)
        {
            richTextBox2.AppendText(Environment.NewLine + sMessage);
        }

        #region WIFI
        string make = string.Empty;
        string model = string.Empty;
        string securitymodel = string.Empty;
        string SSIDName = string.Empty;
        string MACaddress = string.Empty;
        string networktype = string.Empty;
        string signalstrength = string.Empty;
        string frequency = string.Empty;
        string wifiIPaddress = string.Empty;
        string Channel = string.Empty;
        string windowsize = string.Empty;
        int Ttl = 0;

        string wssid = string.Empty;
        string networktypew = string.Empty;
        string authentication = string.Empty;
        string encryption = string.Empty;
        string BSSID = string.Empty;
        string Signal = string.Empty;
        string Channelw = string.Empty;
        string Basicrates = string.Empty;
        string Otherrates = string.Empty;
        string Interfacename = string.Empty;
        string MACAddress = string.Empty;
        StringBuilder SSIDRouter = new StringBuilder();
        string authenticationRouter = string.Empty;
        string routernetworktype = string.Empty;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        /* old code
        private void ScanWifi()
        {
            try
            { 
                WlanClient client = new WlanClient();
                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {
                    
                   // wlanIface.Scan();

                    Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                    foreach (Wlan.WlanAvailableNetwork network in networks)
                    {
                        Wlan.Dot11Ssid ssid = network.dot11Ssid;
                        string networkName = Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                        WifiRangeMessage(("SSID   " + network.dot11DefaultCipherAlgorithm.ToString()));
                        SSIDName = network.dot11DefaultCipherAlgorithm.ToString();
                        WifiRangeMessage(("Signal quality   " + network.wlanSignalQuality + "%"));
                        signalstrength = network.wlanSignalQuality + "%";
                        WifiRangeMessage(("Security model   " + network.dot11DefaultAuthAlgorithm.ToString()));
                        securitymodel = network.dot11DefaultAuthAlgorithm.ToString();
                    }
                    string profileName = string.Empty;
                    foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                    {
                        string name = profileInfo.profileName;
                        string xml = wlanIface.GetProfileXml(profileInfo.profileName);
                        profileName = name;
                    }
                    Wlan.WlanBssEntry[] wlanBssEntries = wlanIface.GetNetworkBssList();
                        foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntries)
                        {
                            byte[] macAddr = wlanBssEntry.dot11Bssid;
                            var macAddrLen = (uint)macAddr.Length;
                            var str = new string[(int)macAddrLen];
                            for (int i = 0; i < macAddrLen; i++)
                            {
                                str[i] = macAddr[i].ToString("x2");
                            }
                            string mac = string.Join("-", str);
                            Wlan.Dot11Ssid ssid = wlanBssEntry.dot11Ssid;
                           
                            string ssidn2 = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
                           
                            dictionary.Add(ssidn2, mac);
                        }
                    // Connects to a known network with WEP security
                    Ping pingsender = new Ping();
                    string mac1 = "52544131303235572D454137443638";
                    string key = "hello";
                    string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>WEP</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>networkKey</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey><keyIndex>0</keyIndex></security></MSM></WLANProfile>", profileName, mac1, key);
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
                    WifiRangeMessage("Description/Router:   " + wlanIface.NetworkInterface.Description);
                    make = Convert.ToString(wlanIface.NetworkInterface.Description);
                    WifiRangeMessage("Name:          " + wlanIface.NetworkInterface.Name);

                    WifiRangeMessage("Network Type:  " + wlanIface.NetworkInterface.NetworkInterfaceType);
                    networktype = Convert.ToString(wlanIface.NetworkInterface.NetworkInterfaceType);
                    WifiRangeMessage("Speed:         " + wlanIface.NetworkInterface.Speed);
                    windowsize = "";
                    string ChannelName = wlanIface.Channel.ToString();
                    WifiRangeMessage("Channel:       " + ChannelName);
                    Channel = ChannelName.Trim().ToString();
                }

                string[][] output1;
                byte[] aryData = null;
                Hashtable ht = new Hashtable();
                ht.Add("1", "2.412");
                ht.Add("2", "2.417");
                ht.Add("3", "2.422");
                ht.Add("4", "2.427");
                ht.Add("5", "2.432");
                ht.Add("6", "2.437");
                ht.Add("7", "2.447");
                ht.Add("8", "2.452");
                ht.Add("9", "2.457");
                ht.Add("10", "2.462");
                ht.Add("11", "2.467");
                ht.Add("12", "2.472");
                ht.Add("13", "2.484");
               
                var s = ExecuteCommandSync("netsh wlan show network mode=bssid");
                var t = s;
                TextReader txtReader = new StringReader(s);
                string line;
                while ((line = txtReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        if (line.Trim().Contains("Interface name"))
                        {
                            var test = line.Trim().Split(':');
                            string Interfacename1 = test[1].Trim().ToString();
                            Interfacename = Interfacename1;
                        }
                        if ((line.Trim().Contains("SSID")) && (!line.Trim().Contains("BSSID")))
                        {
                            int x = line.IndexOf(":");
                            wssid = line.Substring(x + 1).Trim().ToString();
                            SSIDRouter.Append(wssid);
                            SSIDRouter.Append("|");
                        }
                        if (dictionary.ContainsKey(wssid))
                        {
                            string value = dictionary[wssid];
                            MACaddress=value.ToString();
                        }
                        if (line.Trim().Contains("Network type"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string networktype1 = string.Empty;
                                networktype1 = test[1].Trim().ToString();
                                networktypew = networktype1.ToString();
                            }
                        }
                        if (line.Trim().Contains("Authentication"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string authentication1 = null;
                                authentication1 = test[1].Trim().ToString();
                                authentication = authentication1;
                            }
                        }
                        if (line.Trim().Contains("Encryption"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string encryption1 = string.Empty;
                                encryption1 = test[1].Trim().ToString();
                                encryption = encryption1;
                            }
                        }
                        if (line.Trim().Contains("BSSID"))
                        {
                            int x = line.IndexOf(":");
                            string BSSID1 = line.Substring(x + 1).Trim().ToString();
                            BSSID = BSSID1.Trim().ToString();
                        }

                        if (line.Trim().Contains("Signal"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Signal1 = string.Empty;
                                Signal1 = test[1].Trim().ToString();
                                Signal = Signal1;
                            }
                        }
                        if (line.Trim().Contains("Channel"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Channel1 = string.Empty;
                                Channel1 = test[1].Trim().ToString();
                                Channelw = Channel1;
                            }
                        }
                        if (line.Trim().Contains("Basic rates"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Basicrates1 = string.Empty;
                                Basicrates1 = test[1].Trim().ToString();
                                Basicrates = Basicrates1;
                            }
                        }
                        if (line.Trim().Contains("Other rates"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Otherrates1 = string.Empty;
                                Otherrates1 = test[1].Trim().ToString();
                                Otherrates = Otherrates1;
                            }
                        }
                    }

                    if ((!string.IsNullOrEmpty(wssid.Trim())) &&
                    (!string.IsNullOrEmpty(networktypew.Trim())) &&
                    (!string.IsNullOrEmpty(authentication.Trim())) &&
                    (!string.IsNullOrEmpty(encryption.Trim())) &&
                    (!string.IsNullOrEmpty(BSSID.Trim())) &&
                    (!string.IsNullOrEmpty(Signal.Trim())) &&
                    (!string.IsNullOrEmpty(Channelw.Trim())) &&
                    (!string.IsNullOrEmpty(Basicrates.Trim())) &&
                    (!string.IsNullOrEmpty(Otherrates.Trim())))
                    {
                        foreach (string k in ht.Keys)
                        {
                            if (string.Compare(k, Channelw) == 0)
                            {
                                string wifiMAC = (string)ht[k];
                                WifiRangeMessage("Frequency:  " + wifiMAC + " Hz");
                                frequency = wifiMAC + " Hz";
                            }
                        }
                        output1 = new string[][]{
                                                new string[]{make,model,authentication,wssid,MACaddress,networktypew,Signal,frequency,Channelw,windowsize ,wifiIPaddress} 
                                        };
                        CommonClass.CreatingCsvFiles(output1, 5);

                        wssid =         string.Empty;
                        networktypew =  string.Empty;
                        authentication =string.Empty;
                        encryption =    string.Empty;
                        BSSID =         string.Empty;
                        Signal =        string.Empty;
                        Channelw =      string.Empty;
                        Basicrates =    string.Empty;
                        Otherrates =    string.Empty;
                        MACaddress =    string.Empty;
                    }
                    else
                    { }
                }

                aryData = CommonClass.GetByteData("wifi_devices_details_range.csv");

                obj.UploadFiles(aryData, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 3);
                //-------------------------Wifi Router---------------------
                string ssidn = string.Empty;
                WlanClient wlan = new WlanClient();
                Collection<String> connectedSsids = new Collection<string>();

                foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
                {
                    Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                    ssidn = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
                }
                var ssidsearch = ExecuteCommandSync("netsh wlan show profile name=" + ssidn.ToString());

                TextReader routerReader = new StringReader(ssidsearch);
                string routerline;

                while ((routerline = routerReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(routerline.Trim()))
                    {
                        if (routerline.Trim().Contains("Network type"))
                        {
                            int x = routerline.IndexOf(":");
                            routernetworktype = routerline.Substring(x + 1).Trim().ToString();
                        }
                        if (routerline.Trim().Contains("Authentication"))
                        {
                            int x = routerline.IndexOf(":");
                            authenticationRouter = routerline.Substring(x + 1).Trim().ToString();
                        }
                    }
                }
                int timeout = 5000;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                var lstAvailableNetworks = CommonClass.CheckAvailableNetworks();

                if (lstAvailableNetworks != null && lstAvailableNetworks.Count > 0)
                {

                    foreach (var oNetwork in lstAvailableNetworks)
                    {
                        if (oNetwork.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                        {
                            if (oNetwork.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211)
                            {
                                foreach (var ip in oNetwork.GetIPProperties().DhcpServerAddresses)
                                {
                                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                    {
                                        WifiRangeMessage("IP Address   :" + ip.ToString());
                                        wifiIPaddress = ip.ToString();
                                        //pingSender.Send(ip.Address, timeout, buffer, options);
                                        PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                                        switch (reply.Status)
                                        {
                                            case IPStatus.Success:
                                                string roundtrip = reply.RoundtripTime.ToString();
                                                Ttl = reply.Options.Ttl;
                                                break;
                                            case IPStatus.TimedOut:
                                                LocalnetMessage("Ping request timedout.");
                                                break;
                                            case IPStatus.Unknown:
                                                LocalnetMessage("Unexpected result.");
                                                break;
                                            case IPStatus.NoResources:
                                                LocalnetMessage("Ping request failed due to the insufficient resources");
                                                break;
                                            default:
                                                break;
                                        }
                                        //MACAddress = oNetwork.GetPhysicalAddress().ToString();
                                        //if (MACAddress.Length >= 10)
                                        //{
                                        //    MACAddress = MACAddress.Insert(2, "-"); //E894F60FEAC1
                                        //    MACAddress = MACAddress.Insert(5, "-");
                                        //    MACAddress = MACAddress.Insert(8, "-");
                                        //    MACAddress = MACAddress.Insert(11, "-");
                                        //    MACAddress = MACAddress.Insert(14, "-");
                                        //}
                                        var s2 = ExecuteCommandSync("arp -a " + ip.ToString());
                                        TextReader txtReaderMac = new StringReader(s2);
                                        string linemac;
                                        while ((linemac = txtReaderMac.ReadLine()) != null)
                                        {
                                            if (linemac.Trim().Contains("dynamic"))
                                            {
                                                var test = linemac.Trim().Split(' ');
                                                string MACAddress2 = test[9].Trim().ToString();
                                                MACAddress = MACAddress2;
                                            }

                                        }
                                        WifiRangeMessage("MAC Address  :" + MACAddress.ToString());
                                         WlanClient client1 = new WlanClient();
                                         foreach (WlanClient.WlanInterface wlanIface in client1.Interfaces)
                                         {
                                             Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                                             foreach (Wlan.WlanAvailableNetwork network in networks)
                                             {
                                                 Wlan.Dot11Ssid ssid = network.dot11Ssid;
                                                 string networkName = Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                                                 WifiRangeMessage(("SSID   " + network.dot11DefaultCipherAlgorithm.ToString()));
                                                 SSIDName = network.dot11DefaultCipherAlgorithm.ToString();
                                                 WifiRangeMessage(("Signal quality   " + network.wlanSignalQuality + "%"));
                                                 signalstrength = network.wlanSignalQuality + "%";
                                                 WifiRangeMessage(("Security model   " + network.dot11DefaultAuthAlgorithm.ToString()));
                                                 securitymodel = network.dot11DefaultAuthAlgorithm.ToString();
                                             }
                                             string ChannelName = wlanIface.Channel.ToString();
                                             Channel = ChannelName.Trim().ToString();

                                         }
                                         foreach (string k in ht.Keys)
                                         {
                                             if (string.Compare(k, Channel) == 0)
                                             {
                                                 string wifiMAC = (string)ht[k];
                                                 WifiRangeMessage("Frequency:  " + wifiMAC + " Hz");
                                                 frequency = wifiMAC + " Hz";
                                             }
                                         }
                                        windowsize = Convert.ToString(Convert.ToInt32(oNetwork.Speed) / Ttl);
                                    }
                                }
                            }
                        }
                    }
                    output1 = new string[][]{
                                                 new string[]{make,model,authenticationRouter,ssidn.ToString(),MACAddress,routernetworktype,signalstrength,frequency,Channel,wifiIPaddress}
                                                     };
                    CommonClass.CreatingCsvFiles(output1, 6);
                }
                byte[] arr = CommonClass.GetByteData("tbl_wificonnect.csv");
                obj.UploadFiles(arr, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 4);

                //-------------------------Complete Wifi Router---------------

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inside WiFi : Details - " + ex.Message.ToString());
            }

        }

        */

        private void Wifi()
        {
            try
            {
                //need to delete just for testing
                WlanClient client = new WlanClient();
                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {
                    // Lists all networks with WEP security  

                    Thread.Sleep(10);//milliseconds=100
                    Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                    foreach (Wlan.WlanAvailableNetwork network in networks)
                    {
                        Wlan.Dot11Ssid ssid = network.dot11Ssid;

                        string networkName = Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                        WifiRangeMessage(("SSID   " + network.dot11DefaultCipherAlgorithm.ToString()));
                        SSIDName = network.dot11DefaultCipherAlgorithm.ToString();
                        WifiRangeMessage(("Signal quality   " + network.wlanSignalQuality + "%"));
                        signalstrength = network.wlanSignalQuality + "%";
                        WifiRangeMessage(("Security model   " + network.dot11DefaultAuthAlgorithm.ToString()));
                        securitymodel = network.dot11DefaultAuthAlgorithm.ToString();
                    }
                    Thread.Sleep(100);    //milliseconds=100
                    string profileName = string.Empty;
                    foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                    {
                        string name = profileInfo.profileName;
                        string xml = wlanIface.GetProfileXml(profileInfo.profileName);
                        profileName = name;
                    }
                    Wlan.WlanBssEntry[] wlanBssEntries = wlanIface.GetNetworkBssList();
                    foreach (Wlan.WlanBssEntry wlanBssEntry in wlanBssEntries)
                    {
                        byte[] macAddr = wlanBssEntry.dot11Bssid;
                        var macAddrLen = (uint)macAddr.Length;
                        var str = new string[(int)macAddrLen];
                        for (int i = 0; i < macAddrLen; i++)
                        {
                            str[i] = macAddr[i].ToString("x2");
                        }
                        string mac1 = string.Join("-", str);
                        Wlan.Dot11Ssid ssid = wlanBssEntry.dot11Ssid;

                        string ssidn2 = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));

                        if (!dictionary.ContainsKey(ssidn2))
                        {
                            dictionary.Add(ssidn2, mac1);
                        }
                    }
                    // Connects to a known network with WEP security

                    make = Convert.ToString(wlanIface.NetworkInterface.Description);
                    WifiRangeMessage("Name:          " + wlanIface.NetworkInterface.Name);

                    WifiRangeMessage("Network Type:  " + wlanIface.NetworkInterface.NetworkInterfaceType);
                    networktype = Convert.ToString(wlanIface.NetworkInterface.NetworkInterfaceType);
                    WifiRangeMessage("Speed:         " + wlanIface.NetworkInterface.Speed);
                    windowsize = "";
                    string ChannelName = wlanIface.Channel.ToString();
                    Channel = ChannelName.Trim().ToString();
                }

                string[][] output1;
                byte[] aryData = null;
                Hashtable ht = new Hashtable();
                ht.Add("1", "2.412");
                ht.Add("2", "2.417");
                ht.Add("3", "2.422");
                ht.Add("4", "2.427");
                ht.Add("5", "2.432");
                ht.Add("6", "2.437");
                ht.Add("7", "2.447");
                ht.Add("8", "2.452");
                ht.Add("9", "2.457");
                ht.Add("10", "2.462");
                ht.Add("11", "2.467");
                ht.Add("12", "2.472");
                ht.Add("13", "2.484");

                var s = ExecuteCommandSync("netsh wlan show network mode=bssid");
                var t = s;
                TextReader txtReader = new StringReader(s);
                string line;
                while ((line = txtReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        if (line.Trim().Contains("Interface name"))
                        {
                            var test = line.Trim().Split(':');
                            string Interfacename1 = test[1].Trim().ToString();
                            Interfacename = Interfacename1;
                        }
                        if ((line.Trim().Contains("SSID")) && (!line.Trim().Contains("BSSID")))
                        {
                            int x = line.IndexOf(":");
                            wssid = line.Substring(x + 1).Trim().ToString();
                            SSIDRouter.Append(wssid);
                            SSIDRouter.Append("|");
                        }
                        if (dictionary.ContainsKey(wssid))
                        {
                            string value = dictionary[wssid];
                            MACaddress = value.ToString();
                        }
                        if (line.Trim().Contains("Network type"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string networktype1 = string.Empty;
                                networktype1 = test[1].Trim().ToString();
                                networktypew = networktype1.ToString();
                            }
                        }
                        if (line.Trim().Contains("Authentication"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string authentication1 = null;
                                authentication1 = test[1].Trim().ToString();
                                authentication = authentication1;
                            }
                        }
                        if (line.Trim().Contains("Encryption"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string encryption1 = string.Empty;
                                encryption1 = test[1].Trim().ToString();
                                encryption = encryption1;
                            }
                        }
                        if (line.Trim().Contains("BSSID"))
                        {
                            int x = line.IndexOf(":");
                            string BSSID1 = line.Substring(x + 1).Trim().ToString();
                            BSSID = BSSID1.Trim().ToString();
                        }
                        if (line.Trim().Contains("Signal"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Signal1 = string.Empty;
                                Signal1 = test[1].Trim().ToString();
                                Signal = Signal1;
                            }
                        }

                        if (line.Trim().Contains("Channel"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Channel1 = string.Empty;
                                Channel1 = test[1].Trim().ToString();
                                Channelw = Channel1;
                            }
                        }
                        if (line.Trim().Contains("Basic rates"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Basicrates1 = string.Empty;
                                Basicrates1 = test[1].Trim().ToString();
                                Basicrates = Basicrates1;
                            }
                        }
                        if (line.Trim().Contains("Other rates"))
                        {
                            var test = line.Trim().Split(':');
                            if (test.Length >= 2)
                            {
                                string Otherrates1 = string.Empty;
                                Otherrates1 = test[1].Trim().ToString();
                                Otherrates = Otherrates1;
                            }
                        }
                    }
                    //string sd = wssid + "|" + networktypew + "|" + authentication + "|" + encryption + "|" + BSSID + "|" + Signal + "|" + Radiotypew + "|" + Channelw + "|" + Basicrates + "|" + Otherrates;
                    if ((!string.IsNullOrEmpty(wssid.Trim())) &&
                    (!string.IsNullOrEmpty(networktypew.Trim())) &&
                    (!string.IsNullOrEmpty(authentication.Trim())) &&
                    (!string.IsNullOrEmpty(encryption.Trim())) &&
                    (!string.IsNullOrEmpty(BSSID.Trim())) &&
                    (!string.IsNullOrEmpty(Signal.Trim())) &&
                    (!string.IsNullOrEmpty(Channelw.Trim())) &&
                    (!string.IsNullOrEmpty(Basicrates.Trim())) &&
                    (!string.IsNullOrEmpty(Otherrates.Trim())))
                    {
                        foreach (string k in ht.Keys)
                        {
                            if (string.Compare(k, Channelw) == 0)
                            {
                                string wifiMAC = (string)ht[k];
                                WifiRangeMessage("Frequency:  " + wifiMAC + " Hz");
                                frequency = wifiMAC + " Hz";
                            }
                        }
                        output1 = new string[][]{
                                                new string[]{make,model,authentication,wssid,MACaddress,networktypew,Signal,frequency,Channelw,windowsize ,wifiIPaddress} 
                                        };
                        CommonClass.CreatingCsvFiles(output1, 5);

                        wssid = string.Empty;
                        networktypew = string.Empty;
                        authentication = string.Empty;
                        encryption = string.Empty;
                        BSSID = string.Empty;
                        Signal = string.Empty;
                        Channelw = string.Empty;
                        Basicrates = string.Empty;
                        Otherrates = string.Empty;
                        MACaddress = string.Empty;
                    }
                    else
                    { }
                }

                aryData = CommonClass.GetByteData("wifi_devices_details_range.csv");

                obj.UploadFiles(aryData, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 3);
                //-------------------------Wifi Router Connected---------------------
                string ssidn = string.Empty;
                WlanClient wlan = new WlanClient();
                Collection<String> connectedSsids = new Collection<string>();

                foreach (WlanClient.WlanInterface wlanInterface in wlan.Interfaces)
                {
                    Wlan.Dot11Ssid ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                    ssidn = new String(Encoding.ASCII.GetChars(ssid.SSID, 0, (int)ssid.SSIDLength));
                }
                var ssidsearch = ExecuteCommandSync("netsh wlan show profile name=" + ssidn.ToString());

                TextReader routerReader = new StringReader(ssidsearch);
                string routerline;

                while ((routerline = routerReader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(routerline.Trim()))
                    {
                        if (routerline.Trim().Contains("Network type"))
                        {
                            int x = routerline.IndexOf(":");
                            routernetworktype = routerline.Substring(x + 1).Trim().ToString();
                        }
                        if (routerline.Trim().Contains("Authentication"))
                        {
                            int x = routerline.IndexOf(":");
                            authenticationRouter = routerline.Substring(x + 1).Trim().ToString();
                        }
                    }
                }

                int timeout = 5000;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                var lstAvailableNetworks = CommonClass.CheckAvailableNetworks();

                if (lstAvailableNetworks != null && lstAvailableNetworks.Count > 0)
                {

                    foreach (var oNetwork in lstAvailableNetworks)
                    {
                        if (oNetwork.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                        {
                            if (oNetwork.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211)
                            {
                                foreach (var ip in oNetwork.GetIPProperties().DhcpServerAddresses)
                                {
                                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                    {
                                        WifiRangeMessage("IP Address   :" + ip.ToString());
                                        wifiIPaddress = ip.ToString();
                                        PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                                        switch (reply.Status)
                                        {
                                            case IPStatus.Success:
                                                string roundtrip = reply.RoundtripTime.ToString();
                                                Ttl = reply.Options.Ttl;
                                                break;
                                            case IPStatus.TimedOut:
                                                LocalnetMessage("Ping request timedout.");

                                                break;
                                            case IPStatus.Unknown:
                                                LocalnetMessage("Unexpected result.");
                                                break;
                                            case IPStatus.NoResources:
                                                LocalnetMessage("Ping request failed due to the insufficient resources");
                                                break;
                                            default:
                                                break;
                                        }
                                        if (dictionary.ContainsKey(ssidn))
                                        {
                                            MACAddress = dictionary[ssidn];
                                        }
                                        WlanClient client1 = new WlanClient();
                                        foreach (WlanClient.WlanInterface wlanIface in client1.Interfaces)
                                        {
                                            Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
                                            foreach (Wlan.WlanAvailableNetwork network in networks)
                                            {
                                                Wlan.Dot11Ssid ssid = network.dot11Ssid;
                                                string networkName = Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                                                WifiRangeMessage(("SSID   " + network.dot11DefaultCipherAlgorithm.ToString()));
                                                SSIDName = network.dot11DefaultCipherAlgorithm.ToString();
                                                WifiRangeMessage(("Signal quality   " + network.wlanSignalQuality + "%"));
                                                signalstrength = network.wlanSignalQuality + "%";
                                                WifiRangeMessage(("Security model   " + network.dot11DefaultAuthAlgorithm.ToString()));
                                                securitymodel = network.dot11DefaultAuthAlgorithm.ToString();
                                            }
                                            string ChannelName = wlanIface.Channel.ToString();
                                            Channel = ChannelName.Trim().ToString();

                                        }
                                        foreach (string k in ht.Keys)
                                        {
                                            if (string.Compare(k, Channel) == 0)
                                            {
                                                string wifiMAC = (string)ht[k];
                                                WifiRangeMessage("Frequency:  " + wifiMAC + " Hz");
                                                frequency = wifiMAC + " Hz";
                                            }
                                        }

                                        windowsize = Convert.ToString(Convert.ToInt32(oNetwork.Speed) / Ttl);
                                    }
                                }
                            }
                        }
                    }
                    output1 = new string[][]{
                                                 new string[]{make,model,authenticationRouter,ssidn.ToString(),MACAddress,routernetworktype,signalstrength,frequency,Channel,wifiIPaddress}
                                                     };

                    CommonClass.CreatingCsvFiles(output1, 6);
                }
                byte[] arr = CommonClass.GetByteData("tbl_wificonnect.csv");
                obj.UploadFiles(arr, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 4);

                //-------------------------Complete Wifi Router Connected Data---------------

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inside WiFi : Details - " + ex.Message.ToString());
            }

        }

        #endregion

        #region Local Network

        string portnumber = string.Empty;
        List<int> lstPorts = new List<int>();

        List<int> lstPort = new List<int>();

        private void LocalNetwork()
        {
            log.Info("Process start for local devices");
            #region IP
            DateTime dtS = DateTime.Now;
            lstPort.Add(1); lstPort.Add(5); lstPort.Add(7);
            lstPort.Add(18); lstPort.Add(20); lstPort.Add(21);
            lstPort.Add(22); lstPort.Add(23); lstPort.Add(25);
            lstPort.Add(29); lstPort.Add(37);
            lstPort.Add(42); lstPort.Add(43);
            lstPort.Add(49); lstPort.Add(53);
            lstPort.Add(69); lstPort.Add(70);
            lstPort.Add(79); lstPort.Add(80);
            lstPort.Add(103); lstPort.Add(108);
            lstPort.Add(109); lstPort.Add(110);
            lstPort.Add(115); lstPort.Add(118);
            lstPort.Add(119); lstPort.Add(137);
            lstPort.Add(139); lstPort.Add(143);
            lstPort.Add(150); lstPort.Add(156);
            lstPort.Add(161); lstPort.Add(179);
            lstPort.Add(190); lstPort.Add(194);
            lstPort.Add(197); lstPort.Add(389);
            lstPort.Add(396); lstPort.Add(443);
            lstPort.Add(444); lstPort.Add(445);
            lstPort.Add(458); lstPort.Add(546);
            lstPort.Add(547); lstPort.Add(563);
            lstPort.Add(569); lstPort.Add(1080);

            log.Info("Started Port Scan  " + DateTime.Now);

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;
            LocalnetMessage("Network Computer IP Address");
            //-------------------Vinay-------
            log.Info("Executing arp -a");
            var s = ExecuteCommandSync("arp -a");

            var iLineCount = 1;
            string sIpAddress = string.Empty;
            string macaddress = string.Empty;
            var roundTrip = 0.00;
            double buffersize = 0.00;
            double Ttl = 0.00;

            TextReader txtReader = new StringReader(s);
            string line;

            while ((line = txtReader.ReadLine()) != null)
            {
                if (iLineCount > 2)
                {
                    if (!string.IsNullOrEmpty(line.Trim()))
                    {
                        var test = line.Trim().Split(' ');
                        if (test != null && test.Length > 2)
                        {
                            sIpAddress = test[0];

                            log.Info("Found IP address : " + sIpAddress);

                            //MAC address
                            for (var iCount = 1; iCount < test.Length; iCount++)
                            {
                                if (!string.IsNullOrEmpty(test[iCount]))
                                {
                                    macaddress = test[iCount];
                                    break;
                                }
                            }
                            for (var j1 = 0; j1 < 5; j1++)
                            {
                                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                                byte[] buffer = Encoding.ASCII.GetBytes(data);
                                int timeout = 5000;
                                PingReply reply = pingSender.Send(sIpAddress, timeout, buffer, options);

                                switch (reply.Status)
                                {
                                    case IPStatus.Success:
                                        LocalnetMessage(string.Format("Address: {0}", reply.Address.ToString()));
                                        LocalnetMessage(string.Format("RoundTrip time: {0}", reply.RoundtripTime));
                                        roundTrip += reply.RoundtripTime;
                                        LocalnetMessage(string.Format("Time to live: {0}", reply.Options.Ttl));
                                        Ttl += reply.Options.Ttl;
                                        LocalnetMessage(string.Format("Buffer size: {0}", reply.Buffer.Length));
                                        buffersize += reply.Buffer.Length;
                                        break;
                                    case IPStatus.TimedOut:
                                        LocalnetMessage("Ping request timedout.");
                                        buffersize = 0.00;
                                        break;
                                    case IPStatus.Unknown:
                                        LocalnetMessage("Unexpected result.");
                                        break;
                                    case IPStatus.NoResources:
                                        LocalnetMessage("Ping request failed due to the insufficient resources");
                                        break;
                                    default:
                                        break;
                                }
                            }

                            roundTrip = roundTrip / 5;
                            buffersize = buffersize / 5;
                            Ttl = Ttl / 5;


                            ScanPort(sIpAddress, lstPort);
                            string sModifiedString = sbport.ToString();

                            if (roundTrip >= 0.50)
                                roundTrip = 1;

                            StringBuilder sb = new StringBuilder();
                            sb.Append(sIpAddress);
                            sb.Append(",");
                            sb.Append(macaddress);
                            sb.Append(",");
                            sb.Append(localvendor);
                            sb.Append(",");
                            sb.Append(localping);
                            sb.Append(",");
                            sb.Append(string.Format("{0:0.00}", roundTrip));
                            sb.Append(",");
                            sb.Append(Ttl);
                            sb.Append(",");
                            sb.Append(buffersize.ToString());
                            sb.Append(",");
                            sb.Append(sModifiedString);

                            log.Info(sb.ToString());

                            string[][] output1 = new string[][]{
                                             new string[]{sIpAddress,macaddress.ToString(),localvendor,localping, string.Format("{0:0.00}", roundTrip) ,Ttl.ToString(),buffersize.ToString(),sModifiedString}
                                                };
                            CommonClass.CreatingCsvFiles(output1, 3);
                            //sbport = new StringBuilder();
                            sbport.Remove(0, sbport.Length);
                            sIpAddress = string.Empty;
                            macaddress = string.Empty;
                            localvendor = string.Empty;
                            localping = string.Empty;
                            buffersize = 0;
                            roundTrip = 0;
                            Ttl = 0;
                            sModifiedString = string.Empty;

                        }
                    }
                }
                iLineCount++;
            }
            //-----------------end vinay----------------------------
            DateTime dtE = DateTime.Now;
            TimeSpan dtC = dtE.Subtract(dtS);
            log.Info("Time Consuming for Ping all IP and Port Scan all IP: " + dtC.Minutes + " Minutes");
            log.Info("===========================================" + "End Port Scan  :" + DateTime.Now + "===========================================");
            byte[] aryData = CommonClass.GetByteData("local_network.csv");
            obj.UploadFiles(aryData, "Data" + Guid.NewGuid() + ".csv", iGlobalID, 1);
        }
            #endregion



        #endregion

        public bool UploadLogFile()
        {
            log4net.Appender.RollingFileAppender fileAppenderTrace = new log4net.Appender.RollingFileAppender();
            fileAppenderTrace.ImmediateFlush = true;
            fileAppenderTrace.LockingModel = new FileAppender.MinimalLock();

            bool returnValue = false;
            FileStream fs = null;
            byte[] data = null;
            string logFileName = string.Empty;
            try
            {
                string logFilePath = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"];
                if (!string.IsNullOrEmpty(logFilePath))
                {
                    if (File.Exists(logFilePath))
                    {
                        logFileName = iGlobalID.ToString() + "_" + IPAddressGlobal.ToString() + "_" + DateTime.Now.Date.ToString("dd/MM/yyyy hh:mm:ss").Replace("/", "").Replace(":", "").Replace(" ", "").ToString() + ".txt";
                        fs = new FileStream(logFilePath, FileMode.Open);
                        data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        fs.Flush();
                        fs.Close();
                        fs.Dispose();

                        obj.UploadTextFile(data, logFileName, 2);
                        returnValue = true;
                        //File.Delete(logFilePath);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return returnValue;
        }

        private string ScanPort(string sIpAddress, List<int> ports)
        {
            sbport = new StringBuilder();
            try
            {
                foreach (var port in ports)
                {
                    using (var tcp = new TcpClient())
                    {
                        var ar = tcp.BeginConnect(IPAddress.Parse(sIpAddress), port, null, null);
                        using (ar.AsyncWaitHandle)
                        {
                            //Wait 2 seconds for connection.
                            if (ar.AsyncWaitHandle.WaitOne(100, false))
                            {
                                try
                                {
                                    tcp.EndConnect(ar);
                                    sbport.Append(port.ToString() + "|");
                                    //Connect was successful.
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                //Connection timed out.
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(sbport.ToString()) && sbport.ToString().LastIndexOf('|') == sbport.Length - 1)
                {
                    sbport.Remove(sbport.ToString().LastIndexOf('|'), 1);
                }
                return sbport.ToString();
            }

            catch (Exception ex)
            {
                return sbport.ToString();
            }

            //sbport.ToString();
        }

    }


}
