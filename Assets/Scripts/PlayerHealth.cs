using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int shell = 0;

    private AudioSource HITAUDIO;

    private AudioSource Step1_AUDIO;
    private AudioSource Step2_AUDIO;
    private AudioSource Step3_AUDIO;
    private AudioSource pickup;
    private GameObject shellSpr;
    private ShellCanvas shellCanvas;
    private SpriteRenderer sr;
    SpriteRenderer sr_H;

    private void Awake()
    {
        HITAUDIO = this.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        Step1_AUDIO = this.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        Step2_AUDIO = this.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        Step3_AUDIO = this.transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        pickup = this.transform.GetChild(4).gameObject.GetComponent<AudioSource>();
        shellSpr = this.transform.GetChild(5).gameObject;
        shellCanvas = GameObject.FindObjectOfType<ShellCanvas>();
        shell = 0;
        shellCanvas.gameObject.SetActive(false);
        sr = this.GetComponent<SpriteRenderer>();
        sr_H = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(!shellCanvas.gameObject.activeSelf)
            if (AudioManager.instance.playedOpening)
                shellCanvas.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SUPER basic proximity trigger
        if (collision.CompareTag("ENEMYATTACK"))
        {
            shell -= 1;
            HITAUDIO.Play();
            CameraEffects.instance.Shake(8, 0.2F);

            if (shell < 0) {
                AudioManager.instance.playAudio(AUDIO.DIE);
            }
        }
        else if (collision.CompareTag("SHELL"))
        {
            shell += 1;
            //DO SOMETHING COOL
            pickup.Play();

            if(shell<9)
            {
                if (!AudioManager.instance.playedFirstShell)
                    AudioManager.instance.playAudio(AUDIO.FIRST_SHELL);
                else if (!AudioManager.instance.playedRandoOnCollect)
                    AudioManager.instance.playAudio(AUDIO.RANDO_OnCollect);
                else if (!AudioManager.instance.playedRando_1)
                    AudioManager.instance.playAudio(AUDIO.RANDO_1);
                else if (!AudioManager.instance.playedRando_2)
                    AudioManager.instance.playAudio(AUDIO.RANDO_2);
                else if (!AudioManager.instance.playedRando_3)
                    AudioManager.instance.playAudio(AUDIO.RANDO_3);
                else
                {
                    //Debug.Log("HERPA DERP DERP DERP");
                }
            }
            Destroy(collision.gameObject);
        }

        //update the shell grid. if the health is less then send 0 (no partial shells)
        shellCanvas.shells = shell > 0 ? shell : 0;

        if (shellCanvas.shells >= 9)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
            shellSpr.SetActive(true);
            AudioManager.instance.isPlayingEnd = true;
            AudioManager.instance.playAudio(AUDIO.ENDING);
            sr.flipX = false;
            sr_H.flipX = false;
            sr_H.gameObject.transform.localPosition = new Vector3(0.8F, -0.02F, 0);
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
