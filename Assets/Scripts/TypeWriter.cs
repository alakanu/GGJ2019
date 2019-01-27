using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class TypeWriter
{
    public bool Writing
    {
        get
        {
            return writing;
        }
    }

    public IEnumerator WriteText(string text, Text uiText)
    {
        fastForward = false;
        writing = true;
        int parsedCharactersCount = 0;
        while (parsedCharactersCount < text.Length)
        {
            if (fastForward)
            {
                uiText.text = text;
                parsedCharactersCount = text.Length;
                fastForward = false;
            }
            else
            {
                uiText.text = text.Substring(0, ++parsedCharactersCount);
                yield return waitBetweenEachLetter;
            }
        }

        writing = false;
    }

    public void FastForward()
    {
        fastForward = true;
    }

    public void Reset()
    {
        fastForward = false;
        writing = false;
    }

    bool fastForward;
    bool writing;
    WaitForSeconds waitBetweenEachLetter = new WaitForSeconds(0.05f);
}

