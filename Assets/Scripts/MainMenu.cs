using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public KeyCode altKey = KeyCode.Joystick1Button0;
    public Image image;
    public Image tutorial;
    private bool transition;
    private Color color;
    private float alpha;
    private int contador;

    // Start is called before the first frame update
    void Start()
    {
        transition = false;
        alpha = 0;
        contador = 0;
        tutorial.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(key) || Input.GetKeyDown(altKey)))
        {
            transition = true;
            if (contador > 0)
            {
                SceneManager.LoadScene("Historia");
            }
        }
        if (transition)
        {
            alpha += Time.deltaTime;
            if (alpha >= 2)
            {
                contador++;
                tutorial.enabled = true;
            }
            color = new Color(0, 0, 0, alpha);
            image.color = color;
        }
    }
}
