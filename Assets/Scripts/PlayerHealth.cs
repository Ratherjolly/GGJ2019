using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int shell = 10;

    private AudioSource HITAUDIO;

    private AudioSource Step1_AUDIO;
    private AudioSource Step2_AUDIO;
    private AudioSource Step3_AUDIO;
    private AudioSource pickup;


    private void Awake()
    {
        HITAUDIO = this.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        Step1_AUDIO = this.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        Step2_AUDIO = this.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        Step3_AUDIO = this.transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        pickup = this.transform.GetChild(4).gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SUPER basic proximity trigger
        if (collision.CompareTag("ENEMYATTACK"))
        {
            shell -= 1;
            HITAUDIO.Play();
            CameraEffects.instance.Shake(8, 0.2F);
        }
        if (collision.CompareTag("SHELL"))
        {
            shell -= 1;
            //DO SOMETHING COOL
            pickup.Play();
            Destroy(collision.gameObject);
        }
    }

    public void playStep(int step) {
        if (step == 0)
            Step1_AUDIO.Play();
        else if (step == 1)
            Step2_AUDIO.Play();
        else if (step == 2)
            Step3_AUDIO.Play();
    }
}
