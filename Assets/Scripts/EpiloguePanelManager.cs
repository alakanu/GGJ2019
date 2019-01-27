using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class EpiloguePanelManager : MonoBehaviour
{
    public Image[] happyEndingPortraits;
    public Image[] sadEndingPortraits;

    void Start()
    {
        epilogueText = GetComponentInChildren<Text>();
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
        StartCoroutine(Display(characters));
    }

    IEnumerator Display(Character[] characters)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < characters.Length; i++)
        {
            Character character = characters[i];
            bool happyEnding = character.totalScore > 0;
            happyEndingPortraits[i].gameObject.SetActive(happyEnding);
            sadEndingPortraits[i].gameObject.SetActive(!happyEnding);
            yield return typeWriter.WriteText(happyEnding ? character.HappyEnding : character.SadEnding, epilogueText);
            while (!moveNextEnding)
            {
                yield return null;
            }

            moveNextEnding = false;

            happyEndingPortraits[i].gameObject.SetActive(false);
            sadEndingPortraits[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    bool moveNextEnding;
    TypeWriter typeWriter = new TypeWriter();
    Text epilogueText;
}

