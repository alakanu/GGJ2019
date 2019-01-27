using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
class ButtonAudio : MonoBehaviour
{
    public AudioClip buttonClickAudioClip;
    AudioSource audioSource;
    // Update is called once per frame
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Stop();
        audioSource.clip = buttonClickAudioClip;
        GetComponent<Button>().onClick.AddListener(() => audioSource.Play());
    }
}
