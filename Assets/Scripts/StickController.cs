using UnityEngine;
using System.Collections;

public class StickController : MonoBehaviour {

    private Animator anim;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    public float speed;
    public int player;
    public int jumpForce = 200;

    bool doubleJ1 = false;
    bool doubleJ2 = false;

    //Collision stuff
    bool dead1 = false;
    bool dead2 = false;

    bool headCollision1 = false;
    bool grounded1 = false;
    public Transform groundCheck1;
    public Transform headCheck1;

    bool headCollision2 = false;
    bool grounded2 = false;
    public Transform groundCheck2;
    public Transform headCheck2;

    public LayerMask whatIsGround;
    float collisionRadius = 0.1f;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded1 = Physics2D.OverlapCircle(groundCheck1.position, collisionRadius, whatIsGround);
        headCollision1 = Physics2D.OverlapCircle(headCheck1.position, collisionRadius, whatIsGround);

        if (grounded1) doubleJ1 = true;

        grounded2 = Physics2D.OverlapCircle(groundCheck2.position, collisionRadius, whatIsGround);
        headCollision2 = Physics2D.OverlapCircle(headCheck2.position, collisionRadius, whatIsGround);

        if (grounded2) doubleJ2 = true;

        if (grounded1 && headCollision1)
        {
            dead1 = true;
        }
        if (grounded2 && headCollision2)
        {
            dead2 = true;
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

        if (moveHorizontal == 0)
        {
            anim.SetInteger("state", 0);
        }
        else
        {
            anim.SetInteger("state", 1);
        }
        if (player == 1 && !dead1)
        {
            rb2d.AddForce(movement.normalized * speed);
        }
        if (player == 2 && !dead2)
        {
            rb2d.AddForce(movement.normalized * speed);
        }
        if (player == 1 && Input.GetButtonDown("Jump1") && grounded1 && !dead1 || player == 2 && Input.GetButtonDown("Jump2") && grounded2 && !dead2)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
        else if (player == 1 && Input.GetButtonDown("Jump1") && doubleJ1 && !dead1)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            doubleJ1 = false;
        }
        else if (player == 2 && Input.GetButtonDown("Jump2") && doubleJ2 && !dead2)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            doubleJ2 = false;
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
