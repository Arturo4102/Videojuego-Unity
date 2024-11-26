using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGameVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
