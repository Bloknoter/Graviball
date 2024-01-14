using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace WebCommunication
{
    public class WebBridge
    {
        /// <summary>
        /// Requests data from web page
        /// </summary>
        /// <param name="key">The key for data</param>
        /// <returns>string as received data</returns>
        [DllImport("__Internal")]
        public static extern string GetValue(string key);

        /// <summary>
        /// Sends data to the web page
        /// </summary>
        /// <param name="key">The key for data</param>
        /// <param name="val">Data</param>
        /// <returns></returns>
        [DllImport("__Internal")]
        public static extern void SendValue(string key, string val);

        /// <summary>
        /// Shows a message box on the web page
        /// </summary>
        /// <param name="info">Information to display in message box</param>
        /// <returns></returns>
        [DllImport("__Internal")]
        public static extern void ShowMessageBox(string info);
    }
}
