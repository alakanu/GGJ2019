using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    public DialoguePanelManager dialogueUI;

    void Start()
    {
        MyJsonUtility.LoadCharacters(out charactersDict, out characters);
        dialogueUI.OnAnswerSelected += OnAnswerSelected;
    }

    void OnAnswerSelected(int answerIndex)
    {
        Dialogue currentDialogue = currentCharacter.CurrentDialogue;
        if (currentDialogue.IsFinalDialogue)
        {
            //TODO Reset stuff or don't know
            currentCharacter = null;
        }
        else
        {
            currentCharacter.CurrentDialogue = currentDialogue.Answers[answerIndex].NextDialogue;
            dialogueUI.DisplayCharacter(currentCharacter);
        }
    }

    Character currentCharacter;
    Dictionary<string, Character> charactersDict;
    Character[] characters;
}
