using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{

    private AudioClip[] audioClips;
    private AudioSource[] audioSources;
    public enum Sounds { CONFIRM, ENDTURN, ERROR, LOSE, MISSILEHIT, MISSILELAUNCH, MONEY, MOVE, POPUP, WIN };

    void Awake()
    {
        audioClips = Glob.GetAudioClips();
        audioSources = new AudioSource[audioClips.Length];
        for (int index = 0; index < audioClips.Length; index++)
        {
            audioSources[index] = this.gameObject.AddComponent<AudioSource>();
            audioSources[index].clip = audioClips[index];
            audioSources[index].playOnAwake = false;
        }
        audioSources[0].volume = 0.5f;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public void PlaySound(Sounds pSound)
    {
        switch (pSound)
        {
            case Sounds.CONFIRM:
                if(!audioSources[0].isPlaying)
                {
                    audioSources[0].Play();
                }
                break;
            case Sounds.ENDTURN:
                audioSources[1].Play();
                break;
            case Sounds.ERROR:
                audioSources[2].Play();
                break;
            case Sounds.LOSE:
                audioSources[3].Play();
                break;
            case Sounds.MISSILEHIT:
                audioSources[4].Play();
                break;
            case Sounds.MISSILELAUNCH:
                audioSources[5].Play();
                break;
            case Sounds.MONEY:
                if (!audioSources[6].isPlaying)
                {
                    audioSources[6].Play();
                }
                break;
            case Sounds.MOVE:
                audioSources[7].Play();
                break;
            case Sounds.POPUP:
                audioSources[8].Play();
                break;
            case Sounds.WIN:
                audioSources[9].Play();
                break;
        }

    }



}
