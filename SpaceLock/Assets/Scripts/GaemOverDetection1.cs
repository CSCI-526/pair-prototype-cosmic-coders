using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GaemOverDetection1 : MonoBehaviour {
    public GameObject player;
    public GameObject Cube2;
    public Image gameOverImage;
    public TextMeshProUGUI numberGrapples;

    void Start() {

        if (player.GetComponent<Collider>() == null || Cube2.GetComponent<Collider>() == null)
        {
            Debug.LogError("Player and Cube2 must both have colliders.");
        }

        if (player.GetComponent<Rigidbody>() == null && Cube2.GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("At least one of the objects must have a Rigidbody.");
        }

        player.GetComponent<Collider>().isTrigger = true;
        Cube2.GetComponent<Collider>().isTrigger = true;

        gameOverImage.enabled = false;
    }

    void Update() {
        if (player.transform.position.x > 60) {
            Debug.Log("Player's position.x is greater than 60.");
            gameOverImage.enabled = true;
            numberGrapples.enabled = false;
            Invoke("RestartGame", 2f);
        }
    }

    void RestartGame() {
        Debug.Log("RestartGame called.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
