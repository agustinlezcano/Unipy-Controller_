using UnityEngine;
using Vuforia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;


public class MyListener : MonoBehaviour
{
    public int connectionPort = 25001;
    TcpListener listener;
    string[] stringArray;
    float[] Objetivo;  
    float[] posInicial;
    float[] currAngle;
    string msg;
    public Transform[] Link = new Transform[6];
    Quaternion[] startRotation = new Quaternion[6];
    Quaternion[] currentRotation = new Quaternion[6];
    Quaternion[] smoothRotation = new Quaternion[6];
    Quaternion[] endRotation = new Quaternion[6];
    //public float duration; // Duración de la interpolación esférica
    //private TrackableBehaviour mTrackableBehaviour;

    int flagGiro, remoto;   //Indica si está girando o no segun consigna TCP/IP
    public int flag;
    float duration = 3f;

    public TMP_Text Text1;
    public TMP_Text Text2;
    public TMP_Text Text3;
    public TMP_Text Text4;
    public TMP_Text Text5;
    public TMP_Text Text6;

    void Start()
    {
        flagGiro = 0;
        flag=0;
        remoto = 0;
        /*
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        */
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();
        for (int i=0;i<6;i++){
            startRotation[i] = Quaternion.Euler(0, 0, 0);
        }
    }
    /*
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            StartCoroutine(RotateObject(duration));
        }
    }
    */
    void Update()
    {
        if (!listener.Pending()){
            flag=0; 
            for (int i = 0; i < 6; i++)
            {
                Link[i].localRotation = endRotation[i]; // asegurarse de que se alcance la rotación final exacta
            }
            if (remoto==1)
            {
                for (int i = 0; i < 6; i++)
                {
                    Link[i].localRotation = endRotation[i];
                }
                
            }
            } 
        else {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            msg = reader.ReadToEnd();
            stringArray = ParseData(msg);            
            Text1.text = stringArray[0];
            Text2.text = stringArray[1];
            Text3.text = stringArray[2];
            Text4.text = stringArray[3];
            Text5.text = stringArray[4];
            Text6.text = stringArray[5];
            //x,y,z también pueden ser un array
            float y = float.Parse(stringArray[0]);
            float x = float.Parse(stringArray[1]);
            float x1 = float.Parse(stringArray[2]);
            float y1 = float.Parse(stringArray[3]);
            float z = float.Parse(stringArray[4]);
            float x3 = float.Parse(stringArray[5]);

            endRotation[0] = Quaternion.Euler(Link[0].localEulerAngles.x, y, Link[0].localEulerAngles.z); //endRotation[0] = Quaternion.Euler(0, y/div, 0);
            endRotation[1] = Quaternion.Euler(x, Link[1].localEulerAngles.y, Link[1].localEulerAngles.z);
            endRotation[2] = Quaternion.Euler(x1, Link[2].localEulerAngles.y, Link[2].localEulerAngles.z);
            endRotation[3] = Quaternion.Euler(Link[3].localEulerAngles.x, y1, Link[3].localEulerAngles.z);
            endRotation[4] = Quaternion.Euler(Link[4].localEulerAngles.x, Link[4].localEulerAngles.y, z);
            endRotation[5] = Quaternion.Euler(x3, Link[5].localEulerAngles.y, Link[5].localEulerAngles.z); 

            // Obtener las rotaciones iniciales de los objetos secundarios
            for (int i = 0; i < 6; i++)
            {
                startRotation[i] = Link[i].localRotation;
            } 
                StartCoroutine(RotateObject(duration));
            }
    }

    public static string[] ParseData(string dataString)
    {
        Debug.Log(dataString);
        if (dataString[0]==':'){
            dataString = dataString.Remove(dataString.Length-4);
            dataString = dataString.Substring(1);       
        }
        // Split the elements into an array
        string[] stringArray = dataString.Split('/');
        return stringArray;
    }
    
IEnumerator RotateObject(float duration)
{

    for (int i = 0; i < 6; i++)
    {
        startRotation[i] = Link[i].localRotation;
    }
    
    float elapsedTime = 0f;
    while (elapsedTime < duration)
    {
        for (int i = 0; i < 6; i++)
        {
            smoothRotation[i] = Quaternion.Slerp(startRotation[i], endRotation[i], elapsedTime / duration);
            Link[i].localRotation = smoothRotation[i];
        }
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    for (int i = 0; i < 6; i++)
    {
        Link[i].localRotation = endRotation[i]; // asegurarse de que se alcance la rotación final exacta
        Vector3 endPosition = Link[i].localPosition;
        Link[i].localPosition = endPosition;
    }
    remoto = 1;

}
}
