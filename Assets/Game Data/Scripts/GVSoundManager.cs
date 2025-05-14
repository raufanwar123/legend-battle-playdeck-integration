using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GVSoundManager : MonoBehaviour
{
    public static string KEY_isSoundON = "isSoundON";
    public static string KEY_isMusicON = "isMusicON";

    public AudioClip _Clip_Btnclick,backBtnClickClip;
    public AudioClip[] audioClips;

    private AudioSource _audioSourceSFX;
    private AudioSource _audioSourceMusic;
    private static GVSoundManager _instance = new GVSoundManager();
    bool isFromAwake = false;
    private GVSoundManager() { }

    public static GVSoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (!isFromAwake)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        isFromAwake = true;
    }

    private void Start()
    {
        _audioSourceSFX = GetComponents<AudioSource>()[0];
        _audioSourceMusic = GetComponents<AudioSource>()[1];
    }

    public void PlayBtnClickSound()
    {
        _audioSourceSFX.clip = null;
        _audioSourceSFX.clip = _Clip_Btnclick;

        if (_audioSourceSFX.clip == null)
        {
            GVLogsManager.instance.DebugLog(this, "SFX not found or name mismatch, Add in Array of SoundManager");
            return;
        }

        if (_audioSourceSFX.clip != null && isSoundON())
            _audioSourceSFX.Play();
        
    }


    public void PlayBackBtnClickSound()
    {
        _audioSourceSFX.clip = null;
        _audioSourceSFX.clip = backBtnClickClip;

        if (_audioSourceSFX.clip == null)
        {
            GVLogsManager.instance.DebugLog(this, "SFX not found or name mismatch, Add in Array of SoundManager");
            return;
        }

        if (_audioSourceSFX.clip != null && isSoundON())
            _audioSourceSFX.Play();

    }


    public void PlaySound(string fileName)
    {
        _audioSourceSFX.clip = null;
        
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name.Equals(fileName))
            {
                _audioSourceSFX.clip = clip;
                break;
            }
        }

        if (_audioSourceSFX.clip == null)
        {
            GVLogsManager.instance.DebugLog(this, "SFX not found or name mismatch, Add in Array of SoundManager");
            return;
        }

        if (_audioSourceSFX.clip != null && isSoundON())
            _audioSourceSFX.Play();
    }

    public void StopBgMusic()
	{
		_audioSourceMusic.Stop();
        _audioSourceMusic.volume = 0f;
    }

    public void PlayBGMusic(string fileName)
    {
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name.Equals(fileName))
            {
                _audioSourceMusic.clip = clip;
                break;
            }

        }

        if (_audioSourceMusic.clip == null)
        {
            GVLogsManager.instance.DebugLog(this, "Music File not found or name mismatch, Add in Array of SoundManager");
            return;
        }

        if (_audioSourceMusic.clip != null && isMusicON())
            _audioSourceMusic.Play();
        _audioSourceMusic.volume = 1f;
    }

    public  bool isSoundON()
    {
        if (PlayerPrefs.GetInt(KEY_isSoundON, 1) == 1)
            return true;
        else
            return false;
    }

    public  bool isMusicON()
    {
        if (PlayerPrefs.GetInt(KEY_isMusicON, 1) == 1)
            return true;
        else
            return false;
    }

    public  void setSoundValue(int value)
    {
        PlayerPrefs.SetInt(KEY_isSoundON, value);
    }

    public  void setMusicValue(int value)
    {
        PlayerPrefs.SetInt(KEY_isMusicON, value);
    }
}
