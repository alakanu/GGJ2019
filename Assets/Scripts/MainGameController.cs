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
        currentCharacter = charactersDict["AI"];
        dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
    }

    void OnAnswerSelected(int answerIndex)
    {
        Dialogue currentDialogue = currentCharacter.CurrentDialogue;
        if (currentDialogue.HasFinalChoice)
        {
            currentCharacter.CurrentDialogue = currentDialogue.Answers[answerIndex].NextDialogue;
            dialogueUI.DisplayDialogue(currentDialogue);
        }
        else
        {
            currentCharacter = null;
            dialogueUI.ClearDialogue();
        }
    }

    void OnCharacterSelected(int index)
    {
        currentCharacter = characters[index];
        Debug.Log("SELECTED " + index + " " + currentCharacter.Name);
        dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
        hintPanel.DisplayCharacterHints(currentCharacter);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogueUI.DialogueFastForward();
        }
    }

    Character currentCharacter;
    Dictionary<string, Character> charactersDict;
    Character[] characters;
}
