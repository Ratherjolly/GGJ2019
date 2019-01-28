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
    [HideInInspector]
    public int EnemyCount = 0;
    public static Movement instance;
    public Transform enemyT;

    void Awake()
    {
        instance = this;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim_H = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        sr_H = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update()
    {
        if (AudioManager.instance.playedOpening && !AudioManager.instance.isPlayingEnd)
        {
            if(EnemyCount==0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(enemyT, new Vector3(Random.Range(-50, 650) * 0.1F, -15, 0), this.transform.rotation);
                }
                EnemyCount += 10;
            }

            //VERTICAL MOVEMENT===============
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                yVel = speed;
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                xVel = -speed;
                sr.flipX = true;
                sr_H.flipX = true;
                sr_H.gameObject.transform.localPosition = new Vector3(-0.8F, -0.02F, 0);
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
            }
            else
            {
                anim_H.SetBool("isAttacking", false);
            }
        }
        else {
            if(anim_H.GetBool("isAttacking"))
                anim_H.SetBool("isAttacking",false);
            if (anim.GetBool("isWalking"))
                anim.SetBool("isWalking", false);

        }

    }
    void FixedUpdate()
    {
        if (AudioManager.instance.playedOpening && !AudioManager.instance.isPlayingEnd)
        {
            //STUPID SIMPLE LEVEL COLLISION
            if (transform.position.y > 4.25F && yVel > 0)
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
        else {
            rb.velocity = new Vector2(0, 0);
        }
    }
}