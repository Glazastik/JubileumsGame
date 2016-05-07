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
        if (pos.y > 1)
        {
            var camTrans = cam.transform;
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 1, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
	}
}
