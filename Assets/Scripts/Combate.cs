using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tipo
{
    Slime,
    Orc,
    Ghost,
    Dragon

}
public class Combate : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public KeyCode altKey = KeyCode.Joystick1Button0;
    public BossLife boss;
    public PlayerLife player;
    public Acciones acc;
    public Texto scriptTexto;
    private int accion;
    private Stats stats;

    [Header("Attack")]
    public Scrollbar attackBar;
    private float attackBarValue;
    private bool attack;
    private float multAttack;

    [Header("Magic")]
    public Image magicBar;
    private float magicBarValue;
    private bool magic;
    private float multMagic;
    public float maxCharge;
    private float charge;
    private float maxMana;

    [Header("Defend")]
    public Scrollbar defendBar;
    private float defendBarValue;
    private bool defend;
    private float multDefend;
    public Image defendRelleno;
    private float defendRellenoValue;
    public float maxDefend;
    private float timeDefend;
    private float defendValue;
    private bool pulsado;
    public Text defendText;

    [Header("Enemy")]
    public Tipo tipo;
    private bool defensaEnemiga;
    private bool attackUp;

    private bool fin = false;

    
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        accion = -1;
        attackBar.gameObject.SetActive(false);
        magicBar.gameObject.SetActive(false);
        defendBar.gameObject.SetActive(false);
        attackBarValue = 0;
        magicBarValue = 0.01f;
        attack = false;
        magic = false;
        multAttack = 1;
        multMagic = 20;
        defensaEnemiga = false;
        charge = 0;
        defend = false;
        multDefend = 1;
        defendRellenoValue = 0;
        timeDefend = 0;
        defendValue = 0;
        pulsado = false;
        defendText.text = "0%";
        attackUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fin)
        {
            if (accion != -1)
            {
                if (accion == 1)
                {
                    scriptTexto.cambiarTexto("Attack!");
                    attack = true;
                    attackBar.gameObject.SetActive(true);

                }
                else if (accion == 0)
                {
                    maxMana = player.MaxMana();
                    if (maxMana >= 50) maxMana = 1;
                    else if (maxMana >= 30) maxMana = 0.95f;
                    else if (maxMana >= 15) maxMana = 0.5f;
                    else maxMana = 0.2f;
                    scriptTexto.cambiarTexto("Charge!");
                    magic = true;
                    magicBar.gameObject.SetActive(true);
                    magicBar.fillAmount = 0.01f;
                    magicBarValue = 0.01f;
                }
                else
                {
                    scriptTexto.cambiarTexto("Defend!");
                    defend = true;
                    defendBar.gameObject.SetActive(true);
                    defendBar.value = 0;
                    defendBarValue = 0;
                    defendRelleno.fillAmount = 0.01f;
                    defendRellenoValue = 0.01f;
                    defendValue = 0;
                    defendText.text = "0%";
                }

                accion = -1;
            }
            //Ataque Fisico
            if (attack)
            {
                if ((Input.GetKeyDown(key) || Input.GetKeyDown(altKey)))
                {
                    attack = false;
                    attackBar.gameObject.SetActive(false);
                    Ataque(attackBarValue);
                }
                attackBarValue += Time.deltaTime * multAttack;
                if (attackBarValue <= 0)
                {
                    attackBarValue = 0;
                    multAttack = 1;
                }
                else if (attackBarValue >= 1)
                {
                    attackBarValue = 1;
                    multAttack = -1;
                }
                attackBar.value = attackBarValue;
            }

            //Ataque Mágico
            if (magic)
            {
                magicBarValue -= Time.deltaTime / multMagic;
                if (magicBarValue < 0.01f) magicBarValue = 0.01f;
                if ((Input.GetKeyDown(key) || Input.GetKeyDown(altKey)))
                {
                    magicBarValue += Time.deltaTime * 2;
                }
                if (magicBarValue > 1) magicBarValue = 1;
                if (magicBarValue > maxMana) magicBarValue = maxMana;
                //Colors
                if (magicBarValue <= 0.2f) magicBar.color = Color.red;
                else if (magicBarValue <= 0.5f) magicBar.color = Color.magenta;
                else if (magicBarValue <= 0.95f) magicBar.color = Color.blue;
                else magicBar.color = Color.green;

                magicBar.fillAmount = magicBarValue;

                charge += Time.deltaTime;
                if (charge >= maxCharge)
                {
                    charge = 0;
                    magic = false;
                    magicBar.gameObject.SetActive(false);
                    AtaqueMagico(magicBar.fillAmount);
                }
            }
            //Defensa
            if (defend)
            {
                defendBarValue += Time.deltaTime * multDefend / 10;
                if (defendBarValue <= 0)
                {
                    defendBarValue = 0;
                    multDefend = 1;
                }
                else if (defendBarValue >= 1)
                {
                    defendBarValue = 1;
                    multDefend = -1;
                }
                defendBar.value = defendBarValue;

                if ((Input.GetKeyDown(key) || Input.GetKeyDown(altKey))) pulsado = true;
                if ((Input.GetKeyUp(key) || Input.GetKeyUp(altKey))) pulsado = false;
                if (pulsado)
                {
                    defendRellenoValue += Time.deltaTime / 5;
                }
                else
                {
                    defendRellenoValue -= Time.deltaTime / 5;
                }
                if (defendRellenoValue < 0.01f) defendRellenoValue = 0.01f;
                if (defendRellenoValue > 1) defendRellenoValue = 1;
                defendRelleno.fillAmount = defendRellenoValue;
                if (defendBar.value <= defendRelleno.fillAmount && defendBar.value >= defendRelleno.fillAmount - 0.07f)
                {
                    defendValue += Time.deltaTime / 5;
                    float mostrar = defendValue * 100;
                    defendText.text = mostrar.ToString("00") + "%";
                }

                timeDefend += Time.deltaTime;
                if (timeDefend >= maxDefend)
                {
                    timeDefend = 0;
                    defend = false;
                    defendBar.gameObject.SetActive(false);
                    Defensa(defendValue);
                }
            }
        }
        
    }

    public void Accion(int selec)
    {
        accion = selec;
    }

    public void Ataque(float value)
    {
        float daño=0;
        if (value >= 0.50 && value <= 0.52)
        {
            daño = 100;
            scriptTexto.cambiarTexto("Critical Hit!");
        }
        else if ((value >= 0.340 && value <= 0.499) || (value >= 0.521 && value <= 0.699))
        {
            daño = 84;
            scriptTexto.cambiarTexto("It's super effective!");
        }
        else if ((value >= 0.160 && value <= 0.339) || (value >= 0.700 && value <= 0.849))
        {
            daño = 67;
            scriptTexto.cambiarTexto("Well... this maybe hurts");
        }
        else if ((value >= 0 && value <= 0.159) || (value >= 0.850 && value <= 1))
        {
            daño = 1;
            scriptTexto.cambiarTexto("Oh wow... what a failure!");
        }
        else if (defensaEnemiga)
        {
            daño *= 3f / 4f;
            defensaEnemiga = false;
        }
        daño *= stats.daño;
        boss.QuitarVida(daño, false);
        Invoke("EnemyAction", 1f);
    }
    public void AtaqueMagico(float value)
    {
        float daño = 0f;
        if (value <= 0.2f)
        {
            daño = 1f;
            scriptTexto.cambiarTexto("Do you really know something of magic?");
        }
        else if (value <= 0.5f)
        {
            daño = 67f;
            player.QuitarMana(15f);
            scriptTexto.cambiarTexto("I recommend you to buy a flamethrower next time...");
        }else if (value <= 0.95f)
        {
            daño = 84f;
            player.QuitarMana(30f);
            scriptTexto.cambiarTexto("That's a great fireball!");
        }
        else
        {
            daño = 100f;
            player.QuitarMana(50f);
            scriptTexto.cambiarTexto("Wow! That's is a real inferno!");
        }
        daño *= stats.magia;
        boss.QuitarVida(daño, true);
        Invoke("EnemyAction", 1f);
    }

    private void Defensa(float value)
    {
        value *= 100;
        float vida = 0;
        if (value < 50)
        {
            scriptTexto.cambiarTexto("Defense has failed... ");
        }else if (value < 75)
        {
            player.DefensaUp();
            scriptTexto.cambiarTexto("Defense up!");
        }else if(value < 95)
        {
            player.DefensaUp();
            vida = 20;
            scriptTexto.cambiarTexto("Defense up! \nLife up!");
        }
        else
        {
            player.DefensaUp();
            vida = 35;
            scriptTexto.cambiarTexto("Defense up! \nLife up++!");
        }
        player.CurarVida(vida);
        Invoke("EnemyAction", 1f);
    }
    private void PlayerStart()
    {
        acc.StartAction();
    }
    private void EnemyAction()
    {
        float option = Random.Range(1f, 100f);
        scriptTexto.cambiarTexto(boss.nameBoss+" enemy is attacking");
        if (tipo == Tipo.Slime)
        {
            if (option <= 70)
                Invoke("EnemyAttack", 2f);
            else
                Invoke("SlimeDefend", 2f);
        }else if (tipo == Tipo.Orc)
        {
            if (option <= 70)
                Invoke("EnemyAttack", 2f);
            else
                Invoke("OrcDefend", 2f);
        }else if (tipo == Tipo.Ghost)
        {
            if (option <= 40)
                Invoke("EnemyAttack", 2f);
            else if(option<=80)
                Invoke("EnemyMagicAttack", 2f);
            else
                Invoke("GhostAttack", 2f);
        }else if (tipo == Tipo.Dragon)
        {
            if(option<=35)
                Invoke("EnemyAttack", 2f);
            else if (option<=70)
                Invoke("EnemyMagicAttack", 2f);
            else
                Invoke("DragonDefend", 2f);

        }


    }

    private void EnemyAttack()
    {
        int n = Random.Range(1, 101);
        float daño = Random.Range(boss.dañoFisico, boss.dañoFisico * 2);
        if (attackUp)
        {
            daño *= 1.5f;
            attackUp = false;
        }
        if (n <= 5) {
            player.QuitarVida(daño * 2f);
            scriptTexto.cambiarTexto("Critical Hit!");
        }
        else if(n>=6 && n <= 90)
        {
            player.QuitarVida(daño);
            scriptTexto.cambiarTexto("The enemy music hurts you");
        }
        else
        {
            scriptTexto.cambiarTexto("You dodge the hit! Lucky! ");
        }

        Invoke("PlayerStart", 2f);

    }

    private void EnemyMagicAttack()
    {
        int n = Random.Range(1, 101);
        float daño = Random.Range(boss.dañoMagico, boss.dañoMagico * 2);
        if (attackUp)
        {
            daño *= 1.5f;
            attackUp = false;
        }
        if (n <= 5)
        {
            player.QuitarVida(daño * 2f);
            scriptTexto.cambiarTexto("Critical Hit!");
        }
        else if (n >= 6 && n <= 90)
        {
            player.QuitarVida(daño);
            scriptTexto.cambiarTexto("The enemy music hurts your mind");
        }
        else
        {
            scriptTexto.cambiarTexto("You dodge the hit! Lucky! ");
        }

        Invoke("PlayerStart", 2f);

    }


    private void SlimeDefend()
    {
        int n = Random.Range(1, 101);
        if (n <= 50)
        {
            boss.CurarVida(Random.Range(25f, 50f));
            scriptTexto.cambiarTexto("The power of jazz seems to heal them ");
        }
        else
        {
            defensaEnemiga = true;
            scriptTexto.cambiarTexto("The power of jazz seems to make them more hardy\n\nDefense up!");
        }
        Invoke("PlayerStart", 2f);
    }

    private void OrcDefend()
    {
        int n = Random.Range(1, 101);
        if (n <= 50)
        {
            player.DefensaDown();
            scriptTexto.cambiarTexto("His words about your mother hurt in your heart\n\nDefense down!");
        }
        else
        {
            attackUp = true;
            scriptTexto.cambiarTexto("Something about \"bebesita\" make him stronger\n\nAttack up! ");
        }
        Invoke("PlayerStart", 2f);
    }

    private void GhostAttack()
    {
        int n = Random.Range(1, 101);
        float daño = Random.Range(boss.dañoFisico, boss.dañoFisico * 2);
        if (attackUp)
        {
            daño *= 1.5f;
            attackUp = false;
        }
        if (n <= 90)
        {
            player.QuitarVida(daño * 2);
            scriptTexto.cambiarTexto("This remix fucks your mind");
        }
        else
        {
            scriptTexto.cambiarTexto("You dodge the hit! Lucky! ");
        }

        Invoke("PlayerStart", 2f);
    }

    private void DragonDefend()
    {
        int n = Random.Range(1, 31);
        if (n <= 10)
        {
            boss.CurarVida(Random.Range(50f, 100f));
            scriptTexto.cambiarTexto("The power of metal fill he with determination");
        }else if (n <= 20)
        {
            defensaEnemiga = true;
            scriptTexto.cambiarTexto("You can't damage this metal!\n\nDefense Up!");
        }else
        {
            attackUp = true;
            scriptTexto.cambiarTexto("Prepare your body for the next attack!\n\nAttack up! ");
        }
        Invoke("PlayerStart", 2f);
    }

    public void finCombate(bool win)
    {
        if (win)
        {
            string texto = "You Win!\nLevel Up!\n\n";
            int n = Random.Range(0, 3);
            switch (n)
            {
                case 0:
                    texto += "Your sword is stronger";
                    stats.daño += 0.5f;
                    break;
                case 1:
                    texto += "Your magic power is stronger";
                    stats.mana += 50;
                    stats.magia += 0.5f;
                    break;
                case 2:
                    texto += "Your mind and body are stronger";
                    stats.life += 50;
                    break;
            }
            scriptTexto.cambiarTexto(texto);
        }
        else
        {
            scriptTexto.cambiarTexto("You Lose!");
            stats.escena = 27;
        }
        fin = true;
        stats.finJuego();
        Destroy(this);
        

    }

    
}
