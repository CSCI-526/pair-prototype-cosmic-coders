using UnityEngine;

public class ObstacleSpeed : MonoBehaviour
{
    private float minSpeed = 1f;
    private float speed;
    private GameObject player;
    public Material farObstacle;
    public Material nearObstacle;
    public float thresholdDistance = 30f ;

    void Start()
    {

      player = GameObject.FindWithTag("Player");
      

      Vector3 scale = transform.localScale;

      float scaleFactor = (scale.x + scale.y + scale.z) / 3f;

      speed =  minSpeed * scaleFactor;
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
