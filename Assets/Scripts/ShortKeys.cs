using UnityEngine;
using UnityEngine.SceneManagement;

public class ShortKeys : MonoBehaviour
{
    [SerializeField]
    private GameTime gametime;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gametime.PlayTime == 0)
                gametime.PlayTime = 1;
            else if (gametime.PlayTime == 1)
                gametime.PlayTime = 0;
            
        }
    }
}