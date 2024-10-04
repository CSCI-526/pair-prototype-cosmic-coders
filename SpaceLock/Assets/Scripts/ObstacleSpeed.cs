using UnityEngine;

public class ObstacleSpeed : MonoBehaviour
{
    private float minSpeed = 3f;
    private float speed;
    private GameObject player;
    public Material farObstacle;
    public Material nearObstacle;
    public float thresholdDistance = 100f;

    void Start()
    {

      player = GameObject.FindWithTag("Player");
      if (player == null) {
          Debug.LogError("Player1 tag not found! Make sure your player object has the correct tag.");
          return;
      }

      Vector3 scale = transform.localScale;

      float scaleFactor = (scale.x + scale.y + scale.z) / 3f;

      speed =  minSpeed * scaleFactor;

      // Debug.Log("Scale = " + scaleFactor + ", speed = " + speed);
    }

    void Update()
    {
      transform.Translate(speed * Time.deltaTime * Vector3.right);

      float distance = Vector3.Distance(player.transform.position, transform.position);

      if (distance < thresholdDistance) {
          GetComponent<Renderer>().material = nearObstacle;
      } else {
          GetComponent<Renderer>().material = farObstacle;
      }

    }
}
