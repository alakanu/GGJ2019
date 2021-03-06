﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class EpiloguePanelManager : MonoBehaviour
{
    public Image[] happyEndingPortraits;
    public Image[] sadEndingPortraits;
    public AudioSource typeWriterSound;

    void Start()
    {
        epilogueText = GetComponentInChildren<Text>();
        for (int i = 0; i < happyEndingPortraits.Length; i++)
        {
            happyEndingPortraits[i].gameObject.SetActive(false);
            sadEndingPortraits[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void FastForwardOrMoveNext()
    {
        if (typeWriter.Writing)
        {
            typeWriter.FastForward();
        }
        else
        {
            moveNextEnding = true;
        }
    }

    public void DisplayEndings(Character[] characters)
    {
        gameObject.SetActive(true);
        StartCoroutine(Display(characters));
    }

    IEnumerator Display(Character[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            Character character = characters[i];
            bool happyEnding = character.totalScore > 0;
            happyEndingPortraits[i].gameObject.SetActive(happyEnding);
            sadEndingPortraits[i].gameObject.SetActive(!happyEnding);
            epilogueText.text = string.Empty;
            yield return typeWriter.WriteText(happyEnding ? character.HappyEnding : character.SadEnding, epilogueText,
                typeWriterSound);

            while (!moveNextEnding)
            {
                yield return null;
            }
            moveNextEnding = false;

            happyEndingPortraits[i].gameObject.SetActive(false);
            sadEndingPortraits[i].gameObject.SetActive(false);
        }
        SceneManager.LoadScene("SplashScreenScene");
    }

    bool moveNextEnding;
    TypeWriter typeWriter = new TypeWriter();
    Text epilogueText;
}

