using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSwing : MonoBehaviour
{

    private AudioSource SWINGAUDIO;

    private void Awake()
    {
        SWINGAUDIO = this.GetComponent<AudioSource>();
    }

    public void PlaySwing() {
        SWINGAUDIO.Play();
    }
}
