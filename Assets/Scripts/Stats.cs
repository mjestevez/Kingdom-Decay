using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public KeyCode altKey = KeyCode.Joystick1Button0;
    public float daño;
    public float magia;
    public float mana;
    public float life;
    public int nextCombat;

    public int escena;
    public Text texto;
    public Image panel;
    private bool transition;
    private Color color;
    private float alpha;
    public Image kingdom;
    public Sprite[] imagenes;
    private bool combate;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        daño = 1;
        magia = 1;
        mana = 0;
        life = 0;
        nextCombat = 0;
        escena = 1;
        combate = false;
    }

    public void Reload()
    {
        transition = false;
        alpha = 0;
        kingdom= GameObject.FindGameObjectWithTag("Imagen").GetComponent<Image>();
        panel = GameObject.FindGameObjectWithTag("Panel").GetComponent<Image>();
        texto = GameObject.FindGameObjectWithTag("Texto").GetComponent<Text>();
    }

    private void Update()
    {
        if(panel!=null || texto != null)
        {
            if (!combate)
            {

                if (!transition)
                {
                    if ((Input.GetKeyDown(key) || Input.GetKeyDown(altKey)))
                    {
                        escena++;
                    }
                    switch (escena)
                    {
                        case 1:
                            texto.text = "The Kingdom of Aurenia was a prosperous kingdom and its habitants lived happily under the reign of King Apollo III.";
                            break;
                        case 2:
                            texto.text = "One fateful day the king got sicked and died. His son, Orpheus I, succeeded him on the throne.";
                            break;
                        case 3:
                            texto.text = "King Orpheus I, due to his inexperience, led his kingdom to ruin...";
                            break;
                        case 4:
                            kingdom.sprite = imagenes[1];
                            texto.text = "The decay of the kingdom of Aurenia spread rapidly through the land.";
                            break;
                        case 5:
                            texto.text = "Taking advantage of his weakness, the rhythm army, led by the powerful FuzroDà, invaded the kingdom.";
                            break;
                        case 6:
                            texto.text = "But they didn't expected a little hero who would end the invasion...";
                            break;
                        case 7:
                            transition = true;
                            break;
                        case 8:
                            kingdom.sprite = imagenes[2];
                            texto.text = "The battle seems to have been easy, but part of the castle has been destroyed.";
                            break;
                        case 9:
                            texto.text = "Suddenly, a sound from the underworld pierces your ears and invades your mind.";
                            break;
                        case 10:
                            texto.text = "A group of orcs enter the citadel, led by the most fearsome of them: Cruel Bunny...";
                            break;
                        case 11:
                            transition = true;
                            break;
                        case 12:
                            kingdom.sprite = imagenes[3];
                            texto.text = "You didn't know if the difficult was in the battle or in the infamous sound...";
                            break;
                        case 13:
                            texto.text = "But you manage to end the group of orcs. Although the castle continues to suffer damage.";
                            break;
                        case 14:
                            texto.text = "After taking a well-deserved rest, a shuddering scream wakes you up.";
                            break;
                        case 15:
                            texto.text = "Spectral forms emerge from the bodies of the fallen.";
                            break;
                        case 16:
                            texto.text = "Your fears are true, the leaders of the ghosts have arrived!";
                            break;
                        case 17:
                            transition = true;
                            break;
                        case 18:
                            kingdom.sprite = imagenes[4];
                            texto.text = "This time there isn't rest!";
                            break;
                        case 19:
                            texto.text = "A gigantic shadow flies over the remains of the castle.";
                            break;
                        case 20:
                            texto.text = "After beating all his minions, the all-mighty FuzroDà lands in the throne room.";
                            break;
                        case 21:
                            texto.text = "Furious, charge against you.";
                            break;
                        case 22:
                            transition = true;
                            break;
                        case 23:
                            kingdom.sprite = imagenes[5];
                            texto.text = "That rhythm was too intense. You die!";
                            break;
                        case 24:
                            texto.text = "Luckily, the powerful dragon also falls under your feet.";
                            break;
                        case 25:
                            texto.text = "The kingdom you swore to defend is safe from invasion.\n But you still dead...";
                            break;
                        case 26:
                            texto.text = "Thanks for playing!";
                            break;
                        case 27:
                            transition = true;
                            break;
                        case 28:
                            kingdom.sprite = imagenes[6];
                            texto.text = "That rhythm was too intense. You die!";
                            break;
                        case 29:
                            texto.text = "The kingdom you swore to defend is lost.";
                            break;
                        case 30:
                            texto.text = "Thanks for playing!";
                            break;
                        case 31:
                            transition = true;
                            break;
                    }
                }
                else
                {
                    alpha += Time.deltaTime;
                    if (alpha >= 2)
                    {
                        switch (escena)
                        {
                            case 7:
                                combate = true;
                                Invoke("cargarNuevoCombate", 1f);
                                break;
                            case 11:
                                combate = true;
                                Invoke("cargarNuevoCombate", 1f);
                                break;
                            case 17:
                                combate = true;
                                Invoke("cargarNuevoCombate", 1f);
                                break;
                            case 22:
                                combate = true;
                                Invoke("cargarNuevoCombate", 1f);
                                break;
                            case 27:
                                Invoke("volverMenu", 1f);
                                break;
                            case 31:
                                Invoke("volverMenu", 1f);
                                break;
                        }
                    }
                    color = new Color(0, 0, 0, alpha);
                    panel.color = color;
                }
            }
        }
        
        
        
    }

    public void finJuego()
    {
        Invoke("cargarHistoria", 3f);
    }

    public void cargarNuevoCombate()
    {

        nextCombat++;
        SceneManager.LoadScene("Combat" + nextCombat);
    }

    public void cargarHistoria()
    {
        combate = false;
        escena++;
        SceneManager.LoadScene("Historia");
    }

    public void volverMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(this.gameObject);
        
    }

}
