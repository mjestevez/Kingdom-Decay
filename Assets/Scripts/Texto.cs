using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto : MonoBehaviour
{
    private Text texto;
    private string cadena;
    private bool cambio;
    // Start is called before the first frame update
    void Start()
    {
        texto = GetComponent<Text>();
        cambio = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cambio)
        {
            texto.text = cadena;
            cambio = false;
        }
    }

    public void cambiarTexto(string texto)
    {
        cadena = texto;
        cambio = true;
    }

}
