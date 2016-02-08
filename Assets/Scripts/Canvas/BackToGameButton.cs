using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToGameButton : MonoBehaviour {

    public void OnClick_BackToGame()
    {
        SceneManager.LoadScene(0);
    }
}
