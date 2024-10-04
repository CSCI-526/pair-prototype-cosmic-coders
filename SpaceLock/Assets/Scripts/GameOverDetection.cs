using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverDetection : MonoBehaviour {
    public GameObject player;
    public GameObject Cube3;
    public Image gameOverImage;
    public TextMeshProUGUI numberGrapples;
    // public Button restartButton;
    void Start() {
        if (player.GetComponent<Collider>() == null || Cube3.GetComponent<Collider>() == null)
        {
            Debug.LogError("Player, Cube3, and backWall must all have colliders.");
        }

        if (player.GetComponent<Rigidbody>() == null && Cube3.GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("At least one of the objects must have a Rigidbody.");
        }

        player.GetComponent<Collider>().isTrigger = true;
        Cube3.GetComponent<Collider>().isTrigger = true;
        // restartButton.gameObject.SetActive(false);
        gameOverImage.enabled = false;
        // restartButton.onClick.AddListener(RestartGame);
    }

    void Update() {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player || other.gameObject == Cube3)
        {
            Debug.Log("Trigger detected between player and wall.");
            gameOverImage.enabled = true;
            numberGrapples.enabled = false;
            // restartButton.gameObject.SetActive(true);
            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame() {
        Debug.Log("RestartGame called.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
