using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour {

    private Animator anim;
    private string direction = "right";
    private Rigidbody2D rb2d;
    public int speed;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement.normalized * speed * Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0, 100));
        }
	}
}
