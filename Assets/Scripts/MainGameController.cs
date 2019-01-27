using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    public DialoguePanelManager dialogueUI;
    public CharacterPanelManager charactersUI;
    public HintPanelManager hintPanel;
    public EpiloguePanelManager epilogueUI;
    public AudioManager audioManager;
    public GameObject terrain;
    public GameObject forest;

    void Start()
    {
        terrain.SetActive(false);
        forest.SetActive(false);
        MyJsonUtility.LoadData(out charactersDict, out characters, out dialogues);
        dialogueUI.OnAnswerSelected += OnAnswerSelected;
        dialogueUI.IntroStage += OnIntroStageChange;
        charactersUI.OnCharacterSelected += OnCharacterSelected;
        SubmitLogic.ScoresComputed += OnScoresComputed;
        currentCharacter = charactersDict["AI"];
        dialogueUI.DisplayIntro(currentCharacter.CurrentDialogue);
        charactersUI.SetCharacters(characters);
        audioManager.PlayIntro();
    }

    void OnIntroStageChange(int lineIndex)
    {
        if (lineIndex == 5)
        {
            terrain.SetActive(true);
            forest.SetActive(true);
        }
    }

    void OnAnswerSelected(int answerIndex)
    {
        Dialogue currentDialogue = currentCharacter.CurrentDialogue;
        if (currentDialogue.HasOptions)
        {
            Answer answer = currentDialogue.Options[answerIndex];
            if (answer.HasDiscovery)
            {
                int index = -1;
                switch (answer.DiscoveryType)
                {
                    case DiscoveryType.CharacterLiked:
                        index = 0;
                        break;
                    case DiscoveryType.CharacterDisliked:
                        index = 1;
                        break;
                    case DiscoveryType.MapSideLiked:
                        index = 2;
                        break;
                    case DiscoveryType.MapSideDisliked:
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
            if (currentCharacter.Name == "AI")
            {
                audioManager.PlayLoop();
                charactersUI.EnablePanel();
            }
            currentCharacter.CurrentDialogue = currentCharacter.StartingDialogue;
            currentCharacter = null;
            dialogueUI.ResetDialogues();
        }
    }

    void OnCharacterSelected(int index)
    {
        if (currentCharacter != null)
        {
            currentCharacter.CurrentDialogue = currentCharacter.StartingDialogue;
        }
        currentCharacter = characters[index];
        dialogueUI.DisplayDialogue(currentCharacter.CurrentDialogue);
        hintPanel.DisplayCharacterHints(currentCharacter);
    }

    void OnScoresComputed()
    {
        ending = true;
        dialogueUI.ResetDialogues();
        epilogueUI.DisplayEndings(characters);
        audioManager.PlayEpilogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ending)
            {
                epilogueUI.FastForwardOrMoveNext();
            }
            else
            {
                dialogueUI.FastForwardOrMoveNext();
            }
        }
    }

    bool ending;
    Character currentCharacter;
    Dictionary<string, Character> charactersDict;
    Dictionary<string, Dialogue> dialogues;
    Character[] characters;
}
