using System.Collections.Generic;
using UnityEngine;

class MainGameController : MonoBehaviour
{
    public UIDialogueManager dialogueManager;

    void Start()
    {
        MyJsonUtility.LoadCharacters(out charactersDict, out characters);

        //TODO: subscribe to buttons
    }

    Dictionary<string, Character> charactersDict;
    Character[] characters;
}
