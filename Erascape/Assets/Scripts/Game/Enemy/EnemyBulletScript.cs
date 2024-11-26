using Game.Other;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyBulletScript : MonoBehaviour
    {
        private LifeBars playerLifeBars;

        private void Start()
        {
            playerLifeBars = FindObjectOfType<LifeBars>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                EnemyGun gun = GetComponentInParent<EnemyGun>();
                playerLifeBars.TakeDamage(gun.damage);
            }
        }
    }
}