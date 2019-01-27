using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

static class MyJsonUtility
{
    const string DIALOGUES_PATH = "/JsonFiles/Dialogues.json";
    const string CHARACTERS_PATH = "/JsonFiles/Characters.json";

    public static void LoadData(
        out Dictionary<string, Character> characterDict,
        out Character[] characterArray,
        out Dictionary<string, Dialogue> dialogues)
    {
        dialogues = LoadDialogues();

        string characterFileContent = GetFileContent(Application.dataPath + CHARACTERS_PATH);
        int currentPosition = 1;
        string characterKey, characterJsonEntry;
        List<Character> charactersList = new List<Character>();
        characterDict = new Dictionary<string, Character>();
        while (TryGetNextEntry(characterFileContent, ref currentPosition, out characterKey, out characterJsonEntry))
        {
            CharacterJson characterRaw = JsonUtility.FromJson<CharacterJson>(characterJsonEntry);

            Character character = new Character();
            character.Name = characterKey;
            character.StartingDialogue = dialogues[characterRaw.StartingDialogue];
            character.CurrentDialogue = character.StartingDialogue;

            if (characterKey != "AI")
            {
                character.LikedCharacter = characterRaw.LikedCharacter;
                character.DislikedCharacter = characterRaw.DislikedCharacter;
                character.LikedMapSide = (MapSide)Enum.Parse(typeof(MapSide), characterRaw.LikedMapSide);
                character.DislikedMapSide = (MapSide)Enum.Parse(typeof(MapSide), characterRaw.DislikedMapSide);
                character.DiscoveredHints = new HashSet<string>();
                character.HappyEnding = characterRaw.HappyEnding;
                character.SadEnding = characterRaw.SadEnding;
                charactersList.Add(character);
            }
            characterDict.Add(characterKey, character);
        }

        characterArray = charactersList.ToArray();

    }

    static Dictionary<string, Dialogue> LoadDialogues()
    {
        Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();

        string dialogueFileContent = GetFileContent(Application.dataPath + DIALOGUES_PATH);
        int currentPosition = 1;
        string key, jsonEntry;

        while (TryGetNextEntry(dialogueFileContent, ref currentPosition, out key, out jsonEntry))
        {
            DialogueJson dialogueRaw = JsonUtility.FromJson<DialogueJson>(jsonEntry);
            Dialogue dialogue;
            if (dialogues.TryGetValue(key, out dialogue) == false)
            {
                dialogue = new Dialogue();
                dialogues.Add(key, dialogue);
            }

            dialogue.DialogueBody = dialogueRaw.DialogueBody;
            OptionJson[] answersRaw = dialogueRaw.Options;
            if (answersRaw != null)
            {
                Option[] dialogueOptions = new Option[answersRaw.Length];
                for (int i = 0; i < answersRaw.Length; i++)
                {
                    var optionRaw = answersRaw[i];
                    var option = new Option();
                    option.OptionText = optionRaw.OptionText;
                    string nextDialogueKey = optionRaw.NextDialogue;
                    Dialogue nextDialogue;

                    if (dialogues.TryGetValue(nextDialogueKey, out nextDialogue) == false)
                    {
                        nextDialogue = new Dialogue();
                        dialogues.Add(nextDialogueKey, nextDialogue);
                    }

                    option.NextDialogue = nextDialogue;
                    option.Hint = optionRaw.Hint;

                    dialogueOptions[i] = option;
                }
                dialogue.Options = dialogueOptions;
            }
            else
            {
                dialogue.Options = new Option[0];
            }
        }


        return dialogues;
    }

    static string GetFileContent(string path)
    {
        return Regex.Replace(File.ReadAllText(path), "\t|\r|\n", "");
    }

    static bool TryGetNextEntry(
        string jsonFileContent,
        ref int currentPosition,
        out string key,
        out string jsonEntry)
    {
        key = null;
        jsonEntry = null;

        if (TrySeekUntilNextEntry(jsonFileContent, ref currentPosition))
        {
            key = GetKey(jsonFileContent, ref currentPosition);

            int openBracesCount = 0;
            int start = currentPosition;
            int length;
            bool seeking = true;

            for (length = 0; seeking; length++)
            {
                switch (jsonFileContent[start + length])
                {
                    case '{':
                        ++openBracesCount;
                        break;
                    case '}':
                        seeking = --openBracesCount > 0;
                        break;
                }
            }

            currentPosition = start + length;
            jsonEntry = jsonFileContent.Substring(start, length);
            return true;
        }
        return false;

    }

    static string GetKey(string jsonFileContent, ref int currentPosition)
    {
        int length = 1;
        while (jsonFileContent[currentPosition + length] != '"')
        {
            length++;
        }

        string result = jsonFileContent.Substring(currentPosition + 1, length - 1);
        currentPosition += length;

        while (jsonFileContent[currentPosition] != ':')
        {
            currentPosition++;
        }
        currentPosition++;
        return result;
    }

    static bool TrySeekUntilNextEntry(string jsonFileContent, ref int currentPosition)
    {
        while (currentPosition < jsonFileContent.Length - 1)
        {
            if (jsonFileContent[currentPosition] == '"')
            {
                return true;
            }
            currentPosition++;
        }

        return false;
    }


}

