using UnityEngine;
using System.Collections;

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
            yOffset = Random.Range(0.0f, 10.0f);
            randomBlock = Random.Range(0, block.Length);
            var spawnheight = Mathf.Max(player[0].transform.position.y, player[1].transform.position.y);
            Transform obj = (Transform) Instantiate(block[randomBlock], new Vector3(xOffset, spawnheight + 10), Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f);
            spawnTic = 0;
            if (spawnDelay >= 10)
            {
                spawnDelay -= 0.1f;
            }
        }
	}
}
