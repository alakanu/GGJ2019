using System;
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

    public void DisplayCharacter(Character character)
    {
        //TODO DISPLAY PICTURE FOR CHARACTER


        Dialogue currentDialogue = character.CurrentDialogue;
        DialogueText.text = currentDialogue.DialogueText;
        if (currentDialogue.IsFinalDialogue)
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

    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Button[] AnswerButtons;

    Text[] AnswerTexts;
}
