﻿using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    public DialoguePanelManager dialogueUI;
    public CharacterPanelManager charactersUI;
    public HintPanelManager hintPanel;
    public EpiloguePanelManager epilogueUI;

    void Start()
    {
        MyJsonUtility.LoadData(out charactersDict, out characters, out dialogues);
        dialogueUI.OnAnswerSelected += OnAnswerSelected;
        charactersUI.OnCharacterSelected += OnCharacterSelected;
        SubmitLogic.ScoresComputed += OnScoresComputed;
        currentCharacter = charactersDict["AI"];
        dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
    }

    void OnAnswerSelected(int answerIndex)
    {
        Dialogue currentDialogue = currentCharacter.CurrentDialogue;
        if (currentDialogue.HasFinalChoice)
        {
            Answer answer = currentDialogue.Answers[answerIndex];
            if (answer.HasDiscovery)
            {
                int index = -1;
                switch (answer.DiscoveryType)
                {
                    case DiscoveryType.CharacterLike:
                        index = 0;
                        break;
                    case DiscoveryType.CharacterDislike:
                        index = 1;
                        break;
                    case DiscoveryType.MapSideLike:
                        index = 2;
                        break;
                    case DiscoveryType.MapSideDislike:
                        index = 3;
                        break;
                }
                currentCharacter.Discoveries[index] = true;
            }

            currentCharacter.CurrentDialogue = answer.NextDialogue;
            dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
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
        dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
        hintPanel.DisplayCharacterHints(currentCharacter);
    }

    void OnScoresComputed()
    {
        ending = true;
        epilogueUI.DisplayEndings(characters);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogueUI.DialogueFastForward();
        }
    }

    bool ending;
    Character currentCharacter;
    Dictionary<string, Character> charactersDict;
    Dictionary<string, Dialogue> dialogues;
    Character[] characters;
}
