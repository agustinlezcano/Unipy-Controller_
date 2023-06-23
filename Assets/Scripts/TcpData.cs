using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;


public class TcpData : MonoBehaviour
{
    //****************************************
    // IP y PORT para comunicación TCP/IP
    private static String IP = "127.0.0.1";
    private static int PORT = 13001;
    //==================================================
    // Set-Get de parámetros anteriores


    public static String GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }  

    public static String getIP()
    {
        return IP;
    }

    public static void setIP(String newIP)
    {
        IP = newIP;
    }

    public static int getPORT()
    {
        return PORT;
    }

    public static void setPORT(int newPORT)
    {
        PORT = newPORT;
    }
    
    /*
    // Start is called before the first frame update
    void Start()
    {
       const int portNumber = 13; 
    }

    // Update is called once per frame
    void Update()
    {
        

try
{
    // Use the Pending method to poll the underlying socket instance for client connection requests.
    IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
    TcpListener tcpListener = new TcpListener(ipAddress, portNumber);
    tcpListener.Start();

    if (!tcpListener.Pending())
    {
        Console.WriteLine("Sorry, no connection requests have arrived");
    }
    else
    {
        //Accept the pending client connection and return a TcpClient object initialized for communication.
        TcpClient tcpClient = tcpListener.AcceptTcpClient();
        // Using the RemoteEndPoint property.
        Console.WriteLine("I am listening for connections on " +
            IPAddress.Parse(((IPEndPoint)tcpListener.LocalEndpoint).Address.ToString()) +
            "on port number " + ((IPEndPoint)tcpListener.LocalEndpoint).Port.ToString());

        //Close the tcpListener and tcpClient instances
        tcpClient.Close();
    }

    tcpListener.Stop();
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}

    }*/
}
