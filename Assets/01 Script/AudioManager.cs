using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // inspector에 강제로 띄움 
public class Sound {
    public string name;              // 사운드 이름
    public AudioClip clip;           // 사운드 파일
    private AudioSource source;      // 사운드 플레이어

    public float Volumn;
    public bool loop;

    public void SetSourcec(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }
    
    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }
}


public class AudioManager : MonoBehaviour {


    static public AudioManager instance;

    [SerializeField]
    public Sound[] sounds;


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
		for(int i = 0; i <sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + "=" + sounds[i].name);
            sounds[i].SetSourcec(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);  // soundObject의 부모를 설정 .
        }
	}
	

    public void Play(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolumn(string _name, float _Volumn)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Volumn = _Volumn;
                sounds[i].SetVolumn();
                return;
            }
        }
    }
}
