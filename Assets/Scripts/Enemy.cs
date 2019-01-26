using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    GameObject target;
    bool canSeePlayer = false;
    bool isAggressive = false;

    bool rando;
    float speed = 0.01F;
    float dist = 10.0F;

    [SerializeField]
    float health = 10.0F;
    float tempHealth = 10.0F;
    
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        rando = (Random.value > 0.5F);
        isAggressive = rando;
    }
    
    void FixedUpdate()
    {
        //AGGRESSIVE=====================
        if(canSeePlayer && isAggressive){
            if ((this.transform.position - target.transform.position).sqrMagnitude < dist * dist) {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                anim.SetBool("isWalking",true);
                if (transform.position.x < target.transform.position.x)
                    sr.flipX = false;
                else {
                    sr.flipX = true;
                }
            }
            else {
                canSeePlayer = false;
                anim.SetBool("isWalking",false);
            }
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
        }
    }
    
}
