using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] players;
    public GameObject bz;
    private Vector3 pos1;
    private Vector3 pos2;
    private Camera cam;
    private float originalCamsize;
    private int zoomDelay = 60;
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
            zoomDelay = 60;
        }
        else if (cam.orthographicSize > originalCamsize && zoomDelay == 0)
        {
            cam.orthographicSize = cam.orthographicSize - 0.25f;
        }
        else if (pos1.y > 0.75 || pos2.y > 0.75)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 0.125f, camTrans.position.z);
            cam.transform.position = camTrans.position;
            bz.transform.position = new Vector3(bz.transform.position.x, bz.transform.position.y + 0.0675f, bz.transform.position.z);
        }
        /*else if (pos1.y < 0.2 && pa1 ||pos2.y < 0.2 && pa2)
        {
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y - 0.2f, camTrans.position.z);
            cam.transform.position = camTrans.position;
        }*/
        else
        {
            zoomDelay--;
        }
    }

    public void Reset()
    {
        cam.transform.position = new Vector3(0, -2, -10);
        cam.orthographicSize = originalCamsize;
    }
}
