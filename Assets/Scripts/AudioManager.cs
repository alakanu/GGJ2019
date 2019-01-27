using UnityEngine;

class AudioManager : MonoBehaviour
{
    public AudioClip Intro;
    public AudioClip Loop;

    AudioSource audioSource;

    void Start()
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
}
