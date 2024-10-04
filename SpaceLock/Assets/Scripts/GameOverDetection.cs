using UnityEngine;
using UnityEngine.UI;

public class GameOverDetection : MonoBehaviour {
    public GameObject player;
    public GameObject Cube3;
    public Image gameOverImage;

    void Start() {
        // Check for colliders
        if (player.GetComponent<Collider>() == null || Cube3.GetComponent<Collider>() == null)
        {
            Debug.LogError("Both player and Cube3 must have colliders.");
        }

        // Ensure at least one object has a Rigidbody
        if (player.GetComponent<Rigidbody>() == null && Cube3.GetComponent<Rigidbody>() == null)
        {
            Debug.LogError("At least one of the objects must have a Rigidbody.");
        }

        // Ensure isTrigger is false for both colliders to allow OnCollisionEnter to be called
        player.GetComponent<Collider>().isTrigger = false;
        Cube3.GetComponent<Collider>().isTrigger = false;

        gameOverImage.enabled = false;
    }

    void Update() {
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("OnCollisionEnter called.");
        // Check if the collision involves the player or Cube3
        if (collision.gameObject == player || collision.gameObject == Cube3)
        {
            Debug.Log("Collision detected between player and Cube3.");
            gameOverImage.enabled = true;
        }
        else
        {
            Debug.Log("Collision with an unrelated object.");
        }
    }
}
