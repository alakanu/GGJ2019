using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
class ButtonAudio : MonoBehaviour
{
    public AudioClip buttonClickAudioClip;
    public AudioSource audioSource;
    // Update is called once per frame
    void Start()
    {
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Stop();
        audioSource.clip = buttonClickAudioClip;
        GetComponent<Button>().onClick.AddListener(() => audioSource.Play());
    }
}
