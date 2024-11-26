using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    
    void FixedUpdate()
    {
        if (GetComponent<EnemyScript>().health <= 0f)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
