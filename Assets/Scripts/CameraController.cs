using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Transform[] players;
    private Vector3 pos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        pos = Camera.current.WorldToViewportPoint(players[0].position);
        if (pos.y > 1)
        {
            var camTrans = Camera.current.transform;
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y + 1, camTrans.position.z);
            Camera.current.transform.position = camTrans.position;
        }
	}
}
