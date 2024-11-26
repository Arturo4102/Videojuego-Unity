using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTransitionScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private MainMenu mainMenu;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
        mainMenu = GetComponentInParent<MainMenu>();
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        mainMenu.StartNewGame();
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
