//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinningScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    private GameObject winCamera;

    [SerializeField]
    private GameObject HUD;

    [SerializeField]
    private GameObject CrossHair;

    [SerializeField]
    private GameObject SpaceShipClosed;

    [SerializeField]
    private float shipBoostTimer;

    public int MoveSpeed;
    
    private bool win = false;
    private GameObject player;


    private void Awake()
    {
        winCamera.SetActive(false);
        player = GameObject.Find("Player");
    }

    private IEnumerator creditDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(mainCamera);
            Destroy(player);
            winCamera.SetActive(true);
            win = true;
            StartCoroutine(creditDelay());
            CrossHair.SetActive(false);
            HUD.SetActive(false);
        }
    }

    private void Update()
    {
        if (win)
        {
            GetComponent<MeshRenderer>().enabled = false;
            SpaceShipClosed.SetActive(true);
            winCamera.GetComponent<LookAt>().enabled = true;
            SpaceShipClosed.transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
            StartCoroutine(shipSpeed(shipBoostTimer));
        }

    }
    IEnumerator shipSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        MoveSpeed += 5;
        StartCoroutine(shipSpeed(shipBoostTimer));
    }
}