using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] players;
    private Vector3 pos;
    private Camera cam;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        pos = cam.WorldToViewportPoint(players[0].position);
        var camTrans = cam.transform;
        if (pos.y > 0.8)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 0.1f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
        else if (pos.y < 0.2)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y - 0.1f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
	}
}
