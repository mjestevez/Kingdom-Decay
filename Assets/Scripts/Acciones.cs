using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acciones : MonoBehaviour
{
    public KeyCode key=KeyCode.Space;
    public KeyCode altKey = KeyCode.Joystick1Button0;
    public Image triangle;
    public Sprite[] acciones;
    private string[] textos;
    public float time;
    public Texto scriptTexto;
    public Combate combat;
    private float contador;
    private int selec;
    private bool action;
    private string texto;
    // Start is called before the first frame update
    void Start()
    {
        textos = new string[3];
        textos[0]= "Select your action:\n\nAttack";
        textos[1] = "Select your action:\n\nDefend";
        textos[2] = "Select your action:\n\nMagic";
        contador = 0;
        selec = 1;
        action = false;
        texto = textos[0];
        scriptTexto.cambiarTexto(texto);
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (action && contador >= time) contador = 0;
        if (!action && contador >= time)
        {
            texto = textos[selec];
            triangle.sprite = acciones[selec];
            selec++;
            selec %= acciones.Length;
            contador = 0;
            scriptTexto.cambiarTexto(texto);
        }
        if (!action && (Input.GetKeyDown(key) || Input.GetKeyDown(altKey)))
        {
            action = true;
            Invoke("Combate", 0.1f);
        }

    }

    public void StartAction()
    {
        action = false;
        contador = 0;
    }

    public void Combate (){
        combat.Accion(selec);
    }
}
