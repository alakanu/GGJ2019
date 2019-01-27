using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class DialoguePanelManager : MonoBehaviour
{
    public event Action<int> OnAnswerSelected;
    public event Action<int> IntroStage;

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

    public void DisplayIntro(Dialogue intro)
    {
        ClearDialogue();
        StartCoroutine(DisplayIntroCoroutine(intro));

    }

    public void DisplayDialogue(Dialogue currentDialogue)
    {
        typeWriter.Reset();
        StopAllCoroutines();
        ClearDialogue();
        StartCoroutine(DisplayDialogueCoroutine(currentDialogue));
    }

    public void FastForwardOrMoveNext()
    {
        if (typeWriter.Writing)
        {
            typeWriter.FastForward();
        }
        else
        {
            moveToNextLine = true;
        }
    }

    public void ResetDialogues()
    {
        moveToNextLine = false;
        typeWriter.Reset();
        StopAllCoroutines();
        ClearDialogue();
    }

    void ClearDialogue()
    {
        moveToNextLine = false;
        DialogueText.text = string.Empty;
        SpeakerNameText.text = string.Empty;
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayIntroCoroutine(Dialogue currentDialogue)
    {
        int currentLineIndex = 0;
        DialogueLine[] lines = currentDialogue.DialogueBody;
        while (currentLineIndex < lines.Length)
        {
            IntroStage(currentLineIndex);
            DialogueLine line = lines[currentLineIndex];
            SpeakerNameText.text = line.Speaker + ":";
            yield return typeWriter.WriteText(line.Line, DialogueText);

            if (currentLineIndex != lines.Length - 1)
            {
                while (!moveToNextLine)
                {
                    yield return null;
                }
            }

            moveToNextLine = false;
            ++currentLineIndex;
        }

        if (currentDialogue.HasOptions)
        {
            Option[] answers = currentDialogue.Options;
            int i;
            for (i = 0; i < answers.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(true);
                AnswerTexts[i].text = answers[i].OptionText;
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

    IEnumerator DisplayDialogueCoroutine(Dialogue currentDialogue)
    {
        int currentLineIndex = 0;
        DialogueLine[] lines = currentDialogue.DialogueBody;
        while (currentLineIndex < lines.Length)
        {
            DialogueLine line = lines[currentLineIndex];
            SpeakerNameText.text = line.Speaker + ":";
            yield return typeWriter.WriteText(line.Line, DialogueText);

            if (currentLineIndex != lines.Length - 1)
            {
                while (!moveToNextLine)
                {
                    yield return null;
                }
            }

            moveToNextLine = false;
            ++currentLineIndex;
        }

        if (currentDialogue.HasOptions)
        {
            Option[] answers = currentDialogue.Options;
            int i;
            for (i = 0; i < answers.Length; i++)
            {
                AnswerButtons[i].gameObject.SetActive(true);
                AnswerTexts[i].text = answers[i].OptionText;
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

    const string FINAL_ANSWER = "End interaction with the subject";
    TypeWriter typeWriter = new TypeWriter();
    bool moveToNextLine;

    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Text SpeakerNameText;
    [SerializeField]
    Button[] AnswerButtons;

    Text[] AnswerTexts;
}
