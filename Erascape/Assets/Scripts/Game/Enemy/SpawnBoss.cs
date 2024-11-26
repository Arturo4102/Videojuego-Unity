using UnityEngine;

namespace Game.Other
{
    public class SpawnBoss : MonoBehaviour
    {
        public GameObject boss;
        public DoorBehavior door;
        public GameObject[] lasers;
        public GameObject panel;
        void Start()
        {
            foreach (var laser in lasers)
            {
                laser.SetActive(false);
            }
            boss.SetActive(false);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && door.IsDoorDefaultPosition())
            {
                foreach (var laser in lasers)
                {
                    laser.SetActive(true);
                }
                boss.SetActive(true);
                gameObject.SetActive(false);
                door.GetComponent<BoxCollider>().enabled = false;
                panel.SetActive(false);
            }
        }
    }
}