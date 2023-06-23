using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using TMPro;

public class TcpCommunication : MonoBehaviour
{
    //myListener: ver el resto del programa
    Thread thread;
    TcpListener server;
    TcpClient client;
    bool running;

    TcpListener listener;
    public String msg, IP = "127.0.0.1";
    public Int32 PORT = 13001;
    public string TextValue;
    public TMP_Text textElement;
    private string input;

    // Start is called before the first frame update
    void Start()
    {       
        //Capsule.SetActive(false);
        //inputPort = GameObject.Find("InputPort");
        //Mirar como aplicar el enviar desde el input a una variable
        IP = TcpData.GetLocalIPAddress();
        //IP = TcpData.getIP();        
        PORT = TcpData.getPORT();
        listener = new TcpListener(IPAddress.Parse(IP), PORT);
        listener.Start();
        print("TCP/IP Communication ready - IP:" + IP + " PORT:" + PORT);
        TextValue = "Comunicacion exitosa";
    }

    // Update is called once per frame
    void Update()
    {
        try{
            if (!listener.Pending()){
            Console.WriteLine("Sorry, no connection requests have arrived");
        } else {
            //print("socket comes");
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            //Capsule.SetActive(true);
            msg = reader.ReadToEnd();
            textElement.text = msg;
            print(msg);
            msgParser(msg);
            client.Close();
        }
        listener.Stop();
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

 
/*
    public void EditedIP(){
        int editedPort = int.Parse(inputPort.GetComponent<InputField>().text);
        TcpData.setPORT(editedPort);
        Debug.Log("PORT changed to: " + editedPort);
    }
*/
    public void ReadString(string s){
        input = s;
        Debug.Log(input);
    }
    public void interpreterString(string s){
        if (s=="1"){
            print("HOLA");
        }
        
    }

    public void msgParser(String msg){
    if (msg[0] != ':'){
        Debug.Log("Error: Invalid msg");
        return;
    }    
    }
}
