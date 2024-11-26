using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    private RawImage mirino; // Riferimento al mirino
    private Animator parentAnimator; // Riferimento all'animator del parent
    public Gun gun; // Riferimento alla pistola
    
    private void Start()
    {
        mirino = GameObject.FindGameObjectWithTag("Mirino").GetComponent<RawImage>(); // Ottieni il riferimento al mirino
        parentAnimator = GetComponentInParent<Animator>(); // Ottieni il riferimento all'animator del parent
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyEgyptian") || other.CompareTag("EnemyMedieval"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            int damage;
            // check if cheats are on PlayerPrefs, if yes, damage is equal to enemy health
            bool cheats = Convert.ToBoolean(PlayerPrefs.GetInt("instakill", 0));
            if (!cheats)
            {
                damage  = gun.damage;
            }
            else
            {
                damage = (int) enemy.health;
            }

            if (enemy != null)
            {
                StartCoroutine(HitMarker());
                //Debug.Log("Lo zombie è stato colpito!");
                
                    enemy.TakeDamage(damage);
                
            }

            //Debug.Log("nemico colpito, ha perso " + damage + " punti vita e ora ne ha" + enemy.health);
        }
    }

    private IEnumerator HitMarker()
    {
        mirino.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mirino.color = Color.white;
    }
}
