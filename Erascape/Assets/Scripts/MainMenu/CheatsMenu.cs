using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

public class CheatsMenu : MonoBehaviour
{
   [SerializeField] private PlayerController player;
   [SerializeField] private GameObject cheatsIcon;
   private bool cht1, cht2, cht3;
   public BulletScript[] bullets;
   public Gun[] guns;
   
   private void Awake()
   {
       cheatsIcon.SetActive(false);
       cht1 = cht2 = cht3 = false;
   }

   public void UnlimitedHealth(bool unlimited)
   {
       if (unlimited)
       {
           player.UnlimitedHealth(unlimited);
           cht1 = true;
       }
       else
       {
           player.NormalHealth();
           cht1 = false;
       }

   }
   
   public void UnlimitedAmmo(bool ammo)
   {
       if(ammo){ 
           cht2 = true;
           PlayerPrefs.SetInt("unlimitedAmmo", 1);
           GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>().UpdateAmmoText();
       }
       else
       {
           cht2 = false;
           PlayerPrefs.SetInt("unlimitedAmmo", 0);
           GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>().UpdateAmmoText();
       }
   }
   
   public void InstaKill(bool insta)
   {
       if (insta)
       {
            cht3 = true;
            PlayerPrefs.SetInt("instakill", 1);
       }
       else
       {
            cht3 = false;
            PlayerPrefs.SetInt("instakill", 0);
       }
   }

   public void Update()
   {
       if (!cht1 && !cht2 && !cht3)
       {
           cheatsIcon.SetActive(false);
       }
       else
       {
           cheatsIcon.SetActive(true);
       }
       
   }
}
