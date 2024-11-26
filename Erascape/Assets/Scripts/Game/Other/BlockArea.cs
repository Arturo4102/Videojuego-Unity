using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockArea : MonoBehaviour
{
    public PlayerController player;
    public int enemiesToDefeat;
    public TextMeshProUGUI enemiesDefeatedText;
    public int totalEnemiesArea;
    private int enemiesDefeated;
    public int areaNumber;
    public GameObject nextWall;
    public bool shouldActivateNextWall = true;

    private void Start()
    {
        GetCurrentEnemiesRemaining();
    }
    private void FixedUpdate()
    {
        if (enemiesDefeated == enemiesToDefeat)
        {
            if (shouldActivateNextWall)
            {
                nextWall.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        GetCurrentEnemiesRemaining();
        enemiesDefeatedText.text = "Enemies remaining in area "+areaNumber+": " + enemiesDefeated + "/" + enemiesToDefeat;
    }

    private void GetCurrentEnemiesRemaining()
    {
        int enemiesDefeatedBeforeArea = totalEnemiesArea - enemiesToDefeat;
        int enemiesDefeatedInCurrentArea = Mathf.Max(player.GetEnemiesDefeated() - enemiesDefeatedBeforeArea, 0);

        enemiesDefeated = Mathf.Min(enemiesDefeatedInCurrentArea, enemiesToDefeat);
    }
    
    public bool IsBlocked()
    {
        return gameObject.activeSelf;
    }
}