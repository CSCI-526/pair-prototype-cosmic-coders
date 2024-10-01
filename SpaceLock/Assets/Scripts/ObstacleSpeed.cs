using UnityEngine;

public class ObstacleSpeed : MonoBehaviour
{
    private float minSpeed = 3f;
    private float speed;
    void Start()
    {
      Vector3 scale = transform.localScale;

      float scaleFactor = (scale.x + scale.y + scale.z) / 3f;

      speed =  minSpeed * scaleFactor;

      Debug.Log("Scale = " + scaleFactor + ", speed = " + speed);
    }

    void Update()
    {
      transform.Translate(speed * Time.deltaTime * Vector3.right);
    }
}
