using UnityEngine;

class AudioManager : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip Loop;
    public AudioClip Epilogue;

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

    public void PlayEpilogue()
    {
        audioSource.clip = Epilogue;
        audioSource.loop = false;
        audioSource.Play();
    }
}
