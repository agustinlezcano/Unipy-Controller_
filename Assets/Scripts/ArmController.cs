using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;   //para poder referenciar los Slider y botones
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ArmController : MonoBehaviour
{
    public MyListener ml;
    public Slider Slider_1;
    public Slider Slider_2;
    public Slider Slider_3;
    public Slider Slider_4;
    public Slider Slider_5;
    public Slider Slider_6;
    
    private float Slider_1_value = 0.0f;
    private float Slider_2_value = 0.0f;
    private float Slider_3_value = 0.0f;
    private float Slider_4_value = 0.0f;
    private float Slider_5_value = 0.0f;
    private float Slider_6_value = 0.0f;
    public Transform[] Link = new Transform[6];
    Quaternion[] startRotation = new Quaternion[6];
    Quaternion[] currentRotation = new Quaternion[6];
    Quaternion[] smoothRotation = new Quaternion[6];
    Quaternion[] endRotation = new Quaternion[6];

    //Valores fijos de cada articulación
    float[,] fixedValues = new float[6,2];

    //Velocidad y límites para la rotacion (ver datasheet del robot)
    public float TurnRate = 1.0f;
    //Los valores angulares están invertidos en el modelo de Unity
    private float Link1YRot = 0.0f;
    public float Link1YRotMin = -160.0f;
    public float Link1YRotMax = 160.0f;

    private float Link2XRot = 0.0f;
    public float Link2XRotMin = -45.0f;
    public float Link2XRotMax = 225.0f;
    
    private float Link3XRot = 0.0f;
    public float Link3XRotMin = -225.0f;
    public float Link3XRotMax = 45.0f;

    private float Link4YRot = 0.0f;
    public float Link4YRotMin = -100.0f;
    public float Link4YRotMax = 170.0f;

    private float Link5ZRot = 0.0f;
    public float Link5ZRotMin = -100.0f;
    public float Link5ZRotMax = 100.0f;

    private float Link6XRot = 0.0f;
    public float Link6XRotMin = -266.0f;
    public float Link6XRotMax = 266.0f;

    //Para capturar el valor del Slider 
    void CheckInput()
    {
        Slider_1_value = Slider_1.value;
        Slider_2_value = Slider_2.value;
        Slider_3_value = Slider_3.value;
        Slider_4_value = Slider_4.value;
        Slider_5_value = Slider_5.value;
        Slider_6_value = Slider_6.value;
    }


    void ProcessMovement()
    {
        Link1YRot = Slider_1_value; // * TurnRate;
        Link1YRot = Mathf.Clamp(Link1YRot, Link1YRotMin, Link1YRotMax);
        Link[0].localEulerAngles = new Vector3(fixedValues[0,0], Link1YRot, fixedValues[0,1]); 

        Link2XRot = Slider_2_value; // * TurnRate;
        Link2XRot = Mathf.Clamp(Link2XRot, Link2XRotMin, Link2XRotMax);
        Link[1].localEulerAngles = new Vector3(Link2XRot, fixedValues[1,0], fixedValues[1,1]);
        //rotating our Link arm of the robot here around the X axis and multiplying
        //the rotation by the slider's value and the turn rate for the Link arm.
        Link3XRot = Slider_3_value; // * TurnRate;
        Link3XRot = Mathf.Clamp(Link3XRot, Link3XRotMin, Link3XRotMax);
        Link[2].localEulerAngles = new Vector3(Link3XRot, fixedValues[2,0], fixedValues[2,1]);

        Link4YRot = Slider_4_value; // * TurnRate;
        Link4YRot = Mathf.Clamp(Link4YRot, Link4YRotMin, Link4YRotMax);
        Link[3].localEulerAngles = new Vector3(fixedValues[3,0], Link4YRot, fixedValues[3,1]);

        Link5ZRot = Slider_5_value; // * TurnRate;
        Link5ZRot = Mathf.Clamp(Link5ZRot, Link5ZRotMin, Link5ZRotMax);
        Link[4].localEulerAngles = new Vector3(fixedValues[4,0], fixedValues[4,1], Link5ZRot);

        Link6XRot = Slider_6_value; // * TurnRate;
        Link6XRot = Mathf.Clamp(Link6XRot, Link6XRotMin, Link6XRotMax);
        Link[5].localEulerAngles = new Vector3(Link6XRot, fixedValues[5,0], fixedValues[5,1]);
    }

    void Start()
    {
        Slider_1.minValue = Link1YRotMin;   //-1;
        Slider_1.maxValue = Link1YRotMax;   //1;
        Slider_2.minValue = Link2XRotMin;
        Slider_2.maxValue = Link2XRotMax;
        Slider_3.minValue = Link3XRotMin;
        Slider_3.maxValue = Link3XRotMax;
        Slider_4.minValue = Link4YRotMin;
        Slider_4.maxValue = Link4YRotMax;
        Slider_5.minValue = Link5ZRotMin;
        Slider_5.maxValue = Link5ZRotMax;
        Slider_6.minValue = Link6XRotMin;
        Slider_6.maxValue = Link6XRotMax;

        fixedValues[0,0] = Link[0].localEulerAngles.x;
        fixedValues[0,1] = Link[0].localEulerAngles.z;
        fixedValues[1,0] = Link[1].localEulerAngles.y;
        fixedValues[1,1] = Link[1].localEulerAngles.z;
        fixedValues[2,0] = Link[2].localEulerAngles.y;
        fixedValues[2,1] = Link[2].localEulerAngles.z;
        fixedValues[3,0] = Link[3].localEulerAngles.x;
        fixedValues[3,1] = Link[3].localEulerAngles.z;
        fixedValues[4,0] = Link[4].localEulerAngles.x;
        fixedValues[4,1] = Link[4].localEulerAngles.y;
        fixedValues[5,0] = Link[5].localEulerAngles.y;
        fixedValues[5,1] = Link[5].localEulerAngles.z;
         
        for (int i=0;i<6;i++){
            startRotation[i] = Quaternion.Euler(0, 0, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        ProcessMovement();
    }
}

