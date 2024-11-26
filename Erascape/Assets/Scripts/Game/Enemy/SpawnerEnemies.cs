using System.Collections;
using Game.Enemy;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;  // Il prefab del nemico da spawnare
    public float spawnInterval = 2f;  // Intervallo di tempo tra ogni spawn
    public int maxEnemies;  // Numero massimo di nemici da spawnare
    private int spawnedEnemies = 0;  // Numero di nemici spawnati finora
    private int enemiesKilled = 0;  // Numero di nemici uccisi finora
    
    private void Start()
    {
        // Avvia la coroutine per lo spawn dei nemici
        spawnedEnemies = enemiesKilled;
        if(spawnedEnemies < maxEnemies)
            StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);
        // se il numero di nemici uccisi + il numero di nemici spawnati è minore del numero massimo di nemici, spawna un nemico
        while (spawnedEnemies < maxEnemies)
        {
            // Crea una copia del prefab del nemico nella posizione dello spawner
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyScript>().SetSpawnerName(gameObject.name);
            // Incrementa il contatore dei nemici spawnati
            spawnedEnemies++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void IncrementEnemiesKilled()
    {
        enemiesKilled++;
    }

    public void SetEnemiesKilled(int enemiesKilled)
    {
        this.enemiesKilled = enemiesKilled;
        spawnedEnemies = enemiesKilled;
    }
    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }
    
    public void ShouldDisableSpawner()
    {
        if (spawnedEnemies >= maxEnemies)
        {
            gameObject.SetActive(false);
        }
    }
}

