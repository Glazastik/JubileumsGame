using UnityEngine;
using System.Collections;

public class RoundSystem : MonoBehaviour {

    public StickController player1;
    public StickController player2;
    public BlockSpawner blockSpawner;

    private bool done = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (done)
        {
            return;
        }

        if (player1.isDead())
        {
            player2.addWin();
            StartCoroutine(NewGame());
            done = true;
        }
        else if (player2.isDead())
        {
            player1.addWin();
            StartCoroutine(NewGame());
            done = true;
        }
        else
        {
            return;
        }

	}

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(3f);

        blockSpawner.Reset();
        player1.reset();
        player2.reset();
        done = false;
    }
}
