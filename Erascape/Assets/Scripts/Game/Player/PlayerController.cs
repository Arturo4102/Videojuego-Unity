using System;
using System.Collections;
using Game.Other;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 40f;
    public float MouseSens = 200f;
    private float rotation = 0f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    private float vspeed = 4f;
    private Animator animator;
    public Transform cameraTransform;
    private LifeBars playerLifeBars;
    public AudioClip medkitSound;
    public AudioClip ammoSound;
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    private int enemiesDefeated;
    public int enemiesToDefeat;
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI keysCollectedText;
    public AudioClip enemiesDefeatedSound;
	private bool endLevel = false;
    private int keysCollected = 0;
    public int increaseLife;
    public int increaseAmmo;
    public GameObject portal;
    public TextMeshProUGUI hintText;
    public GameObject panel;
    public String currentGunName;
    public bool showHint = true;

    void Awake()
    {
        audioMixer.SetFloat("Volume", PlayerPrefs.HasKey("Volume") ? PlayerPrefs.GetFloat("Volume") : 0);
        MouseSens = PlayerPrefs.HasKey("Sensitivity") ? PlayerPrefs.GetFloat("Sensitivity") : 250f;
        //Debug.Log("MouseSens: " + MouseSens);

        playerLifeBars = FindObjectOfType<LifeBars>();
        audioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponent<Animator>();
        PlayerPrefs.SetInt("instakill", 0);
        PlayerPrefs.SetInt("unlimitedAmmo", 0);
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        animator.SetBool("isWalking", false);
        portal.SetActive(false);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hzMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vtMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        
        animator.SetBool("isWalking", hzMove != 0 || vtMove != 0);
        Vector3 move = transform.right * hzMove + transform.forward * vtMove;
        
        if (controller.isGrounded)
        {
            if(velocity.y < 0)
                velocity.y = -2f;

            float jump = Input.GetAxis("Jump") * vspeed;
            velocity.y = jump;
        }


        controller.Move(move * (speed * Time.deltaTime));
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
            
        //Player rotation
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);

        //Vertical movement camera
        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);

        if (CanChangeLevel() && !endLevel){
			audioSource.PlayOneShot(enemiesDefeatedSound);
            portal.SetActive(true);
			endLevel = true;
        }

        if (endLevel)
        {
            portal.SetActive(true);
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 2 && showHint)
        {
            showHint = false;
            StartCoroutine(SecondLevelHint());
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Medkit") && playerLifeBars.life < 150)
        {
            audioSource.PlayOneShot(medkitSound);
            other.gameObject.SetActive(false);
            //Aumento vita
            playerLifeBars.IncreaseLife(increaseLife);
        }
        if (other.CompareTag("AmmoPickup"))
        {
            audioSource.PlayOneShot(ammoSound);
            other.gameObject.SetActive(false);
            //Aumento munizioni totali
            FindObjectOfType<Gun>().IncreaseAmmo(increaseAmmo);
        }
    }
    
    public void EnemyDefeated()
    {
        enemiesDefeated++;
        ChangeEnemiesDefeatedText();
    }
    
    public void ChangeEnemiesDefeatedText()
    {
        enemiesDefeatedText.text = "Pinna defeated: " + enemiesDefeated + "/" + enemiesToDefeat;
    }
    
    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }
    
    public int GetEnemiesToDefeat()
    {
        return enemiesToDefeat;
    }
    
    public Vector3 GetPlayerPosition ()
    {
        return transform.position;
    }
    
    public Quaternion GetPlayerRotation ()
    {
        return transform.rotation;
    }
    
    public float GetPlayerLife ()
    {
        return playerLifeBars.life;
    }
    
    public void UpdatePlayerPosition (Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    
    public void UpdatePlayerRotation (Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }
    
    public void UpdatePlayerLife (int newLife)
    {
        playerLifeBars.life = newLife;
    }


    public void UnlimitedHealth(bool unlimited)
    {
        UpdatePlayerLife((int) playerLifeBars.GetLifeMax());
        playerLifeBars.CheatsOn(unlimited);
    }
    
    public void NormalHealth()
    {
        playerLifeBars.CheatsOn(false);
    }
    
    public void IncreaseKeysCollected()
    {
        keysCollected++;
        ChangeTextKeysCollected();
    }
    
    public void ChangeTextKeysCollected()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
            keysCollectedText.text = "Keys collected: " + keysCollected + "/3";
    }
    
    public int GetKeysCollected()
    {
        return keysCollected;
    }
    public bool CanChangeLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        switch (sceneIndex) {
            case 1: return GetEnemiesDefeated() == GetEnemiesToDefeat();
            case 2: return keysCollected == 3;
            default: return false;
        }
    }
    
    public void SetEndLevel(bool endLevel)
    {
        this.endLevel = endLevel;
    }


    private IEnumerator SecondLevelHint()
    {
        panel.SetActive(true);
        hintText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        hintText.gameObject.SetActive(false);
        panel.SetActive(false);
    }
    
    public void SetEnemiesKilled (int enemiesKilled)
    {
        enemiesDefeated = enemiesKilled;
        ChangeEnemiesDefeatedText();
    }
    
    public void SetKeysCollected (int keysCollected)
    {
        this.keysCollected = keysCollected;
        ChangeTextKeysCollected();
    }

    public void SetCurrentGunName(string name)
    {
        currentGunName = name;
    }
    
    public string GetCurrentGunName()
    {
        return currentGunName;
    }

    public int GetMagazineAmmo ()
    {
        return GameObject.Find(currentGunName).GetComponent<Gun>().GetCurrentAmmo();
    }
    
    public int GetTotalAmmo ()
    {
        return GameObject.Find(currentGunName).GetComponent<Gun>().GetMaxAmmo();
    }

    public void SetShowHint(bool show)
    {
        showHint = show;
    }
    
    public bool GetShowHint()
    {
        return showHint;
    }
}
