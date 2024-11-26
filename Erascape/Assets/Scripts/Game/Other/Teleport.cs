using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject doorText; // canvas della porta
    private bool IsPlayerNearDoor;
    public GameObject playerChar; // GameObject del player
    public Transform player, destination; // Destinazione del player
    public GameObject panel; // Pannello che indica che il player può interagire con la porta
    
    private void Awake()
    {
        panel.gameObject.SetActive(false);
        IsPlayerNearDoor = false;
        doorText.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
        if (gameObject.name == "Esterno Armeria")
        {
            gameObject.SetActive(true);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearDoor)
        {
            // Disattivo il player, lo teletrasporto e lo riattivo, altrimenti unity non riesce a gestire il teletrasporto 1(GODO)
            playerChar.SetActive(false);
            player.transform.position = destination.transform.position;
            playerChar.SetActive(true);
            panel.gameObject.SetActive(false);
            doorText.gameObject.SetActive(false);
            IsPlayerNearDoor = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.gameObject.SetActive(true);
            doorText.gameObject.SetActive(true);
            IsPlayerNearDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.gameObject.SetActive(false);
            doorText.gameObject.SetActive(false);
            IsPlayerNearDoor = false;
        }
    }
}