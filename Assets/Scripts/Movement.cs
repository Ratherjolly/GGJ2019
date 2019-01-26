using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //COMPONENT REFERENCES===========
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;

    //MOVEMENT VARIABLES=============
    int xVel;
    int yVel;
    int speed = 5;
    bool isMove= false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        //VERTICAL MOVEMENT===============
        if (Input.GetKey(KeyCode.W))
            yVel = speed;
        else if (Input.GetKey(KeyCode.S))
            yVel = -speed;
        else
        {
            if (yVel != 0)
                yVel = 0;
        }

        //HORIZONTAL MOVEMENT=============
        if (Input.GetKey(KeyCode.A)) {
            xVel = -speed;
            sr.flipX = true;
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.D)) {
            xVel = speed;
            sr.flipX = false;
            anim.SetBool("isWalking", true);
        }
        else {
            if (xVel != 0)
                xVel = 0;
            anim.SetBool("isWalking", false);
        }

        //ATTACKING========================
        if (Input.GetKey(KeyCode.Space))
            anim.SetBool("isAttacking", true);
        else {
            anim.SetBool("isAttacking", false);
        }
    }
    private void FixedUpdate()
    {
        //LEVEL COLLISION
        if (transform.position.y > 5.0F && yVel >0) {
            yVel = 0;
        }
        else if (transform.position.y < -5.0F && yVel<0) {
            yVel = 0;
        }

        if (isMove)
            rb.velocity = new Vector2(xVel, yVel);

        if (xVel == 0 && yVel == 0)
            isMove = false;
        else {
            isMove = true;
        }
    }
}
