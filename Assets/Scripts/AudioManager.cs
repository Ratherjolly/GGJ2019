using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AUDIO {
    MUSIC,
    OPENING,
    FIRST_SHELL,
    FIRST_CRAB,
    FIRST_KILL,
    DIE,
    WIN,
    ENDING,
    RANDO_OnCollect,
    RANDO_1,
    RANDO_2,
    RANDO_3
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource MUSIC;
    private AudioSource OPENING;
    private AudioSource FIRST_SHELL;
    private AudioSource FIRST_CRAB;
    private AudioSource FIRST_KILL;
    private AudioSource DIE;
    private AudioSource WIN;
    private AudioSource ENDING;
    private AudioSource RANDO_OnCollect;
    private AudioSource RANDO_1;
    private AudioSource RANDO_2;
    private AudioSource RANDO_3;

    private bool playedOpening = false;
    private bool playedFirstShell = false;
    private bool playedFirstCrab = false;
    private bool playedFirstKill = false;
    private bool playedRandoOnCollect = false;
    private bool playedRando_1 = false;
    private bool playedRando_2 = false;
    private bool playedRando_3 = false;

    private void Awake()
    {
        /*
        FOR THE RECORD... THIS IS THE WRONG WAY OF HOW TO DO THIS.
        PLEASE. LOOK AWAY. AHHHHHHH!!!      
        */
        instance = this;

        MUSIC = transform.GetChild(0).GetComponent<AudioSource>();
        OPENING = transform.GetChild(1).GetComponent<AudioSource>();
        FIRST_SHELL = transform.GetChild(2).GetComponent<AudioSource>();
        FIRST_CRAB = transform.GetChild(3).GetComponent<AudioSource>();
        FIRST_KILL = transform.GetChild(4).GetComponent<AudioSource>();
        DIE = transform.GetChild(5).GetComponent<AudioSource>();
        WIN = transform.GetChild(6).GetComponent<AudioSource>();
        ENDING = transform.GetChild(7).GetComponent<AudioSource>();
        RANDO_OnCollect = transform.GetChild(8).GetComponent<AudioSource>();
        RANDO_1 = transform.GetChild(9).GetComponent<AudioSource>();
        RANDO_2 = transform.GetChild(10).GetComponent<AudioSource>();
        RANDO_3 = transform.GetChild(11).GetComponent<AudioSource>();
    }

    private void Start()
    {
        playAudio(AUDIO.OPENING);
    }



    public void playAudio(AUDIO aud) {
        if(aud==AUDIO.RANDO_OnCollect || aud == AUDIO.RANDO_1 || aud == AUDIO.RANDO_2 || aud == AUDIO.RANDO_3) {
            if (!OPENING.isPlaying && !FIRST_SHELL.isPlaying && !FIRST_CRAB.isPlaying && !FIRST_KILL.isPlaying && !DIE.isPlaying && !WIN.isPlaying && !ENDING.isPlaying
                && !RANDO_OnCollect.isPlaying && !RANDO_1.isPlaying && !RANDO_2.isPlaying && !RANDO_3.isPlaying) {
                StartCoroutine(PLAYAUDIO(aud));
            }

        }
        else if(!GetPlayed(aud)){
            StartCoroutine(PLAYAUDIO(aud));
        }
    }

    private void STOPAUDIO() {
        if (OPENING.isPlaying)
            OPENING.Stop();
        if (FIRST_SHELL.isPlaying)
            FIRST_SHELL.Stop();
        if (FIRST_CRAB.isPlaying)
            FIRST_CRAB.Stop();
        if (FIRST_KILL.isPlaying)
            FIRST_KILL.Stop();
        if (DIE.isPlaying)
            DIE.Stop();
        if (WIN.isPlaying)
            WIN.Stop();
        if (ENDING.isPlaying)
            ENDING.Stop();
        if (RANDO_OnCollect.isPlaying)
            RANDO_OnCollect.Stop();
        if (RANDO_1.isPlaying)
            RANDO_1.Stop();
        if (RANDO_2.isPlaying)
            RANDO_2.Stop();
        if (RANDO_3.isPlaying)
            RANDO_3.Stop();

    }

    private AudioSource GETSOURCE(AUDIO aud)
    {
        if (aud == AUDIO.OPENING)
        {
            return OPENING;
        }
        else if (aud == AUDIO.FIRST_SHELL)
        {
            return FIRST_SHELL;
        }
        else if (aud == AUDIO.FIRST_CRAB)
        {
            return FIRST_CRAB;
        }
        else if (aud == AUDIO.FIRST_KILL)
        {
            return FIRST_KILL;
        }
        else if (aud == AUDIO.DIE)
        {
            return DIE;
        }
        else if (aud == AUDIO.WIN)
        {
            return WIN;
        }
        else if (aud == AUDIO.ENDING)
        {
            return ENDING;
        }
        else if (aud == AUDIO.RANDO_OnCollect)
        {
            return RANDO_OnCollect;
        }
        else if (aud == AUDIO.RANDO_1)
        {
            return RANDO_1;
        }
        else if (aud == AUDIO.RANDO_2)
        {
            return RANDO_2;
        }
        else if (aud == AUDIO.RANDO_3)
        {
            return RANDO_3;
        }
        else {
            return MUSIC;
        }
    }

    private IEnumerator PLAYAUDIO(AUDIO aud) {
        AudioSource temp = GETSOURCE(aud);
            yield return null;
        STOPAUDIO();
            yield return null;
        temp.Play();
            yield return null;
        SETAUDIOBOOL(aud,true);
            yield return null;
        setMusic(0.2F);
            yield return null;
        while (temp.isPlaying) {

            yield return new WaitForSeconds(0.1F);
        }
        setMusic(0.5F);
        yield return null;
    }

    private void SETAUDIOBOOL(AUDIO aud, bool setBool)
    {
        if (aud == AUDIO.OPENING)
        {
            playedOpening = setBool;
        }
        else if (aud == AUDIO.FIRST_SHELL)
        {
            playedFirstShell = setBool;
        }
        else if (aud == AUDIO.FIRST_CRAB)
        {
            playedFirstCrab = setBool;
        }
        else if (aud == AUDIO.FIRST_KILL)
        {
            playedFirstKill = setBool;
        }
        else if (aud == AUDIO.RANDO_OnCollect)
        {
            playedRandoOnCollect = setBool;
        }
        else if (aud == AUDIO.RANDO_1)
        {
            playedRando_1 = setBool;
        }
        else if (aud == AUDIO.RANDO_2)
        {
            playedRando_2 = setBool;
        }
        else if (aud == AUDIO.RANDO_3)
        {
            playedRando_3 = setBool;
        }
        else
        {
            //GET THAT ROFL-COPTER OUTTA HERE!
        }
    }

    private bool GetPlayed(AUDIO aud)
    {
        if (aud == AUDIO.OPENING)
        {
            return playedOpening;
        }
        else if (aud == AUDIO.FIRST_SHELL)
        {
            return playedFirstShell;
        }
        else if (aud == AUDIO.FIRST_CRAB)
        {
            return playedFirstCrab;
        }
        else if (aud == AUDIO.FIRST_KILL)
        {
            return playedFirstKill;
        }
        else if (aud == AUDIO.RANDO_OnCollect)
        {
            return playedRandoOnCollect;
        }
        else if (aud == AUDIO.RANDO_1)
        {
            return playedRando_1;
        }
        else if (aud == AUDIO.RANDO_2)
        {
            return playedRando_2;
        }
        else if (aud == AUDIO.RANDO_3)
        {
            return playedRando_3;
        }
        else
        {
            return false;
        }
    }

    private void setMusic(float vol)
    {
        MUSIC.volume = vol;
    }

}
