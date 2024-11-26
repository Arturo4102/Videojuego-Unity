using UnityEngine;

public class NpcTrigger : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
    
    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }
}
