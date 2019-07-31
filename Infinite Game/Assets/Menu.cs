﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject creditsPopUp;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadGameScene()
    {
        print("ansdhbdvsf");
        SceneManager.LoadScene("Game");
    }

    public void gameExit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void creditsButton()
    {
        creditsPopUp.SetActive(true);
    }

    public void creditsButton(bool b)
    {
        creditsPopUp.SetActive(b);
    }


}
