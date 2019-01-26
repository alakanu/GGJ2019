using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class DialoguePanelManager : MonoBehaviour
{
    public event Action<int> OnAnswerSelected;

    void Start()
    {
        AnswerTexts = new Text[AnswerButtons.Length];
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            int answerIndex = i;
            AnswerButtons[i].onClick.AddListener(() =>
            {
                OnAnswerSelected(answerIndex);
            });
            AnswerTexts[i] = AnswerButtons[i].GetComponentInChildren<Text>();
            AnswerButtons[i].gameObject.SetActive(false);
        }
    }


    public void DisplayDialogue(Dialogue currentDialogue)
    {
        fastForward = false;
        StopAllCoroutines();
        StartCoroutine(DisplayDialogueCoroutine(currentDialogue));
    }

    public void DialogueFastForward()
    {
        fastForward = true;
    }

    public void ClearDialogue()
    {
        DialogueText.text = string.Empty;
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayDialogueCoroutine(Dialogue currentDialogue)
    {
        int currentLineIndex = 0;
        DialogueLine[] lines = currentDialogue.DialogueBody;
        while (currentLineIndex < lines.Length)
        {
            DialogueLine line = lines[currentLineIndex];
            string lineText = line.Line;
            int parsedCharactersCount = 0;
            while (parsedCharactersCount < lineText.Length)
            {
                if (fastForward)
                {
                    DialogueText.text = lineText;
                    parsedCharactersCount = lineText.Length;
                    fastForward = false;
                }
                else
                {
                    DialogueText.text = lineText.Substring(0, ++parsedCharactersCount);
                    yield return waitBetweenEachLetter;
                }
            }

            if (currentLineIndex != lines.Length - 1)
            {
                while (!fastForward)
                {
                    yield return null;
                }
                fastForward = false;
            }

            ++currentLineIndex;
        }

        if (currentDialogue.HasFinalChoice)
        {
            Answer[] answers = currentDialogue.Answers;
            int i;
            for (i = 0; i < answers.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(true);
                AnswerTexts[i].text = answers[i].AnswerText;
            }

            for (; i < AnswerTexts.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(false);
            }
        }
        else
        {
            AnswerButtons[0].gameObject.SetActive(true);
            AnswerTexts[0].text = FINAL_ANSWER;
            for (int i = 1; i < AnswerTexts.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(false);

            }
        }
    }

    const string FINAL_ANSWER = "Goodbye.";
    bool fastForward;
    WaitForSeconds waitBetweenEachLetter = new WaitForSeconds(0.2f);

    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Button[] AnswerButtons;

    Text[] AnswerTexts;
}
