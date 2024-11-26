using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class EnemyGun : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip hitSound;
        public new ParticleSystem particleSystem;
        public int damage = 10;
        public float bulletSpeed = 10f;
        private EnemyScript enemy; // Riferimento all'enemy script
        private int shouldShoot = 0;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            particleSystem = GetComponentInChildren<ParticleSystem>();
            enemy = GetComponentInParent<EnemyScript>(); // Trova il player nella scena
        }

        private void FixedUpdate()
        {
            shouldShoot++;
            if ( (shouldShoot%120==0) && enemy.GetDetected())
            {
                shouldShoot = 0;
                Shoot();
            }
        }

        private void Shoot()
        {
            audioSource.PlayOneShot(hitSound);
            particleSystem.Play();
        }
    }
}
