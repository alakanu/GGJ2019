using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    public DialoguePanelManager dialogueUI;
    public CharacterPanelManager charactersUI;
    public HintPanel hintPanel;

    void Start()
    {
        MyJsonUtility.LoadCharacters(out charactersDict, out characters);
        dialogueUI.OnAnswerSelected += OnAnswerSelected;
        charactersUI.OnCharacterSelected += OnCharacterSelected;
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

    void OnCharacterSelected(int index)
    {
        currentCharacter = characters[index];
        dialogueUI.DisplayCharacter(currentCharacter);
        hintPanel.DisplayCharacterHints(currentCharacter);
    }

    Character currentCharacter;
    Dictionary<string, Character> charactersDict;
    Character[] characters;
}
