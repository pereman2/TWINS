using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    public bool loop;
    public AudioSource source;

    public void Initialize(AudioSource reference)
    {
        source = reference;
        source.clip = clip;

        source.volume = volume;
        source.pitch = pitch;

        source.loop = loop;
    }
}
