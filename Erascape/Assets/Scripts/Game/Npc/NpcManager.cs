using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public NpcScript npcScript;
    public GameObject oldWeapon;
    public GameObject newWeapon;
    public GameObject spawner;
    public GameObject[] internalTeleports;
    public GameObject[] externalTeleports;
    private Boolean isUnlocked  = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!npcScript.IsFirstInteraction() && !isUnlocked)
        {
            oldWeapon.SetActive(false);
            newWeapon.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetCurrentGunName(newWeapon.name);
            spawner.SetActive(true);
            ActivateTeleports();
            isUnlocked = true;
        }
    }

    private void ActivateTeleports()
    {
        if (externalTeleports.Length > 0 && internalTeleports.Length > 0)
        {
            foreach (GameObject teleport in internalTeleports)
            {
                teleport.SetActive(true);
            }
            foreach (GameObject teleport in externalTeleports)
            {
                teleport.SetActive(true);
            }
        }
        
    }
}