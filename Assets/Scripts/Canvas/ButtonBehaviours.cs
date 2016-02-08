﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviours : MonoBehaviour
{
    Transform credits;
    Transform options;
    Transform startScreen;

    GameObject player;

    [Tooltip("Makes the Mouse Cursor invisible if box is checked.")]
    public bool CursorIsVisible = false;

    void Awake()
    {
        startScreen = transform.FindChild("StartScreen");

        options = startScreen.FindChild("Options");
        options.gameObject.SetActive(false);

        credits = startScreen.FindChild("Credit_Image");
        credits.gameObject.SetActive(false);

        player = GameObject.Find("Player");
    }

    // Game Start
    public void OnClick_GameStart()
    {
        startScreen.gameObject.SetActive(false);
        Cursor.visible = CursorIsVisible;
    }
    // Credits
    public void OnClick_Credits()
    {
        credits.gameObject.SetActive(true);
    }
    public void OnClick_Credits_Back()
    {
        credits.gameObject.SetActive(false);
    }
    public void OnClick_Björn()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClick_Basti()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClick_Irina()
    {
        SceneManager.LoadScene(3);
    }
    // Options
    public void OnClick_Options()
    {
        options.gameObject.SetActive(true);
    }
    public void OnClick_Options_Back()
    {
        options.gameObject.SetActive(false);
    }
    // Options - MouseVisible
    public void OnClick_Options_MouseVisible()
    {
        if (CursorIsVisible)
        {
            // text.text bla == CursorIsVisible
        }
    }
    // Retry
    public void OnClick_Retry()
    {
        SceneManager.LoadScene(0);
    }
}
