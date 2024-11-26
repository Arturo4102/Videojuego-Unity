using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaWallScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public GameObject panel;
    public string defaultMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            textObject.gameObject.SetActive(true);
            ShowMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            textObject.gameObject.SetActive(false);
        }
    }

    private void ShowMessage()
    {
        textObject.text = defaultMessage;
    }

}
