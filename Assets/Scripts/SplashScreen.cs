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
        float a = col.a + alphaSpeed * Time.deltaTime;
        if (a >= 1f)
        {
            a = 1f;

            if (Input.anyKeyDown)
            {

                SceneManager.LoadScene("MainScene");

            }
        }
        col.a = a;
        bg.color = col;
    }

    float alphaSpeed = 0.66f;
    Image bg;
}
