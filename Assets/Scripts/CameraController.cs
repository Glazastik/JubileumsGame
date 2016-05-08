using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] players;
    private Vector3 pos1;
    private Vector3 pos2;
    private Camera cam;
    private float originalCamsize;
    private int zoomDelay = 10;
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        originalCamsize = cam.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        var pa1 = players[0].gameObject.activeInHierarchy;
        var pa2 = players[1].gameObject.activeInHierarchy;
        bool bpls = pa1 && pa2;
        pos1 = cam.WorldToViewportPoint(players[0].position);
        pos2 = cam.WorldToViewportPoint(players[1].position);
        var camTrans = cam.transform;
        if (pos1.y < 0.1 && pos2.y > 0.9 && bpls || pos2.y < 0.1 && pos1.y > 0.9 && bpls ||pos1.y < 0 && pa1 || pos2.y < 0 && pa2)
        {
            cam.orthographicSize = cam.orthographicSize + 0.25f;
            zoomDelay = 10;
        }
        else if (cam.orthographicSize > originalCamsize && zoomDelay == 0)
        {
            cam.orthographicSize = cam.orthographicSize - 0.25f;
        }
        else if (pos1.y > 0.8 || pos2.y > 0.8)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 0.2f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
        else if (pos1.y < 0.2 && pa1 ||pos2.y < 0.2 && pa2)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y - 0.2f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }
        else
        {
            zoomDelay--;
        }
    }
}
