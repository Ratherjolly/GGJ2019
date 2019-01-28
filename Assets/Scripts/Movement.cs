using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //COMPONENT REFERENCES===========
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim_H;
    SpriteRenderer sr_H;

    //MOVEMENT VARIABLES=============
    int xVel;
    int yVel;
    int speed = 3;
    bool isMove = false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim_H = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        sr_H = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (AudioManager.instance.playedOpening)
        {
            //VERTICAL MOVEMENT===============
            if (Input.GetKey(KeyCode.W))
            {
                yVel = speed;
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                yVel = -speed;
                anim.SetBool("isWalking", true);
            }
            else
            {
                if (yVel != 0)
                {
                    yVel = 0;
                }
            }

            //HORIZONTAL MOVEMENT=============
            if (Input.GetKey(KeyCode.A))
            {
                xVel = -speed;
                sr.flipX = true;
                sr_H.flipX = true;
                sr_H.gameObject.transform.localPosition = new Vector3(-0.8F, -0.02F, 0);
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                xVel = speed;
                sr.flipX = false;
                sr_H.flipX = false;
                sr_H.gameObject.transform.localPosition = new Vector3(0.8F, -0.02F, 0);
                anim.SetBool("isWalking", true);
            }
            else
            {
                if (xVel != 0)
                    xVel = 0;
            }

            if (xVel == 0 && yVel == 0)
                if (anim.GetBool("isWalking"))
                    anim.SetBool("isWalking", false);

            //ATTACKING========================
            if (Input.GetKey(KeyCode.Space))
            {
                anim_H.SetBool("isAttacking", true);
                AudioManager.instance.playAudio(AUDIO.RANDO_1);
            }
            else
            {
                anim_H.SetBool("isAttacking", false);
            }
        }
        
    }
    void FixedUpdate()
    {
        //STUPID SIMPLE LEVEL COLLISION
        if (transform.position.y > 5.0F && yVel > 0)
        {
            yVel = 0;
        }
        else if (transform.position.y < -5.0F && yVel < 0)
        {
            yVel = 0;
        }

        if (transform.position.x > 67.0F && xVel > 0)
        {
            xVel = 0;
        }
        else if (transform.position.x < -17.0F && xVel < 0)
        {
            xVel = 0;
        }

        if (isMove)
            rb.velocity = new Vector2(xVel, yVel);

        if (xVel == 0 && yVel == 0)
        {
            if (isMove != false)
            {
                isMove = false;
            }
        }
        else
        {
            if (!isMove)
            {
                isMove = true;
            }
        }
    }
}