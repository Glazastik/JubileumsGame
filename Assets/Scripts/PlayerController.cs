﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float speed;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement.normalized * speed * Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0, 100));
        }
    }
}
