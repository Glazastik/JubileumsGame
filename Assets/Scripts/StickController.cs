using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StickController : MonoBehaviour {

    private Animator anim;
    private bool facingRight = true;
    private Rigidbody2D rb2d;
    public float speed;
    public int player;
    public int jumpForce = 200;

    public GameObject deathParticle;
    public AudioSource deathSound;

    bool doubleJ = false;

    //Collision stuff
    bool dead = false;

    bool headCollision = false;
    bool grounded = false;
    public Transform groundCheck;
    public Transform headCheck;

    public Transform playerTag;

    private int wins;
    private float height;
    private float heightRecord = 0;
    public Text heightText;
    public Text winText;


    public LayerMask whatIsGround;
    public LayerMask whatIsCrush;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movePlayer();
	}

    void LateUpdate()
    {
        updateHeight();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        playerTag.localScale = new Vector2(playerTag.localScale.x * -1, playerTag.localScale.y);
    }

    void updateHeight()
    {
        heightText.text = "Height: " + height + " / " + heightRecord;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("blastzone"))
        {
            if (!dead)
            {
                StartCoroutine(Die());
                rb2d.velocity = new Vector2(0f, 0f);
                rb2d.AddForce(new Vector2(0, 10f));
            }
        }
    }

    IEnumerator Die()
    {
        if (!dead)
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
            dead = true;
            anim.SetBool("Dead", true);
            rb2d.freezeRotation = false;
            rb2d.AddTorque(-2f);
            deathSound.Play();
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        
    }

    void movePlayer()
    {
        grounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, whatIsGround);
        headCollision = Physics2D.Raycast(headCheck.position, Vector2.up, 0.1f, whatIsCrush);

        if (grounded) doubleJ = true;

        if (grounded && headCollision && !dead)
        {
            StartCoroutine(Die());
        }
        else if (dead)
        {
            return;
        }

        float moveHorizontal = 0f;
        if (player == 1)
        {
            moveHorizontal = Input.GetAxis("Horizontal1");
        }
        else if (player == 2)
        {
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
        else if (moveHorizontal != 0 && grounded)
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
        else if (((player == 1 && Input.GetButtonDown("Jump1")) || (player == 2 && Input.GetButtonDown("Jump2"))) && doubleJ && !dead)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0, jumpForce));
            doubleJ = false;
        }

        height = Mathf.Floor(groundCheck.position.y)+10;
        if (height > heightRecord)
        {
            heightRecord = height;
        }
    }

    
}
