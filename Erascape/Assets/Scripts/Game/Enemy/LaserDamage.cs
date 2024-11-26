using Game.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{

    private LifeBars playerLifeBars; // Riferimento allo script LifeBars del giocatore

    // Start is called before the first frame update
    void Start()
    {
        playerLifeBars = FindObjectOfType<LifeBars>(); // Trova l'oggetto con lo script LifeBars e ottieni il riferimento
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLifeBars.TakeDamage(10f);
        }
    }
    
}
