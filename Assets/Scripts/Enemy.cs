using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform shell;
    Animator anim;
    SpriteRenderer sr;
    GameObject target;
    bool canSeePlayer = false;
    bool isAggressive = false;

    bool rando;
    float speed = 0.05F;
    float dist = 15.0F;

    [SerializeField]
    float health = 10.0F;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        rando = (Random.value > 0.5F);
        isAggressive = true;//rando;
    }
    
    void FixedUpdate()
    {
        //AGGRESSIVE=====================
        if(canSeePlayer && isAggressive && health>0){
            if ((this.transform.position - target.transform.position).sqrMagnitude < dist * dist) {

                //ATTACK LOGIC==========
                if ((this.transform.position - target.transform.position).sqrMagnitude < 1.8F)
                    anim.SetBool("isAttacking", true);
                else
                {
                    anim.SetBool("isAttacking", false);
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                    anim.SetBool("isWalking", true);
                    if (transform.position.x < target.transform.position.x)
                    {
                        sr.flipX = false;
                    }
                    else
                    {
                        sr.flipX = true;
                    }
                }
            }
            else {
                anim.SetBool("isWalking", false);
                canSeePlayer = false;
            }
        }
        else if (health <= 0) {
            Instantiate(shell, this.transform.position, this.transform.rotation);
            CameraEffects.instance.Shake(8, 0.1F);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SUPER basic proximity trigger
        if (collision.CompareTag("Player")) {
            canSeePlayer = true;
        }
        if (collision.gameObject.CompareTag("PLAYERFIST"))
        {
            health -= 2;
            TextManager.instance.callText(this.transform.position, "-1");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSeePlayer = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
    
}
