using Game.Other;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game.Enemy
{
    public class EnemyScript : MonoBehaviour
    {
        private Transform playerTransform; // Riferimento al Transform del giocatore
        private NavMeshAgent zombieAgent; // Riferimento al componente NavMeshAgent dello zombie
        public AudioSource audioSource;
        public AudioClip enemyDeathSound;
        public float detectionRange; // Distanza di rilevamento per iniziare l'inseguimento
        private bool isDetected = false; // Flag per indicare se lo zombie ha rilevato il giocatore
        private LifeBars playerLifeBars; // Riferimento allo script LifeBars del giocatore
        public float health = 100f; // Vita dello zombie
        public float maxHealth; // Vita massima dello zombie
        private Animator animator; // Riferimento all'Animator dello zombie
        public TextMeshPro damageText;
        private new Collider collider; // Riferimento al collider dello zombie
        private int rotationSpeed = 5; // Velocità di rotazione dello zombie
        public GameObject enemyGun;
        public string spawnerName = "none";
        
        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Trova il giocatore e ottieni il riferimento al Transform
            animator = GetComponent<Animator>(); // Ottieni il riferimento all'Animator
            zombieAgent = GetComponent<NavMeshAgent>(); // Ottieni il riferimento al componente NavMeshAgent
            zombieAgent.enabled = false; // Disabilita il NavMeshAgent all'avvio per impedire lo spostamento iniziale dello zombie
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            ShowDamageText(health);
            playerLifeBars = FindObjectOfType<LifeBars>(); // Trova l'oggetto con lo script LifeBars e ottieni il riferimento
            collider = GetComponent<Collider>(); // Ottieni il riferimento al collider dello zombie
        }

        private void Update()
        {
            // Calcola la direzione dallo zombie al giocatore
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f;

            // Ruota lo zombie verso il giocatore
            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            // Calcola la distanza tra lo zombie e il giocatore
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer > detectionRange)
            {
                animator.SetBool("isWalking", false); // Avvia l'animazione di idle
                zombieAgent.enabled = false; // Disabilita il NavMeshAgent se il giocatore è fuori dalla distanza di rilevamento
                isDetected = false;
            }

            // Se il giocatore è entro la distanza di rilevamento, avvia l'inseguimento
            if (distanceToPlayer <= detectionRange)
            {
                animator.SetBool("isWalking", true); // Avvia l'animazione di camminata
                if (!isDetected && !animator.GetBool("isDead"))
                {
                    audioSource.loop = false;
                    audioSource.Stop();
                    audioSource.Play();
                    isDetected = true;
                }
                zombieAgent.enabled = true; // Abilita il NavMeshAgent per permettere allo zombie di muoversi
                
                zombieAgent.SetDestination(playerTransform.position);  // Imposta il punto di destinazione del NavMeshAgent al giocatore
            }

            if (distanceToPlayer <= zombieAgent.stoppingDistance)
            {
                //Debug.Log("Lo zombie è vicino al giocatore!");
                zombieAgent.enabled = false;
                animator.SetBool("isNear", true); // Avvia l'animazione di attacco
            }
            else
            {
                animator.SetBool("isNear", false); // Disattiva l'animazione di attacco
            }

        }

        public void TakeDamage(int damage)
        {
            
            health -= damage;
            
            if (health <= 0 && !animator.GetBool("isDead"))
            {
                collider.enabled = false;
                zombieAgent.enabled = false;
                isDetected = false;
                damageText.text = "";
                animator.SetBool("isWalking", false);
                animator.SetBool("isDead", true); // Avvia l'animazione di morte
                animator.Play("Death");
                audioSource.PlayOneShot(enemyDeathSound);
                // Aggiungi 1 al contatore di nemici sconfitti
                enemyGun.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnemyDefeated();
                if(!gameObject.CompareTag("Boss"))
                    GameObject.Find(spawnerName).GetComponent<SpawnerEnemies>().IncrementEnemiesKilled();
            } else {
                if(animator.GetBool("isDead")) return;
                ShowDamageText(health);
            }
        }

        /**
         * Used by the animation event to disable the NavMeshAgent when is dead
         */
        public void StopMovementAnimation()
        {
            // Disabilita il NavMeshAgent per impedire lo spostamento dello zombie durante l'animazione
            zombieAgent.enabled = false;
        }

        /**
         * Used by the animation event to destroy the enemy object
         */
        public void OnDeadAnimation()
        {
            // Distruggi l'oggetto "enemy" colpito
            Destroy(gameObject); 
        }
    
        private void ShowDamageText(float health)
        {
            damageText.text = health.ToString();
            // Posiziona il testo vicino al nemico come preferisci
            damageText.transform.position = transform.position + Vector3.up * 2f;
        }
        
        public bool GetDetected()
        {
            return isDetected;
        }

        public void SetSpawnerName(string name)
        {
            spawnerName = name;
        }
        
        public string GetSpawnerName()
        {
            return spawnerName;
        }
    }
}
