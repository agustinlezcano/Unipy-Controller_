using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour
{
    //Tutorial de Unity
	public int sceneNmbr;
    
    public int SceneNmbr
    {
        get{
            return sceneNmbr;
        }
        set{
            sceneNmbr = value;
        }
    }

	//////////////////////////////////////////
    // Atributo asociado a la rutina asíncrona de
    // carga de escena
    AsyncOperation asyncLoad;

    

    IEnumerator LoadAsyncronously(int sceneNmbr){

        asyncLoad = SceneManager.LoadSceneAsync(sceneNmbr);
        while (!asyncLoad.isDone){
            yield return null; // Esperar el próximo cuadro antes de continuar
        }
    }

    void OnFadeComplete(){
        asyncLoad.allowSceneActivation = true;
    }
}

