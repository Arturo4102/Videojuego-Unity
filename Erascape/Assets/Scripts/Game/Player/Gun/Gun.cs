using System.Collections;
using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public float maxDistance = 10f;
    public AudioSource audioSource;
    public int maxAmmo = 30;
    public int damage = 10;
    public new ParticleSystem particleSystem;
    private int currentAmmo;
    public int maxMagazineAmmo = 30;
    private bool isReloading = false;
    public TextMeshProUGUI ammoText;
    private bool canShoot = true;
    private Animator parentAnimator;
    public float reloadTime = 1f;
    public float timeBetweenShots = 0.5f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxMagazineAmmo;
        ammoText = GameObject.FindWithTag("AmmoText").GetComponent<TextMeshProUGUI>();
        UpdateAmmoText();
        parentAnimator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (isReloading && !Convert.ToBoolean(PlayerPrefs.GetInt("unlimitedAmmo", 0)))
        {
            ammoText.text = "Ammo: Reloading...";
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();

            if (Convert.ToBoolean(PlayerPrefs.GetInt("unlimitedAmmo", 0)))
            {
                StartCoroutine(BulletShot());
            }
        }
    }

    private IEnumerator Reload()
    {
        if (maxAmmo <= 0)
        {
            yield break;
        }
        
        isReloading = true;
        //Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime); // Tempo di ricarica simulato (default 1 secondo)
        
        currentAmmo = maxMagazineAmmo;
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("unlimitedAmmo", 0)))
        {
            maxAmmo -= maxMagazineAmmo;
        } 
        

        isReloading = false;
        UpdateAmmoText();
        //Debug.Log("Reloaded!");
    }

    private void Shoot()
    {
        
        if (currentAmmo <= 0)
        {
            return;
        }
        
        audioSource.Play();
        particleSystem.Play();
        
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("unlimitedAmmo", 0)))
        {
            currentAmmo--;
        }
        
        UpdateAmmoText();
    }

    public void UpdateAmmoText()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("unlimitedAmmo", 0)))
        {
            ammoText.text = "Ammo: " + currentAmmo + "/∞";
        }
        else
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }
    
    public void CanShoot(bool canShoot)
    {
        // Abilita lo sparo
        this.canShoot = canShoot;
    }
    
    private IEnumerator BulletShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
    
    public void IncreaseAmmo(int ammo)
    {
        if (maxAmmo == 0)
        {
            currentAmmo = maxMagazineAmmo;
        }
        maxAmmo += ammo;
        UpdateAmmoText();
    }
    
    public void SetCurrentAmmo(int ammo)
    {
        currentAmmo = ammo;
        UpdateAmmoText();
    }
    
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
    
    public void SetMaxAmmo(int ammo)
    {
        maxAmmo = ammo;
        UpdateAmmoText();
    }
    
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
