using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public static BGMManager instance;

    public AudioClip[] clips;

    private AudioSource audioSource;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        // start 보다 먼저 실행 됨. 
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();


	}

    public void Play(int _playMusicTrack)
    {
        audioSource.volume = 1f;
        audioSource.clip = clips[_playMusicTrack];
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }


    public void Pause()
    {
        audioSource.Pause();
    }


    public void UnPause()
    {
        audioSource.UnPause();
    }


    public void SetVolumn(float _volumn)
    {
        audioSource.volume = _volumn;
    }


    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
	
    IEnumerator FadeOutMusicCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime; // 성능상 이득. 
        }
    }


    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0.0f; i <= 1f; i += 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime; // 성능상 이득. 
        }
    }
}

