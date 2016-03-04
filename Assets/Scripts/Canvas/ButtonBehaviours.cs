using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviours : MonoBehaviour
{
    private Transform credits;
    private Transform options;
    private Transform startScreen;

    private GameObject player;
    private GameTime gameTime;

    [Tooltip("Makes the Mouse Cursor invisible if box is checked.")]
    public bool CursorIsVisible = false;

    private void Awake()
    {
        startScreen = transform.FindChild("StartScreen");

        options = startScreen.FindChild("Options");
        options.gameObject.SetActive(false);

        credits = startScreen.FindChild("Credit_Image");
        credits.gameObject.SetActive(false);

        player = GameObject.Find("Player");

        gameTime = GameObject.Find("GlobalScripts").transform.GetComponent<GameTime>();
    }

    // Game Start
    public void OnClick_GameStart()
    {
        startScreen.gameObject.SetActive(false);
        Cursor.visible = CursorIsVisible;
        gameTime.PlayTime = 1;
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

    // Quit
    public void OnClick_Quit()
    {
        Application.Quit();
    }
    // Tutorial
    public void OnClick_Tutorial()
    {
        SceneManager.LoadScene(4);
    }

    // Retry
    public void OnClick_Retry()
    {
        SceneManager.LoadScene(0);
    }
}