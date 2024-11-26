using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveManager
{
    private GameObject player; // Riferimento al player
    private string saveFilePath = Application.persistentDataPath + "/SaveDataFile.json";
    private GameObject[] npcs;
    public void SavePlayerData(SpawnerEnemies[] spawners, NpcScript[] npcs, BlockArea[] areas)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Crea un oggetto JSON per i dati del giocatore
        PlayerDataModel playerData = new PlayerDataModel();
        playerData.playerPosition = player.GetComponent<PlayerController>().GetPlayerPosition();
        playerData.playerRotation = player.GetComponent<PlayerController>().GetPlayerRotation();
        playerData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerData.life = (int) player.GetComponent<PlayerController>().GetPlayerLife();
        playerData.enemyKilled = player.GetComponent<PlayerController>().GetEnemiesDefeated();
        playerData.keysCollected = player.GetComponent<PlayerController>().GetKeysCollected();
        playerData.currentMaxAmmo = player.GetComponent<PlayerController>().GetTotalAmmo();
        playerData.currentMagazine = player.GetComponent<PlayerController>().GetMagazineAmmo();
        playerData.currentGunName = player.GetComponent<PlayerController>().GetCurrentGunName();
        playerData.showHint = player.GetComponent<PlayerController>().GetShowHint();
        playerData.enemyDataModel = EnemyDataModel.createArrayFromSpawners(spawners);
        playerData.npcDataModel = NpcDataModel.createArrayFromNpcs(npcs);
        if(SceneManager.GetActiveScene().buildIndex == 1)
            playerData.blockAreaModel = BlockAreaModel.createArrayFromAreas(areas);
        // Serializza i dati del giocatore in formato JSON
        string json = JsonUtility.ToJson(playerData);
        //Debug.Log(saveFilePath);
        // Salva i dati del giocatore su file
        File.WriteAllText(saveFilePath, json);
    }

    public PlayerDataModel LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            // Carica i dati del giocatore dal file JSON
            string json = File.ReadAllText(saveFilePath);
            PlayerDataModel playerData = JsonUtility.FromJson<PlayerDataModel>(json);

            //Debug.Log(json);
            return playerData;
        }
        //Debug.LogError("File di salvataggio non trovato!");
        return null;
    }
}

[Serializable]
public class PlayerDataModel
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int sceneIndex;
    public int life;
    public string currentGunName;
    public int enemyKilled;
    public int keysCollected;
    public int currentMaxAmmo;
    public int currentMagazine;
    public bool showHint;
    public EnemyDataModel[] enemyDataModel; //spawner nella scena
    public NpcDataModel[] npcDataModel;
    public BlockAreaModel[] blockAreaModel;
}

[Serializable]
public class EnemyDataModel
{
    public String spawnerName;
    public int enemiesKilledInSpawner;

    public static EnemyDataModel[] createArrayFromSpawners(SpawnerEnemies[] spawners)
    {
        EnemyDataModel[] enemyModelArray = new EnemyDataModel[spawners.Length];
        foreach (var spawner in spawners)
        {
            EnemyDataModel enemyDataModel = new EnemyDataModel();
            enemyDataModel.spawnerName = spawner.name;
            enemyDataModel.enemiesKilledInSpawner = spawner.GetEnemiesKilled();
            enemyModelArray.SetValue(enemyDataModel, Array.IndexOf(spawners, spawner));
        }
        return enemyModelArray;
    }
}

[Serializable]
public class NpcDataModel
{
    public String npcName;
    public bool isFirstInteraction;
    
    public static NpcDataModel[] createArrayFromNpcs(NpcScript[] npcs)
    {
        NpcDataModel[] npcModelArray = new NpcDataModel[npcs.Length];
        foreach (var npc in npcs)
        {
            NpcDataModel npcDataModel = new NpcDataModel();
            npcDataModel.npcName = npc.name;
            npcDataModel.isFirstInteraction = npc.IsFirstInteraction();
            npcModelArray.SetValue(npcDataModel, Array.IndexOf(npcs, npc));
        }
        return npcModelArray;
    }
}

[Serializable]
public class BlockAreaModel
{
    public string areaName;
    public bool isBlocked;
    
    public static BlockAreaModel[] createArrayFromAreas(BlockArea[] areas)
    {
        BlockAreaModel[] areaBlockModelArray = new BlockAreaModel[areas.Length];
        foreach (var area in areas)
        {
            BlockAreaModel blockAreaModel = new BlockAreaModel();
            blockAreaModel.areaName = area.name;
            blockAreaModel.isBlocked = area.IsBlocked();
            areaBlockModelArray.SetValue(blockAreaModel, Array.IndexOf(areas, area));
        }
        return areaBlockModelArray;
    }
}
