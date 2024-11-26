using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Other
{
    public class DoorBehavior : MonoBehaviour
    {
        private bool isKeyPressed; // Flag per indicare se il player ha premuto il tasto E
        private bool isDoorUp; // Flag per indicare se la porta è alzata o abbassata
        public GameObject doorText; // canvas della porta
        public AudioSource doorAudioSource;
        private Rigidbody rb;
        private bool isDoorMovingUp = false;
        private bool isDoorMovingDown = false;
        private Vector3 initialPosition;
        public float maxLiftHeight = 4f;
        public float liftSpeed = 2f;
        public TextMeshProUGUI doorLockedText;
        public GameObject panel;
        private Boolean IsDoorUnlocked;
        private Boolean IsPlayerNearDoor;
        private int enemiesDefeated;
        private int enemiesToDefeat;
        private bool lockDoor = false;
        private void Awake()
        {
            IsDoorUnlocked = false;
            IsPlayerNearDoor = false;
            panel.gameObject.SetActive(false);
            doorText.gameObject.SetActive(false);
            doorLockedText.gameObject.SetActive(false);
        }

        private void Start()
        {
            doorAudioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
            initialPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                panel.gameObject.SetActive(true);
                doorText.gameObject.SetActive(true);
                isKeyPressed = false;
                isDoorUp = false;
                IsPlayerNearDoor = true;
                enemiesDefeated = other.GetComponent<PlayerController>().GetEnemiesDefeated();
                enemiesToDefeat = other.GetComponent<PlayerController>().GetEnemiesToDefeat();
                // Debug.Log("Stampo la canvas");
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (lockDoor)
            {
                panel.gameObject.SetActive(true);
                doorText.SetActive(true);
            }
            
            if (other.CompareTag("Player") && isKeyPressed && !isDoorUp && !lockDoor)
            {
                // Debug.Log("Alzo la porta");
                panel.gameObject.SetActive(false);
                doorText.SetActive(false);
                isKeyPressed = true;
                RaiseDoor(other);
            }
        }   
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isDoorMovingUp)
                {
                    isDoorMovingDown = false;
                    isDoorMovingUp = false;
                }
                panel.gameObject.SetActive(false);
                doorText.gameObject.SetActive(false);
                doorLockedText.gameObject.SetActive(false);
                LowerDoor(other);
                IsPlayerNearDoor = false;
            }
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !isKeyPressed && IsPlayerNearDoor)
            {
                IsDoorUnlocked = enemiesDefeated >= enemiesToDefeat || !gameObject.CompareTag("BossDoor");
                if (gameObject.CompareTag("BossDoor") && !IsDoorUnlocked)
                {
                    doorText.gameObject.SetActive(false);
                
                    panel.gameObject.SetActive(true);
                    doorLockedText.gameObject.SetActive(true);
                }
                else
                {
                    isKeyPressed = true;
                }
        
            }
        
        }
        private void FixedUpdate()
        {
            if (isDoorMovingUp)
            {
                // Calcola l'altezza target
                float targetHeight = initialPosition.y + maxLiftHeight;
        
                // Alza gradualmente la porta verso l'altezza target
                if (transform.position.y < targetHeight)
                {
                    float newYPosition = Mathf.MoveTowards(transform.position.y, targetHeight, liftSpeed * Time.fixedDeltaTime);
                    Vector3 newPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
                    rb.MovePosition(newPosition);
                }
                else
                {
                    isDoorMovingUp = false;
                }
            }
            else if (isDoorMovingDown)
            {
                // Abbassa gradualmente la porta verso l'altezza iniziale
                if (transform.position.y > initialPosition.y)
                {
                    float newYPosition = Mathf.MoveTowards(transform.position.y, initialPosition.y, liftSpeed * Time.fixedDeltaTime);
                    Vector3 newPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
                    rb.MovePosition(newPosition);
                }
                else
                {
                    isDoorMovingDown = false;
                }
            }
        }

        private void RaiseDoor(Collider other)
        {
            // Verifica se la porta è già alzata o in movimento
            if (!isDoorMovingUp && !isDoorMovingDown && transform.position.y < initialPosition.y + maxLiftHeight)
            {
                isDoorMovingUp = true;
                ReproduceAudio();        
            }
        }

        private void LowerDoor(Collider other)
        {
            // Verifica se la porta è abbassata o in movimento
            if (!isDoorMovingDown && !isDoorMovingUp && transform.position.y > initialPosition.y)
            {
                isDoorMovingDown = true;
                ReproduceAudio();
            }
            
        }
    
        private void ReproduceAudio()
        {
            doorAudioSource.Stop();
            doorAudioSource.Play();
        }
        
        public bool IsDoorDefaultPosition()
        {
            return transform.position.y == initialPosition.y;
        }
        
        public void LockDoor(bool lockDoor)
        {
            this.lockDoor = lockDoor;
        }
    }
}
