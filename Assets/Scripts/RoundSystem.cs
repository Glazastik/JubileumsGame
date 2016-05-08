using UnityEngine;
using System.Collections;

public class RoundSystem : MonoBehaviour {

    public StickController player1;
    public StickController player2;
    public BlockSpawner blockSpawner;
    public GameObject blastZone;
    public CameraController cameraController;

    private bool done = false;

	// Use this for initialization
	void Start () {

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player1.clearWins();
            player2.clearWins();
            StartCoroutine(NewGame(0.2f));
        }
    }

	// Update is called once per frame
	void LateUpdate () {

        if (done)
        {
            if (player1.isDead() && player2.isDead())
            {
                StartCoroutine(NewGame(3f));
            }
            return;
        }

        if (player1.isDead())
        {
            player2.addWin();
            done = true;
        }
        else if (player2.isDead())
        {
            player1.addWin();
            done = true;
        }
        else
        {
            return;
        }

	}

    IEnumerator NewGame(float wait)
    {
        yield return new WaitForSeconds(wait);

        blockSpawner.Reset();
        cameraController.Reset();
        blastZone.transform.position = new Vector3(0, -12, -3);
        player1.reset();
        player2.reset();
        done = false;
    }
}
