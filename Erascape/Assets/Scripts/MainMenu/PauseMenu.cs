using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu, settingMenu, cheatsMenu;
    private GameObject hudGameObject;
    public GameObject questionGameObject;
    private bool activeMenu;
    public AudioMixer audioMixer;
    private Gun gun;
    private bool canOpenMenu = true;
    
    private void Awake()
    {
        activeMenu  = false;
        hudGameObject = GameObject.FindGameObjectWithTag("HUD");
        questionGameObject.SetActive(false);
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        cheatsMenu.SetActive(false);
        gun = FindObjectOfType<Gun>().GetComponent<Gun>();
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    }
    
    private void Update()
    {
        if (canOpenMenu && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            if (!activeMenu)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            
        }

    }
    
    public void Pause()
    {
        gun = FindObjectOfType<Gun>().GetComponent<Gun>();
        pauseMenu.SetActive(true);
        hudGameObject.SetActive(false);
        activeMenu = true;
        Time.timeScale = 0f;
        gun.CanShoot(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        questionGameObject.SetActive(false);
        cheatsMenu.SetActive(false);
        hudGameObject.SetActive(true);
        activeMenu = false;
        Time.timeScale = 1f;
        gun.CanShoot(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadData()
    {
        Time.timeScale = 1f;
        gun.CanShoot(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerPrefs.SetInt("ShouldLoadGame", 1);
        PlayerPrefs.Save();
        SaveManager saveManager = new SaveManager();
        PlayerDataModel playerDataModel = saveManager.LoadPlayerData();
        SceneManager.LoadScene(playerDataModel.sceneIndex);
    }

    public void SaveData(GameManager gameManager)
    {
        gameManager.SaveGame();
    }

    public void QuestionMenu()
    {
        activeMenu = false;
        pauseMenu.SetActive(false);
        questionGameObject.SetActive(true);
    }
    public void BackMenu()
    {
        questionGameObject.SetActive(false);
        activeMenu = true;
        pauseMenu.SetActive(true);
    }
    
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void SettingMenu()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void BackSetting()
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void CheatsMenu()
    {
        pauseMenu.SetActive(false);
        cheatsMenu.SetActive(true);
    }
    
    public void SetCanOpenMenu(bool canOpenMenu)
    {
        this.canOpenMenu = canOpenMenu;
    }
}
