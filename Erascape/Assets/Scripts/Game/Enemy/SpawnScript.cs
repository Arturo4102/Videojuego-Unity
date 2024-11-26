using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnScript : MonoBehaviour
{
    public GameObject enemies;
    public GameObject panel;
    public TextMeshProUGUI enemiesSpawnedText;
    public bool showMessage = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemies.SetActive(true);
            if (showMessage)
            {
                panel.SetActive(true);
                enemiesSpawnedText.gameObject.SetActive(true);
            }
            
            StartCoroutine(Wait());
        }
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        if (showMessage)
        {
            panel.SetActive(false);
            enemiesSpawnedText.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
