using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource source;

    #region AudioClipsVar

    public AudioClip backgroundSound;
    public AudioClip[] levelSong;

    #endregion


    public static AudioController instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }

        //PlaySound(backgroundSound);
        
    }

    void Update()
    {

    }


    #region SoundPlay


    public void PlaySound(AudioClip soundToBePlayed)
    {
        source.PlayOneShot(soundToBePlayed);
        source.volume = 1f;
    }


    public void PlayLevelSong(int levelNumber)
    {
        source.clip = levelSong[levelNumber];
        source.Play();


    }
    public void StopSound()
    {
        if (source != null)
        {
            source.Stop();
        }
    }

    #endregion




}