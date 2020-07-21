using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SFXManager : Subscriber
{
    private static SFXManager instance = null;
    public static SFXManager Instance
    {
        get { return instance; }
    }

    private bool muteAllMusic;
    private bool muteAllEffects;


    [SerializeField]
    private Sound[] musics;
    [SerializeField]
    private Sound[] SFXs;

    [SerializeField]
    private GameObject muteLineImage;
    [SerializeField]
    private GameObject effectsLineImage;

    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer effectsMixer;

    private bool isAMuteAll;

    void Awake()
    {
        EnsureSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Initialize();
        PlayMainMenuMusic();
        EventChannel.SubscribeTo(CustomEvent.EventType.cardFlips, this);
    }

    private void Initialize()
    {
        InitializeMusics();
        InitializeSFXs();
    }

    private void InitializeMusics()
    {
        foreach (Sound music in musics)
        {
            AudioSource musicAudioSource = gameObject.AddComponent<AudioSource>();
            musicAudioSource.outputAudioMixerGroup = musicMixer.FindMatchingGroups("Master")[0];
            music.Initialize(musicAudioSource);
        }
    }

    private void InitializeSFXs()
    {
        foreach (Sound sound in SFXs)
        {
            AudioSource sfxAudioSource = gameObject.AddComponent<AudioSource>();
            sfxAudioSource.outputAudioMixerGroup = effectsMixer.FindMatchingGroups("Master")[0];
            sound.Initialize(sfxAudioSource);
        }
    }

    public void PlayMainMenuMusic()
    {
        if (AreSFXsMuted())
            return;

        PlayMusic("MainMenu");
    }

    public void PlayIngameMusic()
    {
        if (AreSFXsMuted())
            return;

        PlayMusic("Ingame");
    }

    public void PlayGameOverMusic()
    {
        if (AreSFXsMuted())
            return;

        PauseSFXs();

        PlayMusic("GameOver");
    }

    public void PlayWinStateMusic()
    {
        if (AreSFXsMuted())
            return;

        PauseSFXs();

        PlayMusic("WinState");
    }

    public void PlayTicTac()
    {
        if (AreSFXsMuted())
            return;

        PlaySFX("TicTac");
    }

    public void ToggleTicTac()
    {
        Sound s = Array.Find(SFXs, sound => sound.name == "TicTac");
        s.source.mute = !s.source.mute;
    }

    public void StopTicTac()
    {
        Sound s = Array.Find(SFXs, sound => sound.name == "TicTac");
        s?.source?.Pause();
    }

    public void PlaySFX(string name)
    {
        if (AreSFXsMuted())
            return;

        Sound s = Array.Find(SFXs, sound => sound.name == name);

        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        if (AreSFXsMuted())
            return;

        PauseMusic();

        Sound s = Array.Find(musics, sound => sound.name == name);
        s?.source?.Play();
    }

    private void PauseMusic()
    {
        foreach (Sound s in musics)
            s?.source?.Pause();
    }

    public void PauseSFXs()
    {
        foreach (Sound s in SFXs)
            s?.source?.Pause();
    }

    public void ApplyLowpassEffectToMusic()
    {
        musicMixer.SetFloat("LowpassVolume", 0f);
    }

    public void BypassLowpassEffectToMusic()
    {
        musicMixer.SetFloat("LowpassVolume", -80f);
    }

    public void UpdateMusicVolume(float value)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void UpdateEffectsVolume(float value)
    {
        effectsMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
    }

    public void RestartMusic()
    {
        PlayIngameMusic();
    }

    public bool AreSFXsMuted()
    {
        return muteAllEffects;
    }

    public bool AreMusicsMuted()
    {
        return muteAllMusic;
    }

    public void MuteAll()
    {
        isAMuteAll = true;
        if (AreSFXsMuted() && AreMusicsMuted())
        {
            MuteMusic();
            MuteSFXs();
        }
        else if (AreSFXsMuted())
        {
            MuteMusic();
        }
        else if (AreMusicsMuted())
        {
            MuteSFXs();
        }
        else
        {
            MuteMusic();
            MuteSFXs();
        }
    }

    public void MuteMusic()
    {
        ToggleMusic();
        if (!isAMuteAll || (AreMusicsMuted() && AreSFXsMuted()))
            SwitchMusicsMuteLine();
        isAMuteAll = false;
    }

    private void ToggleMusic()
    {
        muteAllMusic = !muteAllMusic;

        foreach (Sound music in musics)
            music.source.mute = muteAllMusic;
    }

    private void SwitchMusicsMuteLine()
    {
        GameObject callingButton = EventSystem.current.currentSelectedGameObject;
        callingButton.transform.GetChild(0).gameObject.SetActive(muteAllMusic);
    }

    public void MuteSFXs()
    {
        ToggleSFXs();
        if (!isAMuteAll || (AreMusicsMuted() && AreSFXsMuted()))
            SwitchEffectsMuteLine();
        isAMuteAll = false;
    }

    private void ToggleSFXs()
    {
        muteAllEffects = !muteAllEffects;

        foreach (Sound sfx in SFXs)
            sfx.source.mute = muteAllEffects;
    }

    private void SwitchEffectsMuteLine()
    {
        GameObject callingButton = EventSystem.current.currentSelectedGameObject;
        callingButton.transform.GetChild(0).gameObject.SetActive(muteAllEffects);
    }

    public void PlayButtonSFX()
    {
        PlaySFX("ButtonClicked");
    }

    public void PlayFlipSFX()
    {
        PlaySFX("FlipCard");
    }

    public void PlayMatchingSFX()
    {
        PlaySFX("MatchingPair");
    }

    public void PlayTimeAlertSFX()
    {
        PlaySFX("TimeAlert");
    }

    public override void OnEvent(CustomEvent customEvent)
    {
        PlayFlipSFX();
    }
}
