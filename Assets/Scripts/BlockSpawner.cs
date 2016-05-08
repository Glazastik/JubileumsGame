using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour {

    public GameObject[] player;
    public Transform[] block;
    private Transform chosenBlock;
    public int initSpawnDelay;
    private float spawnDelay;
    private float spawnTic;
    private float xOffset;
    private float yOffset;
    private int randomBlock;
    private List<GameObject> blocks = new List<GameObject>();

	// Use this for initialization
	void Start () {
        spawnDelay = initSpawnDelay;
        spawnTic = 0;
        xOffset = 0;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTic += 1;
        if (spawnTic >= spawnDelay)
        {
            xOffset = Random.Range(-10.0f, 10.0f);
            yOffset = Random.Range(10.0f, 15.0f);
            randomBlock = Random.Range(0, block.Length);
            var spawnheight = Mathf.Max(player[0].transform.position.y, player[1].transform.position.y);

            Transform obj = (Transform) Instantiate(block[randomBlock], new Vector3(xOffset, spawnheight + yOffset), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1.5f);
            blocks.Add(obj.gameObject);

            spawnTic = 0;
            spawnDelay = Random.Range(10, 120);
        }
	}
    
    public void Reset()
    {
        foreach(GameObject o in blocks)
        {
            Destroy(o);
        }
        blocks.Clear();
    }
}
