using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{

    public Vector3 position;
    public Quaternion rotation;
    public int health;
    public string scene;
    public string time;
}