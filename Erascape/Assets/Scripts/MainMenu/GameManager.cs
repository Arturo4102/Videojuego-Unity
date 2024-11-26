using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject; // Riferimento al GameObject del player
    public SpawnerEnemies[] spawners; // Riferimento ai GameObject degli spawner
    public GameObject[] gunsToSwap; // Riferimento ai GameObject delle armi da sostituire
    public NpcScript[] npcs; // Riferimento ai GameObject degli npc
    public BlockArea[] blockAreas; // Riferimento ai GameObject dei muri delle aree bloccate
    private SaveManager saveManager; // Riferimento al componente per il salvataggio dei dati
    private bool shouldLoadGame = false;
    public GameObject saveGameImage;
    private Boolean isFirstLoad = true;
    
    private void Start()
    {
        Time.timeScale = 1f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        saveManager = new SaveManager();
        playerObject.GetComponent<PlayerController>().SetEndLevel(false);
        
        if (PlayerPrefs.HasKey("ShouldLoadGame"))
        {
            int shouldLoad = PlayerPrefs.GetInt("ShouldLoadGame");
            shouldLoadGame = shouldLoad == 1;
        }
        
        if (shouldLoadGame)
        {
            playerObject.SetActive(false);
            LoadGame(); // Carica il salvataggio del gioco
            playerObject.SetActive(true);
        }
        else
        {
            SaveGame();
        }
        isFirstLoad = false;
    }

    public void LoadGame()
    {
        PlayerDataModel playerDataModel = saveManager.LoadPlayerData(); // Carica i dati del giocatore salvati
        
        // Assegna i dati del giocatore al player e alla barra della vita
        PlayerController playerController = playerObject.GetComponent<PlayerController>();
        playerController.SetCurrentGunName(playerDataModel.currentGunName);
        Gun currentGun = SwapGun(playerDataModel.currentGunName);
        playerController.UpdatePlayerPosition(playerDataModel.playerPosition);
        playerController.UpdatePlayerRotation(playerDataModel.playerRotation);
        playerController.UpdatePlayerLife(playerDataModel.life);
        playerController.SetEnemiesKilled(playerDataModel.enemyKilled);
        playerController.SetKeysCollected(playerDataModel.keysCollected);
        playerController.SetShowHint(false);
        if (gunsToSwap != null)
        {
            currentGun.SetMaxAmmo(playerDataModel.currentMaxAmmo);
            currentGun.SetCurrentAmmo(playerDataModel.currentMagazine);
        }
        
        EnemyDataModel[] enemyDataModels = playerDataModel.enemyDataModel;
        SetupSpawners(enemyDataModels);
        NpcDataModel[] npcDataModels = playerDataModel.npcDataModel;
        SetupNpcs(npcDataModels);
        BlockAreaModel[] blockAreaDataModels = playerDataModel.blockAreaModel;
        SetupBlockAreas(blockAreaDataModels);
        PlayerPrefs.SetInt("ShouldLoadGame", 0);
        PlayerPrefs.Save();
        
    }
    
    public void SaveGame()
    {
        if(!isFirstLoad)
            StartCoroutine(ShowSaveGameMessage());
        saveManager.SavePlayerData(spawners, npcs, blockAreas); // Salva i dati del giocatore
    }
    
    private IEnumerator ShowSaveGameMessage()
    {
        Time.timeScale = 0.00001f;
        saveGameImage.SetActive(true);
        yield return new WaitForSeconds(0.00001f);
        saveGameImage.SetActive(false);
        if(Time.timeScale == 0.00001f)
            Time.timeScale = 0f;
    }

    public void SetupSpawners(EnemyDataModel[] enemyDataModels)
    {
        foreach (var enemyDataModel in enemyDataModels)
        {
            foreach (var spawner in spawners)
            {
                if (spawner.name == enemyDataModel.spawnerName)
                {
                    spawner.GetComponent<SpawnerEnemies>().SetEnemiesKilled(enemyDataModel.enemiesKilledInSpawner);
                    spawner.GetComponent<SpawnerEnemies>().ShouldDisableSpawner();
                }
                
            }
        }
    }
    
    public void SetupNpcs(NpcDataModel[] npcDataModels)
    {
        int i = 0;
        foreach (var npcDataModel in npcDataModels)
        {
            npcs[i].SetFirstInteraction(npcDataModel.isFirstInteraction);
            i++;
        }
    }
    
    public void SetupBlockAreas(BlockAreaModel[] blockAreaDataModels)
    {
        int i = 0;
        foreach (var blockAreaDataModel in blockAreaDataModels)
        {
            blockAreas[i].gameObject.SetActive(blockAreaDataModel.isBlocked);
            i++;
        }
    }
    
    public Gun SwapGun(string gunName)
    {
        foreach (var gun in gunsToSwap)
        {
            gun.SetActive(gun.name == gunName);
            if (gun.activeSelf)
            {
                return gun.GetComponent<Gun>();
            }
        }

        return null;
    }
    
}
