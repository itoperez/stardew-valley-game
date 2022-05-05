using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch;

    [SerializeField] AudioClip playOnStart;

    private void Start()
    {
        Play(playOnStart, true);
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Play(audioClip);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Play(audioClip2);
        }

    }
    */

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if(musicToPlay == null) { return; }

        if (interrupt)
        {
            audioSource.volume = 1.0f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
        
    }

    AudioClip switchTo;
    float volume;

    IEnumerator SmoothSwitchMusic()
    {
        volume = 1.0f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if(volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);
    }


}
