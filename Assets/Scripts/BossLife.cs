using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLife : MonoBehaviour
{
    public string nameBoss;
    public Sprite firstImagen;
    public float time;
    public Sprite secondImagen;
    private float currentTime;
    public Image uiBar;
    public float maxLife;
    public float dañoFisico;
    public float dañoMagico;
    public float defensaFisica;
    public float defensaMagica;
    public Acciones player;
    private float life;
    private float v;
    private Image boss;
    private bool imagenUno;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        boss = GetComponent<Image>();
        imagenUno = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (life <= 0)
        {
            boss.fillAmount -= Time.deltaTime;
            if (boss.fillAmount <= 0) boss.fillAmount = 0;
        }else if (currentTime >= time)
        {
            if (imagenUno)
            {
                boss.sprite = secondImagen;
                imagenUno = false;
            }
            else
            {
                boss.sprite = firstImagen;
                imagenUno = true;
            }
            
            currentTime = 0;
        }

        if (boss.fillAmount <= 0)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Combate");
            go.GetComponent<Combate>().finCombate(true);
            Destroy(this);
        }
        
        uiBar.fillAmount = life / maxLife;
    }

    public void QuitarVida(float ataque,bool magico)
    {
        v = Random.Range(85f, 100f);
        float daño;
        if (magico) daño = 0.01f * v * (ataque - defensaMagica);
        else daño= 0.01f * v * (ataque - defensaFisica);
        int dañoInt=(int)daño;
        if (dañoInt > 0) life -= dañoInt;
        if (life <= 0) life = 0;
    }

    public void CurarVida(float vida)
    {
        int vidaInt = (int)vida;
        life += vidaInt;
        if (life > maxLife) life = maxLife;
    }
    
}
