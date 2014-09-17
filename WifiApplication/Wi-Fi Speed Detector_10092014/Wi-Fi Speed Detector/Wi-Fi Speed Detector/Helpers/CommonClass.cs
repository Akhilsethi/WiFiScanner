using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Collections;
using CsvHelper;
using CsvHelper.Configuration;
using System.Configuration;
namespace WiFiSpeedDetector.Helpers
{
    class CommonClass
    {
        public const string DATA = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public static bool CheckInternet()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;
                // this allow to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                // discard virtual cards (virtual box, virtual pc, etc.)
                if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;
                // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;
                return true;
            }
            return false;
        }

        public static List<NetworkInterface> CheckAvailableNetworks()
        {
            List<NetworkInterface> lstAvailableNetworks = new List<NetworkInterface>();

            NetworkInterface[] networkInterFace = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var oTemp in networkInterFace)
            {
                lstAvailableNetworks.Add(oTemp);
            }
            return lstAvailableNetworks;
        }


        public static IEnumerable<IPAddress> GetTraceRoute(string hostNameOrAddress)
        {
            return GetTraceRoute(hostNameOrAddress, 1);
        }
        private static IEnumerable<IPAddress> GetTraceRoute(string hostNameOrAddress, int ttl)
        {
            Ping pinger = new Ping();
            PingOptions pingerOptions = new PingOptions(ttl, true);
            int timeout = 10000;
            byte[] buffer = Encoding.ASCII.GetBytes(DATA);
            PingReply reply = default(PingReply);

            reply = pinger.Send(hostNameOrAddress, timeout, buffer, pingerOptions);

            List<IPAddress> result = new List<IPAddress>();
            if (reply.Status == IPStatus.Success)
            {
                result.Add(reply.Address);
            }
            else if (reply.Status == IPStatus.TtlExpired)
            {
                
                result.Add(reply.Address);
                IEnumerable<IPAddress> tempResult = default(IEnumerable<IPAddress>);
                tempResult = GetTraceRoute(hostNameOrAddress, ttl + 1);
                result.AddRange(tempResult);
            }
            else if (reply.Status == IPStatus.TimedOut)
            {
                //failure 
                result.Add(reply.Address);
                IEnumerable<IPAddress> tempResult = default(IEnumerable<IPAddress>);
                tempResult = GetTraceRoute(hostNameOrAddress, ttl + 1);
                result.AddRange(tempResult);
            }
            else
            {

            }
            return result;
        }
        public static void CreatingCsvFiles(string[][] output, int iOption)
        {
            string filePath = string.Empty;
            //string currentPath = Directory.GetCurrentDirectory();
            //if (!Directory.Exists(Path.Combine(currentPath, "WifiCSV Folder")))
            //{
            //    Directory.CreateDirectory(Path.Combine(currentPath, "WifiCSV Folder"));
            //    filePath = Path.Combine(currentPath, "Wifi");
            //}
            //else
            //{
            //    filePath = filePath.ToString();
            //}
            switch (iOption)
            {
                    
                case 0:
                    filePath = "../global.csv";
                    //filePath = filePath + "/" + "/global.csv";
                    break;
                case 1:
                    filePath = "../externalping.csv";
                    break;
                case 3:
                    filePath = "../local_network.csv";
                    break;
                case 4:
                    filePath = "../ping_tracerout.csv";
                    break;
                case 5:
                    filePath = "../wifi_devices_details_range.csv";
                    break;
                case 6:
                    filePath = "../tbl_wificonnect.csv";
                    break;
                case 7:
                    filePath = "../tblportscan.csv";
                    break;
                case 8:
                    filePath = "../tblexternalping.csv";
                    break;
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string delimiter = ",";
            int length = output.GetLength(0);
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < length; index++)
                sb.AppendLine(string.Join(delimiter, output[index]));
            File.AppendAllText(filePath, sb.ToString());
        }


        public void WriteData(Wi_Fi_Speed_Detector.Helpers.GlobalData record)
        {
            using (var sw = new StreamWriter(@"tblGlobal.csv"))
            {   
                var writer = new CsvWriter(sw);
                writer.WriteField(record.IPAddress);
                writer.WriteField(record.ISP);
                writer.WriteField(record.Latitude);
                writer.WriteField(record.Longitude);
                writer.WriteField(record.City);
                writer.WriteField(record.Country);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            using (var sw = new StreamWriter(@"ping_tracerout.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteField(record.IPToPing);
                writer.WriteField(record.traceroute);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            using (var sw = new StreamWriter(@"externalping.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteField(record.PingS);
                writer.WriteField(record.UploadTm);
                writer.WriteField(record.DownloadTm);
                writer.WriteField(record.IPAddressGlobal);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            using (var sw = new StreamWriter(@"tbl_wificonnect.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteField(record.make);
                writer.WriteField(record.model);
                writer.WriteField(record.securitymodel);
                writer.WriteField(record.networktype);
                writer.WriteField(record.signalstrength);
                writer.WriteField(record.frequency);
                writer.WriteField(record.ChannelName);
                writer.WriteField(record.wifiIPaddress);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            using (var sw = new StreamWriter(@"tbl_wifi_devices_details_range.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteField(record.makeD);
                writer.WriteField(record.modelD);
                writer.WriteField(record.securitymodelD);
                writer.WriteField(record.networktypeD);
                writer.WriteField(record.signalstrengthD);
                writer.WriteField(record.frequencyD);
                writer.WriteField(record.ChannelNameD);
                writer.WriteField(record.windowsizeD);
                writer.WriteField(record.wifiIPaddressD);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            using (var sw = new StreamWriter(@"tbl_local_network.csv"))
            {

                var writer = new CsvWriter(sw);
                writer.WriteField(record.localipaddress);
                writer.WriteField(record.localmacaddress);
                writer.WriteField(record.localvendor);
                writer.WriteField(record.localping);
                writer.WriteField(record.portscan);
                writer.WriteField(record.Buffersize);
                writer.WriteField(record.Ttl);
                writer.WriteField(record.roundtrip);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }


        internal void WriteData()
        {
            throw new NotImplementedException();
        }

        public static byte[] GetByteData(string sFileName)
        {   
            byte[] Buffer  =null;

            //string filePath = string.Empty;
            //string currentPath = Directory.GetCurrentDirectory();
            //if (Directory.Exists(Path.Combine(currentPath, "Wifi")))
            //{
            //    filePath = Path.Combine(currentPath, "Wifi");
            //}
            //else 
            //{ 
            
            //}
            if (File.Exists("../" +  sFileName))
            {
                string FilePath = "../" + sFileName;
                int Offset = 0;
                int ChunkSize = 65536;
                Buffer = new byte[ChunkSize];
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                try
                {
                    long FileSize = new FileInfo(FilePath).Length;
                    fs.Position = Offset;
                    int BytesRead = 0;
                    while (Offset != FileSize)
                    {
                        BytesRead = fs.Read(Buffer, 0, ChunkSize);
                        if (BytesRead != Buffer.Length)
                        {
                            ChunkSize = BytesRead;
                            byte[] TrimmedBuffer = new byte[BytesRead];
                            Array.Copy(Buffer, TrimmedBuffer, BytesRead);
                            Buffer = TrimmedBuffer;
                        }

                        Offset += BytesRead; 
                    }
                    return Buffer;
                }
                    
                catch (Exception ex)
                {
                    
                    return null;
                }
                finally
                {
                    fs.Close();
                }
            }
            return null;
            
        }

        
        
    }
}
