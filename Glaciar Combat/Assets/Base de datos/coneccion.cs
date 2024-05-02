using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName= "Servidor", menuName = "Morion/Servidor", order =1)]
public class coneccion : MonoBehaviour
{
   public void jalarDatos()
    {
        // A correct website page.
        StartCoroutine(GetRequest("http://localhost/prueba/index.php"));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
        

    public string servidor;
    public Servicio[] servicios;

    public bool ocupado = false;
    public Respuesta respuesta;
    public void ConsumirServicio(string nombre, string[] datos)
    {
        
        ocupado = true;
        WWWForm fromulario = new WWWForm();
        Servicio s;
        for (int i = 0; i < servicios.Length; i++) 
        {
            if (servicios[i].Equals(nombre))
            {
                s = servicios[i];
            }
        }
        UnityWebRequest www = UnityWebRequest.Post(servidor,
            "{ 'field1': 1, 'field2': 2 }",
            "application/json");
    }
}

[System.Serializable]
public class Servicio
{
    public string   nombre;
    public string   URL;
    public string[] parametros;
}
public class Respuesta
{
    public int codigo;
    public string mensaje;
}
//hd

