using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class AudioUtil : MonoBehaviour
{


    private AudioSource audioSource;
    private bool audio_play;
    private bool audio_toggleChange;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool isPlaying => audioSource.isPlaying;


    public void Play(AudioClip clip)
    {
        audio_play = true;
        audio_toggleChange = true;
        //Check if you just set the toggle to positive.
        if (audio_play == true && audio_toggleChange == true)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
            audio_toggleChange = false;
        }
        //Check if you just set the toggle to false
        if (audio_play == false && audio_toggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn't play more than once
            audio_toggleChange = false;
        }
    }


    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    public void Stop()
    {
        audioSource.Stop();
    }


    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }
}
