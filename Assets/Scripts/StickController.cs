using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour {

    private Animator anim;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    public float speed;
    public int player;
    public int jumpForce = 200;

    bool doubleJ = false;

    //Collision stuff
    bool dead = false;

    bool headCollision = false;
    bool grounded = false;
    public Transform groundCheck;
    public Transform headCheck;

    public LayerMask whatIsGround;
    float collisionRadius = 0.1f;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, collisionRadius, whatIsGround);
        headCollision = Physics2D.OverlapCircle(headCheck.position, collisionRadius, whatIsGround);

        if (grounded) doubleJ = true;

        if (grounded && headCollision)
        {
            dead = true;
            anim.SetBool("Dead", true);
        }

        float moveHorizontal = 0f;
        if (player == 1) {
            moveHorizontal = Input.GetAxis("Horizontal1");
        } else if (player == 2){
            moveHorizontal = Input.GetAxis("Horizontal2");
        }
        
        Vector2 movement = new Vector2(moveHorizontal, 0);

        if ((moveHorizontal < 0 && facingRight) || (moveHorizontal > 0 && !facingRight))
        {
            Flip();
        }

        if (moveHorizontal == 0 && grounded)
        {
            anim.SetInteger("state", 0);
        }
        else if(moveHorizontal != 0 && grounded)
        {
            anim.SetInteger("state", 1);
        }
        else
        {
            anim.SetInteger("state", 2);
        }
        if (!dead)
        {
            rb2d.AddForce(movement.normalized * speed);
        }
        if (player == 1 && Input.GetButtonDown("Jump1") && grounded && !dead || player == 2 && Input.GetButtonDown("Jump2") && grounded && !dead)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
        else if (((player == 1 && Input.GetButtonDown("Jump1")) || (player == 2 && Input.GetButtonDown("Jump2") )) && doubleJ && !dead)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0, jumpForce));
            doubleJ = false;
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
