using System.Collections;
using UnityEngine;

public class Sphinx : MonoBehaviour
{
    public Camera sphinxCamera;
    public Camera mainCamera;
    public NpcScript npcScript;
    private bool isLastMessage = false;
    private int index = 0;
    
    private void Start()
    {
        sphinxCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SphynxCutSceneText());
        }   
    
    }
    private IEnumerator SphynxCutSceneText()
    {
        isLastMessage = npcScript.messages[index].Equals(" ");

        
        do{
            npcScript.panel.SetActive(true);
            npcScript.textObject.gameObject.SetActive(true);
            Time.timeScale = 0;
            mainCamera.enabled = false;
            sphinxCamera.enabled = true;
            index = npcScript.getCurrentIndex();
            // Debug.Log(index);
            yield return null;
            isLastMessage = npcScript.messages[index].Equals(" ");
        } while (!isLastMessage);

        sphinxCamera.enabled = false;
        mainCamera.enabled = true;
        Time.timeScale = 1;
        npcScript.panel.SetActive(false);
        npcScript.textObject.gameObject.SetActive(false);
    }


}
