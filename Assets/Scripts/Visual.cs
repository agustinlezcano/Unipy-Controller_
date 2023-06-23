using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class Visual : MonoBehaviour
{
    public bool Objectstate = false;
    public GameObject Can_au;
    public GameObject Can_man;
    public GameObject Can_ex;
    public GameObject Object_p1;
    public GameObject Can_back;
    public CambiaEscena Escena;
    //------------------------------
    public GameObject Slider_1;
    public GameObject Slider_2;
    public GameObject Slider_3;
    public GameObject Slider_4;
    public GameObject Slider_5;
    public GameObject Slider_6;
    //-------------------------------
    public GameObject Fondo;
    public GameObject Panel;
    //-------------------------------
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;
    public GameObject Text6;
    //-------------------------------
    public GameObject MyListener;
    public GameObject ArmController;

    //awake se activa antes que start
    void Awake(){
        //Escena = GameObject.GetComponent<CambiaEscena>();
        Can_au = GameObject.Find("AutomaticButton");
        Can_man = GameObject.Find("ManualButton");
        Object_p1= GameObject.Find("GameObject_p1"); 
        Fondo = GameObject.Find("Fondo");
        Panel = GameObject.Find("Panel");
        Object_p1.SetActive(false);    
        Can_man.SetActive(true);
        Can_au.SetActive(true);
        Can_ex.SetActive(true);
        Can_back.SetActive(false);
        //-------------------------
        Slider_1.SetActive(false);
        Slider_2.SetActive(false);
        Slider_3.SetActive(false);
        Slider_4.SetActive(false);
        Slider_5.SetActive(false);
        Slider_6.SetActive(false);
        Panel.SetActive(true);
        Fondo.SetActive(true);
        //-------------------------
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);
        Text4.SetActive(false);
        Text5.SetActive(false);
        Text6.SetActive(false);
        //-------------------------
        MyListener.SetActive(false);
        ArmController.SetActive(false);

    }
    void Update(){
        if (Escena.sceneNmbr == 1){
            Object_p1.SetActive(true);    
            Can_man.SetActive(false);
            Can_au.SetActive(false);
            Can_ex.SetActive(false);
            Can_back.SetActive(true);

            Slider_1.SetActive(true);
            Slider_2.SetActive(true);
            Slider_3.SetActive(true);
            Slider_4.SetActive(true);
            Slider_5.SetActive(true);
            Slider_6.SetActive(true);
            Panel.SetActive(false);
            Fondo.SetActive(false);

            //-------------------------
            Text1.SetActive(false);
            Text2.SetActive(false);
            Text3.SetActive(false);
            Text4.SetActive(false);
            Text5.SetActive(false);
            Text6.SetActive(false);

            //--------------------------
            MyListener.SetActive(false);
            ArmController.SetActive(true);
        }
        if (Escena.sceneNmbr == 2){
            Object_p1.SetActive(true);    
            Can_man.SetActive(false);
            Can_au.SetActive(false);
            Can_ex.SetActive(false);
            Can_back.SetActive(true);

            Slider_1.SetActive(false);
            Slider_2.SetActive(false);
            Slider_3.SetActive(false);
            Slider_4.SetActive(false);
            Slider_5.SetActive(false);
            Slider_6.SetActive(false);
            Panel.SetActive(false);
            Fondo.SetActive(false);
            //-------------------------
            Text1.SetActive(true);
            Text2.SetActive(true);
            Text3.SetActive(true);
            Text4.SetActive(true);
            Text5.SetActive(true);
            Text6.SetActive(true);

            //--------------------------
            MyListener.SetActive(true);
            ArmController.SetActive(false);
        }
        if (Escena.sceneNmbr == 3){
            Object_p1.SetActive(false);    
            Can_man.SetActive(true);
            Can_au.SetActive(true);
            Can_ex.SetActive(true);
            Can_back.SetActive(false);
            //-------------------------
            Slider_1.SetActive(false);
            Slider_2.SetActive(false);
            Slider_3.SetActive(false);
            Slider_4.SetActive(false);
            Slider_5.SetActive(false);
            Slider_6.SetActive(false);
            //-------------------------
            Text1.SetActive(false);
            Text2.SetActive(false);
            Text3.SetActive(false);
            Text4.SetActive(false);
            Text5.SetActive(false);
            Text6.SetActive(false);  
            Panel.SetActive(true);
            Fondo.SetActive(true);
            //--------------------------
            MyListener.SetActive(false);
            ArmController.SetActive(false);
        }
        if (Escena.sceneNmbr == 4){
            Application.Quit();
        }

    }

}
