﻿using System;
using UnityEngine;
using UnityEngine.UI;

class DialoguePanelManager : MonoBehaviour
{
    public event Action<int> OnAnswerSelected;

    void Start()
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].onClick.AddListener(() => OnAnswerSelected(i));
        }
    }

    public void DisplayCharacter(Character character)
    {
        //TODO DISPLAY PICTURE FOR CHARACTER


        Dialogue currentDialogue = character.CurrentDialogue;
        DialogueText.text = currentDialogue.DialogueText;
        if (currentDialogue.IsFinalDialogue)
        {
            AnswerTexts[0].gameObject.SetActive(true);
            AnswerTexts[0].text = FINAL_ANSWER;
            for (int i = 1; i < AnswerTexts.Length; i++)
            {
                AnswerTexts[i].gameObject.SetActive(false);

            }
        }
        else
        {
            Answer[] answers = currentDialogue.Answers;
            int i;
            for (i = 0; i < answers.Length; i++)
            {
                AnswerTexts[i].gameObject.SetActive(true);
                AnswerTexts[i].text = answers[i].AnswerText;
            }

            for (; i < AnswerTexts.Length; i++)
            {
                AnswerTexts[i].gameObject.SetActive(false);
            }
        }
    }

    const string FINAL_ANSWER = "Goodbye.";

    [SerializeField]
    Text DialogueText;
    [SerializeField]
    Button[] AnswerButtons;
    [SerializeField]
    Text[] AnswerTexts;
}