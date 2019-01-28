using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform shell;
    Animator anim;
    SpriteRenderer sr;
    GameObject target;
    //bool canSeePlayer = false;
    //bool isAggressive = false;

    bool rando;
    float speed = 0.03F;
    float dist = 7.0F;

    [SerializeField]
    private int health = 3;
    //float maxHealth = 5.0F;

    private AudioSource HITAUDIO;
    private AudioSource STEP_1_AUDIO;
    private AudioSource STEP_2_AUDIO;

    private bool spawned;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        HITAUDIO = this.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        STEP_1_AUDIO = this.transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        STEP_2_AUDIO = this.transform.GetChild(4).gameObject.GetComponent<AudioSource>();
        //rando = (Random.value > 0.5F);
        //isAggressive = true;//rando;
        health = 3;
        dist = 7.0F;
        if (this.transform.position.y < -10)
        {
            spawned = true;
            StartCoroutine(spawn(Random.Range(-4,4),Random.Range(0,100)*0.1F));
        }
        else
        {
            spawned = false;
        }
    }

    IEnumerator spawn(int yval,float wait) {
        yield return new WaitForSeconds(wait);
        Vector3 up = new Vector3(0, 0.05F, 0);
        anim.SetBool("isWalking", true);
       
        while (this.transform.position.y<yval) {
            this.transform.position += up;
            yield return new WaitForSeconds(0.01F);
        }

        anim.SetBool("isWalking", false);
        spawned = false;
        yield return null;
    }

    void FixedUpdate()
    {
        if (!AudioManager.instance.isPlayingEnd && spawned == false)
        {
            //AGGRESSIVE=====================
            //if (canSeePlayer && isAggressive && health > 0)
            if (health > 0)
            {
                if ((this.transform.position - target.transform.position).sqrMagnitude < dist * dist)
                {
                    AudioManager.instance.playAudio(AUDIO.FIRST_CRAB);
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
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    //canSeePlayer = false;
                }
            }
            else if (health <= 0)
            {
                AudioManager.instance.playAudio(AUDIO.FIRST_KILL);
                Instantiate(shell, this.transform.position, this.transform.rotation);
                CameraEffects.instance.Shake(8, 0.1F);
                Movement.instance.EnemyCount -= 1;
                Destroy(this.gameObject);
            }
        }
        else if(!spawned) {
            if(anim.GetBool("isWalking"))
                anim.SetBool("isWalking", false);
            if (anim.GetBool("isAttacking"))
                anim.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SUPER basic proximity trigger
        //if (collision.CompareTag("Player")) {
       //    canSeePlayer = true;
        //}
        if (collision.gameObject.CompareTag("PLAYERFIST"))
        {
            health -= 1;
            HITAUDIO.Play();
            TextManager.instance.callText(this.transform.position, "!");
        }
    }

  //  private void OnTriggerExit2D(Collider2D collision)
  //  {
        //if (collision.CompareTag("Player"))
       // {
            //canSeePlayer = false;
            //anim.SetBool("isWalking", false);
            //anim.SetBool("isAttacking", false);
        //}
   // }
    public void PlayStep(int step)
    {
        if (step == 0)
            STEP_1_AUDIO.Play();
        else if (step == 1)
            STEP_2_AUDIO.Play();

    }

}
