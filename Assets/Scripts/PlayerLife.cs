using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float maxLife;
    public float maxMana;
    public Image uiLife;
    public Image uiMana;
    private float life;
    private float mana;
    private int defensa;
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        maxLife += stats.life;
        maxMana += stats.mana;
        life = maxLife;
        mana = maxMana;
        defensa = 0;
    }

    // Update is called once per frame
    void Update()
    {
        uiLife.fillAmount = life / maxLife;
        uiMana.fillAmount = mana / maxMana;

        if (life <= 0)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Combate");
            go.GetComponent<Combate>().finCombate(false);
            Destroy(this);
        }

    }
    public void QuitarVida(float vida)
    {
        if (defensa==1)
        {
            vida /= 2;
            defensa = 0;
        }
        else if(defensa==-1)
        {
            vida *= 1.5f;
            defensa = 0;
        }
        life -= vida;
        if (life <= 0) life = 0;
    }

    public void CurarVida(float vida)
    {
        life += vida;
        if (life > maxLife) life = maxLife;
    }

    public void QuitarMana(float manana)
    {
        mana -= manana;
        if (mana <= 0) mana = 0;
    }

    public float MaxMana()
    {
        return mana;
    }

    public void DefensaUp()
    {
        defensa = 1;
    }

    public void DefensaDown()
    {
        defensa = -1;
    }
}
