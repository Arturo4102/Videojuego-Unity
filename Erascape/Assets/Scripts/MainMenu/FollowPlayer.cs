    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject minimap;
    private GameObject player;
    private void Awake()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        minimap.transform.position.Set(player.transform.position.x, player.transform.position.y + 3f, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        minimap.transform.position.Set(player.transform.position.x, player.transform.position.y + 3f, player.transform.position.z);
    }
}
