using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//using NativeWifi;

namespace Wi_Fi_Speed_Detector.Helpers
{
    class PInvoke
    {
        [DllImport("wlanapi.dll")]
        public static extern int WlanQueryInterface(
            [In] IntPtr clientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            //[In] NativeWifi.Wlan.WlanIntfOpcode opCode,
            [In, Out] IntPtr pReserved,
            [Out] out int dataSize,
            [Out] out IntPtr ppData); //,
            //[Out] out WlanOpcodeValueType wlanOpcodeValueType);
    }
}
