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
            AnswerButtons[i].onClick.AddListener(() => OnAnswerSelected(i));
            AnswerTexts[i] = AnswerButtons[i].GetComponentInChildren<Text>();
        }
    }

    void Update()
    {

    }

    public void DisplayDialogue(Dialogue currentDialogue)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayDialogueCoroutine(currentDialogue));
    }

    public void ForceDialogueCompletion()
    {
        forceCurrent = true;
    }

    public void ClearDialogue()
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayDialogueCoroutine(Dialogue currentDialogue)
    {
        int currentLineIndex = 0;
        int parsedCharactersCount = 0;
        DialogueLine[] lines = currentDialogue.DialogueBody;
        while (currentLineIndex < lines.Length)
        {
            DialogueLine line = lines[currentLineIndex];
            string lineText = line.Line;
            while (parsedCharactersCount < lineText.Length)
            {
                if (forceCurrent)
                {
                    DialogueText.text = lineText;
                    parsedCharactersCount = lineText.Length;
                }
                else
                {
                    DialogueText.text = lineText.Substring(0, ++parsedCharactersCount);
                    yield return waitBetweenEachLetter;
                }
            }
            ++currentLineIndex;
        }

        if (currentDialogue.HasFinalChoice)
        {
            AnswerButtons[0].gameObject.SetActive(true);
            AnswerTexts[0].text = FINAL_ANSWER;
            for (int i = 1; i < AnswerTexts.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(false);

            }
        }
        else
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
    }

    const string FINAL_ANSWER = "Goodbye.";
    bool forceCurrent;
    WaitForSeconds waitBetweenEachLetter = new WaitForSeconds(0.3f);

    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Button[] AnswerButtons;

    Text[] AnswerTexts;
}
