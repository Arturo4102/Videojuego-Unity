using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
   public GameObject loadGameText;

   private void Start()
   {
      loadGameText.SetActive(false);
   }

   public void StartNewGame()
   {
      SceneManager.LoadScene("FirstLevelScene");
   }
   
   public void StartTutorial()
   {
      SceneManager.LoadScene("TutorialScene");
   }
   
   public void StartFirstLevel()
   {
      SceneManager.LoadScene("FirstLevelScene");
   }
   
    public void StartSecondLevel()
    {
      SceneManager.LoadScene("SecondLevelScene");
    }
    
    public void StartThirdLevel()
    {
      SceneManager.LoadScene("ThirdLevelScene");
    }
    
    public void LoadData()
    {
       SaveManager saveManager = new SaveManager();
       PlayerDataModel playerDataModel = saveManager.LoadPlayerData();
       if(playerDataModel == null)
       {
          loadGameText.SetActive(true);
       }
       else
       {
          Time.timeScale = 1f;
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          PlayerPrefs.SetInt("ShouldLoadGame", 1);
          PlayerPrefs.Save();
          SceneManager.LoadScene(playerDataModel.sceneIndex);
       }
       
    }
    
   public void Quit()
   {
      Application.Quit();
   }
}