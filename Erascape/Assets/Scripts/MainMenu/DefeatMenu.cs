using UnityEngine;
using UnityEngine.Audio;

public class DefeatMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject pauseCanvasObject;
    public PlayerController player;
    
    public void Show()
    {
        audioMixer.SetFloat("Volume", -80f);
        Time.timeScale = 0f;
        GameObject gun = GameObject.Find(player.currentGunName);
        gun.GetComponent<Gun>().CanShoot(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
        gameObject.GetComponent<PauseMenu>().SetCanOpenMenu(false);
        pauseCanvasObject.SetActive(false);
    }
}
