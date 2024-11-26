using TMPro;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public GameObject panel;
    public GameObject player;
    public NpcTrigger npcTriggered;
    public string[] messages;
    private int currentIndex = 0;
    private bool isFirstInteraction = true;
    public bool handleFirstInteraction = true;
    public bool handleKeyCollecting = true;
    public string defaultMessage;
    public bool isWall = false;
    private void Update()
    {
        if (npcTriggered.IsPlayerInRange() && Input.GetKeyDown(KeyCode.E) && isFirstInteraction)
        {
            ShowNextMessage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentIndex = 0;
            panel.SetActive(true);
            textObject.gameObject.SetActive(true);

            if (!isFirstInteraction && handleFirstInteraction)
            {
                ShowDefaultMessage();
            }
            else
            {
                textObject.text = messages[currentIndex];
                currentIndex++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            textObject.gameObject.SetActive(false);
            currentIndex = 0;
        }
    }

    private void ShowNextMessage()
    {
        if (messages.Length == 0)
        {
            return;
        }
        
        // skippa il messaggio invisibile
        if (messages[currentIndex].Equals(" "))
        {
            currentIndex = (currentIndex + 1) % messages.Length;
        }
        textObject.text = messages[currentIndex];
        currentIndex = (currentIndex + 1) % messages.Length;
        // Debug.Log(currentIndex);
        
        if (currentIndex == 0 && handleKeyCollecting)
        {
            isFirstInteraction = false;
            player.GetComponent<PlayerController>().IncreaseKeysCollected();
        }
    }
    
    private void ShowDefaultMessage()
    {
        textObject.text = defaultMessage;
    }

    public bool IsFirstInteraction()
    {
        return isFirstInteraction;
    }

    public void SetFirstInteraction(bool value)
    {
        isFirstInteraction = value;
    }
    
    public int getCurrentIndex()
    {
        return currentIndex % messages.Length;
    }
}