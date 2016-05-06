using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour {

    private Animator anim;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    public int speed;
    public int jumpForce = 200;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        if ((moveHorizontal < 0 && facingRight) || (moveHorizontal > 0 && !facingRight))
        {
            Flip();
        }

        if (moveHorizontal == 0)
        {
            anim.SetInteger("state", 0);
        }
        else
        {
            anim.SetInteger("state", 1);
        }

        rb2d.AddForce(movement.normalized * speed * Time.fixedDeltaTime);
        if (Input.GetButtonDown("Jump"))
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
