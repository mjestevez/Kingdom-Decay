using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats= GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        stats.Reload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
