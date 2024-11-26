using Game.Enemy;
using Game.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public Transform[] waypoints;   // Array di punti di destinazione (waypoints)
    private float movementSpeed = 15f; // Velocit� di movimento del laser

    private int currentWaypointIndex = 0; // Indice del punto di destinazione corrente

    private EnemyScript bossLife; // Riferimento allo script del boss
    private GameObject bossObject; // Riferimento al boss
    private AudioSource audioSource; // Riferimento all'audio source
    private Material materialBoss; // Materiale del boss

    void Start()
    {
        bossObject = GameObject.Find("MassimoBartoletti"); // Ottieni il riferimento al boss
        bossLife = bossObject.GetComponent<EnemyScript>(); // Ottieni il riferimento allo script EnemyScript
        materialBoss = GameObject.Find("MassimoBartolettiSkin").GetComponent<Renderer>().material; // Ottieni il riferimento al materiale del boss
        audioSource = GetComponent<AudioSource>(); // Ottieni il riferimento all'audio source
        audioSource.mute = true;
    }

    
    void Update()
    {

        if (bossLife.health <= bossLife.maxHealth / 2) 
        {
            // Cambia colore al boss
            materialBoss.color = Color.red;
            materialBoss.SetFloat("_Metallic", 0.381f);

            if (currentWaypointIndex < waypoints.Length) //Avvio del movimento dei laser
            {
                // Muove il laser verso il punto di destinazione corrente
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, movementSpeed * Time.deltaTime);

                // Verifica se il laser ha raggiunto il punto di destinazione corrente
                if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
                {
                    currentWaypointIndex++; // Passa al punto di destinazione successivo
                }

                if (currentWaypointIndex == 4)
                {
                    currentWaypointIndex = 0;
                }

                if (currentWaypointIndex == 2 && gameObject.tag == "VerticalLaser")
                {
                    currentWaypointIndex = 0;
                }
            }
        }

    }
}
