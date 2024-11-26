using UnityEngine;

namespace Game.Other
{
    public class NextScene : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.GetComponent<PlayerController>().CanChangeLevel())
            {
                LevelManager levelManager = new LevelManager();
                levelManager.NextLevel();
            }
        }
    }
}