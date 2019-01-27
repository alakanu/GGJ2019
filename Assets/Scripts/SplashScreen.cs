using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class SplashScreen : MonoBehaviour
{
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        bg = GetComponentInChildren<Image>();
        AudioSource source = GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Color col = bg.color;
        col.a += 0.33f * Time.deltaTime;
        bg.color = col;
        if (Input.anyKeyDown)
        {
            if (bg.color.a >= 1f)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    float alphaSpeed = 0.25f;
    Image bg;
}
