using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour {

    public Transform[] block;
    private Transform chosenBlock;
    public int spawnDelay;
    private int spawnTic;
    private int xOffset;
    private int randomBlock;
	// Use this for initialization
	void Start () {
        spawnTic = 0;
        xOffset = 0;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTic++;
        if (spawnTic == spawnDelay)
        {
            xOffset = Random.Range(-7, 7);
            randomBlock = Random.Range(0, block.Length);
            Instantiate(block[randomBlock], new Vector3(xOffset, 10), Quaternion.identity);
            spawnTic = 0;
        }
	}
}
