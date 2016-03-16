using UnityEngine;

public class WinningScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    private GameObject winCamera;

    public int MoveSpeed;
    private bool win = false;

    private GameObject player;

    private void Awake()
    {
        winCamera.SetActive(false);
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(mainCamera);
            Destroy(player);
            winCamera.SetActive(true);
            win = true;
        }
    }

    private void Update()
    {
        if (win)
        {
            this.gameObject.transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
        }
    }
}