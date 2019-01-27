using UnityEngine;

class AudioManager : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip Loop;
    public AudioClip Epilogue;
    public AudioClip PerfectEpilogue;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayIntro()
    {
        audioSource.clip = Intro;
        audioSource.Play();
    }

    public void PlayLoop()
    {
        audioSource.clip = Loop;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayEpilogue(bool isPerfectEnding)
    {
        audioSource.clip = isPerfectEnding ? PerfectEpilogue : Epilogue;
        audioSource.loop = false;
        audioSource.Play();
    }
}
