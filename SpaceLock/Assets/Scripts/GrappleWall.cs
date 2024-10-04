using UnityEngine;

public class GrappleWall : MonoBehaviour {
    public GameObject player;
    public GameObject Cube3;
    public Material inRangematerial;
    public Material outRangematerial;

    void Update() {
        float distance = Vector3.Distance(player.transform.position, Cube3.transform.position);
        if (distance <= 30f) {
            Cube3.GetComponent<Renderer>().material = inRangematerial;
        } else {

            Cube3.GetComponent<Renderer>().material = outRangematerial;
        }
    }
}
