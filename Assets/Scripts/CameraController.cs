using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] players;
    private Vector3 pos1;
    private Vector3 pos2;
    private Camera cam;
    private float originalCamsize;
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        originalCamsize = cam.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        pos1 = cam.WorldToViewportPoint(players[0].position);
        pos2 = cam.WorldToViewportPoint(players[1].position);
        var camTrans = cam.transform;
        if (pos1.y < 0.1 && pos2.y > 0.9 || pos2.y < 0.1 && pos1.y > 0.9 ||pos1.y < 0 || pos2.y < 0)
        {
            cam.orthographicSize = cam.orthographicSize + 0.5f;
        }
        else if (cam.orthographicSize > originalCamsize)
        {
            cam.orthographicSize = cam.orthographicSize - 0.25f;
        }
        if (pos1.y > 0.8 || pos2.y > 0.8)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 0.1f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
        else if (pos1.y < 0.2 ||pos2.y < 0.2)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y - 0.1f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
	}
}
