using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using System.Net.Sockets;
using TMPro;

namespace Mirror.Examples.Basic
{
    public class ChangeIP : MonoBehaviour
    {
      
        /// changes IP to what is filled in in the input field on the screen
        public TextMeshProUGUI ipText;

        public void Start()
        {
            string ip = "IP IS: " + TheLocalIP();
            ipText.text = ip;
        }

        public void changeIP()
        {
            TMP_InputField inputF = this.GetComponent<TMP_InputField>();
            FindObjectOfType<NetworkRoomManager>().networkAddress = inputF.text;
        }


        public void connect()
        {
            System.Uri finalIp = new System.Uri("kcp://" + FindObjectOfType<NetworkRoomManager>().networkAddress + ":7777");
            NetworkManager.singleton.StartClient(finalIp);
        }

        public static string TheLocalIP()
        {
            IPHostEntry hostEntry;
            string localIP = "0.0.0.0";
            hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }


}
